using Master.Controller.Infra;
using Master.Controller.Manager;
using Master.Entity;
using Master.Entity.Dto.Domain.MixProdutos;
using Master.Service.Domain.MixProdutos;
using MasterBiolabBFF.Entity.Dto.PharmaLink;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System.Threading;
using System.Threading.Tasks;

namespace Master.Controller.Domain.MixProdutos
{
    public class CtrlMixProdutosPage : MasterController
    {
        public CtrlMixProdutosPage(IOptions<LocalNetwork> network, IMemoryCache cache, IAppManager appManager) : base(network, cache, appManager) { }

        const string myRoute = "api/pdv-mix-produtos-page";
        const int maxMinutes = 240;

        [HttpPost]
        [Route(myRoute)]
        public async Task<ActionResult> MixProdutos([FromBody] DtoMixProdutosPageRequest input)
        {
            var currentUser = CurrentUser();

            var dist_tag = string.Empty;
            foreach (var item in input.distribuidores)
                dist_tag += item.IdDistribuidor + "," + item.Ordem + ",";

            var tag = "MixProdutos?" + input.IdPdv + "&" + input.IdPrazoPagamento + "&" + dist_tag;

            var rebuilder = Request.Headers["App"] == "rebuild";

            if (IsProcessCached<PharmaLink_MixProdutosPage>(tag) && !rebuilder)
            {
                var srvPageFromCache = RegisterService<SrvMixProdutosPaginated>();

                if (!srvPageFromCache.Exec(CachedObject as PharmaLink_MixProdutos, input))
                {
                    return BadRequest(srvPageFromCache.Error);
                }

                return Ok(srvPageFromCache.OutDto);
            }
            else
            {
                if (this.Network.node != 1)
                {
                    while (true)
                    {
                        var client = new RestClient(this.Network.cacheLocation);
                        var request = new RestRequest(myRoute, Method.POST);
                        request.AddHeader("Content-Type", "application/json");
                        request.AddHeader("Authorization", Request.Headers["Authorization"].ToString());
                        request.AddJsonBody(input);
                        var response = client.Execute(request);
                        if (response.IsSuccessful)
                        {
                            client.ClearHandlers();

                            var CachedObject = JsonConvert.DeserializeObject<PharmaLink_MixProdutos>(response.Content);

                            this.SaveProcessCache(tag, CachedObject, null, myRoute, maxMinutes);

                            var srvPageRestored = RegisterService<SrvMixProdutosPaginated>();

                            if (!srvPageRestored.Exec(CachedObject, input))
                            {
                                return BadRequest(srvPageRestored.Error);
                            }

                            return Ok(srvPageRestored.OutDto);
                        }

                        Thread.Sleep(50);
                    }
                }
            }

            var srv = RegisterService<SrvMixProdutos>();

            if (!srv.Exec(Network, currentUser, input as DtoMixProdutosRequest))
            {
                return BadRequest(srv.Error);
            }

            SaveProcessCache(tag, srv.OutDto, input, myRoute, maxMinutes);

            var srvPage = RegisterService<SrvMixProdutosPaginated>();

            if (!srvPage.Exec(srv.OutDto, input))
            {
                return BadRequest(srvPage.Error);
            }

            srv.OutDto = null;

            return Ok(srvPage.OutDto);
        }
    }
}
