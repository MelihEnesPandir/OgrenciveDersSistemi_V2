namespace OgrenciveDersSistemi
{
    public abstract class base_class
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public abstract void DisplayInfo();
    }
}