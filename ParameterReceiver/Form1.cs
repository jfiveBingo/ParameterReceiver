using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ParameterReceiver
{
    public partial class Form1 : Form
    {
        //string builder for log show
        StringBuilder strLogText = new StringBuilder();
        private ParameterTcpServer parameterServer;

        private const int ApplicationXPort = 9000;

        byte[] code;

        UdpClient Client;
        private bool _closing = false;

        private ParametreXManager parametreXManager;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //initialisation des series 

            parametreXManager = new ParametreXManager(new ParametreX[]
            {
                ParametreXManager.Creer("TN11",115),
                ParametreXManager.Creer("TN12",116),
                ParametreXManager.Creer("TN13",117),
                ParametreXManager.Creer("TN14",118),
                ParametreXManager.Creer("TN15",119),
                ParametreXManager.Creer("TN16",120),
                ParametreXManager.Creer("TN27",27724),
                ParametreXManager.Creer("TN28",27725),
                ParametreXManager.Creer("TN29",27662),
                ParametreXManager.Creer("TN30",27663)
            });

            parameterServer = new ParameterTcpServer(ApplicationXPort);

            parameterServer.Start();

            BeginRecieve();

            WriteLog("Parameter Server started on port " + ApplicationXPort);




        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            parameterServer?.Stop();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            BeginRecieve();
        }
        private void BeginRecieve()
        {
            try
            {
                Client = new UdpClient(2246);
                Client.JoinMulticastGroup(IPAddress.Parse("239.0.0.5"));
                Client.BeginReceive(new AsyncCallback(recv), null);
                //WriteLog(MyDate() + " : PX recieved !");
            }
            catch (Exception ex)
            {
                //lblPX1.Text = ex.Message.ToString();
                //WriteLog(MyDate() + " : Port xx already used ");
            }
        }


        void recv(IAsyncResult res)
        {
            if (_closing || IsDisposed || Disposing)
                return;

            IPEndPoint RemoteIP = new IPEndPoint(IPAddress.Any, 2246);

            try
            {
                byte[] received = Client.EndReceive(res, ref RemoteIP);
                //Fonction de trouver la valeur xx puis la retourner en valeur string ou double


                //exemple
                parametreXManager.TraiterSerie(received);
                //serieB.TraiterSerie(received);

                // Send parameters to Application X
                SendParameters(parametreXManager);

                code = new byte[] 
                {
                    0x10, 0x04, 0xF0, 0x60
                };

                byte[] data = parametreXManager.ExtraireDataApresCode(received, code);

                if (data != null)
                {
                    string hex = BitConverter.ToString(data);

                    //txb410.Text = hex;
                    SetText410(hex);
                }


            }
            catch (ArgumentException axp)
            {
                MessageBox.Show("Erreur REC00 : " + axp.ToString());
            }
            catch (Exception exp)
            {
                MessageBox.Show("Erreur REC01 : " + exp.ToString());
            }
            //To avoid cross threading we use methode invoker
            this.Invoke(new MethodInvoker(delegate
            {
                // do my affichage code

                //for exemple
                AfficherSerieA();
            }));

            try
            {
                Client.BeginReceive(new AsyncCallback(recv), null);
            }
            catch (Exception expCLBC)
            {
                MessageBox.Show("Erreur : " + expCLBC.Message.ToString());
            }


        }



        private void AfficherSerieA()
        {
            dgvSerieA.Rows.Clear();

            foreach (ParametreX p in parametreXManager.Serie)
            {
                dgvSerieA.Rows.Add(
                    p.Code,
                    p.SymboleIndex,
                    BitConverter.ToString(p.IndexHex),
                    p.ValeurHex != null ? BitConverter.ToString(p.ValeurHex) : "",
                    p.Valeur.ToString("F3")
                );
            }

            //aficher min et max
            //lblMinA.Text = serieA.Min.ToString("F3");
            //lblMaxA.Text = serieA.Max.ToString("F3");
        }

        private async void SendParameters(ParametreXManager manager)
        {
            foreach (ParametreX p in manager.Serie)
            {
                if (p.ValeurHex == null)
                    continue;

                ParameterMessage message = new ParameterMessage
                {
                    Code = p.Code,
                    Index = p.SymboleIndex,
                    Value = p.Valeur
                };

                WriteLog($"Sending parameter: Code={message.Code}, Index={message.Index}, Value={message.Value}");

                await parameterServer.SendAsync(message);
            }
        }

        public void WriteLog(string v)
        {
            //witre log in textBox

            strLogText.AppendLine(v);
            string stringLog = strLogText.ToString();


            //txbLog.Text = stringLog.Remove(stringLog.Length - 2, 2);
            SetText(stringLog.Remove(stringLog.Length - 2, 2));
        }

        delegate void SetTextCallback(string text);

        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.txbLog.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.txbLog.Text = text;
            }
        }

        private void SetText410(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.txb410.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText410);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.txb410.Text = text;
            }
        }

        private void txbLog_TextChanged(object sender, EventArgs e)
        {
            txbLog.SelectionStart = txbLog.Text.Length;
            txbLog.ScrollToCaret();
        }
    }
}
