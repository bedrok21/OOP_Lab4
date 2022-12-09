using System.Text.Json;

namespace OOP_Lab4
{
    internal static class Deserializer
    {
        public static List<Student> Deserialize(string data)
        {
            List<Student> students = new();

            var deserializedData = JsonSerializer.Deserialize<List<Student>>(data);

            if (deserializedData != null)
            {
                students.AddRange(deserializedData);
            }

            return students;
        }
    }
}
