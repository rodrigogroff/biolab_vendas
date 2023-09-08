using Master.Entity;
using Master.Entity.Dto.Domain.PDV;
using Master.Entity.Dto.Infra;
using Master.Service.Base;
using MasterBiolabBFF.Entity.Dto.PharmaLink;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Master.Service.Domain.PDV
{
    public class SrvPDVDistInfo : SrvBase
    {
        public List<DtoPDVDistribuidorInfo> OutDto;

        public bool Exec(LocalNetwork network, DtoAuthenticatedUser user, int idPdv)
        {
            try
            {
                OutDto = new List<DtoPDVDistribuidorInfo>();

                var client = new RestClient(network.api);
                var request = new RestRequest("BiolabECS/api/lojas/obterParametrizacoesLoja/true", Method.POST);

                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer " + user.access_token);

                request.AddJsonBody(new
                {
                    idLoja = idPdv,
                    tipoPedido = "rep",
                    user.UserId
                });

                var response = client.Execute(request);

                var resp = JsonConvert.DeserializeObject<PharmaLink_PDVDist>(response.Content);

                foreach (var item in resp.DistribuidoresPrazoPagamento)
                {
                    OutDto.Add(new DtoPDVDistribuidorInfo
                    {
                        Id = item.DistribuidorId,
                        NomeFantasia = item.Distribuidor.NomeFantasia
                    });
                }

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
