using Master.Entity;
using Master.Entity.Dto.Domain.PDV;
using Master.Entity.Dto.Infra;
using Master.Service.Base;
using MasterBiolabBFF.Entity.Dto.PharmaLink;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Master.Service.Domain.PDV
{
    public class SrvPDVInfo : SrvBase
    {
        public List<DtoPDVInfo> OutDto;

        public bool Exec(LocalNetwork network, DtoAuthenticatedUser user, string pdv_desc)
        {
            try
            {
                OutDto = new List<DtoPDVInfo>();

                var client = new RestClient(network.api);
                var request = new RestRequest("BiolabECS/api/lojas", Method.POST);

                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer " + user.access_token);

                request.AddJsonBody(new
                {
                    Filtro = pdv_desc,
                    Status = true,
                    user.UserId
                });

                var response = client.Execute(request);

                PharmaLink_PDV[] programInfoArray = JsonConvert.DeserializeObject<PharmaLink_PDV[]>(response.Content);

                if (programInfoArray.Any())
                {
                    foreach (var programInfo in programInfoArray)
                    {
                        OutDto.Add(new DtoPDVInfo
                        {
                            Id = programInfo.Id,
                            CNPJ = programInfo.CNPJ,
                            RazaoSocial = programInfo.RazaoSocial,
                        });
                    }
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
