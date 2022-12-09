namespace OOP_Lab4
{
    internal static class Helper
    {
        public static List<int> FoundData = new();

        public static void Search(string element)
        {
            var query = from n in House.Students
                        where n.FullName.ToLower().Contains(element.ToLower())
                        select House.Students.IndexOf(n);

            var query2 = from n in House.Students
                         where n.Room.ToLower().Contains(element.ToLower())
                         select House.Students.IndexOf(n);

            var query3 = from n in House.Students
                         where n.Faculty.ToLower().Contains(element.ToLower())
                         select House.Students.IndexOf(n);

            query = query.Union(query2);
            query = query.Union(query3);
            
            FoundData = query.ToList();
            FoundData.Remove(-1);
        }
    }
}
