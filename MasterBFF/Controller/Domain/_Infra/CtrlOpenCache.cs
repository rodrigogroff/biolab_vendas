using Master.Controller.Infra;
using Master.Controller.Manager;
using Master.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Master.Controller.Domain.Infra
{
    public class CtrlOpenCache : MasterController
    {
        public CtrlOpenCache(IOptions<LocalNetwork> network, IMemoryCache cache, IAppManager appManager) : base(network, cache, appManager) { }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/open_cache")]
        public async Task<ActionResult> OpenCache()
        {
            if (this.Network.node != 1)
                return BadRequest();

            return Ok(this.ListCachedNodes());
        }
    }
}
