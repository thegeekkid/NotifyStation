using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using RestSharp;

namespace Notify_Station_GUI_notification
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public bool windowActive = false;

        public static string WorkingDir = @"C:\ProgramData\Notify_Station\";

        RestClient client;
        private void Form1_Load(object sender, EventArgs e)
        {
            Rectangle res = Screen.PrimaryScreen.Bounds;
            this.Location = new Point(res.Width - Size.Width);
            Thread trd = new Thread(looper);
            trd.Start();
        }

        private void looper()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate ()
                {
                    this.Visible = false;

                });
            }
            else
            {
                this.Visible = false;
            }
            string computerID = File.ReadAllText(WorkingDir + "id");
            string api = File.ReadAllText(WorkingDir + "api");
            bool show = false;

            client = new RestClient(api);
            var request = new RestRequest(@"v1/checkin.php", Method.GET);
            request.AddParameter("id", computerID);
            do
            {
                IRestResponse response = client.Execute(request);
                var content = response.Content;
                if (content.ToString() == "Active=True")
                {
                    show = true;
                }else
                {
                    Thread.Sleep(5000);
                }
            } while (show == false);
            showNotification();
        }

        private void showNotification()
        {
            if (File.Exists(WorkingDir + "EnableLuxafor"))
            {
                string EnableLuxafor = File.ReadAllText(WorkingDir + "EnableLuxafor");
                if (EnableLuxafor == "True")
                {
                    string LuxaforId = File.ReadAllText(WorkingDir + "Luxafor");
                    string LuxaforMode = File.ReadAllText(WorkingDir + "LuxaforMode");
                    string LuxaforColor = File.ReadAllText(WorkingDir + "LuxaforColor");
                    LuxaforRequest lr = new LuxaforRequest();
                    LuxaforActionFields laf = new LuxaforActionFields();
                    laf.color = LuxaforColor;
                    lr.userId = LuxaforId;
                    lr.actionFields = laf;
                    client = new RestClient(@"https://api.luxafor.com/webhook/v1/actions/");
                    var request = new RestRequest(LuxaforMode, Method.POST);
                    request.AddJsonBody(lr);
                    IRestResponse response = client.Execute(request);
                    var content = response.Content;
                }
            }
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate ()
                {
                    this.Visible = true;

                });
            }
            else
            {
                this.Visible = true;
            }
            windowActive = true;
            do
            {
                Thread.Sleep(500);
            } while (windowActive);
            looper();
        }

        private void hideNotification(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
            windowActive = false;
            if (File.Exists(WorkingDir + "EnableLuxafor"))
            {
                string EnableLuxafor = File.ReadAllText(WorkingDir + "EnableLuxafor");
                if (EnableLuxafor == "True")
                {
                    string LuxaforId = File.ReadAllText(WorkingDir + "Luxafor");
                    LuxaforCustomRequest lr = new LuxaforCustomRequest();
                    lr.userId = LuxaforId;
                    LuxaforCustomColor lcc = new LuxaforCustomColor();
                    lcc.color = "custom";
                    lcc.custom_color = "000000";
                    lr.actionFields = lcc;
                    client = new RestClient(@"https://api.luxafor.com/webhook/v1/actions/");
                    var request = new RestRequest("solid_color", Method.POST);
                    request.AddJsonBody(lr);
                    client.Execute(request);
                }
            }
        }

       
    }

    public class LuxaforRequest
    {
        public string userId { get; set; }
        public LuxaforActionFields actionFields { get; set; }
    }
    public class LuxaforActionFields
    {
        public string color { get; set; }
    }
    public class LuxaforCustomRequest
    {
        public string userId { get; set; }
        public LuxaforCustomColor actionFields { get; set; }
    }
    public class LuxaforCustomColor
    {
        public string color { get; set; }
        public string custom_color { get; set; }
    }
}
