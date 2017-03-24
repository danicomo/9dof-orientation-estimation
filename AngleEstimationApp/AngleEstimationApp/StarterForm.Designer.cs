namespace AngleEstimationApp
{
    partial class StarterForm
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
            this.StartButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ComPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.KalmanButton = new System.Windows.Forms.RadioButton();
            this.AHRSButton = new System.Windows.Forms.RadioButton();
            this.CompButton = new System.Windows.Forms.RadioButton();
            this.ErrorLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.Enabled = false;
            this.StartButton.Location = new System.Drawing.Point(360, 195);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(81, 25);
            this.StartButton.TabIndex = 0;
            this.StartButton.Text = "Avvia";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(328, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Selezionare la porta a cui è collegato l\'iNEMO";
            // 
            // ComPort
            // 
            this.ComPort.Location = new System.Drawing.Point(360, 24);
            this.ComPort.Name = "ComPort";
            this.ComPort.Size = new System.Drawing.Size(85, 20);
            this.ComPort.TabIndex = 2;
            this.ComPort.TextChanged += new System.EventHandler(this.ComPort_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(311, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Selezionare l\'algoritmo che si vuole avviare:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.KalmanButton);
            this.groupBox1.Controls.Add(this.AHRSButton);
            this.groupBox1.Controls.Add(this.CompButton);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox1.Location = new System.Drawing.Point(78, 93);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(162, 100);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // KalmanButton
            // 
            this.KalmanButton.AutoSize = true;
            this.KalmanButton.Location = new System.Drawing.Point(20, 65);
            this.KalmanButton.Name = "KalmanButton";
            this.KalmanButton.Size = new System.Drawing.Size(82, 17);
            this.KalmanButton.TabIndex = 2;
            this.KalmanButton.TabStop = true;
            this.KalmanButton.Text = "Kalman filter";
            this.KalmanButton.UseVisualStyleBackColor = true;
            this.KalmanButton.CheckedChanged += new System.EventHandler(this.KalmanButton_CheckedChanged);
            // 
            // AHRSButton
            // 
            this.AHRSButton.AutoSize = true;
            this.AHRSButton.Location = new System.Drawing.Point(20, 42);
            this.AHRSButton.Name = "AHRSButton";
            this.AHRSButton.Size = new System.Drawing.Size(100, 17);
            this.AHRSButton.TabIndex = 1;
            this.AHRSButton.TabStop = true;
            this.AHRSButton.Text = "AHRS algorithm";
            this.AHRSButton.UseVisualStyleBackColor = true;
            this.AHRSButton.CheckedChanged += new System.EventHandler(this.AHRSButton_CheckedChanged);
            // 
            // CompButton
            // 
            this.CompButton.AutoSize = true;
            this.CompButton.Location = new System.Drawing.Point(20, 19);
            this.CompButton.Name = "CompButton";
            this.CompButton.Size = new System.Drawing.Size(119, 17);
            this.CompButton.TabIndex = 0;
            this.CompButton.TabStop = true;
            this.CompButton.Text = "Complementary filter";
            this.CompButton.UseVisualStyleBackColor = true;
            this.CompButton.CheckedChanged += new System.EventHandler(this.CompButton_CheckedChanged);
            // 
            // ErrorLabel
            // 
            this.ErrorLabel.AutoSize = true;
            this.ErrorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.ErrorLabel.Location = new System.Drawing.Point(32, 201);
            this.ErrorLabel.Name = "ErrorLabel";
            this.ErrorLabel.Size = new System.Drawing.Size(0, 20);
            this.ErrorLabel.TabIndex = 6;
            this.ErrorLabel.Visible = false;
            // 
            // StarterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 231);
            this.Controls.Add(this.ErrorLabel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ComPort);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.StartButton);
            this.Name = "StarterForm";
            this.Text = "Starter";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ComPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton KalmanButton;
        private System.Windows.Forms.RadioButton AHRSButton;
        private System.Windows.Forms.RadioButton CompButton;
        private System.Windows.Forms.Label ErrorLabel;
    }
}