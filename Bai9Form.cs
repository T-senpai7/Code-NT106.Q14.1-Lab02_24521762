using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public class Bai9Form : Form
    {
        private Label lblMessage;

        public Bai9Form()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Text = "Bài 9 - LAB 2";
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(500, 250);
            BackColor = Color.White;

            lblMessage = new Label
            {
                Text = "Hiện tại không có bài 9 trong phần bài tập thực hành LAB 2.",
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.DarkRed,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };

            Controls.Add(lblMessage);
        }
    }
}
