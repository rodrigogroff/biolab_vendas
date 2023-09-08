using System;
using System.Collections.Generic;

namespace MasterBiolabBFF.Entity.Dto.PharmaLink
{
    public class PharmaLinkMixResp_ParametroEspelhoPedido
    {
        public object IdPedido { get; set; }
        public object IdPedidoFake { get; set; }
        public string Visualizacao { get; set; }
        public object Email { get; set; }
        public string Agrupador { get; set; }
        public object Usuario { get; set; }
    }

    public class PharmaLinkMixResp_Pedido
    {
        public bool PedidoCriado { get; set; }
        public bool PedidoProgramado { get; set; }
        public bool PedidoEnviadoParaAprovacao { get; set; }
        public string NumeroPedido { get; set; }
        public PharmaLinkMixResp_ParametroEspelhoPedido ParametroEspelhoPedido { get; set; }
        public object ErrosPedido { get; set; }
    }

    public class PharmaLink_MixProdutosEnvioResponse
    {
        public List<PharmaLinkMixResp_Pedido> pedidos { get; set; }
        public bool erro { get; set; }
        public bool Editavel { get; set; }
    }


}
