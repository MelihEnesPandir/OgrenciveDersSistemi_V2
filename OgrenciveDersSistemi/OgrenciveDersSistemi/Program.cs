using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace OgrenciveDersSistemi
{
    class Program
    {
        static List<Ogrenci> ogrenciler = new List<Ogrenci>(); // Öğrenciler listesi
        static List<lessons> courses = new List<lessons>(); // Dersler listesi
        static string dataFilePath = "data.json"; // JSON dosya yolu

        static JsonSerializerSettings jsonSettings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore, // Döngüsel referansları göz ardı et
            Formatting = Formatting.Indented
        };

        static void Main(string[] args)
        {
            // Verileri yükle
            LoadData();

            // Menü
            int secim;
            do
            {
                Console.Clear(); // Ekranı temizle
                Console.WriteLine("Lütfen bir işlem seçin:");
                Console.WriteLine("1. Öğrenci Ekle");
                Console.WriteLine("2. Öğrenci Sil");
                Console.WriteLine("3. Dersleri ve Öğrencileri Görüntüle");
                Console.WriteLine("4. Ders Ekle");
                Console.WriteLine("5. Verileri Kaydet");
                Console.WriteLine("0. Çıkış");
                secim = int.Parse(Console.ReadLine());

                switch (secim)
                {
                    case 1:
                        OgrenciEkle();
                        break;
                    case 2:
                        OgrenciSil();
                        break;
                    case 3:
                        DersleriGoruntule();
                        break;
                    case 4:
                        DersEkle();
                        break;
                    case 5:
                        SaveData();
                        break;
                    case 0:
                        Console.WriteLine("Çıkılıyor...");
                        SaveData(); // Çıkarken verileri kaydet
                        break;
                    default:
                        Console.WriteLine("Geçersiz seçenek, tekrar deneyin.");
                        break;
                }

                if (secim != 0)
                {
                    Console.WriteLine("Devam etmek için bir tuşa basın...");
                    Console.ReadKey(); // Kullanıcının devam etmesi için tuşa basmasını bekleyin
                }
            } while (secim != 0);
        }

        static void OgrenciEkle()
        {
            Console.Clear();
            Console.WriteLine("Öğrenci bilgilerini giriniz:");
            Console.Write("Ad: ");
            string ad = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            var ogrenci = new Ogrenci
            {
                Name = ad,
                Email = email
            };
            ogrenciler.Add(ogrenci);

            Console.WriteLine("Ders seçiniz:");

            for (int i = 0; i < courses.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {courses[i].Name}");
            }

            int dersSecimi = int.Parse(Console.ReadLine()) - 1;
            if (dersSecimi >= 0 && dersSecimi < courses.Count)
            {
                courses[dersSecimi].RegisterStudent(ogrenci);
            }
            else
            {
                Console.WriteLine("Geçersiz ders seçimi.");
            }

            SaveData(); // Öğrenci eklenir eklenmez veriler kaydedilir.
        }

        static void OgrenciSil()
        {
            Console.Clear();
            Console.WriteLine("Silmek istediğiniz öğrenci ID'sini giriniz:");
            int ogrenciId = int.Parse(Console.ReadLine());

            var ogrenci = ogrenciler.FirstOrDefault(o => o.Id == ogrenciId);
            if (ogrenci != null)
            {
                ogrenciler.Remove(ogrenci);
                Console.WriteLine($"Öğrenci {ogrenci.Name} başarıyla silindi.");
            }
            else
            {
                Console.WriteLine("Öğrenci bulunamadı.");
            }
        }

        static void DersleriGoruntule()
        {
            Console.Clear();
            Console.WriteLine("Dersler ve Kayıtlı Öğrenciler:");

            foreach (var course in courses)
            {
                course.DisplayCourseInfo();
            }
        }

        static void DersEkle()
        {
            Console.Clear();
            Console.WriteLine("Yeni Ders Ekle:");
            Console.Write("Ders Adı: ");
            string dersAdi = Console.ReadLine();

            Console.Write("Kredi: ");
            int kredi = int.Parse(Console.ReadLine());

            Console.Write("Öğretim Görevlisi Adı: ");
            string ogretimGorevlisiAdi = Console.ReadLine();

            var ogretimGorevlisi = new OgretimGorevlisi { Name = ogretimGorevlisiAdi };

            var yeniDers = new lessons
            {
                Name = dersAdi,
                Credits = kredi,
                Instructor = ogretimGorevlisi
            };

            courses.Add(yeniDers);
            Console.WriteLine($"{dersAdi} dersi başarıyla eklendi.");

            SaveData(); // Ders eklenir eklenmez veriler kaydedilir.
        }

        static void SaveData()
        {
            var data = new
            {
                Ogrenciler = ogrenciler,
                Dersler = courses
            };

            string json = JsonConvert.SerializeObject(data, jsonSettings);
            File.WriteAllText(dataFilePath, json);
            Console.WriteLine("Veriler başarıyla kaydedildi.");
        }

        static void LoadData()
        {
            if (File.Exists(dataFilePath))
            {
                string json = File.ReadAllText(dataFilePath);
                var data = JsonConvert.DeserializeObject<dynamic>(json);

                ogrenciler = JsonConvert.DeserializeObject<List<Ogrenci>>(Convert.ToString(data.Ogrenciler), jsonSettings);
                courses = JsonConvert.DeserializeObject<List<lessons>>(Convert.ToString(data.Dersler), jsonSettings);

                Console.WriteLine("Veriler başarıyla yüklendi.");
            }
            else
            {
                Console.WriteLine("Kaydedilmiş veri bulunamadı.");
            }
        }
    }
}
