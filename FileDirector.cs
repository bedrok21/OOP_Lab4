namespace OOP_Lab4
{
    internal static class FileDirector
    {
        internal static string Path = string.Empty;

        internal static bool IsChanged = false;

        public static string Open()
        {
            OpenFileDialog openFileDialog = new()
            {
                Filter = "Json files (*.json)|*.json|Text files (*.txt)|*.txt"
            };

            var data = string.Empty;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Path = openFileDialog.FileName;
                data = File.ReadAllText(Path);
                IsChanged = false;
            }

            return data;
        }


        public static void Save(string data)
        {
            if (Path == string.Empty)
            {
                SaveAs(data);
            }
            else
            {
                File.WriteAllText(Path, data);
                IsChanged = false;
            }
        }


        public static void SaveAs(string data)
        {
            SaveFileDialog saveFileDialog = new()
            {
                Filter = "Json files (*.json)|*.json|Text files (*.txt)|*.txt"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Path = saveFileDialog.FileName;
                File.WriteAllText(Path, data);
                IsChanged = false;
            }
        }
    }
}
