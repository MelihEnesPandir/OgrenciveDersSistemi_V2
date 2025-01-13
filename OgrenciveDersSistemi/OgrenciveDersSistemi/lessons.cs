namespace OgrenciveDersSistemi
{
    public class lessons
    {
        public string Name { get; set; }
        public int Credits { get; set; }
        public OgretimGorevlisi Instructor { get; set; }
        public List<Ogrenci> RegisteredStudents { get; set; } = new List<Ogrenci>();

        public void DisplayCourseInfo()
        {
            Console.WriteLine($"Ders Adı: {Name}, Kredi: {Credits}, Öğretim Görevlisi: {Instructor.Name}");

            if (RegisteredStudents.Count > 0)
            {
                Console.WriteLine("Kayıtlı Öğrenciler:");
                foreach (var student in RegisteredStudents)
                {
                    Console.WriteLine($"- {student.Name}");
                }
            }
            else
            {
                Console.WriteLine("Hiçbir öğrenci kaydolmamış.");
            }
        }

        public void RegisterStudent(Ogrenci student)
        {
            RegisteredStudents.Add(student);
            student.Courses.Add(this);
            Console.WriteLine($"{student.Name} adlı öğrenci {Name} dersine kaydoldu.");
        }
    }
}