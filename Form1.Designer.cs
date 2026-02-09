namespace Ndp_KaroOyunu
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnBaslat = new Button();
            txtKullaniciAdi = new TextBox();
            btnEnIyiSkorlar = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            SuspendLayout();
            // 
            // btnBaslat
            // 
            btnBaslat.Font = new Font("Showcard Gothic", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            btnBaslat.Location = new Point(219, 128);
            btnBaslat.Name = "btnBaslat";
            btnBaslat.Size = new Size(200, 50);
            btnBaslat.TabIndex = 0;
            btnBaslat.Text = "BAŞLAT";
            btnBaslat.UseVisualStyleBackColor = true;
            btnBaslat.Click += btnBaslat_Click;
            // 
            // txtKullaniciAdi
            // 
            txtKullaniciAdi.Location = new Point(210, 70);
            txtKullaniciAdi.Name = "txtKullaniciAdi";
            txtKullaniciAdi.Size = new Size(194, 27);
            txtKullaniciAdi.TabIndex = 1;
            // 
            // btnEnIyiSkorlar
            // 
            btnEnIyiSkorlar.Font = new Font("Showcard Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnEnIyiSkorlar.Location = new Point(589, 349);
            btnEnIyiSkorlar.Name = "btnEnIyiSkorlar";
            btnEnIyiSkorlar.Size = new Size(176, 96);
            btnEnIyiSkorlar.TabIndex = 2;
            btnEnIyiSkorlar.Text = "En İyi Skorlar";
            btnEnIyiSkorlar.UseVisualStyleBackColor = true;
            btnEnIyiSkorlar.Click += btnEnIyiSkorlar_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Showcard Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(286, 9);
            label1.Name = "label1";
            label1.Size = new Size(220, 37);
            label1.TabIndex = 3;
            label1.Text = "KARO OYUNU";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Showcard Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(45, 71);
            label2.Name = "label2";
            label2.Size = new Size(159, 26);
            label2.TabIndex = 4;
            label2.Text = "Kullanıcı Adı:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Showcard Gothic", 7.8F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(132, 283);
            label3.Name = "label3";
            label3.Size = new Size(556, 17);
            label3.TabIndex = 5;
            label3.Text = "Nesneleri hareket ettirmek için mouse veya yön tuşlarını kullanabilirsiniz.";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Showcard Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(194, 310);
            label4.Name = "label4";
            label4.Size = new Size(393, 18);
            label4.TabIndex = 6;
            label4.Text = "Oyunu durdurmak için 'P' tuşuna basabilirsiniz.";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.marvel;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(777, 457);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnEnIyiSkorlar);
            Controls.Add(txtKullaniciAdi);
            Controls.Add(btnBaslat);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnBaslat;
        private TextBox txtKullaniciAdi;
        private Button btnEnIyiSkorlar;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}