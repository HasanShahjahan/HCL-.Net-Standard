using System;
using System.Diagnostics;
using System.Windows.Forms;
using LSS.HCM.Core.Domain.Managers;
using Newtonsoft.Json;
using Compartment = LSS.HCM.Core.DataObjects.Models.Compartment;

namespace LSS.HCM.Core.Simulator
{
    public partial class Form1 : Form
    {
        string configurationPath = @"C:\Box24\Project Execution\Hasan.txt";
        public Form1()
        {
            InitializeComponent();
            txtTransactionId.Text = "70b36c41-078b-411b-982c-c5b774aac66f";
            txtLockerId.Text = "PANLOCKER-1";
            txtJwtToken.Text = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJleHAiOjE2MDkzNTU5MjEsInRyYW5zYWN0aW9uX2lkIjoiNzBiMzZjNDEtMDc4Yi00MTFiLTk4MmMtYzViNzc0YWFjNjZmIn0.ujOkQJUq5WY_tZJgKXqe_n4nql3cSAeHMfXGABZO3E4";
            txtJwtSecret.Text = "HWAPI_0BwRn5Bg4rJAe5eyWkRz";
            txtCompartmentId.Text = "M0-1,M0-3";
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
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
                    var lockerManager = new LockerManager(configurationPath);
                    var result = lockerManager.OpenCompartment(compartment);
                    txtResult.Text = JsonConvert.SerializeObject(result, Formatting.Indented);
                }
                else if (radioCompartmentStatus.Checked)
                {
                    var lockerManager = new LockerManager(configurationPath);
                    var result = lockerManager.CompartmentStatus(compartment);
                    txtResult.Text = JsonConvert.SerializeObject(result, Formatting.Indented);
                }
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