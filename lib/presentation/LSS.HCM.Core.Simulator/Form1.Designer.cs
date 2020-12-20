namespace LSS.HCM.Core.Simulator
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtTransactionId = new System.Windows.Forms.TextBox();
            this.lblCompartmentId = new System.Windows.Forms.Label();
            this.radioOpenCompartment = new System.Windows.Forms.RadioButton();
            this.radioCompartmentStatus = new System.Windows.Forms.RadioButton();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.txtLockerId = new System.Windows.Forms.TextBox();
            this.lblLockerId = new System.Windows.Forms.Label();
            this.txtResult = new System.Windows.Forms.RichTextBox();
            this.labelTransactionId = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.jwtEnable = new System.Windows.Forms.CheckBox();
            this.txtJwtSecret = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtJwtToken = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCompartmentId = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.configurationFile = new System.Windows.Forms.Label();
            this.txtConfigurationFile = new System.Windows.Forms.TextBox();
            this.buttonConfigLocker = new System.Windows.Forms.Button();
            this.radioCaptureImage = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // txtTransactionId
            // 
            this.txtTransactionId.Location = new System.Drawing.Point(166, 145);
            this.txtTransactionId.Margin = new System.Windows.Forms.Padding(2);
            this.txtTransactionId.Name = "txtTransactionId";
            this.txtTransactionId.Size = new System.Drawing.Size(248, 23);
            this.txtTransactionId.TabIndex = 0;
            this.txtTransactionId.TextChanged += new System.EventHandler(this.txtCompartmentId_TextChanged);
            // 
            // lblCompartmentId
            // 
            this.lblCompartmentId.AutoSize = true;
            this.lblCompartmentId.Location = new System.Drawing.Point(35, 203);
            this.lblCompartmentId.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCompartmentId.Name = "lblCompartmentId";
            this.lblCompartmentId.Size = new System.Drawing.Size(95, 15);
            this.lblCompartmentId.TabIndex = 1;
            this.lblCompartmentId.Text = "Compartment Id";
            this.lblCompartmentId.Click += new System.EventHandler(this.lblCompartmentId_Click);
            // 
            // radioOpenCompartment
            // 
            this.radioOpenCompartment.AutoSize = true;
            this.radioOpenCompartment.Checked = true;
            this.radioOpenCompartment.Location = new System.Drawing.Point(166, 62);
            this.radioOpenCompartment.Margin = new System.Windows.Forms.Padding(2);
            this.radioOpenCompartment.Name = "radioOpenCompartment";
            this.radioOpenCompartment.Size = new System.Drawing.Size(132, 19);
            this.radioOpenCompartment.TabIndex = 2;
            this.radioOpenCompartment.TabStop = true;
            this.radioOpenCompartment.Text = "Open Compartment";
            this.radioOpenCompartment.UseVisualStyleBackColor = true;
            this.radioOpenCompartment.CheckedChanged += new System.EventHandler(this.radioButtonOpenCompartment_CheckedChanged);
            // 
            // radioCompartmentStatus
            // 
            this.radioCompartmentStatus.AutoSize = true;
            this.radioCompartmentStatus.Location = new System.Drawing.Point(166, 85);
            this.radioCompartmentStatus.Margin = new System.Windows.Forms.Padding(2);
            this.radioCompartmentStatus.Name = "radioCompartmentStatus";
            this.radioCompartmentStatus.Size = new System.Drawing.Size(135, 19);
            this.radioCompartmentStatus.TabIndex = 2;
            this.radioCompartmentStatus.Text = "Compartment Status";
            this.radioCompartmentStatus.UseVisualStyleBackColor = true;
            this.radioCompartmentStatus.CheckedChanged += new System.EventHandler(this.radioButtonCompartmentStatus_CheckedChanged);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(35, 331);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(2);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(379, 50);
            this.btnSubmit.TabIndex = 3;
            this.btnSubmit.Text = "Enter";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // txtLockerId
            // 
            this.txtLockerId.Location = new System.Drawing.Point(166, 172);
            this.txtLockerId.Margin = new System.Windows.Forms.Padding(2);
            this.txtLockerId.Name = "txtLockerId";
            this.txtLockerId.Size = new System.Drawing.Size(248, 23);
            this.txtLockerId.TabIndex = 0;
            this.txtLockerId.TextChanged += new System.EventHandler(this.txtLockerId_TextChanged);
            // 
            // lblLockerId
            // 
            this.lblLockerId.AutoSize = true;
            this.lblLockerId.Location = new System.Drawing.Point(35, 176);
            this.lblLockerId.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLockerId.Name = "lblLockerId";
            this.lblLockerId.Size = new System.Drawing.Size(55, 15);
            this.lblLockerId.TabIndex = 1;
            this.lblLockerId.Text = "Locker Id";
            this.lblLockerId.Click += new System.EventHandler(this.lblLockerId_Click);
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(443, 22);
            this.txtResult.Margin = new System.Windows.Forms.Padding(2);
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(455, 359);
            this.txtResult.TabIndex = 4;
            this.txtResult.Text = "";
            this.txtResult.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // labelTransactionId
            // 
            this.labelTransactionId.AutoSize = true;
            this.labelTransactionId.Location = new System.Drawing.Point(35, 149);
            this.labelTransactionId.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTransactionId.Name = "labelTransactionId";
            this.labelTransactionId.Size = new System.Drawing.Size(78, 15);
            this.labelTransactionId.TabIndex = 1;
            this.labelTransactionId.Text = "TransactionId";
            this.labelTransactionId.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 224);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 15);
            this.label2.TabIndex = 1;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // jwtEnable
            // 
            this.jwtEnable.AutoSize = true;
            this.jwtEnable.Checked = true;
            this.jwtEnable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.jwtEnable.Location = new System.Drawing.Point(166, 242);
            this.jwtEnable.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.jwtEnable.Name = "jwtEnable";
            this.jwtEnable.Size = new System.Drawing.Size(86, 19);
            this.jwtEnable.TabIndex = 5;
            this.jwtEnable.Text = "JWT Enable";
            this.jwtEnable.UseVisualStyleBackColor = true;
            this.jwtEnable.CheckedChanged += new System.EventHandler(this.jwtEnable_CheckedChanged);
            // 
            // txtJwtSecret
            // 
            this.txtJwtSecret.Location = new System.Drawing.Point(166, 266);
            this.txtJwtSecret.Margin = new System.Windows.Forms.Padding(2);
            this.txtJwtSecret.Name = "txtJwtSecret";
            this.txtJwtSecret.Size = new System.Drawing.Size(248, 23);
            this.txtJwtSecret.TabIndex = 0;
            this.txtJwtSecret.TextChanged += new System.EventHandler(this.txtLockerId_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 270);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 15);
            this.label4.TabIndex = 1;
            this.label4.Text = "Secret";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // txtJwtToken
            // 
            this.txtJwtToken.Location = new System.Drawing.Point(166, 293);
            this.txtJwtToken.Margin = new System.Windows.Forms.Padding(2);
            this.txtJwtToken.Name = "txtJwtToken";
            this.txtJwtToken.Size = new System.Drawing.Size(248, 23);
            this.txtJwtToken.TabIndex = 0;
            this.txtJwtToken.TextChanged += new System.EventHandler(this.txtLockerId_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 296);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 15);
            this.label5.TabIndex = 1;
            this.label5.Text = "Token";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // txtCompartmentId
            // 
            this.txtCompartmentId.Location = new System.Drawing.Point(166, 199);
            this.txtCompartmentId.Margin = new System.Windows.Forms.Padding(2);
            this.txtCompartmentId.Name = "txtCompartmentId";
            this.txtCompartmentId.Size = new System.Drawing.Size(248, 23);
            this.txtCompartmentId.TabIndex = 6;
            this.txtCompartmentId.TextChanged += new System.EventHandler(this.txtConnectionString_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(35, 75);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 15);
            this.label6.TabIndex = 7;
            this.label6.Text = "Command";
            // 
            // configurationFile
            // 
            this.configurationFile.AutoSize = true;
            this.configurationFile.Location = new System.Drawing.Point(35, 25);
            this.configurationFile.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.configurationFile.Name = "configurationFile";
            this.configurationFile.Size = new System.Drawing.Size(102, 15);
            this.configurationFile.TabIndex = 9;
            this.configurationFile.Text = "Configuration File";
            this.configurationFile.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // txtConfigurationFile
            // 
            this.txtConfigurationFile.Location = new System.Drawing.Point(166, 22);
            this.txtConfigurationFile.Margin = new System.Windows.Forms.Padding(2);
            this.txtConfigurationFile.Name = "txtConfigurationFile";
            this.txtConfigurationFile.Size = new System.Drawing.Size(174, 23);
            this.txtConfigurationFile.TabIndex = 8;
            // 
            // buttonConfigLocker
            // 
            this.buttonConfigLocker.Location = new System.Drawing.Point(358, 22);
            this.buttonConfigLocker.Name = "buttonConfigLocker";
            this.buttonConfigLocker.Size = new System.Drawing.Size(56, 23);
            this.buttonConfigLocker.TabIndex = 10;
            this.buttonConfigLocker.Text = "Config";
            this.buttonConfigLocker.UseVisualStyleBackColor = true;
            this.buttonConfigLocker.Click += new System.EventHandler(this.buttonConfigLocker_Click);
            // 
            // radioCaptureImage
            // 
            this.radioCaptureImage.AutoSize = true;
            this.radioCaptureImage.Location = new System.Drawing.Point(166, 109);
            this.radioCaptureImage.Name = "radioCaptureImage";
            this.radioCaptureImage.Size = new System.Drawing.Size(103, 19);
            this.radioCaptureImage.TabIndex = 11;
            this.radioCaptureImage.TabStop = true;
            this.radioCaptureImage.Text = "Capture Image";
            this.radioCaptureImage.UseVisualStyleBackColor = true;
            this.radioCaptureImage.CheckedChanged += new System.EventHandler(this.radioCaptureImage_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 404);
            this.Controls.Add(this.radioCaptureImage);
            this.Controls.Add(this.buttonConfigLocker);
            this.Controls.Add(this.configurationFile);
            this.Controls.Add(this.txtConfigurationFile);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtCompartmentId);
            this.Controls.Add(this.jwtEnable);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.radioCompartmentStatus);
            this.Controls.Add(this.radioOpenCompartment);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelTransactionId);
            this.Controls.Add(this.lblLockerId);
            this.Controls.Add(this.lblCompartmentId);
            this.Controls.Add(this.txtJwtToken);
            this.Controls.Add(this.txtJwtSecret);
            this.Controls.Add(this.txtLockerId);
            this.Controls.Add(this.txtTransactionId);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTransactionId;
        private System.Windows.Forms.Label lblCompartmentId;
        private System.Windows.Forms.RadioButton radioOpenCompartment;
        private System.Windows.Forms.RadioButton radioCompartmentStatus;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.TextBox txtLockerId;
        private System.Windows.Forms.Label lblLockerId;
        private System.Windows.Forms.RichTextBox txtResult;
        private System.Windows.Forms.Label labelTransactionId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox jwtEnable;
        private System.Windows.Forms.TextBox txtJwtSecret;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtJwtToken;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCompartmentId;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblConfigurationPath;
        private System.Windows.Forms.Label lblConfiguration;
        private System.Windows.Forms.Label configurationFile;
        private System.Windows.Forms.TextBox txtConfigurationFile;
        private System.Windows.Forms.Button buttonConfigLocker;
        private System.Windows.Forms.RadioButton radioCaptureImage;
    }
}



