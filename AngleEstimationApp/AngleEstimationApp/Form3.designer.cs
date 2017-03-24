namespace AngleEstimationApp
{
    partial class Form3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3));
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.buttonAvvia = new System.Windows.Forms.Button();
            this.lblAzione = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Enabled = false;
            this.richTextBox1.Location = new System.Drawing.Point(-2, 1);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(287, 154);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // buttonAvvia
            // 
            this.buttonAvvia.Location = new System.Drawing.Point(83, 181);
            this.buttonAvvia.Name = "buttonAvvia";
            this.buttonAvvia.Size = new System.Drawing.Size(104, 27);
            this.buttonAvvia.TabIndex = 2;
            this.buttonAvvia.Text = "Avvia";
            this.buttonAvvia.UseVisualStyleBackColor = true;
            this.buttonAvvia.Click += new System.EventHandler(this.buttonAvvia_Click);
            // 
            // lblAzione
            // 
            this.lblAzione.AutoSize = true;
            this.lblAzione.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAzione.ForeColor = System.Drawing.Color.Red;
            this.lblAzione.Location = new System.Drawing.Point(2, 221);
            this.lblAzione.Name = "lblAzione";
            this.lblAzione.Size = new System.Drawing.Size(278, 16);
            this.lblAzione.TabIndex = 3;
            this.lblAzione.Text = "Effettuare rotazione con Z parallelo a G";
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.lblAzione);
            this.Controls.Add(this.buttonAvvia);
            this.Controls.Add(this.richTextBox1);
            this.Name = "Form3";
            this.Text = "Calibrazione magnetometro";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button buttonAvvia;
        private System.Windows.Forms.Label lblAzione;
    }
}