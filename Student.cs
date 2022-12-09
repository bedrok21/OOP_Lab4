namespace OOP_Lab4
{
    internal class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Faculty { get; set; }
        public int Year { get; set; }
        public string StudentAdress { get; set; }
        public string Room { get; set; }

        public Student()
        {
            Id = House.idCounter;
            FullName = string.Empty;
            Faculty = string.Empty;
            Year = 1;
            Room = string.Empty;
            StudentAdress = string.Empty;
            BirthDate = DateTime.Now;
        }
    }
}
