using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
using RestSharp;
using System.Diagnostics;

namespace Cluster_UI_Stresser
{
    public partial class ClusterForm : Form
    {
        public ClusterForm()
        {
            InitializeComponent();
        }

        public bool CheckAPI()
        {
            #region - code - 

            var client = new RestClient(txtApi.Text);
            var request = new RestRequest("/regStatus", Method.POST);

            request.AddHeader("Content-Type", "application/json");

            IRestResponse response = client.Execute(request);

            return response.IsSuccessful;

            #endregion
        }

        private void ChkApi_Click(object sender, EventArgs e)
        {
            #region - code - 

            ChkApi.Enabled = false;

            if (CheckAPI())
                MessageBox.Show("API OK!");
            else
                Process.Start(Directory.GetCurrentDirectory() + "\\Api\\MasterStresser.exe");

            #endregion
        }

        private void ChkNodes_Click(object sender, EventArgs e)
        {
            #region - code - 

            var client = new RestClient(txtApi.Text);
            var request = new RestRequest("/nodes", Method.GET);

            request.AddHeader("Content-Type", "application/json");

            IRestResponse response = client.Execute(request);

            LblNodes.Text = response.Content;

            if (LblNodes.Text != string.Empty)
            {
                if (LblNodes.Text != "0")
                {
                    TxtMaxNodes.Value = Convert.ToInt32(LblNodes.Text);
                }
            }

            #endregion
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            #region - code - 

            BtnStart.Enabled = false;
            ChkNodes.Enabled = false;
            Report.Enabled = false;

            var inc = Convert.ToInt32(TxtIncrement.Value);

            for (int i = 0; i < TxtMaxNodes.Value; i += inc)
            {
                lblStats.Text = (i + inc) + "/" + TxtMaxNodes.Value;
                System.Windows.Forms.Application.DoEvents();

                // start
                {
                    var client = new RestClient(txtApi.Text);
                    var request = new RestRequest("/startWave", Method.POST);

                    request.AddHeader("Content-Type", "application/json");

                    request.AddJsonBody(new
                    {
                        NodesTotal = i + inc
                    });

                    client.Execute(request);
                    client.ClearHandlers();
                }

                // wait to finish
                {
                    IRestResponse response;

                    do
                    {
                        Thread.Sleep(1000);

                        var client = new RestClient(txtApi.Text);
                        var request = new RestRequest("/checkWave", Method.POST);

                        request.AddHeader("Content-Type", "application/json");

                        response = client.Execute(request);

                        var stop = response.IsSuccessful;

                        client.ClearHandlers();

                        if (stop)
                            break;
                    }
                    while (true);
                }

                ReportEndCycle();

                System.Windows.Forms.Application.DoEvents();
            }

            ReportFinal();

            MessageBox.Show("Test Complete!");

            lblStats.Text = "Status";
            System.Windows.Forms.Application.DoEvents();

            BtnStart.Enabled = true;
            ChkNodes.Enabled = true;
            Report.Enabled = true;

            #endregion
        }

        public bool ReportEndCycle()
        {
            #region - code - 

            RestClient client = new RestClient(txtApi.Text);
            RestRequest request = new RestRequest("/regReport", Method.POST);

            request.AddHeader("Content-Type", "application/json");

            IRestResponse response = client.Execute(request);

            return response.IsSuccessful;

            #endregion
        }

        public bool ReportFinal()
        {
            #region - code - 

            RestClient client = new RestClient(txtApi.Text);
            RestRequest request = new RestRequest("/regReportFinal", Method.POST);

            request.AddHeader("Content-Type", "application/json");

            IRestResponse response = client.Execute(request);

            if (!response.IsSuccessful)
                return false;

            string responseBody = response.Content;
            string _jsonFile = "final.json";

            if (File.Exists(_jsonFile))
            {
                File.Delete(_jsonFile);
            }

            File.WriteAllText(_jsonFile, responseBody);

            return true;

            #endregion
        }

