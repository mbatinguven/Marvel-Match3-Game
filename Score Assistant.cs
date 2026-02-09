using System;
using System.Collections.Generic;
using System.IO;

namespace Ndp_KaroOyunu
{
    public static class SkorYardimcisi
    {
        // Skorları kaydedeceğimiz dosya yolu
        private static readonly string filePath = "scores.txt";

        /// <summary>
        /// Bir skoru dosyaya kaydeder.
        /// </summary>
        /// <param name="puan">Kaydedilecek skor.</param>
        public static void SkorKaydet(int puan)
        {
            try
            {
                // Dosya yoksa oluştur ve skoru yaz
                using (StreamWriter sw = new StreamWriter(filePath, true))
                {
                    sw.WriteLine(puan);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: Skor kaydedilemedi. {ex.Message}");
            }
        }

        /// <summary>
        /// En iyi skorları dosyadan okur.
        /// </summary>
        /// <returns>En iyi skorların bir listesi.</returns>
        public static List<int> EnIyiSkorlariOku()
        {
            List<int> skorlar = new List<int>();

            try
            {
                if (File.Exists(filePath))
                {
                    string[] lines = File.ReadAllLines(filePath);

                    foreach (string line in lines)
                    {
                        if (int.TryParse(line, out int skor))
                        {
                            skorlar.Add(skor);
                        }
                    }

                    // Skorları büyükten küçüğe sıralayın
                    skorlar.Sort((a, b) => b.CompareTo(a));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: Skorlar okunamadı. {ex.Message}");
            }

            return skorlar;
        }
    }
}
