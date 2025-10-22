namespace WindowsFormsApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelStudentInfo;
        private System.Windows.Forms.Label labelMSSV;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelButtons;
        private System.Windows.Forms.Button[] exerciseButtons;
        private System.Windows.Forms.Button btnBai1;
        private System.Windows.Forms.Button btnBai2;
        private System.Windows.Forms.Button btnBai3;
        private System.Windows.Forms.Button btnBai4;
        private System.Windows.Forms.Button btnBai5;
        private System.Windows.Forms.Button btnBai6;
        private System.Windows.Forms.Button btnBai7;
        private System.Windows.Forms.Button btnBai8;
        private System.Windows.Forms.Button btnBai9;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelStudentInfo = new System.Windows.Forms.Label();
            this.labelMSSV = new System.Windows.Forms.Label();
            this.tableLayoutPanelButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnBai1 = new System.Windows.Forms.Button();
            this.btnBai2 = new System.Windows.Forms.Button();
            this.btnBai3 = new System.Windows.Forms.Button();
            this.btnBai4 = new System.Windows.Forms.Button();
            this.btnBai5 = new System.Windows.Forms.Button();
            this.btnBai6 = new System.Windows.Forms.Button();
            this.btnBai7 = new System.Windows.Forms.Button();
            this.btnBai8 = new System.Windows.Forms.Button();
            this.btnBai9 = new System.Windows.Forms.Button();

            // Setup form
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.MinimumSize = new System.Drawing.Size(600, 500);
            this.Name = "Form1";
            this.Text = "Lab1 - Main Menu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.White;
            
            // Thêm event để recenter khi resize
            this.Resize += new System.EventHandler(this.Form1_Resize);

            // Header Panel
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(32, 32, 32);
            this.panelHeader.Height = 100;
            this.panelHeader.Padding = new System.Windows.Forms.Padding(0);

            // Title Label
            this.labelTitle.AutoSize = false;
            this.labelTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelTitle.Size = new System.Drawing.Size(400, 40);
            this.labelTitle.Location = new System.Drawing.Point((1000 - 400) / 2, 15);
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelTitle.Text = "LAB 1 - BÀI TẬP";

            // Student Info Label
            this.labelStudentInfo.AutoSize = false;
            this.labelStudentInfo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelStudentInfo.Size = new System.Drawing.Size(300, 20);
            this.labelStudentInfo.Location = new System.Drawing.Point((1000 - 300) / 2, 55);
            this.labelStudentInfo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelStudentInfo.ForeColor = System.Drawing.Color.LightGray;
            this.labelStudentInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelStudentInfo.Text = "Họ và Tên: Hồ Hoàng Tiến";

            // MSSV Label
            this.labelMSSV.AutoSize = false;
            this.labelMSSV.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelMSSV.Size = new System.Drawing.Size(200, 20);
            this.labelMSSV.Location = new System.Drawing.Point((1000 - 200) / 2, 75);
            this.labelMSSV.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelMSSV.ForeColor = System.Drawing.Color.LightGray;
            this.labelMSSV.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelMSSV.Text = "MSSV: 24521762";

            // TableLayoutPanel for buttons - 2 hàng x 5 cột
            this.tableLayoutPanelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelButtons.Padding = new System.Windows.Forms.Padding(30, 30, 30, 30);
            this.tableLayoutPanelButtons.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanelButtons.ColumnCount = 5;
            this.tableLayoutPanelButtons.RowCount = 2;
            this.tableLayoutPanelButtons.ColumnStyles.Clear();
            this.tableLayoutPanelButtons.RowStyles.Clear();
            
            // Thiết lập kích thước cột đồng đều (5 cột)
            for (int i = 0; i < 5; i++)
            {
                this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            }
            
            // Thiết lập kích thước hàng đồng đều (2 hàng)
            for (int i = 0; i < 2; i++)
            {
                this.tableLayoutPanelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            }
            
            // Create 9 exercise buttons
            exerciseButtons = new System.Windows.Forms.Button[9];

            for (int i = 0; i < 9; i++)
            {
                exerciseButtons[i] = new System.Windows.Forms.Button();
                exerciseButtons[i].Dock = System.Windows.Forms.DockStyle.Fill;
                exerciseButtons[i].Margin = new System.Windows.Forms.Padding(10);
                exerciseButtons[i].Text = "Bài " + (i + 1);
                exerciseButtons[i].Name = "btnBai" + (i + 1);
                exerciseButtons[i].Font = new System.Drawing.Font("Segoe UI", 10F);
                exerciseButtons[i].FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                exerciseButtons[i].FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(220, 220, 220);
                exerciseButtons[i].FlatAppearance.BorderSize = 1;
                exerciseButtons[i].BackColor = System.Drawing.Color.White;
                exerciseButtons[i].ForeColor = System.Drawing.Color.Black;
                exerciseButtons[i].Cursor = System.Windows.Forms.Cursors.Hand;
                exerciseButtons[i].Tag = i + 1;
                exerciseButtons[i].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                // Add click event
                exerciseButtons[i].Click += new System.EventHandler(this.ExerciseButton_Click);

                // Add hover events
                exerciseButtons[i].MouseEnter += new System.EventHandler(this.Button_MouseEnter);
                exerciseButtons[i].MouseLeave += new System.EventHandler(this.Button_MouseLeave);

                // Add to table layout panel
                // Hàng đầu: 5 nút (Bài 1-5)
                // Hàng thứ hai: 4 nút (Bài 6-9)
                int row = i / 5;
                int col = i % 5;
                
                // Nếu là hàng thứ 2 (4 nút), căn giữa bằng cách bỏ qua cột 0
                if (row == 1)
                {
                    col = (i % 5) + 0; // Có thể thêm offset nếu muốn căn giữa hoàn toàn
                }
                
                this.tableLayoutPanelButtons.Controls.Add(exerciseButtons[i], col, row);
            }

            // Add controls to form
            this.panelHeader.Controls.Add(this.labelTitle);
            this.panelHeader.Controls.Add(this.labelStudentInfo);
            this.panelHeader.Controls.Add(this.labelMSSV);
            this.Controls.Add(this.tableLayoutPanelButtons);
            this.Controls.Add(this.panelHeader);

            this.ResumeLayout(false);
        }

        // Event để tự động căn giữa lại khi form được resize
        private void Form1_Resize(object sender, System.EventArgs e)
        {
            int formWidth = this.ClientSize.Width;
            
            this.labelTitle.Location = new System.Drawing.Point((formWidth - this.labelTitle.Width) / 2, 15);
            this.labelStudentInfo.Location = new System.Drawing.Point((formWidth - this.labelStudentInfo.Width) / 2, 55);
            this.labelMSSV.Location = new System.Drawing.Point((formWidth - this.labelMSSV.Width) / 2, 75);
        }

        private void Button_MouseEnter(object sender, System.EventArgs e)
        {
            System.Windows.Forms.Button btn = sender as System.Windows.Forms.Button;
            btn.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            btn.ForeColor = System.Drawing.Color.White;
        }

        private void Button_MouseLeave(object sender, System.EventArgs e)
        {
            System.Windows.Forms.Button btn = sender as System.Windows.Forms.Button;
            btn.BackColor = System.Drawing.Color.White;
            btn.ForeColor = System.Drawing.Color.Black;
        }

        private void ExerciseButton_Click(object sender, System.EventArgs e)
        {
            System.Windows.Forms.Button btn = sender as System.Windows.Forms.Button;
            int exerciseNumber = (int)btn.Tag;

            switch (exerciseNumber)
            {
                case 1:
                    using (var form = new Bai1Form())
                    {
                        form.ShowDialog();
                    }
                    break;
                case 2:
                    using (var form = new Bai2Form())
                    {
                        form.ShowDialog();
                    }
                    break;
                case 3:
                    using (var form = new Bai3Form())
                    {
                        form.ShowDialog();
                    }
                    break; 
                case 4:
                    using (var form = new Bai4Form())
                    {
                        form.ShowDialog();
                    }
                    break;
                case 5:
                    using (var form = new Bai5Form())
                    {
                        form.ShowDialog();
                    }
                    break; 
                case 6:
                    using (var form = new Bai6Form())
                    {
                        form.ShowDialog();
                    }
                    break; 
                case 7:
                    using (var form = new Bai7Form())
                    {
                        form.ShowDialog();
                    }
                    break;
                case 8:
                    using (var form = new Bai8Form())
                    {
                        form.ShowDialog();
                    }
                    break;
                case 9:
                    using (var form = new Bai9Form())
                    {
                        form.ShowDialog();
                    }
                    break;
                default:
                    System.Windows.Forms.MessageBox.Show($"Bài {exerciseNumber} đang được phát triển.",
                        "Thông báo", System.Windows.Forms.MessageBoxButtons.OK,
                        System.Windows.Forms.MessageBoxIcon.Information);
                    break;
            }
        }

        #endregion
    }
}