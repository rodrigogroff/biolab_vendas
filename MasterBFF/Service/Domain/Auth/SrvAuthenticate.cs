using Master.Entity;
using Master.Entity.Dto.Domain.Auth;
using Master.Entity.Dto.Infra;
using Master.Service.Base;
using MasterBiolabBFF.Entity.Dto.PharmaLink;
using Newtonsoft.Json;
using RestSharp;
using System;

namespace Master.Service.Domain.Auth
{
    public class SrvAuthenticate : SrvBase
    {
        public DtoAuthenticatedUser OutCurrentAuth = new DtoAuthenticatedUser();

        public bool Exec(LocalNetwork network, DtoLoginInformation dto)
        {
            try
            {
                var client = new RestClient(network.api);
                var request = new RestRequest("BiolabECS/token", Method.POST);

                request.AddHeader("Content-Type", "application/json");

                request.AddParameter("grant_type", "password");
                request.AddParameter("UserName", dto.email);
                request.AddParameter("Password", dto.password);

                var response = client.Execute(request);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    Error = new DtoServiceError
                    {
                        message = "Dados incorretos",
                        debugInfo = ""
                    };

                    return false;
                }

                PharmaLink_AccessTokenResponse rep = JsonConvert.DeserializeObject<PharmaLink_AccessTokenResponse>(response.Content);

                client.ClearHandlers();

                OutCurrentAuth.access_token = rep.access_token;
                OutCurrentAuth.token_type = rep.token_type;
                OutCurrentAuth.refresh_token = rep.refresh_token;
                OutCurrentAuth.UserId = rep.UserId;
                OutCurrentAuth.Nome = rep.Nome;
                OutCurrentAuth.PerfilId = rep.PerfilId;
                OutCurrentAuth.Perfil = rep.Perfil;

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
