using System;
using System.Collections.Generic;

namespace MasterBiolabBFF.Entity.Dto.PharmaLink
{
    public class PharmaLink_MixDistRequest
    {
        public int IdDistribuidor { get; set; }
        public int Ordem { get; set; }
    }

    public class PharmaLink_MixRequest
    {
        public int IdPrazoPagamento { get; set; }

        public int PdvId { get; set; }

        public int TipoDePedido { get; set; }

        public bool UtilizaComboOferta { get; set; }

        public string CNPJ { get; set; }

        public int CondicaoComercialBaseId { get; set; }

        public List<PharmaLink_MixDistRequest> IdsDistribuidores = new List<PharmaLink_MixDistRequest>();
    }

    public class PharmaLink_CondicaoComercial
    {
        public int IdDescontoBase { get; set; }
        public double PercentualDescontoBase { get; set; }
        public int IdDescontoNegociado { get; set; }
        public double PercentualDescontoNegociado { get; set; }
        public int IdDescontoNegociacao1 { get; set; }
        public double PercentualDescontoNegociacao1 { get; set; }
        public int IdDescontoNegociacao2 { get; set; }
        public double PercentualDescontoNegociacao2 { get; set; }
        public int IdDescontoNegociacao3 { get; set; }
        public double PercentualDescontoNegociacao3 { get; set; }
        public int IdDescontoNegociacao4 { get; set; }
        public double PercentualDescontoNegociacao4 { get; set; }
        public int IdDescontoCupom { get; set; }
        public double PercentualDescontoCupom { get; set; }
        public double DescontoDisponivel { get; set; }
        public double DescontoGestor { get; set; }
        public double PercentualDescontoOLSpread { get; set; }
    }

    public class PharmaLink_FaixasDesconto
    {
        public int QuantidadeMinima { get; set; }
        public int QuantidadeMaxima { get; set; }
        public PharmaLink_CondicaoComercial CondicaoComercial { get; set; }
    }

    public class PharmaLink_FamiliasProduto
    {
        public int IdFamilia { get; set; }
        public string Descricao { get; set; }
        public string CaminhoFoto { get; set; }
    }

    public class PharmaLink_Produtos
    {
        public List<PharmaLink_Sku> Skus { get; set; }
        public object Tabloide { get; set; }
        public List<object> CombosOferta { get; set; }
    }

    public class PharmaLink_ProdutosDistribuidore
    {
        public int idProduto { get; set; }
        public int IdPrazoPagamento { get; set; }
        public List<int> idsDistribuidores { get; set; }
    }

    public class PharmaLink_MixProdutos
    {
        public PharmaLink_Produtos Produtos { get; set; }
        public List<PharmaLink_FamiliasProduto> FamiliasProdutos { get; set; }
        public List<PharmaLink_TiposProduto> TiposProdutos { get; set; }
        public List<PharmaLink_ProdutosDistribuidore> ProdutosDistribuidores { get; set; }
    }

    public class PharmaLink_MixProdutosPage : PharmaLink_MixProdutos
    {
        public int totalItens { get; set; }
        public int totalPages { get; set; }
        public int pageSize { get; set; }
    }

    public class PharmaLink_Sku
    {
        public int IdProduto { get; set; }
        public string Ean { get; set; }
        public double Desconto { get; set; }
        public string Descricao { get; set; }
        public string Laboratorio { get; set; }
        public int IdFamilia { get; set; }
        public string Familia { get; set; }
        public double Preco { get; set; }
        public int QuantidadeMinima { get; set; }
        public List<object> FiltrosPersonalizados { get; set; }
        public bool Destaque { get; set; }
        public string Status { get; set; }
        public List<PharmaLink_FaixasDesconto> FaixasDesconto { get; set; }
        public DateTime MenorDataVigencia { get; set; }
        public object QuantidadeEstoque { get; set; }
        public bool PrecoDistribuidor { get; set; }
        public int Quantidade { get; set; }
        public string CaminhoFoto { get; set; }
        public int IdTipoProduto { get; set; }
        public bool IsDemonstraGridPedido { get; set; }
        public bool ConcedeDesconto { get; set; }
        public bool RecebeDesconto { get; set; }
        public double PrecoMinimo { get; set; }
        public List<object> DescontoPorVolumeBD { get; set; }
        public object MetricaMdtr { get; set; }
    }

    public class PharmaLink_TiposProduto
    {
        public int IdTipoProduto { get; set; }
        public string Descricao { get; set; }
    }
}
