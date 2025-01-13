using Newtonsoft.Json;
using System.IO;
namespace OgrenciveDersSistemi
{
    public class CourseManager
    {
        private List<lessons> courses = new List<lessons>();

        public void AddCourse(lessons course)
        {
            courses.Add(course);
            Console.WriteLine($"{course.Name} dersi sisteme eklendi.");
        }

        public void ListCourses()
        {
            Console.WriteLine("Mevcut Dersler:");
            foreach (var course in courses)
            {
                Console.WriteLine($"- {course.Name} (Kredi: {course.Credits})");
            }
        }

        public void SaveCourses(string filePath)
        {
            var json = JsonConvert.SerializeObject(courses, Formatting.Indented);
            File.WriteAllText(filePath, json);
            Console.WriteLine("Ders bilgileri başarıyla kaydedildi.");
        }

        public void LoadCourses(string filePath)
        {
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                courses = JsonConvert.DeserializeObject<List<lessons>>(json);
                Console.WriteLine("Ders bilgileri başarıyla yüklendi.");
            }
            else
            {
                Console.WriteLine("Dosya bulunamadı.");
            }
        }
    }
}