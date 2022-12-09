namespace OOP_Lab4
{
    internal partial class EditForm : Form
    {
        private readonly Student student;

        public EditForm(Student obj)
        {
            student = obj;
            InitializeComponent();
            UpdateTextboxes();
        }

        private void UpdateTextboxes()
        {
            label8.Text = student.Id.ToString();
            textBox2.Text = student.FullName;
            dateTimePicker1.Value = student.BirthDate;
            textBox4.Text = student.Faculty;
            textBox5.Text = student.Year.ToString();
            textBox6.Text = student.StudentAdress;
            textBox7.Text = student.Room;
        }
        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                student.Id = int.Parse(label8.Text);
                student.FullName = textBox2.Text;
                student.BirthDate = dateTimePicker1.Value.Date;
                student.Faculty = textBox4.Text;
                student.Year = int.Parse(textBox5.Text);
                if (student.Year > 6 || student.Year < 1)
                {
                    throw new Exception();
                }
                student.StudentAdress = textBox6.Text;
                student.Room = textBox7.Text;
                DialogResult = DialogResult.OK;
            }
            catch
            {
                MessageBox.Show(
                "'Курс' приймає цілі значення від 1 до 6  ",
                "Помилка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1);
            }
        }
        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
