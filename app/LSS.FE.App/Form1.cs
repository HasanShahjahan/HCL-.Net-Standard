using LSS.BE.Core.Common.Base;
using LSS.BE.Core.Domain.Services;
using LSS.Common.Logging;
using LSS.HCM.Core.DataObjects.Settings;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LSS.FE.App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var model = new MemberInfo
            {
                ClientId = "ef3350f9-ace2-4900-9da0-bba80402535a",
                ClientSecret = "FA1s0QmZFxXh44QUkVOcEj19hvhjWTsfl1sslwGO",
                ConfigurationPath = @"C:\Box24\Project Execution\config.json",
                UriString = "http://18.138.61.187",
                Version = "v1"
            };

            var gatewayService = new GatewayService(model);
            if(gatewayService.TokenResponse.StatusCode == 200) textBox1.Text = "By .Net framework 4.7.2, logging is executed.";
        }
    }
}
