Marvel Karo Eşleştirme Oyunu
Bu proje, C# Windows Forms kullanılarak geliştirilmiş, dinamik ve etkileşimli bir "Match-3" (Eşleştirme) bulmaca oyunudur. Oyuncular, Marvel evreninden ikonik kahramanları eşleştirerek kısıtlı süre içerisinde en yüksek puanı toplamaya çalışır.

-Oyun Özellikleri-
Dinamik Grid Sistemi: Ekran boyutuna göre otomatik olarak yeniden boyutlanan 8x8 bir oyun alanı.

Özel Jokerler (Power-ups):

Roket: Bulunduğu tüm satırı ve sütunu temizler.

Bomba: Çevresindeki 8 karelik alanı patlatır.

Helikopter: Izgara üzerindeki rastgele bir öğeyi yok eder.

Gökkuşağı: Seçilen bir karakterin ekrandaki tüm kopyalarını temizler.

Skor ve Liderlik Tablosu: Skorlar yerel bir dosyaya (scores.txt) kaydedilir ve ilk 5 yüksek skor ana menüde görüntülenebilir.

Oyun Mekanikleri: Patlama animasyonları, boşluk doldurma mantığı, 60 saniyelik geri sayım, durdurma (pause) ve yeniden başlatma özellikleri.

-Teknik Detaylar-
Dil: C# (.NET 6.0 Windows).

Arayüz: Windows Forms ile özelleştirilmiş olay yönetimi (event handling).

Mantık: İç içe geçmiş eşleştirme algoritmaları, dinamik UI üretimi ve dosya tabanlı (File I/O) veri saklama.
