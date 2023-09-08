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
    public class CtrlMixProdutos : MasterController
    {
        public CtrlMixProdutos(IOptions<LocalNetwork> network, IMemoryCache cache, IAppManager appManager) : base(network, cache, appManager) { }

        const string myRoute = "api/pdv-mix-produtos";
        const int maxMinutes = 240;

        [HttpPost]
        [Route(myRoute)]
        public async Task<ActionResult> MixProdutos([FromBody] DtoMixProdutosRequest input)
        {

            var currentUser = CurrentUser();

            var dist_tag = string.Empty;
            foreach (var item in input.distribuidores)
                dist_tag += item.IdDistribuidor + "," + item.Ordem + ",";

            var tag = "MixProdutos?" + input.IdPdv + "&" + input.IdPrazoPagamento + "&" + dist_tag;

            var rebuilder = Request.Headers["App"] == "rebuild";

            if (IsProcessCached<PharmaLink_MixProdutos>(tag) && !rebuilder)
            {
                return Ok(CachedObject);
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
                            return Ok(this.SaveProcessCache(tag, JsonConvert.DeserializeObject<PharmaLink_MixProdutos>(response.Content), null, myRoute, maxMinutes));
                        }

                        Thread.Sleep(50);
                    }
                }
            }

            var srv = RegisterService<SrvMixProdutos>();

            if (!srv.Exec(Network, currentUser, input))
            {
                return BadRequest(srv.Error);
            }

            return Ok(SaveProcessCache(tag, srv.OutDto, input, myRoute, maxMinutes));
        }
    }
}
