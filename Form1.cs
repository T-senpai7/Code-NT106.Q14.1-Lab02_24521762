using System;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetupEventHandlers();
        }

        private void SetupEventHandlers()
        {
            // Các event handlers đã được thiết lập trong Form1.Designer.cs
            // thông qua ExerciseButton_Click method
        }

        private void OpenForm(string formName)
        {
            Form form = null;
            switch (formName)
            {
                case "Lab01-Bai01":
                    // Create and show form for Exercise 1
                    MessageBox.Show("Will open " + formName);
                    break;
                case "Lab01-Bai02":
                    // Create and show form for Exercise 2
                    MessageBox.Show("Will open " + formName);
                    break;
                case "Lab01-Bai03":
                    // Create and show form for Exercise 3
                    MessageBox.Show("Will open " + formName);
                    break;
                case "Lab01-Bai04":
                    // Create and show form for Exercise 4
                    MessageBox.Show("Will open " + formName);
                    break;
                case "Lab01-Bai05":
                    // Create and show form for Exercise 5
                    MessageBox.Show("Will open " + formName);
                    break;
                case "Lab01-Bai06":
                    // Create and show form for Exercise 6
                    MessageBox.Show("Will open " + formName);
                    break;
                case "Lab01-Bai07":
                    // Create and show form for Exercise 7
                    MessageBox.Show("Will open " + formName);
                    break;
                case "Lab01-Bai08":
                    // Create and show form for Exercise 8
                    MessageBox.Show("Will open " + formName);
                    break;
                case "Lab01-Bai09":
                    // Create and show form for Exercise 9
                    MessageBox.Show("Will open " + formName);
                    break;
            }

            if (form != null)
            {
                form.ShowDialog();
            }
        }
    }
}