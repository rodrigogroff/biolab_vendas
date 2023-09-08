using Master.Entity.Dto.Domain.PDV;
using System.Collections.Generic;

namespace Master.Entity.Dto.Domain.MixProdutos
{
    public class DtoMixProdutosRequest
    {
        public int IdPdv { get; set; }
        public int IdPrazoPagamento { get; set; }
        public string cnpj { get; set; }

        public List<DtoPDVDistribuidor> distribuidores { get; set; }
    }
}
