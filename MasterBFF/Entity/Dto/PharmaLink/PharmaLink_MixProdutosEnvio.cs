using System;
using System.Collections.Generic;

namespace MasterBiolabBFF.Entity.Dto.PharmaLink
{
    public class PharmaLinkEnvio_CondicaoComercial
    {
        public int IdDescontoBase { get; set; }
        public int PercentualDescontoBase { get; set; }
        public int IdDescontoNegociado { get; set; }
        public int PercentualDescontoNegociado { get; set; }
        public int IdDescontoNegociacao1 { get; set; }
        public int PercentualDescontoNegociacao1 { get; set; }
        public int IdDescontoNegociacao2 { get; set; }
        public int PercentualDescontoNegociacao2 { get; set; }
        public int IdDescontoNegociacao3 { get; set; }
        public int PercentualDescontoNegociacao3 { get; set; }
        public int IdDescontoNegociacao4 { get; set; }
        public int PercentualDescontoNegociacao4 { get; set; }
        public int IdDescontoCupom { get; set; }
        public int PercentualDescontoCupom { get; set; }
        public int DescontoDisponivel { get; set; }
        public int DescontoGestor { get; set; }
        public int PercentualDescontoOLSpread { get; set; }
    }

    public class PharmaLinkEnvio_DistribuidorB
    {
        public bool Ativo { get; set; }
        public bool HabilitaContaCorrente { get; set; }
        public int Id { get; set; }
        public string NomeFantasia { get; set; }
        public object RazaoSocial { get; set; }
        public int ValorMinimoDePedido { get; set; }
    }

    public class PharmaLinkEnvio_DistribuidorA
    {
        public int PdvId { get; set; }
        public int DistribuidorId { get; set; }
        public int OrdemDePreferencia { get; set; }
        public int OrdemMelhorAtendimento { get; set; }
        public bool selecionado { get; set; }

        public PharmaLinkEnvio_DistribuidorB Distribuidor { get; set; }
    }

    public class PharmaLinkEnvio_DistribuidoresPrazoPagamento
    {
        public int idProduto { get; set; }
        public int IdPrazoPagamento { get; set; }
        public List<int> idsDistribuidores { get; set; }
    }

    public class PharmaLinkEnvio_FaixasDesconto
    {
        public int QuantidadeMinima { get; set; }
        public int QuantidadeMaxima { get; set; }
        public PharmaLinkEnvio_CondicaoComercial CondicaoComercial { get; set; }
        public bool atual { get; set; }
        public int PercentualDesconto { get; set; }
    }

    public class PharmaLinkEnvio_FaixaSelecionada
    {
        public int QuantidadeMinima { get; set; }
        public int QuantidadeMaxima { get; set; }
        public PharmaLinkEnvio_CondicaoComercial CondicaoComercial { get; set; }
        public bool atual { get; set; }
        public int PercentualDesconto { get; set; }
    }

    public class PharmaLinkEnvio_ItensDoPedido
    {
        public List<object> FiltrosPersonalizados { get; set; }
        public string descricao { get; set; }
        public string apresentacaoDUN { get; set; }
        public bool destaque { get; set; }
        public string ean { get; set; }
        public string DUN { get; set; }
        public List<PharmaLinkEnvio_FaixasDesconto> faixasDesconto { get; set; }
        public string familia { get; set; }
        public int idProduto { get; set; }
        public string idProdutoDun { get; set; }
        public int idTipoProduto { get; set; }
        public int idFamilia { get; set; }
        public string laboratorio { get; set; }
        public bool isDemonstraGridPedido { get; set; }
        public DateTime menorDataVigencia { get; set; }
        public double preco { get; set; }
        public bool precoDistribuidor { get; set; }
        public object quantidadeEstoque { get; set; }
        public int quantidadeMinima { get; set; }
        public bool compradoAte30Dias { get; set; }
        public bool compradoAcima30Dias { get; set; }
        public int quantidade { get; set; }
        public int QuantidadeDUN { get; set; }
        public double valorBruto { get; set; }
        public double valorLiquido { get; set; }
        public string descontoAdicional { get; set; }
        public string descontoItem { get; set; }
        public int descontoTotal { get; set; }
        public object caminhoFoto { get; set; }
        public double precoPor { get; set; }
        public bool cupomDesconto { get; set; }
        public string StatusDistribuidor { get; set; }
        public List<int> Distribuidores { get; set; }
        public List<int> DistribuidoresFlat { get; set; }
        public PharmaLinkEnvio_DistribuidoresPrazoPagamento DistribuidoresPrazoPagamento { get; set; }
        public List<object> DescontoPorVolumeBDShow { get; set; }
        public bool isDun { get; set; }
        public object metricaMdtr { get; set; }
        public bool DescontoEditado { get; set; }
        public PharmaLinkEnvio_FaixaSelecionada faixaSelecionada { get; set; }
        public int quantidadeAnterior { get; set; }
        public bool abrirRange { get; set; }
        public int DistribuidorQueAtende { get; set; }
        public int DescontoAdicional { get; set; }
        public int DescontoDispGest { get; set; }
        public int DescontoDispRep { get; set; }
        public int IdProduto { get; set; }
        public int Quantidade { get; set; }
        public int DescontoTotal { get; set; }
        public string Desconto { get; set; }
        public string idProdutoDUN { get; set; }
        public int QtdeDUN { get; set; }
        public int QtdeProdutoTotal { get; set; }
        public bool isDUN { get; set; }
        public List<object> condicoesMixIdeal { get; set; }
        public bool usandoMixIdeal { get; set; }
        public int descontoMix { get; set; }
        public int quantidadeMinimaMix { get; set; }
    }

    public class PharmaLinkEnvio_MixProdutos_Request
    {
        public bool IsPedidoComboOferta { get; set; }
        public string TipoPedido { get; set; }
        public Guid AgrupadorPedido { get; set; }
        public string CNPJ { get; set; }
        public PharmaLinkEnvio_DistribuidorA Distribuidor { get; set; }
        public List<PharmaLinkEnvio_DistribuidorA> Distribuidores { get; set; }
        public int DistribuidorId { get; set; }
        public List<PharmaLinkEnvio_ItensDoPedido> ItensDoPedido { get; set; }
        public List<object> CombosDoPedido { get; set; }
        public int CondicaoComercialBaseId { get; set; }
        public int IdTabloide { get; set; }
        public bool ForcarGerenciamento { get; set; }
        public int IdPrazoPagamento { get; set; }
        public string Origem { get; set; }
        public int PdvId { get; set; }
        public string Prazo { get; set; }
        public string ReferenciaPedido { get; set; }
        public string SomenteValidar { get; set; }
        public string TipoLooping { get; set; }
        public string UserId { get; set; }
        public Guid ChaveUnicaPedido { get; set; }
        public Guid ReferenciaDePedido { get; set; }
        public List<object> ProdutosCombo { get; set; }
        public int totalApresentacoesSKU { get; set; }
        public int totalUnidadesSKU { get; set; }
        public int descontoTotalSKU { get; set; }
        public int descontoRecebidoSKU { get; set; }
        public double totalBrutoSKU { get; set; }
        public double totalLiquidoSKU { get; set; }
        public int ValorMinimoDistribuidor { get; set; }
        public bool PedidoAtingeMinimoDistribuidor { get; set; }
        public int totalQtdeReal { get; set; }
        public List<object> DatasProgramadas { get; set; }
        public string Observacao { get; set; }
    }
}
