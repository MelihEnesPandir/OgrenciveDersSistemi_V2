using static OgrenciveDersSistemi.base_class;
using static OgrenciveDersSistemi.interfacee;
using System.Xml.Linq;

namespace OgrenciveDersSistemi
{
    public class OgretimGorevlisi : base_class, ILogin
    {
        public override void DisplayInfo()
        {
            Console.WriteLine($"Öğretim Görevlisi ID: {Id}, Ad: {Name}, Email: {Email}");
        }

        public void Login()
        {
            Console.WriteLine($"{Name} adlı öğretim görevlisi sisteme giriş yaptı.");
        }
    }
}