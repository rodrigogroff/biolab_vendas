using System.Collections.Generic;

namespace Master.Entity.Dto.Domain.MixProdutos
{
    public class DtoMixProdutosPageRequest : DtoMixProdutosRequest
    {
        public string texto { get; set; }

        public List<int> IdTipoProduto { get; set; }

        public bool destaque { get; set; }

        public bool recente { get; set; }

        public bool mais30 { get; set; }

        public int pageIndex { get; set; }

        public int pageSize { get; set; }
    }
}
