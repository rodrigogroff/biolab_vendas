using MasterBiolabBFF.Entity.Dto.PharmaLink;
using System.Collections.Generic;

namespace Master.Entity.Dto.Domain.MixProdutos
{
    public class DtoMixProdutosEnvioRequest
    {
        public string cnpj { get; set; }

        public int pdvId { get; set; }

        public List<DtoMixProdutosDistirbuidoresEnvio> distribuidores { get; set; }
    }

    public class DtoMixProdutosDistirbuidoresEnvio
    {
        public int DistribuidorId { get; set; }

        public string NomeFantasia { get; set; }

        public string Prazo { get; set; }

        public int IdPrazo { get; set; }

        public string TipoPedido { get; set; }

        public float totalBrutoSKU { get; set; }

        public float totalLiquidoSKU { get; set; }

        public int totalUnidades { get; set; }

        public int totalApresentacoes{ get; set; }

        public List<DtoMixProdutosPedidoEnvio> pedidos { get; set; }
    }

    public class DtoMixProdutosPedidoEnvio : PharmaLink_Sku
    {
        public int qtd { get; set; }
        public string discount { get; set; }
        public string bruto { get; set; }
        public string liq { get; set; }
    }
}
