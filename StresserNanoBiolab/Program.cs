using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using RestSharp;
using System.Collections.Concurrent;

public class Program
{
    static string api_location = "";

    static Guid myNodeId,
                myTestId;

    static void Main()
    {
        var id = "D_" + Guid.NewGuid();
        var chromeDriverPath = Environment.CurrentDirectory + "\\" + id;

        Directory.CreateDirectory(chromeDriverPath);
        
        File.Copy(Environment.CurrentDirectory + "\\" + "msedgedriver.exe", Directory.GetCurrentDirectory() + "\\" + id + "\\" + "msedgedriver.exe");

        api_location = File.ReadAllText("api.txt");

        Console.WriteLine(api_location);
        Console.CursorVisible = false;

        register();
        sinc();

        // -----------------------
        // work cycle
        // -----------------------

        IRestResponse response;

        while (true)
        {
            do
            {
                RestClient client = new RestClient(api_location);
                RestRequest request = new RestRequest("wait", Method.POST);

                request.AddHeader("Content-Type", "application/json");

                request.AddJsonBody(new
                {
                    id = myNodeId,
                });

                response = client.Execute(request);

                if (response.Content.ToString().ToLower() == "\"w\"")
                {
                    WorkerThread(1, chromeDriverPath);
                    workDone();
                    sinc();
                }
                Thread.Sleep(1000);
            }
            while (!response.IsSuccessful);
        }        
    }

    static void register()
    {
        #region - code - 

        // ------------
        // node
        // ------------

        {
            IRestResponse response;

            do
            {
                RestClient client = new RestClient(api_location);
                RestRequest request = new RestRequest("registerNode", Method.POST);

                request.AddHeader("Content-Type", "application/json");

                myNodeId = Guid.NewGuid();

                request.AddJsonBody(new
                {
                    id = myNodeId,
                });

                response = client.Execute(request);
            }
            while (!response.IsSuccessful);
        }

        // ------------
        // test id
        // ------------

        {
            IRestResponse response;

            do
            {
                RestClient client = new RestClient(api_location);
                RestRequest request = new RestRequest("regId", Method.POST);

                request.AddHeader("Content-Type", "application/json");

                response = client.Execute(request);

                myTestId = Guid.Parse(response.Content.Replace ("\"", ""));
            }
            while (!response.IsSuccessful);
        }

        #endregion
    }

    static void RegisterTime(string label)
    {
        #region - code - 

        RestClient client = new RestClient(api_location);
        RestRequest request = new RestRequest("regTime", Method.POST);

        request.AddHeader("Content-Type", "application/json");

        request.AddJsonBody(new
        {
            id = myTestId,
            label = label
        });

        var response = client.Execute(request);

        #endregion
    }

    static void sinc()
    {
        #region - code - 

        IRestResponse response;

        do
        {
            Thread.Sleep(100);

            RestClient client = new RestClient(api_location);
            RestRequest request = new RestRequest("sinc", Method.GET);

            request.AddHeader("Content-Type", "application/json");

            response = client.Execute(request);
        }
        while (!response.IsSuccessful);

        #endregion
    }

    static void workDone()
    {
        #region - code - 

        IRestResponse response;

        do
        {
            RestClient client = new RestClient(api_location);
            RestRequest request = new RestRequest("workDone", Method.POST);

            request.AddHeader("Content-Type", "application/json");

            request.AddJsonBody(new
            {
                id = myNodeId,
            });

            response = client.Execute(request);
        }
        while (!response.IsSuccessful);

        #endregion
    }

