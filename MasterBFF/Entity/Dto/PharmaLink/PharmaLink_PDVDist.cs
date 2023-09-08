using System;
using System.Collections.Generic;

namespace MasterBiolabBFF.Entity.Dto.PharmaLink
{
    public class PharmaLink_ParametrizacaoLooping
    {
        public bool UsarLooping { get; set; }
        public int Tipo { get; set; }
        public int Origem { get; set; }
        public bool OrdenacaoDesconto { get; set; }
        public bool MostrarDescontoMedio { get; set; }
        public bool UtilizaCasasDecimais { get; set; }
        public bool UtilizaPrecoDistribuidor { get; set; }
        public bool ExibirDescontoBase { get; set; }
        public bool ExibirDescontoFaixa1 { get; set; }
        public bool ExibirDescontoFaixa2 { get; set; }
        public bool ExibirDescontoFaixa3 { get; set; }
        public bool ExibirDescontoFaixa4 { get; set; }
        public bool ExibirDescontoNegociado { get; set; }
        public bool UtilizaComboOferta { get; set; }
    }

    public class PharmaLink_Distribuidor
    {
        public int Id { get; set; }
        public bool Ativo { get; set; }
        public double ValorMinimoDePedido { get; set; }
        public string NomeFantasia { get; set; }
        public object RazaoSocial { get; set; }
        public bool HabilitaContaCorrente { get; set; }
    }

    public class PharmaLink_DistribuidoresPrazoPagamento
    {
        public int PdvId { get; set; }
        public int DistribuidorId { get; set; }
        public int OrdemDePreferencia { get; set; }
        public int OrdemMelhorAtendimento { get; set; }
        public PharmaLink_Distribuidor Distribuidor { get; set; }
    }

    public class PharmaLink_PrazoPagamento
    {
        public int IdPrazoPagamento { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public int Prazo { get; set; }
        public bool Rep { get; set; }
        public bool Padrao { get; set; }
        public bool Especial { get; set; }
        public bool RepDefault { get; set; }
        public bool PadraoDefault { get; set; }
        public bool EspecialDefault { get; set; }
        public List<object> TiposDePedido { get; set; }
        public List<object> PrazosDefault { get; set; }
        public List<PharmaLink_DistribuidoresPrazoPagamento> Distribuidores { get; set; }
        public bool Selecionado { get; set; }
    }

    public class PharmaLink_CondicaoComercialBase
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int Nivel { get; set; }
        public double ValorMinimoDePedido { get; set; }
    }

    public class PharmaLink_PDVDist
    {
        public PharmaLink_ParametrizacaoLooping ParametrizacaoLooping { get; set; }
        public List<PharmaLink_DistribuidoresPrazoPagamento> DistribuidoresPrazoPagamento { get; set; }
        public object DistribuidoresPdv { get; set; }
        public List<PharmaLink_PrazoPagamento> PrazoPagamento { get; set; }
        public object Tabloides { get; set; }
        public PharmaLink_CondicaoComercialBase CondicaoComercialBase { get; set; }
        public string ChaveUnicaPedido { get; set; }
    }

}
