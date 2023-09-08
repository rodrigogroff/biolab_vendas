using Master.Controller.Infra;
using Master.Controller.Manager;
using Master.Entity;
using Master.Entity.Dto.Domain.Auth;
using Master.Service.Domain.Config;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System.Threading;
using System.Threading.Tasks;

namespace Master.Controller.Domain.Auth
{
    public class CtrlInfra : MasterController
    {
        public CtrlInfra(IOptions<LocalNetwork> network, IMemoryCache cache, IAppManager appManager) : base(network, cache, appManager) { }

        const string myRoute = "api/config";
        const int maxMinutes = 3;

        [AllowAnonymous]
        [HttpPost]
        [Route(myRoute)]
        public async Task<ActionResult> Config()
        {
            var tag = "GetConfiguration";
            var rebuilder = Request.Headers["App"] == "rebuild";

            if (this.IsProcessCached<DtoConfiguration>(tag) && !rebuilder)
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
                        var response = client.Execute(request);
                        if (response.IsSuccessful)
                        {
                            client.ClearHandlers();
                            return Ok(this.SaveProcessCache(tag, JsonConvert.DeserializeObject<DtoConfiguration>(response.Content), null, myRoute, maxMinutes));
                        }

                        Thread.Sleep(50);
                    }
                }
            }

            var srv = this.RegisterService<SrvConfig>();

            if (!srv.Exec(this.Network))
            {
                return BadRequest(srv.Error);
            }

            return Ok(this.SaveProcessCache(tag, srv.OutDto, null, myRoute, maxMinutes));
        }
    }
}
