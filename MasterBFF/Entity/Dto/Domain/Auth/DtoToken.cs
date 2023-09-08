using Master.Entity.Dto.Infra;

namespace Master.Entity.Dto.Domain.Auth
{
    public class DtoToken
    {
        public string token { get; set; }

        public DtoAuthenticatedUser user { get; set; }
    }
}
