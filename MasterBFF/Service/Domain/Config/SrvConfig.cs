using Master.Entity;
using Master.Entity.Dto.Domain.Auth;
using Master.Entity.Dto.Infra;
using Master.Service.Base;
using MasterBiolabBFF.Entity.Dto.PharmaLink;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace Master.Service.Domain.Config
{
    public class SrvConfig : SrvBase
    {
        public DtoConfiguration OutDto;

        public bool Exec(LocalNetwork network)
        {
            try
            {
                OutDto = new DtoConfiguration();

                Parallel.ForEach("1,2,3".Split(','), item =>
                {
                    switch (item)
                    {
                        case "1":
                            {
                                var client = new RestClient(network.api);
                                var request = new RestRequest("BiolabECS/api/programas", Method.GET);

                                request.AddHeader("Content-Type", "application/json");

                                var response = client.Execute(request);

                                PharmaLink_ProgramInfo[] programInfoArray = JsonConvert.DeserializeObject<PharmaLink_ProgramInfo[]>(response.Content);

                                OutDto.welcome = programInfoArray[0].Descricao;

                                client.ClearHandlers();
                            }
                            break;

                        case "2":
                            {
                                var client = new RestClient(network.api);
                                var request = new RestRequest("BiolabECS/api/canaisatendimentos", Method.GET);

                                request.AddHeader("Content-Type", "application/json");

                                var response = client.Execute(request);

                                foreach (var itemX in JsonConvert.DeserializeObject<PharmaLink_Atendimento[]>(response.Content))
                                {
                                    switch (itemX.IdCanalAtendimentoTipo)
                                    {
                                        case 1: OutDto.telefone = itemX.Descricao; break;
                                        case 2: OutDto.email = itemX.Descricao; break;
                                        case 3: OutDto.chat = itemX.Descricao; break;
                                    }
                                }

                                client.ClearHandlers();
                            }
                            break;

                        case "3":
                            {
                                var client = new RestClient(network.api);
                                var request = new RestRequest("BiolabECS/api/temas", Method.GET);

                                request.AddHeader("Content-Type", "application/json");

                                var response = client.Execute(request);

                                foreach (var itemX in JsonConvert.DeserializeObject<PharmaLink_Tema[]>(response.Content))
                                {
                                    OutDto.PaletaCorA = itemX.PaletaCorA;
                                    OutDto.PaletaCorB = itemX.PaletaCorB;
                                    OutDto.PaletaCorC = itemX.PaletaCorC;
                                    OutDto.PaletaCorD = itemX.PaletaCorD;
                                    break;
                                }

                                client.ClearHandlers();
                            }
                            break;
                    }
                });

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
