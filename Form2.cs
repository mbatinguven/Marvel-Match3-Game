using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
//no:b231200048 isim:Mustafa Batın GÜVEN
namespace Ndp_KaroOyunu
{
    public partial class Form2 : Form
    {
        private const int gridBoyutu = 8; // 8x8 oyun alanı
        private Button[,] oyunGrid; // Grid için düğme matrisi
        private Random rastgele = new Random(); // Rastgele nesneler için

        private Image[] karakterResimleri = {
            Properties.Resources.batman,
            Properties.Resources.captain,
            Properties.Resources.hulk,
            Properties.Resources.ironman,
            Properties.Resources.spiderman,
            Properties.Resources.superman,
            Properties.Resources.ww
        };

        private Button ilkSecilenBtn = null; // İlk seçilen buton
        private int toplamPuan = 0;
        private System.Windows.Forms.Timer oyunTimer;
        private int kalanSure = 60; // 60 saniye süre
        private Label lblSure;
        private Label lblPuan;
        private Button btnPause;
        private Button btnRestart;

        public Form2()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen; // Ortada başlat
            this.WindowState = FormWindowState.Maximized; // Tam ekran başlat
            this.FormBorderStyle = FormBorderStyle.Sizable; // Çerçeveli, yeniden boyutlandırılabilir pencere

            this.Resize += Form2_Resize; // Form boyutu değiştiğinde tetiklenecek
            OyunAlaniOlustur();
            PuanGostericiOlustur();
            ZamanlayiciOlustur();
            PauseButonuOlustur();
            RestartButonuOlustur();
        }


        private void Form2_Resize(object sender, EventArgs e)
        {
            OyunAlaniOlustur(); // Grid yeniden oluşturulur
            OyunAlaniYenidenBoyutlandir();
        }


        private void Form2_Load(object sender, EventArgs e)
        {
            OyunAlaniOlustur();
        }
        private void OyunAlaniOlustur()
        {
            // Önce eski grid öğelerini temizleyelim
            if (oyunGrid != null)
            {
                foreach (Button btn in oyunGrid)
                {
                    if (btn != null)
                        this.Controls.Remove(btn);
                }
            }

            int alanGenisligi = this.ClientSize.Width;
            int alanYuksekligi = this.ClientSize.Height - 150;

            int butonBoyutu = (int)(Math.Min(alanGenisligi / gridBoyutu, alanYuksekligi / gridBoyutu) * 0.9);

            oyunGrid = new Button[gridBoyutu, gridBoyutu];

            for (int i = 0; i < gridBoyutu; i++)
            {
                for (int j = 0; j < gridBoyutu; j++)
                {
                    Button btn = new Button();
                    btn.Size = new Size(butonBoyutu, butonBoyutu);
                    btn.Location = new Point(j * butonBoyutu, i * butonBoyutu + 100);
                    btn.BackgroundImageLayout = ImageLayout.Stretch;

                    // %5 olasılıkla bir joker yerleştir
                    if (rastgele.Next(100) < 5)
                    {
                        switch (rastgele.Next(4))
                        {
                            case 0:
                                btn.BackgroundImage = roketResmi;
                                btn.Tag = "roket";
                                break;
                            case 1:
                                btn.BackgroundImage = kopterResmi;
                                btn.Tag = "kopter";
                                break;
                            case 2:
                                btn.BackgroundImage = bombaResmi;
                                btn.Tag = "bomba";
                                break;
                            case 3:
                                btn.BackgroundImage = gokkusagiResmi;
                                btn.Tag = "gokkusagi";
                                break;
                        }
                    }
                    else
                    {
                        btn.BackgroundImage = RastgeleKarakterResmiGetir(i, j);
                    }

                    btn.Click += Btn_Click;
                    oyunGrid[i, j] = btn;
                    this.Controls.Add(btn);
                }
            }
        }


