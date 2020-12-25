using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using LSS.HCM.Core.DataObjects.Models;
using LSS.HCM.Core.Domain.Managers;
using Newtonsoft.Json;
using Compartment = LSS.HCM.Core.DataObjects.Models.Compartment;
using SockNet.ServerSocket;
using LSS.HCM.Core.Domain.Services;

namespace LSS.HCM.Core.Simulator
{
    public partial class Form1 : Form
    {
        private LockerManager _lockerManager;
        private bool _isConfigured = false;
        public Form1()
        { //5fcf7669a6ceaf330cf4f8f4
            InitializeComponent();
            txtTransactionId.Text = "70b36c41-078b-411b-982c-c5b774aac66f";
            txtLockerId.Text = "PANLOCKER-1";
            txtJwtToken.Text = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJleHAiOjE2MDkzNTU5MjEsInRyYW5zYWN0aW9uX2lkIjoiNzBiMzZjNDEtMDc4Yi00MTFiLTk4MmMtYzViNzc0YWFjNjZmIn0.ujOkQJUq5WY_tZJgKXqe_n4nql3cSAeHMfXGABZO3E4";
            txtJwtSecret.Text = "HWAPI_0BwRn5Bg4rJAe5eyWkRz";
            txtCompartmentId.Text = "M0-1,M0-3";
            txtConfigurationFile.Text = @"D:\config.json";
            btnSubmit.Enabled = false;
        }

        // Socket Scanner
        delegate void SetTextCallback(string text);

        // Socket Scanner
        private void UpdateScannerValue(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.textBoxScanner.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(UpdateScannerValue);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBoxScanner.Text = text;
            }
        }

        private void buttonConfigLocker_Click(object sender, EventArgs e)
        {
            string configurationPath = txtConfigurationFile.Text;
            // Socket Scanner
            //Task.Run(() => new SocketListenerService());
            //Task.Run(() => StartSocketListener());

            _lockerManager = new LockerManager(configurationPath);
            if (_lockerManager != null) 
            {
                _isConfigured = true;
                btnSubmit.Enabled = true;
            }
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if(_isConfigured) {
                    txtResult.Text = "";
                    string transactionId = txtTransactionId.Text;
                    string lockerId = txtLockerId.Text;
                    string token = txtJwtToken.Text;
                    string jwtSecret = txtJwtSecret.Text;
                    string[] validCompartmentIds = txtCompartmentId.Text.Split(',');
                    bool flag = jwtEnable.Checked;

                    var compartment = new Compartment(transactionId, lockerId, validCompartmentIds, flag, jwtSecret, token);
                    if (radioOpenCompartment.Checked)
                    {
                        var result = _lockerManager.OpenCompartment(compartment);
                        txtResult.Text = JsonConvert.SerializeObject(result, Formatting.Indented);
                    }
                    else if (radioCompartmentStatus.Checked)
                    {
                        var result = _lockerManager.CompartmentStatus(compartment);
                        txtResult.Text = JsonConvert.SerializeObject(result, Formatting.Indented);
                    }
                    else if (radioCaptureImage.Checked)
                    {
                        var requestCapture = new Capture(transactionId, lockerId, flag, jwtSecret, token);
                        var result = _lockerManager.CaptureImage(requestCapture);
                        txtResult.Text = JsonConvert.SerializeObject(result, Formatting.Indented);
                    }
                    
                }

            }
            catch (System.IO.FileNotFoundException)
            {
                txtResult.Text = "Please ensure valid configuration file path.";
            }
            catch (ArgumentException)
            {
                txtResult.Text = "Please ensure valid configuration file path.";
            }
            catch (System.IO.DirectoryNotFoundException) 
            {
                txtResult.Text = "Please ensure valid configuration file path.";
            }
            catch (Exception ex)
            {
                txtResult.Text = ex.ToString();
            }

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButtonOpenCompartment_CheckedChanged(object sender, EventArgs e)
        {
            labelTransactionId.Show();
            txtTransactionId.Show();
            lblCompartmentId.Show();
            txtCompartmentId.Show();
        }

        private void radioButtonCompartmentStatus_CheckedChanged(object sender, EventArgs e)
        {
            labelTransactionId.Hide();
            txtTransactionId.Hide();
            lblCompartmentId.Show();
            txtCompartmentId.Show();
        }

        private void radioCaptureImage_CheckedChanged(object sender, EventArgs e)
        {
            labelTransactionId.Show();
            txtTransactionId.Show();
            lblCompartmentId.Hide();
            txtCompartmentId.Hide();

        }

        private void txtCompartmentId_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtLockerId_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtConnectionString_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblCompartmentId_Click(object sender, EventArgs e)
        {

        }

        private void lblLockerId_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void jwtEnable_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void txtCollectionName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        // Socket Scanner
        private void StartSocketListener()
        {
            var server = new SocketServer();
            server.InitializeSocketServer("127.0.0.1", 80);
            server.SetReaderBufferBytes(1024);
            server.StartListening();
            UpdateScannerValue("scanner start");

            while (true) {
                if (server.IsNewData())
                {
                    var data = server.GetData();
                    // Do whatever you want with data
                    //SetText(BitConverter.ToString(data.Value));
                    UpdateScannerValue(System.Text.Encoding.UTF8.GetString(data.Value));
                    //Console.WriteLine("Hello: " + BitConverter.ToString(data.Value));
                }
            }
            
            //server.ResponseToClient(data.Key, "this is cool!");

        }
    }
}