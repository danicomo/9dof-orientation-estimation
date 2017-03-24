namespace AngleEstimationApp_BetaRelease
{
    partial class SetUpForm
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Liberare le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.portLabel = new System.Windows.Forms.Label();
            this.portNumber = new System.Windows.Forms.DomainUpDown();
            this.algorithmLabel = new System.Windows.Forms.Label();
            this.algorithmBox = new System.Windows.Forms.ComboBox();
            this.obsLabel = new System.Windows.Forms.Label();
            this.obsBox = new System.Windows.Forms.ComboBox();
            this.startButton = new System.Windows.Forms.Button();
            this.trendOn = new System.Windows.Forms.RadioButton();
            this.trendOff = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.setParam = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Location = new System.Drawing.Point(13, 13);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(114, 13);
            this.portLabel.TabIndex = 0;
            this.portLabel.Text = "Select connection port";
            // 
            // portNumber
            // 
            this.portNumber.Items.Add("1");
            this.portNumber.Items.Add("2");
            this.portNumber.Items.Add("3");
            this.portNumber.Items.Add("4");
            this.portNumber.Items.Add("5");
            this.portNumber.Items.Add("6");
            this.portNumber.Items.Add("7");
            this.portNumber.Items.Add("8");
            this.portNumber.Items.Add("9");
            this.portNumber.Location = new System.Drawing.Point(241, 11);
            this.portNumber.Name = "portNumber";
            this.portNumber.ReadOnly = true;
            this.portNumber.Size = new System.Drawing.Size(120, 20);
            this.portNumber.Sorted = true;
            this.portNumber.TabIndex = 1;
            this.portNumber.SelectedItemChanged += new System.EventHandler(this.portNumber_SelectedItemChanged);
            // 
            // algorithmLabel
            // 
            this.algorithmLabel.AutoSize = true;
            this.algorithmLabel.Location = new System.Drawing.Point(16, 61);
            this.algorithmLabel.Name = "algorithmLabel";
            this.algorithmLabel.Size = new System.Drawing.Size(134, 13);
            this.algorithmLabel.TabIndex = 2;
            this.algorithmLabel.Text = "Select Estimation Algorithm";
            // 
            // algorithmBox
            // 
            this.algorithmBox.FormattingEnabled = true;
            this.algorithmBox.Items.AddRange(new object[] {
            "Complementary Filter",
            "Quaternion Complementary Filter",
            "Quaternion Kalman Filter"});
            this.algorithmBox.Location = new System.Drawing.Point(240, 53);
            this.algorithmBox.Name = "algorithmBox";
            this.algorithmBox.Size = new System.Drawing.Size(121, 21);
            this.algorithmBox.TabIndex = 3;
            this.algorithmBox.SelectedIndexChanged += new System.EventHandler(this.algorithmBox_SelectedIndexChanged);
            // 
            // obsLabel
            // 
            this.obsLabel.AutoSize = true;
            this.obsLabel.Location = new System.Drawing.Point(16, 108);
            this.obsLabel.Name = "obsLabel";
            this.obsLabel.Size = new System.Drawing.Size(199, 13);
            this.obsLabel.TabIndex = 4;
            this.obsLabel.Text = "Select Observations Estimation Algorithm";
            // 
            // obsBox
            // 
            this.obsBox.FormattingEnabled = true;
            this.obsBox.Items.AddRange(new object[] {
            "Gradient Descent",
            "Gauss-Newton"});
            this.obsBox.Location = new System.Drawing.Point(241, 100);
            this.obsBox.Name = "obsBox";
            this.obsBox.Size = new System.Drawing.Size(121, 21);
            this.obsBox.TabIndex = 5;
            this.obsBox.SelectedIndexChanged += new System.EventHandler(this.obsBox_SelectedIndexChanged);
            // 
            // startButton
            // 
            this.startButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startButton.Location = new System.Drawing.Point(240, 190);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(121, 30);
            this.startButton.TabIndex = 6;
            this.startButton.Text = "START";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // trendOn
            // 
            this.trendOn.AutoSize = true;
            this.trendOn.Location = new System.Drawing.Point(19, 157);
            this.trendOn.Name = "trendOn";
            this.trendOn.Size = new System.Drawing.Size(64, 17);
            this.trendOn.TabIndex = 8;
            this.trendOn.TabStop = true;
            this.trendOn.Text = "Enabled";
            this.trendOn.UseVisualStyleBackColor = true;
            // 
            // trendOff
            // 
            this.trendOff.AutoSize = true;
            this.trendOff.Location = new System.Drawing.Point(84, 157);
            this.trendOff.Name = "trendOff";
            this.trendOff.Size = new System.Drawing.Size(66, 17);
            this.trendOff.TabIndex = 8;
            this.trendOff.TabStop = true;
            this.trendOff.Text = "Disabled";
            this.trendOff.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 141);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Quaternion trend plot";
            // 
            // setParam
            // 
            this.setParam.Location = new System.Drawing.Point(12, 190);
            this.setParam.Name = "setParam";
            this.setParam.Size = new System.Drawing.Size(121, 31);
            this.setParam.TabIndex = 10;
            this.setParam.Text = "Set Parameters";
            this.setParam.UseVisualStyleBackColor = true;
            this.setParam.Click += new System.EventHandler(this.setParam_Click);
            // 
            // SetUpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 233);
            this.Controls.Add(this.setParam);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trendOff);
            this.Controls.Add(this.trendOn);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.obsBox);
            this.Controls.Add(this.obsLabel);
            this.Controls.Add(this.algorithmBox);
            this.Controls.Add(this.algorithmLabel);
            this.Controls.Add(this.portNumber);
            this.Controls.Add(this.portLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SetUpForm";
            this.Text = "SetUpForm";
            this.Load += new System.EventHandler(this.SetUpForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.DomainUpDown portNumber;
        private System.Windows.Forms.Label algorithmLabel;
        private System.Windows.Forms.ComboBox algorithmBox;
        private System.Windows.Forms.Label obsLabel;
        private System.Windows.Forms.ComboBox obsBox;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.RadioButton trendOn;
        private System.Windows.Forms.RadioButton trendOff;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button setParam;
    }
}