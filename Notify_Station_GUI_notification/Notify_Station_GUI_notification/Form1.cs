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
        }

       
    }
}
