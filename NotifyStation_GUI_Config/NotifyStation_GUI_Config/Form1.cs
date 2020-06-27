using System;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using RestSharp;

namespace NotifyStation_GUI_Config
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static string WorkingDir = @"C:\ProgramData\Notify_Station\";

        public RestClient client;

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Directory.Exists(WorkingDir)) {
                try
                {
                    readConfig();
                }catch {
                    bootstrap();
                }
            }else
            {
                bootstrap();
            }
            textBox3.Text = Environment.CurrentDirectory + @"\call.exe";

        }

        private void readConfig()
        {
            textBox2.Text = File.ReadAllText(WorkingDir + "api");
            textBox1.Text = File.ReadAllText(WorkingDir + "id");
            if (File.ReadAllText(WorkingDir + "EnableLuxafor") == "True")
            {
                checkBox2.Checked = true;
            }
            textBox4.Text = File.ReadAllText(WorkingDir + "Luxafor");
            comboBox1.SelectedItem = File.ReadAllText(WorkingDir + "LuxaforMode");
            comboBox2.SelectedItem = File.ReadAllText(WorkingDir + "LuxaforColor");

            checkBox1.Checked = (checkRegKeyExist(@"Software\Microsoft\Windows\CurrentVersion\Run", "NotifyStation"));
        }

        private static bool checkRegKeyExist(string hive, string value)
        {
            RegistryKey startup = Registry.CurrentUser.OpenSubKey(hive, false);
            return (startup.GetValueNames().Contains(value));
        }

        private void bootstrap()
        {
            if (Directory.Exists(WorkingDir))
            {
                Directory.Delete(WorkingDir, true);
            }
            Directory.CreateDirectory(WorkingDir);
            File.WriteAllText(WorkingDir + "api", textBox2.Text);
            Register();
            File.WriteAllText(WorkingDir + "id", textBox1.Text);
        }

        private void Register()
        {
            client = new RestClient(textBox2.Text);
            var request = new RestRequest(@"v1/register.php", Method.GET);
            IRestResponse response = client.Execute(request);
            var content = response.Content;
            string ID = content.ToString().Split('=')[1];
            File.WriteAllText(WorkingDir + @"id", ID);
            textBox1.Text = ID;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!(textBox2.Text.EndsWith(@"/")))
            {
                textBox2.Text += @"/";
            }
            textBox1.Text = "PLEASE RE-GENERATE ID WHEN THE SERVER IS CHANGED!";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            File.WriteAllText(WorkingDir + "api", textBox2.Text);
            File.WriteAllText(WorkingDir + "id", textBox1.Text);
            File.WriteAllText(WorkingDir + "EnableLuxafor", checkBox2.Checked.ToString());
            File.WriteAllText(WorkingDir + "Luxafor", textBox4.Text);
            File.WriteAllText(WorkingDir + "LuxaforMode", comboBox1.SelectedItem.ToString());
            File.WriteAllText(WorkingDir + "LuxaforColor", comboBox2.SelectedItem.ToString());

            if (checkBox1.Checked)
            {
                if (!(checkRegKeyExist(@"Software\Microsoft\Windows\CurrentVersion\Run", "NotifyStation")))
                {
                    RegistryKey startup = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                    startup.SetValue("NotifyStation", Environment.CurrentDirectory + @"\Notify_Station_GUI_notification.exe");
                }
            }else
            {
                if (checkRegKeyExist(@"Software\Microsoft\Windows\CurrentVersion\Run", "NotifyStation"))
                {
                    RegistryKey startup = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                    startup.DeleteValue("NotifyStation");
                }
            }
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = Environment.CurrentDirectory + @"\Notify_Station_GUI_notification.exe";
            proc.Start();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = @"C:\Windows\System32\taskkill.exe";
            proc.StartInfo.Arguments = @"/f /im Notify_Station_GUI_notification.exe";
            proc.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            proc.StartInfo.CreateNoWindow = true;
            proc.Start();
            proc.WaitForExit();
            MessageBox.Show("Notify Station closed.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Register();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            label4.Visible = checkBox2.Checked;
            label5.Visible = checkBox2.Checked;
            label6.Visible = checkBox2.Checked;
            comboBox1.Visible = checkBox2.Checked;
            comboBox2.Visible = checkBox2.Checked;
            textBox4.Visible = checkBox2.Checked;
        }
    }
}
