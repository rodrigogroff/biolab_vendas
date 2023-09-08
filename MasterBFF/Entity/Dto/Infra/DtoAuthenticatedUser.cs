namespace Master.Entity.Dto.Infra
{
    public class DtoAuthenticatedUser
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string refresh_token { get; set; }
        public string UserId { get; set; }
        public string Nome { get; set; }
        public string PerfilId { get; set; }
        public string Perfil { get; set; }
    }
}
