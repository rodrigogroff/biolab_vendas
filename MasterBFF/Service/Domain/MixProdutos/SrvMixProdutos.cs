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
    public class SrvMixProdutos : SrvBase
    {
        public PharmaLink_MixProdutos OutDto;

        public bool Exec(LocalNetwork network, DtoAuthenticatedUser user, DtoMixProdutosRequest req)
        {
            try
            {
                var client = new RestClient(network.api);
                var request = new RestRequest("BiolabECS/api/produtos/obterMixProdutos/?noCache=1693152555987", Method.POST);

                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer " + user.access_token);

                var mx = new PharmaLink_MixRequest
                {
                    IdPrazoPagamento = req.IdPrazoPagamento,
                    PdvId = req.IdPdv,
                    CNPJ = req.cnpj,
                    CondicaoComercialBaseId = 18669,
                    IdsDistribuidores = new List<PharmaLink_MixDistRequest>(),
                    TipoDePedido = 4,
                    UtilizaComboOferta = true,
                };

                foreach (var item in req.distribuidores)
                {
                    mx.IdsDistribuidores.Add(new PharmaLink_MixDistRequest
                    {
                        IdDistribuidor = item.IdDistribuidor,
                        Ordem = item.Ordem,
                    });
                }

                request.AddJsonBody(mx);

                var response = client.Execute(request);

                OutDto = JsonConvert.DeserializeObject<PharmaLink_MixProdutos>(response.Content);

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
