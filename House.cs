namespace OOP_Lab4
{
    internal static class House
    {
        internal static List<Student> Students = new();

        internal static int idCounter = 1;

        public static void IdCount()
        {
            idCounter = 1;

            foreach (Student student in Students)
            {
                if (student.Id >= idCounter)
                {
                    idCounter = student.Id + 1;
                }
            }
        }
    }
}
