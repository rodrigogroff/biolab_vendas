using Master.Entity;
using Master.Entity.Dto.Domain.MixProdutos;
using Master.Entity.Dto.Infra;
using Master.Service.Base;
using MasterBiolabBFF.Entity.Dto.PharmaLink;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Master.Service.Domain.MixProdutos
{
    public class SrvMixProdutosEnvio : SrvBase
    {
        public PharmaLink_MixProdutosEnvioResponse OutDto;

        public bool Exec(LocalNetwork network, DtoAuthenticatedUser user, DtoMixProdutosEnvioRequest req)
        {
            try
            {
                var client = new RestClient(network.api);
                var request = new RestRequest("BiolabECS/api/pedidos/enviarPedidoMix", Method.POST);

                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer " + user.access_token);

                var lst_final = new List<PharmaLinkEnvio_MixProdutos_Request>();

                int totApresentacoes = 0;

                foreach (var item in req.distribuidores)
                {
                    totApresentacoes += item.pedidos.Count;
                }

                var mx_envio = new PharmaLinkEnvio_MixProdutos_Request
                {
                    AgrupadorPedido = Guid.NewGuid(),
                    ChaveUnicaPedido = Guid.NewGuid(),
                    CNPJ = req.cnpj,
                    CombosDoPedido = new List<object>(),
                    CondicaoComercialBaseId = 18669,// parece fixo
                    DatasProgramadas = new List<object> (),
                    descontoRecebidoSKU = 0,
                    descontoTotalSKU = 2, // calcular...
                    Distribuidor = new PharmaLinkEnvio_DistribuidorA(),
                    Distribuidores = new List<PharmaLinkEnvio_DistribuidorA> (),
                    DistribuidorId = req.distribuidores[0].DistribuidorId,
                    ForcarGerenciamento = false,
                    IdPrazoPagamento = req.distribuidores[0].IdPrazo,
                    IdTabloide = 0,
                    IsPedidoComboOferta = false,
                    Observacao = string.Empty,
                    Origem = "WEBClient",
                    PdvId = req.pdvId,
                    PedidoAtingeMinimoDistribuidor = true,
                    Prazo = "AVista",
                    ProdutosCombo = new List<object> (),
                    ReferenciaDePedido = Guid.NewGuid(),
                    ReferenciaPedido = string.Empty,
                    SomenteValidar = "false",
                    TipoLooping = "LoopingAutomatico",
                    TipoPedido = req.distribuidores[0].TipoPedido,
                    totalApresentacoesSKU = totApresentacoes,
                    totalBrutoSKU = req.distribuidores[0].totalBrutoSKU,
                    totalLiquidoSKU = req.distribuidores[0].totalLiquidoSKU,
                    totalQtdeReal = req.distribuidores[0].totalUnidades,
                    totalUnidadesSKU = req.distribuidores[0].totalUnidades,
                    UserId = user.UserId,
                    ValorMinimoDistribuidor = 0,
                    ItensDoPedido = new List<PharmaLinkEnvio_ItensDoPedido>()
                };

                foreach (var dist in req.distribuidores) // vai ter somente 1 na POC
                {
                    mx_envio.Distribuidor = new PharmaLinkEnvio_DistribuidorA
                    {
                        DistribuidorId = dist.DistribuidorId,
                        OrdemDePreferencia = 0,
                        OrdemMelhorAtendimento = 0,
                        PdvId = req.pdvId,
                        selecionado = true,
                        Distribuidor = new PharmaLinkEnvio_DistribuidorB
                        {
                            Ativo = true,
                            HabilitaContaCorrente = false,
                            Id = dist.DistribuidorId,
                            NomeFantasia = dist.NomeFantasia,
                            RazaoSocial = null,
                            ValorMinimoDePedido = 0
                        }
                    };

                    mx_envio.Distribuidores.Add(mx_envio.Distribuidor);

                    foreach (var pedido in dist.pedidos)
                    {
                        mx_envio.ItensDoPedido.Add(new PharmaLinkEnvio_ItensDoPedido
                        {
                            abrirRange = false,
                            apresentacaoDUN = string.Empty,
                            caminhoFoto = null,
                            compradoAte30Dias = true,
                            compradoAcima30Dias = false,
                            condicoesMixIdeal = new List<object>(),
                            cupomDesconto = false,
                            Desconto = pedido.discount,
                            descontoAdicional = "0.00",
                            DescontoAdicional = 0,
                            DescontoDispGest = 0,
                            DescontoDispRep = 0,
                            DescontoEditado = false,
                            descontoItem = pedido.discount,
                            descontoMix = 0,
                            DescontoPorVolumeBDShow = new List<object>(),
                            descontoTotal = Convert.ToInt32(pedido.discount.Replace(",00", "").Replace(".00", "")),
                            DescontoTotal = Convert.ToInt32(pedido.discount.Replace(",00", "").Replace(".00", "")),
                            descricao = pedido.Descricao,
                            destaque = false,
                            Distribuidores = new List<int> { dist.DistribuidorId },
                            DistribuidoresFlat = new List<int> { dist.DistribuidorId },
                            DistribuidoresPrazoPagamento = new PharmaLinkEnvio_DistribuidoresPrazoPagamento()
                            {
                                IdPrazoPagamento = req.distribuidores[0].IdPrazo, // a vista
                                idProduto = pedido.IdProduto,
                                idsDistribuidores = new List<int> { dist.DistribuidorId }
                            },
                            DistribuidorQueAtende = dist.DistribuidorId,
                            DUN = string.Empty,
                            ean = pedido.Ean,
                            faixasDesconto = new List<PharmaLinkEnvio_FaixasDesconto>()
                            {
                                new PharmaLinkEnvio_FaixasDesconto
                                {
                                    atual = true,
                                    CondicaoComercial = new PharmaLinkEnvio_CondicaoComercial
                                    {
                                        DescontoDisponivel = 0,
                                        DescontoGestor = 0,
                                        IdDescontoBase = 7574629,
                                        IdDescontoCupom = 0,
                                        IdDescontoNegociacao1 = 0,
                                        IdDescontoNegociacao2 = 0,
                                        IdDescontoNegociacao3 = 0,
                                        IdDescontoNegociacao4 = 0,
                                        IdDescontoNegociado = 0,
                                        PercentualDescontoBase = Convert.ToInt32(pedido.discount.Replace(",00", "").Replace(".00", "")),
                                        PercentualDescontoCupom = 0,
                                        PercentualDescontoNegociacao1 = 0,
                                        PercentualDescontoNegociacao2 = 0,
                                        PercentualDescontoNegociacao3 = 0,
                                        PercentualDescontoNegociacao4 = 0,
                                        PercentualDescontoNegociado = 0,
                                        PercentualDescontoOLSpread = 0
                                    },
                                    PercentualDesconto = Convert.ToInt32(pedido.discount.Replace(",00", "").Replace(".00", "")),
                                    QuantidadeMaxima = pedido.FaixasDesconto[0].QuantidadeMaxima,
                                    QuantidadeMinima = pedido.FaixasDesconto[0].QuantidadeMinima,
                                },
                            },
                            faixaSelecionada = new PharmaLinkEnvio_FaixaSelecionada
                            {
                                atual = true,
                                CondicaoComercial = new PharmaLinkEnvio_CondicaoComercial
                                {
                                    DescontoDisponivel = 0,
                                    DescontoGestor = 0,
                                    IdDescontoBase = 7574629,
                                    IdDescontoCupom = 0,
                                    IdDescontoNegociacao1 = 0,
                                    IdDescontoNegociacao2 = 0,
                                    IdDescontoNegociacao3 = 0,
                                    IdDescontoNegociacao4 = 0,
                                    IdDescontoNegociado = 0,
                                    PercentualDescontoBase = Convert.ToInt32(pedido.discount.Replace(",00", "").Replace(".00", "")),
                                    PercentualDescontoCupom = 0,
                                    PercentualDescontoNegociacao1 = 0,
                                    PercentualDescontoNegociacao2 = 0,
                                    PercentualDescontoNegociacao3 = 0,
                                    PercentualDescontoNegociacao4 = 0,
                                    PercentualDescontoNegociado = 0,
                                    PercentualDescontoOLSpread = 0
                                },
                                PercentualDesconto = Convert.ToInt32(pedido.discount.Replace(",00", "").Replace(".00", "")),
                                QuantidadeMaxima = 99999, // pegar depois da SKU original
                                QuantidadeMinima = 1,// pegar depois da SKU original
                            },
                            familia = pedido.Familia,
                            FiltrosPersonalizados = new List<object>(),
                            idFamilia = pedido.IdFamilia,
                            idProduto = pedido.IdProduto,
                            IdProduto = pedido.IdProduto,
                            idProdutoDun = string.Empty,
                            idProdutoDUN = string.Empty,
                            idTipoProduto = pedido.IdTipoProduto,
                            isDemonstraGridPedido = true,
                            isDun = false,
                            isDUN = false,
                            laboratorio = pedido.Laboratorio,
                            menorDataVigencia = DateTime.Now.AddYears(1), // pegar da sku
                            metricaMdtr = null,
                            preco = Convert.ToDouble(pedido.bruto),
                            precoDistribuidor = false,
                            precoPor = Convert.ToDouble(pedido.bruto),
                            QtdeDUN = 0,
                            QtdeProdutoTotal = pedido.qtd,
                            quantidade = pedido.qtd,
                            Quantidade = pedido.qtd,
                            quantidadeAnterior = 0,
                            QuantidadeDUN = 0,
                            quantidadeEstoque = null,
                            quantidadeMinima = 0,
                            quantidadeMinimaMix = 0,
                            StatusDistribuidor = "AtendidoPeloPrimeiro",
                            usandoMixIdeal = false,
                            valorBruto = Convert.ToDouble(pedido.bruto),
                            valorLiquido = Convert.ToDouble(pedido.liq),
                        });
                    }
                }

                lst_final.Add(mx_envio);

                request.AddJsonBody(lst_final);

                var str = JsonConvert.SerializeObject(lst_final);

                var response = client.Execute(request);

                OutDto = JsonConvert.DeserializeObject<PharmaLink_MixProdutosEnvioResponse>(response.Content);

                client.ClearHandlers();

                return true;
            }
            catch (Exception ex)
            {
                Error = new DtoServiceError
                {
                    message = GENERIC_FAIL,
                    debugInfo = ex.ToString()
                };

                return false;
            }
        }
    }
}
