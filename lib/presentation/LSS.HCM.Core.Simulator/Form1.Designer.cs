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
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.jwtEnable = new System.Windows.Forms.CheckBox();
            this.txtJwtSecret = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtJwtToken = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCompartmentId = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDatabaseName = new System.Windows.Forms.TextBox();
            this.labelCollectionName = new System.Windows.Forms.Label();
            this.labelDatabaseName = new System.Windows.Forms.Label();
            this.txtCollectionName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtTransactionId
            // 
            this.txtTransactionId.Location = new System.Drawing.Point(142, 74);
            this.txtTransactionId.Margin = new System.Windows.Forms.Padding(2);
            this.txtTransactionId.Name = "txtTransactionId";
            this.txtTransactionId.Size = new System.Drawing.Size(213, 20);
            this.txtTransactionId.TabIndex = 0;
            this.txtTransactionId.TextChanged += new System.EventHandler(this.txtCompartmentId_TextChanged);
            // 
            // lblCompartmentId
            // 
            this.lblCompartmentId.AutoSize = true;
            this.lblCompartmentId.Location = new System.Drawing.Point(40, 153);
            this.lblCompartmentId.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCompartmentId.Name = "lblCompartmentId";
            this.lblCompartmentId.Size = new System.Drawing.Size(81, 13);
            this.lblCompartmentId.TabIndex = 1;
            this.lblCompartmentId.Text = "Compartment Id";
            this.lblCompartmentId.Click += new System.EventHandler(this.lblCompartmentId_Click);
            // 
            // radioOpenCompartment
            // 
            this.radioOpenCompartment.AutoSize = true;
            this.radioOpenCompartment.Checked = true;
            this.radioOpenCompartment.Location = new System.Drawing.Point(142, 20);
            this.radioOpenCompartment.Margin = new System.Windows.Forms.Padding(2);
            this.radioOpenCompartment.Name = "radioOpenCompartment";
            this.radioOpenCompartment.Size = new System.Drawing.Size(116, 17);
            this.radioOpenCompartment.TabIndex = 2;
            this.radioOpenCompartment.TabStop = true;
            this.radioOpenCompartment.Text = "Open Compartment";
            this.radioOpenCompartment.UseVisualStyleBackColor = true;
            this.radioOpenCompartment.CheckedChanged += new System.EventHandler(this.radioButtonOpenCompartment_CheckedChanged);
            // 
            // radioCompartmentStatus
            // 
            this.radioCompartmentStatus.AutoSize = true;
            this.radioCompartmentStatus.Location = new System.Drawing.Point(142, 41);
            this.radioCompartmentStatus.Margin = new System.Windows.Forms.Padding(2);
            this.radioCompartmentStatus.Name = "radioCompartmentStatus";
            this.radioCompartmentStatus.Size = new System.Drawing.Size(120, 17);
            this.radioCompartmentStatus.TabIndex = 2;
            this.radioCompartmentStatus.Text = "Compartment Status";
            this.radioCompartmentStatus.UseVisualStyleBackColor = true;
            this.radioCompartmentStatus.CheckedChanged += new System.EventHandler(this.radioButtonCompartmentStatus_CheckedChanged);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(24, 457);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(2);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(331, 34);
            this.btnSubmit.TabIndex = 3;
            this.btnSubmit.Text = "Enter";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // txtLockerId
            // 
            this.txtLockerId.Location = new System.Drawing.Point(142, 112);
            this.txtLockerId.Margin = new System.Windows.Forms.Padding(2);
            this.txtLockerId.Name = "txtLockerId";
            this.txtLockerId.Size = new System.Drawing.Size(213, 20);
            this.txtLockerId.TabIndex = 0;
            this.txtLockerId.TextChanged += new System.EventHandler(this.txtLockerId_TextChanged);
            // 
            // lblLockerId
            // 
            this.lblLockerId.AutoSize = true;
            this.lblLockerId.Location = new System.Drawing.Point(75, 115);
            this.lblLockerId.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLockerId.Name = "lblLockerId";
            this.lblLockerId.Size = new System.Drawing.Size(46, 13);
            this.lblLockerId.TabIndex = 1;
            this.lblLockerId.Text = "Loker Id";
            this.lblLockerId.Click += new System.EventHandler(this.lblLockerId_Click);
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(379, 11);
            this.txtResult.Margin = new System.Windows.Forms.Padding(2);
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(397, 480);
            this.txtResult.TabIndex = 4;
            this.txtResult.Text = "";
            this.txtResult.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // labelTransactionId
            // 
            this.labelTransactionId.AutoSize = true;
            this.labelTransactionId.Location = new System.Drawing.Point(49, 77);
            this.labelTransactionId.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTransactionId.Name = "labelTransactionId";
            this.labelTransactionId.Size = new System.Drawing.Size(72, 13);
            this.labelTransactionId.TabIndex = 1;
            this.labelTransactionId.Text = "TransactionId";
            this.labelTransactionId.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Location = new System.Drawing.Point(142, 188);
            this.txtConnectionString.Margin = new System.Windows.Forms.Padding(2);
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(213, 20);
            this.txtConnectionString.TabIndex = 0;
            this.txtConnectionString.TextChanged += new System.EventHandler(this.txtLockerId_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 191);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Connection String";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // jwtEnable
            // 
            this.jwtEnable.AutoSize = true;
            this.jwtEnable.Checked = true;
            this.jwtEnable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.jwtEnable.Location = new System.Drawing.Point(142, 328);
            this.jwtEnable.Name = "jwtEnable";
            this.jwtEnable.Size = new System.Drawing.Size(85, 17);
            this.jwtEnable.TabIndex = 5;
            this.jwtEnable.Text = "JWT Enable";
            this.jwtEnable.UseVisualStyleBackColor = true;
            this.jwtEnable.CheckedChanged += new System.EventHandler(this.jwtEnable_CheckedChanged);
            // 
            // txtJwtSecret
            // 
            this.txtJwtSecret.Location = new System.Drawing.Point(142, 365);
            this.txtJwtSecret.Margin = new System.Windows.Forms.Padding(2);
            this.txtJwtSecret.Name = "txtJwtSecret";
            this.txtJwtSecret.Size = new System.Drawing.Size(213, 20);
            this.txtJwtSecret.TabIndex = 0;
            this.txtJwtSecret.TextChanged += new System.EventHandler(this.txtLockerId_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(83, 368);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Secret";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // txtJwtToken
            // 
            this.txtJwtToken.Location = new System.Drawing.Point(142, 403);
            this.txtJwtToken.Margin = new System.Windows.Forms.Padding(2);
            this.txtJwtToken.Name = "txtJwtToken";
            this.txtJwtToken.Size = new System.Drawing.Size(213, 20);
            this.txtJwtToken.TabIndex = 0;
            this.txtJwtToken.TextChanged += new System.EventHandler(this.txtLockerId_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(83, 406);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Token";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // txtCompartmentId
            // 
            this.txtCompartmentId.Location = new System.Drawing.Point(142, 150);
            this.txtCompartmentId.Margin = new System.Windows.Forms.Padding(2);
            this.txtCompartmentId.Name = "txtCompartmentId";
            this.txtCompartmentId.Size = new System.Drawing.Size(213, 20);
            this.txtCompartmentId.TabIndex = 6;
            this.txtCompartmentId.TextChanged += new System.EventHandler(this.txtConnectionString_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(67, 32);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Command";
            // 
            // txtDatabaseName
            // 
            this.txtDatabaseName.Location = new System.Drawing.Point(142, 225);
            this.txtDatabaseName.Margin = new System.Windows.Forms.Padding(2);
            this.txtDatabaseName.Name = "txtDatabaseName";
            this.txtDatabaseName.Size = new System.Drawing.Size(213, 20);
            this.txtDatabaseName.TabIndex = 11;
            // 
            // labelCollectionName
            // 
            this.labelCollectionName.AutoSize = true;
            this.labelCollectionName.Location = new System.Drawing.Point(37, 266);
            this.labelCollectionName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelCollectionName.Name = "labelCollectionName";
            this.labelCollectionName.Size = new System.Drawing.Size(84, 13);
            this.labelCollectionName.TabIndex = 9;
            this.labelCollectionName.Text = "Collection Name";
            // 
            // labelDatabaseName
            // 
            this.labelDatabaseName.AutoSize = true;
            this.labelDatabaseName.Location = new System.Drawing.Point(37, 228);
            this.labelDatabaseName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelDatabaseName.Name = "labelDatabaseName";
            this.labelDatabaseName.Size = new System.Drawing.Size(84, 13);
            this.labelDatabaseName.TabIndex = 10;
            this.labelDatabaseName.Text = "Database Name";
            // 
            // txtCollectionName
            // 
            this.txtCollectionName.Location = new System.Drawing.Point(142, 263);
            this.txtCollectionName.Margin = new System.Windows.Forms.Padding(2);
            this.txtCollectionName.Name = "txtCollectionName";
            this.txtCollectionName.Size = new System.Drawing.Size(213, 20);
            this.txtCollectionName.TabIndex = 8;
            this.txtCollectionName.TextChanged += new System.EventHandler(this.txtCollectionName_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 502);
            this.Controls.Add(this.txtDatabaseName);
            this.Controls.Add(this.labelCollectionName);
            this.Controls.Add(this.labelDatabaseName);
            this.Controls.Add(this.txtCollectionName);
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
            this.Controls.Add(this.txtConnectionString);
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
        private System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox jwtEnable;
        private System.Windows.Forms.TextBox txtJwtSecret;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtJwtToken;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCompartmentId;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDatabaseName;
        private System.Windows.Forms.Label labelCollectionName;
        private System.Windows.Forms.Label labelDatabaseName;
        private System.Windows.Forms.TextBox txtCollectionName;
    }
}