        private void OyunAlaniYenidenBoyutlandir()
        {
            if (oyunGrid == null) return;

            // Formun boyutlarına göre oyun alanı genişliği ve yüksekliği
            int alanGenisligi = this.ClientSize.Width;
            int alanYuksekligi = this.ClientSize.Height - 150; // 150 piksel üst alan boşluğu

            // Buton boyutlarını arttırmak için 0.9 faktörü uygulayın
            int butonBoyutu = (int)(Math.Min(alanGenisligi / gridBoyutu, alanYuksekligi / gridBoyutu) * 0.9);

            for (int i = 0; i < gridBoyutu; i++)
            {
                for (int j = 0; j < gridBoyutu; j++)
                {
                    Button btn = oyunGrid[i, j];
                    if (btn != null)
                    {
                        btn.Size = new Size(butonBoyutu, butonBoyutu);
                        btn.Location = new Point(j * butonBoyutu, i * butonBoyutu + 100); // 100 piksel üst boşluk
                    }
                }
            }
        }


        private Image roketResmi = Properties.Resources.roket;
        private Image kopterResmi = Properties.Resources.helikopter;
        private Image bombaResmi = Properties.Resources.bomba;
        private Image gokkusagiResmi = Properties.Resources.gokkusagi;


        private Image RastgeleKarakterResmiGetir(int i, int j)
        {
            // Mevcut gridde otomatik eşleşme olmaması için kontrol yapıyoruz
            List<Image> uygunResimler = new List<Image>(karakterResimleri);

            // Soldan 2 ve yukarıdan 2 kontrol
            if (j >= 2 && oyunGrid[i, j - 1].BackgroundImage == oyunGrid[i, j - 2].BackgroundImage)
            {
                uygunResimler.Remove(oyunGrid[i, j - 1].BackgroundImage);
            }

            if (i >= 2 && oyunGrid[i - 1, j].BackgroundImage == oyunGrid[i - 2, j].BackgroundImage)
            {
                uygunResimler.Remove(oyunGrid[i - 1, j].BackgroundImage);
            }

            // Rastgele uygun bir resim seç
            return uygunResimler[rastgele.Next(uygunResimler.Count)];
        }



        private void Btn_Click(object sender, EventArgs e)
        {
            Button tiklananBtn = sender as Button;

            if (tiklananBtn != null && tiklananBtn.BackgroundImage != null)
            {
                if (tiklananBtn.Tag != null)
                {
                    string jokerTuru = tiklananBtn.Tag.ToString();

                    switch (jokerTuru)
                    {
                        case "roket":
                            RoketPatlat(tiklananBtn);
                            break;
                        case "kopter":
                            KopterPatlat();
                            break;
                        case "bomba":
                            BombaPatlat(tiklananBtn);
                            break;
                        case "gokkusagi":
                            GokkusagiPatlat();
                            break;
                    }

                    // Jokeri kaldır
                    tiklananBtn.BackgroundImage = null;
                    tiklananBtn.Tag = null;

                    // Joker sonrası grid güncellemesi
                    PatlatVeYenile(new bool[gridBoyutu, gridBoyutu]);
                }
                else
                {
                    KarakterIslem(tiklananBtn);
                    EslestirmeVePatlatma();
                }
            }
        }




        private void RoketPatlat(Button jokerBtn)
        {
            int x = jokerBtn.Location.X / jokerBtn.Width;
            int y = (jokerBtn.Location.Y - 100) / jokerBtn.Height;

            List<Point> patlayanKoordinatlar = new List<Point>();

            for (int i = 0; i < gridBoyutu; i++)
            {
                if (oyunGrid[y, i].BackgroundImage != null)
                    patlayanKoordinatlar.Add(new Point(i, y)); // Yatay patlama
                if (oyunGrid[i, x].BackgroundImage != null)
                    patlayanKoordinatlar.Add(new Point(x, i)); // Dikey patlama

                oyunGrid[y, i].BackgroundImage = null;
                oyunGrid[i, x].BackgroundImage = null;
            }

            toplamPuan += patlayanKoordinatlar.Count * 10; // Jokerin patlattığı öğeler için puan
            lblPuan.Text = "Puan: " + toplamPuan;

            // Patlama animasyonu
            PatlamaAnimasyonuGoster(patlayanKoordinatlar, () =>
            {
                BosluklariDoldur();
            });
        }



