using System;
using System.Diagnostics;
using System.Windows.Forms;
using LSS.HCM.Core.DataObjects.Models;
using LSS.HCM.Core.Domain.Managers;
using Newtonsoft.Json;

namespace LSS.HCM.Core.Simulator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            txtTransactionId.Text = "70b36c41-078b-411b-982c-c5b774aac66f";
            txtLockerId.Text = "PANLOCKER-1";
            txtConnectionString.Text = "mongodb://localhost:27017";
            txtJwtToken.Text = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJleHAiOjE2MDkzNTU5MjEsInRyYW5zYWN0aW9uX2lkIjoiNzBiMzZjNDEtMDc4Yi00MTFiLTk4MmMtYzViNzc0YWFjNjZmIn0.ujOkQJUq5WY_tZJgKXqe_n4nql3cSAeHMfXGABZO3E4";
            txtJwtSecret.Text = "HWAPI_0BwRn5Bg4rJAe5eyWkRz";
            txtDatabaseName.Text = "LssHwapiDb";
            txtCollectionName.Text = "Lockers";
            txtCompartmentId.Text = "M0-1,M0-3";
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                txtResult.Text = "";
                string transactionId = txtTransactionId.Text;
                string lockerId = txtLockerId.Text;
                string connectionString = txtConnectionString.Text;
                string databaseName = txtDatabaseName.Text;
                string collectionName = txtCollectionName.Text;
                string token = txtJwtToken.Text;
                string jwtSecret = txtJwtSecret.Text;
                string[] validCompartmentIds = txtCompartmentId.Text.Split(',');
                bool flag = jwtEnable.Checked;

                var compartment = new Compartment(transactionId, lockerId, validCompartmentIds, flag, jwtSecret, token);
                if (radioOpenCompartment.Checked)
                {
                    var lockerManager = new LockerManager(null);
                    var result = lockerManager.OpenCompartment(compartment);
                    txtResult.Text = JsonConvert.SerializeObject(result, Formatting.Indented);
                }
                if (radioCompartmentStatus.Checked)
                {
                    var lockerManager = new LockerManager(null);
                    var result = lockerManager.CompartmentStatus(compartment);
                    txtResult.Text = JsonConvert.SerializeObject(result, Formatting.Indented);
                }
            }
            catch (Exception ex)
            {
                // Get stack trace for the exception with source file information
                var st = new StackTrace(ex, true);
                // Get the top stack frame
                var frame = st.GetFrame(0);
                // Get the line number from the stack frame
                var filename = frame.GetFileName();
                var line = frame.GetFileLineNumber();
                var method = frame.GetMethod();

                txtResult.Text = ex.ToString();
                /*
                txtResult.Text = "ERR File: " + filename + "\r\n" +
                    "    Method: " + method + "\r\n" +
                    "    Line: " + line + "\r\n" +
                    "    Msg: " + ex.Message;*/
            }

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButtonOpenCompartment_CheckedChanged(object sender, EventArgs e)
        {
            labelTransactionId.Show();
            txtTransactionId.Show();
        }

        private void radioButtonCompartmentStatus_CheckedChanged(object sender, EventArgs e)
        {
            labelTransactionId.Hide();
            txtTransactionId.Hide();
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
    }
}