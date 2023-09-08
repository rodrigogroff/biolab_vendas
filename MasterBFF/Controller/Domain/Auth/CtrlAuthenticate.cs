using Master.Controller.Infra;
using Master.Controller.Manager;
using Master.Entity;
using Master.Entity.Dto.Domain.Auth;
using Master.Entity.Dto.Infra;
using Master.Service.Domain.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Master.Controller.Domain.Auth
{
    public class CtrlAuthenticate : MasterController
    {
        public CtrlAuthenticate(IOptions<LocalNetwork> network, IMemoryCache cache, IAppManager appManager) : base(network, cache, appManager) { }

        const string myRoute = "api/authenticate";

        [AllowAnonymous]
        [HttpPost]
        [Route(myRoute)]
        public async Task<ActionResult> Authenticate([FromBody] DtoLoginInformation input)
        {
            var aesDecrypt = this.AesDecrypt;

            input.email = aesDecrypt.DecryptStringAES(input.email);
            input.password = aesDecrypt.DecryptStringAES(input.password);

            var tag = "LogIn" + input.email;

            if (this.IsProcessCached<DtoToken>(tag))
            {
                return Ok(this.CachedObject);
            }

            var srv = RegisterService<SrvAuthenticate>();

            if (!srv.Exec(this.Network, input))
            {
                return BadRequest(srv.Error);
            }

            var resp = new DtoToken
            {
                token = this.JwtComposer.ComposeTokenForSession(srv.OutCurrentAuth),
                user = new DtoAuthenticatedUser
                {
                    Nome = srv.OutCurrentAuth.Nome,
                    Perfil = srv.OutCurrentAuth.Perfil,
                    PerfilId = srv.OutCurrentAuth.PerfilId,
                    UserId = srv.OutCurrentAuth.UserId,
                },
            };

            return Ok(this.SaveProcessCache(tag, resp, input, myRoute, 240));
        }
    }
}