        private void KopterPatlat()
        {
            int x = rastgele.Next(gridBoyutu);
            int y = rastgele.Next(gridBoyutu);

            List<Point> patlayanKoordinatlar = new List<Point>();
            if (oyunGrid[y, x].BackgroundImage != null)
            {
                patlayanKoordinatlar.Add(new Point(x, y));
                toplamPuan += 10; // Tek bir öğe patlatılır
                lblPuan.Text = "Puan: " + toplamPuan;
                oyunGrid[y, x].BackgroundImage = null;
            }

            // Patlama animasyonu
            PatlamaAnimasyonuGoster(patlayanKoordinatlar, () =>
            {
                BosluklariDoldur();
            });
        }



        private void BombaPatlat(Button jokerBtn)
        {
            int x = jokerBtn.Location.X / jokerBtn.Width;
            int y = (jokerBtn.Location.Y - 100) / jokerBtn.Height;

            List<Point> patlayanKoordinatlar = new List<Point>();

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int nx = x + i;
                    int ny = y + j;

                    if (nx >= 0 && nx < gridBoyutu && ny >= 0 && ny < gridBoyutu)
                    {
                        if (oyunGrid[ny, nx].BackgroundImage != null)
                            patlayanKoordinatlar.Add(new Point(nx, ny));
                        oyunGrid[ny, nx].BackgroundImage = null;
                    }
                }
            }

            toplamPuan += patlayanKoordinatlar.Count * 10;
            lblPuan.Text = "Puan: " + toplamPuan;

            // Patlama animasyonu
            PatlamaAnimasyonuGoster(patlayanKoordinatlar, () =>
            {
                BosluklariDoldur();
            });
        }



        private void GokkusagiPatlat()
        {
            Image rastgeleResim = karakterResimleri[rastgele.Next(karakterResimleri.Length)];
            List<Point> patlayanKoordinatlar = new List<Point>();

            for (int i = 0; i < gridBoyutu; i++)
            {
                for (int j = 0; j < gridBoyutu; j++)
                {
                    if (oyunGrid[i, j].BackgroundImage == rastgeleResim)
                    {
                        patlayanKoordinatlar.Add(new Point(j, i));
                        oyunGrid[i, j].BackgroundImage = null;
                    }
                }
            }

            toplamPuan += patlayanKoordinatlar.Count * 10;
            lblPuan.Text = "Puan: " + toplamPuan;

            // Patlama animasyonu
            PatlamaAnimasyonuGoster(patlayanKoordinatlar, () =>
            {
                BosluklariDoldur();
            });
        }








        private bool YanYanaMi(Button btn1, Button btn2)
        {
            if (btn1 == null || btn2 == null) return false;

            // İlk düğmenin boyutunu al
            int butonBoyutu = oyunGrid[0, 0].Width;

            // Konumları düğme boyutlarına göre hesapla
            Point pos1 = btn1.Location;
            Point pos2 = btn2.Location;

            int x1 = pos1.X / butonBoyutu;
            int y1 = (pos1.Y - 100) / butonBoyutu; // 100 piksel üst boşluğu çıkarıyoruz

            int x2 = pos2.X / butonBoyutu;
            int y2 = (pos2.Y - 100) / butonBoyutu;

            // Yanyana mı kontrol et
            return (Math.Abs(x1 - x2) == 1 && y1 == y2) || (Math.Abs(y1 - y2) == 1 && x1 == x2);
        }


        private void EslestirmeVePatlatma()
        {
            bool[,] eslesmeMatrisi = new bool[gridBoyutu, gridBoyutu];
            int maxEslesme = 0;

            // Yatay eşleşmeleri kontrol et
            for (int i = 0; i < gridBoyutu; i++)
            {
                int eslesmeSayisi = 1;
                for (int j = 1; j < gridBoyutu; j++)
                {
                    if (oyunGrid[i, j].BackgroundImage != null && oyunGrid[i, j].BackgroundImage == oyunGrid[i, j - 1].BackgroundImage)
                    {
                        eslesmeSayisi++;
                    }
                    else
                    {
                        if (eslesmeSayisi >= 3)
                        {
                            maxEslesme = Math.Max(maxEslesme, eslesmeSayisi);
                            for (int k = 0; k < eslesmeSayisi; k++)
                            {
                                eslesmeMatrisi[i, j - 1 - k] = true;
                            }
                        }
                        eslesmeSayisi = 1;
                    }
                }
                if (eslesmeSayisi >= 3)
                {
                    maxEslesme = Math.Max(maxEslesme, eslesmeSayisi);
                    for (int k = 0; k < eslesmeSayisi; k++)
                    {
                        eslesmeMatrisi[i, gridBoyutu - 1 - k] = true;
                    }
                }
            }

            // Dikey eşleşmeleri kontrol et
            for (int j = 0; j < gridBoyutu; j++)
            {
                int eslesmeSayisi = 1;
                for (int i = 1; i < gridBoyutu; i++)
                {
                    if (oyunGrid[i, j].BackgroundImage != null && oyunGrid[i, j].BackgroundImage == oyunGrid[i - 1, j].BackgroundImage)
                    {
                        eslesmeSayisi++;
                    }
                    else
                    {
                        if (eslesmeSayisi >= 3)
                        {
                            maxEslesme = Math.Max(maxEslesme, eslesmeSayisi);
                            for (int k = 0; k < eslesmeSayisi; k++)
                            {
                                eslesmeMatrisi[i - 1 - k, j] = true;
                            }
                        }
                        eslesmeSayisi = 1;
                    }
                }
                if (eslesmeSayisi >= 3)
                {
                    maxEslesme = Math.Max(maxEslesme, eslesmeSayisi);
                    for (int k = 0; k < eslesmeSayisi; k++)
                    {
                        eslesmeMatrisi[gridBoyutu - 1 - k, j] = true;
                    }
                }
            }

            // Puanlama: 3 için 10 puan, 4 için 20 puan, 5 ve üzeri için 50 puan
            int ekstraPuan = maxEslesme switch
            {
                3 => 10,
                4 => 20,
                >= 5 => 50,
                _ => 0
            };
            toplamPuan += ekstraPuan;
            lblPuan.Text = "Puan: " + toplamPuan;

            // Patlama ve yenileme işlemi
            PatlatVeYenile(eslesmeMatrisi);
        }

        private void PatlatVeYenile(bool[,] eslesmeMatrisi)
        {
            List<Point> patlayanKoordinatlar = new List<Point>();

            for (int i = 0; i < gridBoyutu; i++)
            {
                for (int j = 0; j < gridBoyutu; j++)
                {
                    if (eslesmeMatrisi[i, j])
                    {
                        patlayanKoordinatlar.Add(new Point(j, i));
                    }
                }
            }

            toplamPuan += patlayanKoordinatlar.Count * 10;
            lblPuan.Text = "Puan: " + toplamPuan;

            // Patlama animasyonu ve boşluk doldurma
            PatlamaAnimasyonuGoster(patlayanKoordinatlar, () =>
            {
                BosluklariDoldur();

                // Yeni eşleşmeleri kontrol et
                EslestirmeVePatlatma();
            });
        }






        private void BosluklariDoldur()
        {
            for (int j = 0; j < gridBoyutu; j++)
            {
                for (int i = gridBoyutu - 1; i >= 0; i--)
                {
                    if (oyunGrid[i, j].BackgroundImage == null) // Eğer hücre boşsa
                    {
                        for (int k = i - 1; k >= 0; k--)
                        {
                            if (oyunGrid[k, j].BackgroundImage != null) // Üst hücreyi aşağı kaydır
                            {
                                oyunGrid[i, j].BackgroundImage = oyunGrid[k, j].BackgroundImage;
                                oyunGrid[i, j].Tag = oyunGrid[k, j].Tag;
                                oyunGrid[k, j].BackgroundImage = null;
                                oyunGrid[k, j].Tag = null;
                                break;
                            }
                        }

                        // Eğer yukarıda dolu bir hücre bulunmadıysa yeni bir simge oluştur
                        if (oyunGrid[i, j].BackgroundImage == null)
                        {
                            // %5 olasılıkla joker ekle
                            if (rastgele.Next(100) < 5)
                            {
                                switch (rastgele.Next(4))
                                {
                                    case 0:
                                        oyunGrid[i, j].BackgroundImage = roketResmi;
                                        oyunGrid[i, j].Tag = "roket";
                                        break;
                                    case 1:
                                        oyunGrid[i, j].BackgroundImage = kopterResmi;
                                        oyunGrid[i, j].Tag = "kopter";
                                        break;
                                    case 2:
                                        oyunGrid[i, j].BackgroundImage = bombaResmi;
                                        oyunGrid[i, j].Tag = "bomba";
                                        break;
                                    case 3:
                                        oyunGrid[i, j].BackgroundImage = gokkusagiResmi;
                                        oyunGrid[i, j].Tag = "gokkusagi";
                                        break;
                                }
                            }
                            else
                            {
                                // Normal karakter ekle
                                oyunGrid[i, j].BackgroundImage = RastgeleKarakterResmiGetir(i, j);
                                oyunGrid[i, j].Tag = null; // Joker değil
                            }
                        }
                    }
                }
            }
        }








        private void PatlamaAnimasyonuGoster(List<Point> hedefKoordinatlar, Action tamamlandiginda)
        {
            // Patlama görselini göster
            foreach (var koordinat in hedefKoordinatlar)
            {
                int x = koordinat.X;
                int y = koordinat.Y;

                oyunGrid[y, x].BackgroundImage = Properties.Resources.patlama; // Patlama görseli
                oyunGrid[y, x].BackgroundImageLayout = ImageLayout.Stretch;
            }

            // Animasyon için bir süre bekle
            var animasyonTimer = new System.Windows.Forms.Timer();
            animasyonTimer.Interval = 500; // 500ms
            animasyonTimer.Tick += (s, e) =>
            {
                animasyonTimer.Stop();
                animasyonTimer.Dispose();

                // Patlama görselini temizle ve hücreleri boşalt
                foreach (var koordinat in hedefKoordinatlar)
                {
                    int x = koordinat.X;
                    int y = koordinat.Y;

                    oyunGrid[y, x].BackgroundImage = null; // Hücreyi boşalt
                    oyunGrid[y, x].Tag = null; // Joker bilgisi varsa kaldır
                }

                // Patlama sonrası işlem
                tamamlandiginda?.Invoke();
            };

            animasyonTimer.Start();
        }









        private void KarakterIslem(Button tiklananBtn)
        {
            if (ilkSecilenBtn == null)
            {
                ilkSecilenBtn = tiklananBtn;
                ilkSecilenBtn.FlatStyle = FlatStyle.Flat;
                ilkSecilenBtn.FlatAppearance.BorderColor = Color.Red;
                ilkSecilenBtn.FlatAppearance.BorderSize = 2;
            }
            else
            {
                if (YanYanaMi(ilkSecilenBtn, tiklananBtn))
                {
                    // Butonları yer değiştir
                    Image tempImage = ilkSecilenBtn.BackgroundImage;
                    ilkSecilenBtn.BackgroundImage = tiklananBtn.BackgroundImage;
                    tiklananBtn.BackgroundImage = tempImage;

                    // Eşleşmeleri kontrol et ve patlat
                    EslestirmeVePatlatma();
                }

                ilkSecilenBtn.FlatStyle = FlatStyle.Standard;
                ilkSecilenBtn = null;
            }
        }


        private void PuanGostericiOlustur()
        {
            lblPuan = new Label();
            lblPuan.Text = "Puan: 0";
            lblPuan.Font = new Font("Arial", 14, FontStyle.Bold);
            lblPuan.Location = new Point(10, 10);
            lblPuan.AutoSize = true;
            this.Controls.Add(lblPuan);
        }

        private void ZamanlayiciOlustur()
        {
            lblSure = new Label();
            lblSure.Text = "Süre: 60";
            lblSure.Font = new Font("Arial", 14, FontStyle.Bold);
            lblSure.Location = new Point(10, 40);
            lblSure.AutoSize = true;
            this.Controls.Add(lblSure);

            oyunTimer = new System.Windows.Forms.Timer();
            oyunTimer.Interval = 1000;
            oyunTimer.Tick += OyunTimer_Tick;
            oyunTimer.Start();
        }

        private void OyunTimer_Tick(object sender, EventArgs e)
        {
            if (kalanSure > 0)
            {
                kalanSure--;
                lblSure.Text = "Süre: " + kalanSure;
            }
            else
            {
                oyunTimer.Stop();

                // Skoru kaydet
                SkorYardimcisi.SkorKaydet(toplamPuan);

                // En iyi skorları göster
                string mesaj = "Süre doldu! Toplam Puanınız: " + toplamPuan + "\n\nEn İyi 5 Skor:\n";
                List<int> enIyiSkorlar = SkorYardimcisi.EnIyiSkorlariOku();

                for (int i = 0; i < Math.Min(5, enIyiSkorlar.Count); i++)
                {
                    mesaj += $"{i + 1}. {enIyiSkorlar[i]}\n";
                }

                MessageBox.Show(mesaj, "Oyun Bitti", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }


        private void PauseButonuOlustur()
        {
            btnPause = new Button();
            btnPause.Text = "Durdur";
            btnPause.Font = new Font("Arial", 12, FontStyle.Bold);
            btnPause.Size = new Size(100, 40);
            btnPause.Location = new Point(10, 70);
            btnPause.Click += BtnPause_Click;
            this.Controls.Add(btnPause);
        }

        private void BtnPause_Click(object sender, EventArgs e)
        {
            if (oyunTimer.Enabled)
            {
                // Oyun durduruluyor
                oyunTimer.Stop();
                btnPause.Text = "Devam Et";

                // Oyun gridindeki tüm butonları devre dışı bırak
                foreach (Button btn in oyunGrid)
                {
                    btn.Enabled = false;
                }
            }
            else
            {
                // Oyun devam ettiriliyor
                oyunTimer.Start();
                btnPause.Text = "Durdur";

                // Oyun gridindeki tüm butonları etkinleştir
                foreach (Button btn in oyunGrid)
                {
                    btn.Enabled = true;
                }
            }
        }


        private void RestartButonuOlustur()
        {
            btnRestart = new Button();
            btnRestart.Text = "Yeniden Başlat";
            btnRestart.Font = new Font("Arial", 12, FontStyle.Bold);
            btnRestart.Size = new Size(150, 40);
            btnRestart.Location = new Point(120, 70);
            btnRestart.Click += BtnRestart_Click;
            this.Controls.Add(btnRestart);
        }

        private void BtnRestart_Click(object sender, EventArgs e)
        {
            Form2 yeniForm = new Form2();
            yeniForm.Show();
            this.Close();
        }
    }
}