    static void WorkerThread(int threadNumber, string chromeDriverPath)
    {
        #region - code - 

        var helper = new Helper();

        helper.Log(threadNumber, "Started!");
        
        EdgeDriver driver = null;

        while (true)
        {
            try
            {
                Console.WriteLine(" --- Start ");

                string
                    site = "http://localhost:10961/",
                    user = "rep.ecs",
                    senha = "A123456@",
                    pdv = "97.222.376/0001-82",
                    pdv_desc = "VERA CRUZ DROGARIA",
                    ean = "7896241225530",
                    qtd_produtos = "1";

                EdgeOptions options = new EdgeOptions();

                bool bAbort = false;

                options.AddArgument("--headless=new"); // Run Chrome in headless mode
                options.AddArgument("--incognito"); // Disable sandbox
                options.AddArgument("--silent"); // Disable sandbox
                options.AddArgument("--log-level=3");
                options.AddArgument("user-data-dir=" + chromeDriverPath);
                options.AddArgument("--disable-restore-session-state");

                options.AddUserProfilePreference("user_experience_metrics", new
                {
                    personalization_data_consent_enabled = true,
                });

                driver = new EdgeDriver(chromeDriverPath, options);
                driver.Manage().Window.Size = new System.Drawing.Size(1920, 2080);

                RegisterTime("UI Visit");

                driver.Navigate().GoToUrl(site + "login");

                RegisterTime("UI Visit");

                Thread.Sleep(300);

                RegisterTime("UI Login");

                do
                {
                    try
                    {
                        driver.FindElement(By.Id("formUser")).Clear();
                        driver.FindElement(By.Id("formUser")).SendKeys(user);
                        driver.FindElement(By.Id("formPass")).Clear();
                        driver.FindElement(By.Id("formPass")).SendKeys(senha);
                        driver.FindElement(By.Id("btnSubmit")).Click();

                        while (driver.Url.EndsWith("/login"))
                            Thread.Sleep(10);

                        break;
                    }
                    catch
                    {
                        Thread.Sleep(100);
                    }
                }
                while (true);

                RegisterTime("UI Login");

                driver.Navigate().GoToUrl(site + "pedido");

                RegisterTime("UI PDV");

                driver.FindElement(By.Id("txtPDV")).SendKeys("97.222.376/0001-82");
                driver.FindElement(By.Id("txtPDV")).SendKeys(Keys.Enter);

                do
                {
                    try
                    {
                        driver.FindElement(By.Id("repeater"));
                    }
                    catch
                    {
                        continue;
                    }

                    var x = driver.FindElement(By.Id("repeater"));

                    if (x != null)
                    {
                        if (x.Text.Trim() == "97.222.376/0001-82 - VERA CRUZ DROGARIA")
                        {
                            break;
                        }
                    }
                    Thread.Sleep(10);
                }
                while (true);

                RegisterTime("UI PDV");

                driver.FindElement(By.Id("span0")).Click();
                driver.FindElement(By.Id("imgPedidoRep")).Click();

                do
                {
                    if (driver.Url.EndsWith("pedidoDist"))
                        break;
                    Thread.Sleep(100);
                }
                while (true);

                do
                {
                    try
                    {
                        driver.FindElement(By.Id("selPagto")).Click();
                        {
                            var dropdown = driver.FindElement(By.Id("selPagto"));
                            dropdown.FindElement(By.XPath("//option[. = 'À Vista']")).Click();
                        }
                        break;
                    }
                    catch
                    {
                        Thread.Sleep(10);
                    }
                }
                while (true);

                Thread.Sleep(100);
                driver.FindElement(By.Id("btnMoveAllRight")).Click();
                Thread.Sleep(100);

                RegisterTime("UI Produtos");

                driver.FindElement(By.Id("btnAvancar")).Click();
                                
                do
                {
                    try
                    {
                        driver.FindElement(By.XPath("//*[@id=\"btnUp\"]")).Click();
                        break;
                    }
                    catch 
                    { 
                        Thread.Sleep(100);
                    }

                } while (true);

                RegisterTime("UI Produtos");

                IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
                jsExecutor.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");

                RegisterTime("UI Gerar Pedido");

                driver.FindElement(By.Id("btnAvancar")).Click();

                do
                {
                    if (driver.Url.EndsWith("pedidoConfirmar"))
                        break;
                    Thread.Sleep(100);
                }
                while (true);

                driver.FindElement(By.Id("btnEnviar")).Click();

                do
                {
                    try
                    {
                        if (driver.FindElement(By.Id("labelNumPedido")).Text.Length > 0)
                            break;
                    }
                    catch
                    {
                        Thread.Sleep(10);
                    }

                } while (true);

                RegisterTime("UI Gerar Pedido");

                driver.Close();
                driver.Quit();

                break;
            }

            catch (Exception e) 
            {
                helper.Log(threadNumber, "Exception; " + e.ToString());
                Console.WriteLine(" --- Exception handled " + e.ToString());

                if (driver != null)
                {
                    driver.Close();
                    driver.Quit();
                }
            }
        }

        #endregion
    }
}

public class Helper
{
    #region - code - 

    private ConcurrentQueue<string> logQueue = new ConcurrentQueue<string>();
    private object fileLock = new object();
    private bool isWriting = false;

    public void Log(int threadNumber, string msg)
    {
        string logEntry = "#" + threadNumber + "; " + msg + " ; " + DateTime.Now.ToString("HHmmss_ffff");
        logQueue.Enqueue(logEntry);

        if (!isWriting)
        {
            Task.Run(WriteLogsToFile);
        }
    }

     void WriteLogsToFile()
    {
        if (isWriting)
        {
            return;
        }

        lock (fileLock)
        {
            if (isWriting)
            {
                return;
            }

            isWriting = true;

            try
            {
                while (logQueue.TryDequeue(out string logEntry))
                {
                    File.AppendAllText("results.txt", logEntry + "\n");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions that may occur during writing
                Console.WriteLine("Error writing logs: " + ex.Message);
            }
            finally
            {
                isWriting = false;
            }
        }
    }

    public void Wait(IWebDriver driver, By elementLocator, int seconds = 900)
    {
        try
        {
            // Wait until the element is present
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            wait.Until(driver => driver.FindElements(elementLocator).Count > 0);
        }
        catch
        {

        }
    }

    #endregion
}
