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
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Master.Controller.Domain.PDV
{
    public class CtrlPDV : MasterController
    {
        public CtrlPDV(IOptions<LocalNetwork> network, IMemoryCache cache, IAppManager appManager) : base(network, cache, appManager) { }

        const string myRoute = "api/pdv-info";
        const int maxMinutes = 240;

        [HttpPost]
        [Route(myRoute)]
        public async Task<ActionResult> PdvInfo([FromBody] DtoPDVInfoRequest input)
        {
            var currentUser = CurrentUser();

            var tag = "GetPDV?" + input.filter;

            var rebuilder = Request.Headers["App"] == "rebuild";

            if (this.IsProcessCached<List<DtoPDVInfo>>(tag)  && !rebuilder)
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

                        Console.WriteLine(response.Content.ToString());

                        System.IO.File.WriteAllText("res.txt", response.Content.ToString());

                        if (response.IsSuccessful)
                        {
                            client.ClearHandlers();
                            return Ok(this.SaveProcessCache(tag, JsonConvert.DeserializeObject<List<DtoPDVInfo>>(response.Content), null, myRoute, maxMinutes));
                        }

                        Thread.Sleep(50);
                    }
                }
            }

            var srv = this.RegisterService<SrvPDVInfo>();

            if (!srv.Exec(this.Network, currentUser, input.filter))
            {
                return BadRequest(srv.Error);
            }

            return Ok(this.SaveProcessCache(tag, srv.OutDto, input, myRoute, maxMinutes));
        }
    }
}
