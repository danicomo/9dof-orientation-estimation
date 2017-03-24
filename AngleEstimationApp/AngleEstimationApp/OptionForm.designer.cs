namespace AngleEstimationApp
{
    partial class OptionsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtvarx = new System.Windows.Forms.TextBox();
            this.txtvary = new System.Windows.Forms.TextBox();
            this.txtvarz = new System.Windows.Forms.TextBox();
            this.txtoffz = new System.Windows.Forms.TextBox();
            this.txtoffy = new System.Windows.Forms.TextBox();
            this.txtoffx = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtAHRS1 = new System.Windows.Forms.TextBox();
            this.txtAHRS2 = new System.Windows.Forms.TextBox();
            this.txtfreq = new System.Windows.Forms.TextBox();
            this.txtOffmagnz = new System.Windows.Forms.TextBox();
            this.txtOffmagny = new System.Windows.Forms.TextBox();
            this.txtOffmagnx = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.txtSfmagnx = new System.Windows.Forms.TextBox();
            this.txtSfmagny = new System.Windows.Forms.TextBox();
            this.txtSfmagnz = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonSalva = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.Standard = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Varianze giroscopio:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(38, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "X";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(37, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Z";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(37, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Y";
            // 
            // txtvarx
            // 
            this.txtvarx.Location = new System.Drawing.Point(75, 34);
            this.txtvarx.Name = "txtvarx";
            this.txtvarx.Size = new System.Drawing.Size(100, 20);
            this.txtvarx.TabIndex = 4;
            this.txtvarx.TextChanged += new System.EventHandler(this.txtvarx_TextChanged);
            // 
            // txtvary
            // 
            this.txtvary.Location = new System.Drawing.Point(75, 60);
            this.txtvary.Name = "txtvary";
            this.txtvary.Size = new System.Drawing.Size(100, 20);
            this.txtvary.TabIndex = 5;
            this.txtvary.TextChanged += new System.EventHandler(this.txtvary_TextChanged);
            // 
            // txtvarz
            // 
            this.txtvarz.Location = new System.Drawing.Point(75, 86);
            this.txtvarz.Name = "txtvarz";
            this.txtvarz.Size = new System.Drawing.Size(100, 20);
            this.txtvarz.TabIndex = 6;
            this.txtvarz.TextChanged += new System.EventHandler(this.txtvarz_TextChanged);
            // 
            // txtoffz
            // 
            this.txtoffz.Location = new System.Drawing.Point(74, 190);
            this.txtoffz.Name = "txtoffz";
            this.txtoffz.Size = new System.Drawing.Size(100, 20);
            this.txtoffz.TabIndex = 13;
            this.txtoffz.TextChanged += new System.EventHandler(this.txtoffz_TextChanged);
            // 
            // txtoffy
            // 
            this.txtoffy.Location = new System.Drawing.Point(74, 164);
            this.txtoffy.Name = "txtoffy";
            this.txtoffy.Size = new System.Drawing.Size(100, 20);
            this.txtoffy.TabIndex = 12;
            this.txtoffy.TextChanged += new System.EventHandler(this.txtoffy_TextChanged);
            // 
            // txtoffx
            // 
            this.txtoffx.Location = new System.Drawing.Point(74, 138);
            this.txtoffx.Name = "txtoffx";
            this.txtoffx.Size = new System.Drawing.Size(100, 20);
            this.txtoffx.TabIndex = 11;
            this.txtoffx.TextChanged += new System.EventHandler(this.txtoffx_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(36, 164);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "Y";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(36, 190);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 16);
            this.label6.TabIndex = 9;
            this.label6.Text = "Z";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(37, 138);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(16, 16);
            this.label7.TabIndex = 8;
            this.label7.Text = "X";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(12, 109);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(112, 16);
            this.label8.TabIndex = 7;
            this.label8.Text = "Offset giroscopio:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(12, 223);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(149, 16);
            this.label9.TabIndex = 14;
            this.label9.Text = "Primo parametro AHRS";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(12, 251);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(152, 15);
            this.label10.TabIndex = 15;
            this.label10.Text = "Secondo parametro AHRS";
            // 
            // txtAHRS1
            // 
            this.txtAHRS1.AccessibleDescription = "";
            this.txtAHRS1.Location = new System.Drawing.Point(164, 223);
            this.txtAHRS1.Name = "txtAHRS1";
            this.txtAHRS1.Size = new System.Drawing.Size(124, 20);
            this.txtAHRS1.TabIndex = 16;
            this.txtAHRS1.TextChanged += new System.EventHandler(this.txtAHRS1_TextChanged);
            // 
            // txtAHRS2
            // 
            this.txtAHRS2.Location = new System.Drawing.Point(164, 247);
            this.txtAHRS2.Name = "txtAHRS2";
            this.txtAHRS2.Size = new System.Drawing.Size(124, 20);
            this.txtAHRS2.TabIndex = 17;
            this.txtAHRS2.TextChanged += new System.EventHandler(this.txtAHRS2_TextChanged);
            // 
            // txtfreq
            // 
            this.txtfreq.Location = new System.Drawing.Point(164, 273);
            this.txtfreq.Name = "txtfreq";
            this.txtfreq.Size = new System.Drawing.Size(124, 20);
            this.txtfreq.TabIndex = 18;
            this.txtfreq.TextChanged += new System.EventHandler(this.txtfreq_TextChanged);
            // 
            // txtOffmagnz
            // 
            this.txtOffmagnz.Location = new System.Drawing.Point(74, 416);
            this.txtOffmagnz.Name = "txtOffmagnz";
            this.txtOffmagnz.Size = new System.Drawing.Size(124, 20);
            this.txtOffmagnz.TabIndex = 22;
            this.txtOffmagnz.TextChanged += new System.EventHandler(this.txtOffmagnz_TextChanged);
            // 
            // txtOffmagny
            // 
            this.txtOffmagny.Location = new System.Drawing.Point(74, 390);
            this.txtOffmagny.Name = "txtOffmagny";
            this.txtOffmagny.Size = new System.Drawing.Size(124, 20);
            this.txtOffmagny.TabIndex = 23;
            this.txtOffmagny.TextChanged += new System.EventHandler(this.txtOffmagny_TextChanged);
            // 
            // txtOffmagnx
            // 
            this.txtOffmagnx.Location = new System.Drawing.Point(74, 364);
            this.txtOffmagnx.Name = "txtOffmagnx";
            this.txtOffmagnx.Size = new System.Drawing.Size(124, 20);
            this.txtOffmagnx.TabIndex = 24;
            this.txtOffmagnx.TextChanged += new System.EventHandler(this.txtOffmagnx_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(12, 306);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(176, 16);
            this.label13.TabIndex = 27;
            this.label13.Text = "Calibrazione magnetometro:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(12, 277);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(130, 16);
            this.label14.TabIndex = 28;
            this.label14.Text = "Frequenza di lavoro:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(12, 333);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(135, 16);
            this.label11.TabIndex = 29;
            this.label11.Text = "Offset magnetometro:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(34, 391);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(17, 16);
            this.label12.TabIndex = 32;
            this.label12.Text = "Y";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(35, 417);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(16, 16);
            this.label15.TabIndex = 31;
            this.label15.Text = "Z";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(36, 365);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(16, 16);
            this.label16.TabIndex = 30;
            this.label16.Text = "X";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(255, 392);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(17, 16);
            this.label17.TabIndex = 39;
            this.label17.Text = "Y";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(256, 418);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(16, 16);
            this.label18.TabIndex = 38;
            this.label18.Text = "Z";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(257, 366);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(16, 16);
            this.label19.TabIndex = 37;
            this.label19.Text = "X";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(232, 333);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(174, 16);
            this.label20.TabIndex = 36;
            this.label20.Text = "Fattori scala magnetometro:";
            // 
            // txtSfmagnx
            // 
            this.txtSfmagnx.Location = new System.Drawing.Point(295, 365);
            this.txtSfmagnx.Name = "txtSfmagnx";
            this.txtSfmagnx.Size = new System.Drawing.Size(124, 20);
            this.txtSfmagnx.TabIndex = 35;
            this.txtSfmagnx.TextChanged += new System.EventHandler(this.txtSfmagnx_TextChanged);
            // 
            // txtSfmagny
            // 
            this.txtSfmagny.Location = new System.Drawing.Point(295, 391);
            this.txtSfmagny.Name = "txtSfmagny";
            this.txtSfmagny.Size = new System.Drawing.Size(124, 20);
            this.txtSfmagny.TabIndex = 34;
            this.txtSfmagny.TextChanged += new System.EventHandler(this.txtSfmagny_TextChanged);
            // 
            // txtSfmagnz
            // 
            this.txtSfmagnz.Location = new System.Drawing.Point(295, 417);
            this.txtSfmagnz.Name = "txtSfmagnz";
            this.txtSfmagnz.Size = new System.Drawing.Size(124, 20);
            this.txtSfmagnz.TabIndex = 33;
            this.txtSfmagnz.TextChanged += new System.EventHandler(this.txtSfmagnz_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(235, 303);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(171, 22);
            this.button1.TabIndex = 40;
            this.button1.Text = "Calcola automaticamente valori";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // buttonSalva
            // 
            this.buttonSalva.Location = new System.Drawing.Point(468, 469);
            this.buttonSalva.Name = "buttonSalva";
            this.buttonSalva.Size = new System.Drawing.Size(95, 28);
            this.buttonSalva.TabIndex = 41;
            this.buttonSalva.Text = "Salva parametri";
            this.buttonSalva.UseVisualStyleBackColor = true;
            this.buttonSalva.Click += new System.EventHandler(this.buttonSalva_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(339, 25);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(199, 16);
            this.label21.TabIndex = 42;
            this.label21.Text = "Inserire solo valori numerici";
            // 
            // Standard
            // 
            this.Standard.Location = new System.Drawing.Point(334, 469);
            this.Standard.Name = "Standard";
            this.Standard.Size = new System.Drawing.Size(128, 28);
            this.Standard.TabIndex = 43;
            this.Standard.Text = "Usa parametri standard";
            this.Standard.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Standard.UseVisualStyleBackColor = true;
            this.Standard.Click += new System.EventHandler(this.Standard_Click);
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 509);
            this.Controls.Add(this.Standard);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.buttonSalva);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.txtSfmagnx);
            this.Controls.Add(this.txtSfmagny);
            this.Controls.Add(this.txtSfmagnz);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtOffmagnx);
            this.Controls.Add(this.txtOffmagny);
            this.Controls.Add(this.txtOffmagnz);
            this.Controls.Add(this.txtfreq);
            this.Controls.Add(this.txtAHRS2);
            this.Controls.Add(this.txtAHRS1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtoffz);
            this.Controls.Add(this.txtoffy);
            this.Controls.Add(this.txtoffx);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtvarz);
            this.Controls.Add(this.txtvary);
            this.Controls.Add(this.txtvarx);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "OptionsForm";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtvarx;
        private System.Windows.Forms.TextBox txtvary;
        private System.Windows.Forms.TextBox txtvarz;
        private System.Windows.Forms.TextBox txtoffz;
        private System.Windows.Forms.TextBox txtoffy;
        private System.Windows.Forms.TextBox txtoffx;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtAHRS1;
        private System.Windows.Forms.TextBox txtAHRS2;
        private System.Windows.Forms.TextBox txtfreq;
        private System.Windows.Forms.TextBox txtOffmagnz;
        private System.Windows.Forms.TextBox txtOffmagny;
        private System.Windows.Forms.TextBox txtOffmagnx;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtSfmagnx;
        private System.Windows.Forms.TextBox txtSfmagny;
        private System.Windows.Forms.TextBox txtSfmagnz;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonSalva;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button Standard;
    }
}