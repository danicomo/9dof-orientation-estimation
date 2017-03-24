namespace AngleEstimationApp_BetaRelease
{
    partial class ParametersForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.xOff = new System.Windows.Forms.TextBox();
            this.yOff = new System.Windows.Forms.TextBox();
            this.zOff = new System.Windows.Forms.TextBox();
            this.xScale = new System.Windows.Forms.TextBox();
            this.yScale = new System.Windows.Forms.TextBox();
            this.zScale = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.gyroXOff = new System.Windows.Forms.TextBox();
            this.gyroYOff = new System.Windows.Forms.TextBox();
            this.gyroZOff = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Magnetometer";
            // 
            // xOff
            // 
            this.xOff.Location = new System.Drawing.Point(99, 40);
            this.xOff.Name = "xOff";
            this.xOff.Size = new System.Drawing.Size(100, 20);
            this.xOff.TabIndex = 1;
            this.xOff.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // yOff
            // 
            this.yOff.Location = new System.Drawing.Point(99, 66);
            this.yOff.Name = "yOff";
            this.yOff.Size = new System.Drawing.Size(100, 20);
            this.yOff.TabIndex = 1;
            this.yOff.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // zOff
            // 
            this.zOff.Location = new System.Drawing.Point(99, 90);
            this.zOff.Name = "zOff";
            this.zOff.Size = new System.Drawing.Size(100, 20);
            this.zOff.TabIndex = 1;
            this.zOff.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // xScale
            // 
            this.xScale.Location = new System.Drawing.Point(99, 116);
            this.xScale.Name = "xScale";
            this.xScale.Size = new System.Drawing.Size(100, 20);
            this.xScale.TabIndex = 1;
            this.xScale.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // yScale
            // 
            this.yScale.Location = new System.Drawing.Point(99, 142);
            this.yScale.Name = "yScale";
            this.yScale.Size = new System.Drawing.Size(100, 20);
            this.yScale.TabIndex = 1;
            this.yScale.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // zScale
            // 
            this.zScale.Location = new System.Drawing.Point(99, 168);
            this.zScale.Name = "zScale";
            this.zScale.Size = new System.Drawing.Size(100, 20);
            this.zScale.TabIndex = 1;
            this.zScale.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "X Offset";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Y Offset";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Z Offset";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "X Scale Factor";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 149);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Y Scale Factor";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 175);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Z Scale Factor";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tempus Sans ITC", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(245, 142);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(158, 46);
            this.button1.TabIndex = 3;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(227, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 20);
            this.label8.TabIndex = 4;
            this.label8.Text = "Gyroscopes";
            // 
            // gyroXOff
            // 
            this.gyroXOff.Location = new System.Drawing.Point(303, 37);
            this.gyroXOff.Name = "gyroXOff";
            this.gyroXOff.Size = new System.Drawing.Size(100, 20);
            this.gyroXOff.TabIndex = 1;
            this.gyroXOff.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // gyroYOff
            // 
            this.gyroYOff.Location = new System.Drawing.Point(303, 63);
            this.gyroYOff.Name = "gyroYOff";
            this.gyroYOff.Size = new System.Drawing.Size(100, 20);
            this.gyroYOff.TabIndex = 1;
            this.gyroYOff.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // gyroZOff
            // 
            this.gyroZOff.Location = new System.Drawing.Point(303, 89);
            this.gyroZOff.Name = "gyroZOff";
            this.gyroZOff.Size = new System.Drawing.Size(100, 20);
            this.gyroZOff.TabIndex = 1;
            this.gyroZOff.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(242, 40);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "X Offset";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(242, 66);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "Y Offset";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(242, 92);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(45, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "Z Offset";
            // 
            // ParametersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 201);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.zScale);
            this.Controls.Add(this.yScale);
            this.Controls.Add(this.gyroZOff);
            this.Controls.Add(this.zOff);
            this.Controls.Add(this.xScale);
            this.Controls.Add(this.gyroYOff);
            this.Controls.Add(this.yOff);
            this.Controls.Add(this.gyroXOff);
            this.Controls.Add(this.xOff);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ParametersForm";
            this.Text = "Parameters";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox xOff;
        private System.Windows.Forms.TextBox yOff;
        private System.Windows.Forms.TextBox zOff;
        private System.Windows.Forms.TextBox xScale;
        private System.Windows.Forms.TextBox yScale;
        private System.Windows.Forms.TextBox zScale;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox gyroXOff;
        private System.Windows.Forms.TextBox gyroYOff;
        private System.Windows.Forms.TextBox gyroZOff;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
    }
}