namespace Ndp_KaroOyunu
{//no:b231200048 isim:Mustafa Batýn GÜVEN
    public partial class Form1 : Form
    {
        public Form1()
        {
            this.StartPosition = FormStartPosition.CenterScreen; // Ortada baþlat
            this.WindowState = FormWindowState.Maximized; // Tam ekran baþlat
            this.FormBorderStyle = FormBorderStyle.Sizable; // Çerçeveli, yeniden boyutlandýrýlabilir pencere
            InitializeComponent();
        }

        private void btnBaslat_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = txtKullaniciAdi.Text;
            if (string.IsNullOrWhiteSpace(kullaniciAdi))
            {
                MessageBox.Show("Lütfen kullanýcý adýnýzý giriniz!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Form2 oyunFormu = new Form2();
                oyunFormu.Show();
                this.Hide();
            }
        }

        private void btnEnIyiSkorlar_Click(object sender, EventArgs e)
        {
            List<int> enIyiSkorlar = SkorYardimcisi.EnIyiSkorlariOku();

            if (enIyiSkorlar.Count == 0)
            {
                MessageBox.Show("Henüz bir skor kaydedilmedi.", "En Ýyi Skorlar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string mesaj = "En Ýyi 5 Skor:\n";

                for (int i = 0; i < Math.Min(5, enIyiSkorlar.Count); i++)
                {
                    mesaj += $"{i + 1}. {enIyiSkorlar[i]}\n";
                }

                MessageBox.Show(mesaj, "En Ýyi Skorlar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }



        private void Form1_Load(object sender, EventArgs e)
        {

                txtKullaniciAdi.Clear();
                txtKullaniciAdi.Focus();
            

        }
    }
}