using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class AddUserForm : Form
    {
        private TextBox txtHoVaTen;
        private ComboBox cmbQuyenHan;
        private Button btnOK;
        private Button btnCancel;

        public string HoVaTen { get; private set; }
        public string QuyenHan { get; private set; }

        public AddUserForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Text = "Thêm người dùng mới";
            Size = new Size(400, 200);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            var lblHoVaTen = new Label
            {
                Text = "Họ và tên:",
                Location = new Point(20, 30),
                Size = new Size(100, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            txtHoVaTen = new TextBox
            {
                Location = new Point(130, 28),
                Size = new Size(220, 25),
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.FixedSingle
            };

            var lblQuyenHan = new Label
            {
                Text = "Quyền hạn:",
                Location = new Point(20, 70),
                Size = new Size(100, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            cmbQuyenHan = new ComboBox
            {
                Location = new Point(130, 68),
                Size = new Size(220, 25),
                Font = new Font("Segoe UI", 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbQuyenHan.Items.AddRange(new[] { "Admin", "User", "Moderator" });
            cmbQuyenHan.SelectedIndex = 1; // Mặc định chọn "User"

            btnOK = new Button
            {
                Text = "OK",
                Location = new Point(200, 120),
                Size = new Size(80, 35),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(76, 175, 80),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                DialogResult = DialogResult.OK
            };
            btnOK.FlatAppearance.BorderSize = 0;

            btnCancel = new Button
            {
                Text = "Hủy",
                Location = new Point(290, 120),
                Size = new Size(80, 35),
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(158, 158, 158),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                DialogResult = DialogResult.Cancel
            };
            btnCancel.FlatAppearance.BorderSize = 0;

            Controls.AddRange(new Control[] {
                lblHoVaTen, txtHoVaTen, lblQuyenHan, cmbQuyenHan, btnOK, btnCancel
            });

            btnOK.Click += BtnOK_Click;
            txtHoVaTen.KeyPress += TxtHoVaTen_KeyPress;
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHoVaTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ và tên", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoVaTen.Focus();
                return;
            }

            if (cmbQuyenHan.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn quyền hạn", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbQuyenHan.Focus();
                return;
            }

            HoVaTen = txtHoVaTen.Text.Trim();
            QuyenHan = cmbQuyenHan.SelectedItem.ToString();
        }

        private void TxtHoVaTen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                BtnOK_Click(sender, e);
                e.Handled = true;
            }
        }
    }
}