        private void Report_Click(object sender, EventArgs e)
        {
            #region - code - 

            var finalReport = JsonConvert.DeserializeObject<DtoRegTimeReportFinal>(File.ReadAllText("final.json"));

            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            Workbook workbook = excelApp.Workbooks.Add();
            Worksheet worksheet = (Worksheet)workbook.Worksheets[1];

            var stages = finalReport.reports[0].labels;

            int indexer = 1;
            int row = 1;

            var inc = Convert.ToInt32(TxtIncrement.Value);

            foreach (var item in "VU;ID;Stage;Milis;Start;Finish;".Split(';'))
                worksheet.Cells[row, indexer++] = item;

            row++;

            foreach (var rep in finalReport.reports)
            {
                foreach (var r in rep.data)
                {
                    worksheet.Cells[row, 1] = rep.VUs;
                    worksheet.Cells[row, 2] = r.id;
                    worksheet.Cells[row, 3] = r.label;
                    worksheet.Cells[row, 4] = r.milis;
                    worksheet.Cells[row, 5] = r.dtStart.ToString("dd/MM/yyyy HH:mm:ss:fff");
                    worksheet.Cells[row, 6] = r.dtEnd.ToString("dd/MM/yyyy HH:mm:ss:fff");

                    row++;
                }
            }

            row++;

            indexer = 1;

            foreach (var item in "VU;Stage;Avg;Min;Max".Split(';'))
                worksheet.Cells[row, indexer++] = item;

            row++;

            int index_row_start = row;

            foreach (var stage in stages)
            {
                Console.WriteLine(stage);

                int i = inc;

                foreach (var rep in finalReport.reports)
                {
                    foreach (var r in rep.stats)
                    {
                        if (r.label == stage)
                        {
                            Console.WriteLine(stage + " " + i + " > " + r.avg);

                            worksheet.Cells[row, 1] = i;
                            worksheet.Cells[row, 2] = r.label;
                            worksheet.Cells[row, 3] = r.avg;
                            worksheet.Cells[row, 4] = r.min;
                            worksheet.Cells[row, 5] = r.max;
                            row++;
                            i += inc;
                        }
                    }
                }
            }

            int pos_v = 0;

            foreach (var item in stages)
            {
                Microsoft.Office.Interop.Excel.ChartObjects chartObjects = (Microsoft.Office.Interop.Excel.ChartObjects)worksheet.ChartObjects(Type.Missing);
                Microsoft.Office.Interop.Excel.ChartObject chartObject = chartObjects.Add(420, 30 + pos_v, 450, 300);
                Microsoft.Office.Interop.Excel.Chart chart = chartObject.Chart;
                chart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlLine;

                pos_v += 320;

                {
                    Microsoft.Office.Interop.Excel.Range chartRange = worksheet.Range["C" + index_row_start + ":C" + (index_row_start + finalReport.reports.Count() - 1)];
                    Microsoft.Office.Interop.Excel.Range labelsRange = worksheet.Range["A" + index_row_start + ":A" + (index_row_start + finalReport.reports.Count() - 1)];

                    Microsoft.Office.Interop.Excel.SeriesCollection seriesCollection = (Microsoft.Office.Interop.Excel.SeriesCollection)chart.SeriesCollection();
                    Microsoft.Office.Interop.Excel.Series series = seriesCollection.NewSeries();
                    series.Name = item;
                    series.Values = chartRange;
                    series.XValues = labelsRange;

                    index_row_start += finalReport.reports.Count();
                }

                chart.HasTitle = true;
                chart.ChartTitle.Text = item;
            }

            string fileName = Directory.GetCurrentDirectory() + "\\final_results.xlsx";

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            // Save the workbook
            workbook.SaveAs(fileName);

            // Close the workbook and Excel
            workbook.Close();
            excelApp.Quit();

            MessageBox.Show("Excel file generated!");

            #endregion
        }

        private void BtnSetupNodes_Click(object sender, EventArgs e)
        {
            BtnSetupNodes.Enabled = false;

            string dir_base = Directory.GetCurrentDirectory() + "\\Stresser";

            var dirs = Directory.GetDirectories(dir_base);

            foreach (string dir in dirs)
            {
                if (dir.Contains("D_"))
                    Directory.Delete(dir, true);
            }

            for (int i = 0; i < TxtSetupNodes.Value; i++)
            {
                string stresserPath = Path.Combine(dir_base, "Stresser.exe");

                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = stresserPath,
                    WorkingDirectory = dir_base
                };

                Process.Start(psi);

                Thread.Sleep(1000);
                ChkNodes_Click(null, null);
            }
        }
    }

    #region - data - 

    public class DtoRegTime
    {
        public string id { get; set; }

        public string label { get; set; }
    }

    public class DtoRegTimeInfo
    {
        public string id { get; set; }

        public string label { get; set; }

        public int milis { get; set; }

        public DateTime dtStart { get; set; }

        public DateTime dtEnd { get; set; }
    }

    public class DtoRegTimeInfoStat
    {
        public string label { get; set; }

        public double avg { get; set; }

        public int min { get; set; }

        public int max { get; set; }
    }

    public class DtoRegTimeReport
    {
        public List<DtoRegTimeInfo> data { get; set; }

        public int VUs { get; set; }

        public List<string> ids { get; set; }

        public List<string> labels { get; set; }

        public List<DtoRegTimeInfoStat> stats { get; set; }
    }

    public class DtoRegTimeReportFinal
    {
        public List<int> VUs { get; set; }

        public List<DtoRegTimeReport> reports { get; set; }
    }

    #endregion
}
