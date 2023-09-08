using Master.Controller.Infra;
using Master.Controller.Manager;
using Master.Entity;
using Master.Entity.Dto.Domain.MixProdutos;
using Master.Service.Domain.MixProdutos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Master.Controller.Domain.MixProdutos
{
    public class CtrlMixProdutosEnvio : MasterController
    {
        public CtrlMixProdutosEnvio(IOptions<LocalNetwork> network, IMemoryCache cache, IAppManager appManager) : base(network, cache, appManager) { }

        [HttpPost]
        [Route("api/mix-produtos-envio")]
        public async Task<ActionResult> MixProdutos([FromBody] DtoMixProdutosEnvioRequest request)
        {
            var currentUser = CurrentUser();

            var srv = RegisterService<SrvMixProdutosEnvio>();

            if (!srv.Exec(Network, currentUser, request))
            {
                return BadRequest(srv.Error);
            }

            return Ok(srv.OutDto);
        }
    }
}
