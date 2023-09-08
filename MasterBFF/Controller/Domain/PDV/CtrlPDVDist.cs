using Master.Controller.Infra;
using Master.Controller.Manager;
using Master.Entity;
using Master.Entity.Dto.Domain.PDV;
using Master.Service.Domain.PDV;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Master.Controller.Domain.PDV
{
    public class CtrlPDVDist : MasterController
    {
        public CtrlPDVDist(IOptions<LocalNetwork> network, IMemoryCache cache, IAppManager appManager) : base(network, cache, appManager) { }

        const string myRoute = "api/pdv-dist-info";
        const int maxMinutes = 240;

        [HttpPost]
        [Route(myRoute)]
        public async Task<ActionResult> PdvDistInfo([FromBody] DtoPDVDistInfoRequest input)
        {
            var currentUser = CurrentUser();

            var tag = "GetPDVDist?" + input.IdPdv;

            var rebuilder = Request.Headers["App"] == "rebuild";

            if (this.IsProcessCached<List<DtoPDVDistribuidorInfo>>(tag) && !rebuilder)
            {
                return Ok(this.CachedObject);
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
                            return Ok(this.SaveProcessCache(tag, JsonConvert.DeserializeObject<List<DtoPDVDistribuidorInfo>>(response.Content), null, myRoute, maxMinutes));
                        }

                        Thread.Sleep(50);
                    }
                }
            }

            var srv = this.RegisterService<SrvPDVDistInfo>();

            if (!srv.Exec(this.Network, currentUser, input.IdPdv))
            {
                return BadRequest(srv.Error);
            }

            return Ok(this.SaveProcessCache(tag, srv.OutDto, input, myRoute, maxMinutes));
        }
    }
}
