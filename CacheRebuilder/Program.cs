using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using RestSharp;

namespace CacheRebuilder
{
    public class CachedNode
    {
        public string tag { get; set; }
        public string route { get; set; }
        public DateTime expires { get; set; }
        public object input { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var cacheLocation = File.ReadAllText("config.txt");
            var storyFile = "history.txt";

            if (File.Exists(storyFile)) 
                foreach (var item in JsonConvert.DeserializeObject<List<CachedNode>>(File.ReadAllText(storyFile)))
                    ProcessCache(item, cacheLocation);

            Thread.Sleep(60 * 1000);

            while (true)
            {
                try
                {
                    var client = new RestClient(cacheLocation);
                    var request = new RestRequest("api/open_cache", Method.GET);
                    request.AddHeader("Content-Type", "application/json");
                    var response = client.Execute(request);
                    if (response.IsSuccessful)
                    {
                        client.ClearHandlers();

                        if (File.Exists(storyFile))
                            File.Delete(storyFile);

                        File.WriteAllText(storyFile, response.Content);

                        foreach (var item in JsonConvert.DeserializeObject<List<CachedNode>>(response.Content))
                            if (DateTime.Now > item.expires.AddMinutes(-2))
                            {
                                Console.WriteLine(DateTime.Now.ToString() + "Expired! " + item.route);
                                ProcessCache(item, cacheLocation);
                            }
                    }
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine("2>> " + ex.ToString());
                }
                Thread.Sleep(60 * 1000);
            }
        }

        static void ProcessCache(CachedNode item, string cacheLocation)
        {
            try
            {
                Console.WriteLine( DateTime.Now.ToString() +  " --> Rebuilding " + item.route);
                var clientR = new RestClient(cacheLocation);
                var requestR = new RestRequest(item.route, Method.POST);
                requestR.AddHeader("Content-Type", "application/json");
                requestR.AddHeader("App", "rebuild");

                if (item.input != null)
                    requestR.AddBody(item.input);

                var resp = clientR.Execute(requestR);
                clientR.ClearHandlers();
                
                if (resp.IsSuccessful)
                    Console.WriteLine("OK!");
                else
                    Console.WriteLine("FAIL!");
            }
            catch (System.Exception ex) 
            {
                Console.WriteLine("1 >> " + ex.ToString());
            }
        }
    }
}
