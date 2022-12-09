using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace OOP_Lab4
{
    internal static class Serializer
    {
        public static string Serialize(List<Student> students)
        {
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };

            string data = JsonSerializer.Serialize(students, options).ToString();

            return data;
        }
    }
}
