using Master.Entity.Dto.Domain.MixProdutos;
using Master.Entity.Dto.Infra;
using Master.Service.Base;
using MasterBiolabBFF.Entity.Dto.PharmaLink;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Master.Service.Domain.MixProdutos
{
    public class SrvMixProdutosPaginated : SrvBase
    {
        public PharmaLink_MixProdutosPage OutDto;

        public bool Exec(PharmaLink_MixProdutos result, DtoMixProdutosPageRequest request)
        {
            try
            {
                OutDto = new PharmaLink_MixProdutosPage ();

                OutDto.Produtos = result.Produtos;
                OutDto.FamiliasProdutos = result.FamiliasProdutos;
                OutDto.TiposProdutos = result.TiposProdutos;
                OutDto.ProdutosDistribuidores = result.ProdutosDistribuidores;

                var SkusFiltrados = new List<PharmaLink_Sku>();

                foreach (var item in result.Produtos.Skus)
                {
                    bool add = true;

                    if (request.destaque == true)
                    {
                        add = item.Destaque == true;
                    }

                    if (request.recente == true)
                    {
                        add = item.IsDemonstraGridPedido == true;
                    }

                    if (request.mais30 == true)
                    {
                        add = item.Status == "Acima30Dias";
                    }

                    if (request.IdTipoProduto != null)
                    {
                        if (request.IdTipoProduto.Any())
                        {
                            add = false;

                            foreach (var _idTipo in request.IdTipoProduto)
                            {
                                if (item.IdTipoProduto == _idTipo)
                                {
                                    add = true;
                                    break;
                                }
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(request.texto))
                    {
                        add = false;

                        var tex = request.texto.ToLower();

                        if (item.Laboratorio.ToLower() == tex)
                        {
                            add = true;
                        }

                        if (item.Ean == tex)
                        {
                            add = true;
                        }

                        if (item.Descricao.ToLower().Contains(tex))
                        {
                            add = true;
                        }
                    }

                    if (add == true)
                    {
                        SkusFiltrados.Add(item);
                    }
                }

                if (SkusFiltrados.Count == 0)
                {
                    return true;
                }

                OutDto.pageSize = request.pageSize;
                OutDto.totalPages = SkusFiltrados.Count / request.pageSize;

                if (OutDto.totalPages == 0)
                    OutDto.totalPages = 1;

                OutDto.totalItens = SkusFiltrados.Count;
                OutDto.Produtos.Skus = SkusFiltrados.
                                        Skip((request.pageIndex - 1) * request.pageSize).
                                        Take(request.pageSize).
                                        ToList();

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
