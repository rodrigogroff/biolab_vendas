using System;

namespace MasterBiolabBFF.Entity.Dto.PharmaLink
{
    public class PharmaLink_AccessTokenResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
        public string UserId { get; set; }
        public string UserIdCriptografadoAmbienteLegado { get; set; }
        public string Nome { get; set; }
        public string PerfilId { get; set; }
        public string Perfil { get; set; }
        public string AlteraSenha { get; set; }
        public string TokenAmbienteLegado { get; set; }
        public DateTime? issued { get; set; }
        public DateTime? expires { get; set; }
    }
}
