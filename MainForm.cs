using System.Data;

namespace OOP_Lab4
{
    public partial class MainForm : Form
    {
        private readonly DataTable table = new();

        private int findIter = 0;

        public MainForm()
        {
            InitializeComponent();
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("ПІП", typeof(string));
            table.Columns.Add("Дата нар", typeof(DateTime));
            table.Columns.Add("Факультет", typeof(string));
            table.Columns.Add("Курс", typeof(int));
            table.Columns.Add("Адреса", typeof(string));
            table.Columns.Add("Кімната", typeof(string));

            dataGridView1.DataSource = table;
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].Width = 300;
            dataGridView1.Columns[2].Width = 200;
            dataGridView1.Columns[3].Width = 200;
            dataGridView1.Columns[4].Width = 50;
            dataGridView1.Columns[5].Width = 300;
            dataGridView1.Columns[6].Width = 73;

            for (int i = 0; i < 7; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dataGridView1.ReadOnly = true;
            
        }

        public void Update(object sender, EventArgs e)
        {
            table.Clear();

            foreach (var student in House.Students)
            {
                table.Rows.Add(student.Id,
                               student.FullName,
                               student.BirthDate,
                               student.Faculty,
                               student.Year,
                               student.StudentAdress,
                               student.Room);
            }
            House.IdCount();
        }


        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var data = FileDirector.Open();

                if (data != string.Empty)
                {
                    House.Students = Deserializer.Deserialize(data);
                    Update(sender, e);
                }
            }
            catch
            {
                MessageBox.Show(
                "Невірний формат данних...  ",
                "Помилка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1);
            }
        }


        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var data = Serializer.Serialize(House.Students);

            FileDirector.Save(data);
        }


        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var data = Serializer.Serialize(House.Students);

            FileDirector.SaveAs(data);
        }


        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FileDirector.IsChanged == true)
            {
                using var saveDialogForm = new SaveDialogForm();
                saveDialogForm.Text = "Дані не збережено";
                saveDialogForm.Owner = this;
                saveDialogForm.ShowDialog();

                if (saveDialogForm.DialogResult == DialogResult.OK)
                {
                    House.Students.Clear();
                    Update(sender, e);
                }

                if (saveDialogForm.DialogResult == DialogResult.Yes)
                {
                    SaveToolStripMenuItem_Click(sender, e);
                    House.Students.Clear();
                    Update(sender, e);
                }
            }
            else
            {
                House.Students.Clear();
                Update(sender, e);
            }
        }


        private void InfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var infoForm = new InfoForm();
            infoForm.Text = "Про програму";
            infoForm.Owner = this;
            infoForm.ShowDialog();
        }


        private void AddButton_Click(object sender, EventArgs e)
        {
            var obj = new Student();

            using var editForm = new EditForm(obj);
            editForm.Text = "Додавання";
            editForm.Owner = this;
            editForm.ShowDialog();

            if (editForm.DialogResult == DialogResult.OK)
            {
                House.Students.Add(obj);
                Update(sender, e);
                FileDirector.IsChanged = true;
            }
        }
        


        private void EditButton_Click(object sender, EventArgs e)
        {
            try
            {
                int index = dataGridView1.CurrentCell.RowIndex;

                var obj = House.Students[index];

                using var editForm = new EditForm(obj);
                editForm.Text = "Редагування";
                editForm.Owner = this;
                editForm.ShowDialog();
                if (editForm.DialogResult == DialogResult.OK)
                {
                    Update(sender, e);
                    FileDirector.IsChanged = true;
                }
            }
            catch
            {
                AddButton_Click(sender, e);
            }
        }


        private void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                int index = dataGridView1.CurrentCell.RowIndex;

                House.Students.RemoveAt(index);

                Update(sender, e);

                FileDirector.IsChanged = true;
            }
            catch
            {
                MessageBox.Show(
                "Неможливо видалити данні із пустого списку...  ",
                "Помилка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1);
            }

        }

        private void FindButton_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Selected = false;
                }
                dataGridView1.Rows[Helper.FoundData[findIter]].Selected = false;
                findIter = 0;
                Helper.Search(textBox1.Text);
                dataGridView1.Rows[Helper.FoundData[findIter]].Selected = true;
                dataGridView1.FirstDisplayedScrollingRowIndex = Helper.FoundData[findIter];
            }
            catch
            {
                MessageBox.Show(
                "Не вдалось знайти...  ",
                "Результат",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1);
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Selected = false;
                }

                if (textBox1.Text != string.Empty) 
                { 
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        dataGridView1.Rows[i].Selected = false;
                    }
                    findIter = 0;
                    Helper.Search(textBox1.Text);
                    dataGridView1.Rows[Helper.FoundData[findIter]].Selected = true;
                }
            }
            catch
            {
            }
        }

        private void SearchNextButton_Click(object sender, EventArgs e)
        {
            var foundData = Helper.FoundData;

            if (foundData.Count > findIter + 1)
            {
                dataGridView1.Rows[foundData[findIter]].Selected = false;
                findIter++;
                dataGridView1.Rows[foundData[findIter]].Selected = true;
            }
        }
        private void SearchPrevButton_Click(object sender, EventArgs e)
        {
            var foundData = Helper.FoundData;

            if (findIter - 1 >= 0)
            {
                dataGridView1.Rows[foundData[findIter]].Selected = false;
                findIter--;
                dataGridView1.Rows[foundData[findIter]].Selected = true;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FileDirector.IsChanged == true)
            {
                using var saveDialogForm = new SaveDialogForm();
                saveDialogForm.Text = "Дані не збережено";
                saveDialogForm.Owner = this;
                saveDialogForm.ShowDialog();

                if (saveDialogForm.DialogResult == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }

                if (saveDialogForm.DialogResult == DialogResult.Yes)
                {
                    SaveToolStripMenuItem_Click(sender, e);
                }
            }
        }
    }
}