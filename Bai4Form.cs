using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;

namespace WindowsFormsApp
{
    // Student data model
    public class Student
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public string Phone { get; set; }
        public float Course1 { get; set; }
        public float Course2 { get; set; }
        public float Course3 { get; set; }
        public float Average { get; set; }

        public void CalculateAverage()
        {
            Average = (Course1 + Course2 + Course3) / 3;
        }
    }

    public class Bai4Form : Form
    {
    // Controls for the write and read panel
        private Panel pnlWrite;
        private Label lblWriteTitle;
        private TextBox txtName, txtID, txtPhone, txtCourse1, txtCourse2, txtCourse3;
        private Label lblName, lblID, lblPhone, lblCourse1Label, lblCourse2Label, lblCourse3Label;
        private Button btnAdd;
        private ListBox lstStudents;

    
        private Panel pnlRead;
        private Label lblReadTitle;
        private TextBox txtDisplayName, txtDisplayID, txtDisplayPhone;
        private TextBox txtDisplayCourse1, txtDisplayCourse2, txtDisplayCourse3, txtDisplayAverage;
        private Label lblDisplayName, lblDisplayID, lblDisplayPhone;
        private Label lblDisplayCourse1, lblDisplayCourse2, lblDisplayCourse3, lblDisplayAverage;
        private Button btnBack, btnNext;
        private Label lblPageInfo;


        private Button btnWriteToFile, btnReadFromFile;

    // Data storage
        private List<Student> students = new List<Student>();
        private List<Student> loadedStudents = new List<Student>();
        private int currentIndex = 0;

        private const string INPUT_FILE = "input4.txt";
        private const string OUTPUT_FILE = "output4.txt";

        public Bai4Form()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Text = "Bài 4";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(1000, 650);
            BackColor = Color.FromArgb(240, 240, 240);

            
            pnlWrite = new Panel
            {
                Location = new Point(20, 20),
                Size = new Size(450, 580),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            lblWriteTitle = new Label
            {
                Text = "WRITE TO FILE",
                Location = new Point(10, 10),
                Size = new Size(430, 30),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 150, 243),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Name field
            lblName = new Label
            {
                Text = "Name",
                Location = new Point(250, 60),
                Size = new Size(180, 20),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtName = new TextBox
            {
                Location = new Point(30, 60),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10)
            };

            // ID field
            lblID = new Label
            {
                Text = "ID",
                Location = new Point(250, 100),
                Size = new Size(180, 20),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtID = new TextBox
            {
                Location = new Point(30, 100),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10)
            };

            // Phone field
            lblPhone = new Label
            {
                Text = "Phone",
                Location = new Point(250, 140),
                Size = new Size(180, 20),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtPhone = new TextBox
            {
                Location = new Point(30, 140),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10)
            };

            // Course 1 field
            lblCourse1Label = new Label
            {
                Text = "Course 1",
                Location = new Point(250, 180),
                Size = new Size(180, 20),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtCourse1 = new TextBox
            {
                Location = new Point(30, 180),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10)
            };

            // Course 2 field
            lblCourse2Label = new Label
            {
                Text = "Course 2",
                Location = new Point(250, 220),
                Size = new Size(180, 20),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtCourse2 = new TextBox
            {
                Location = new Point(30, 220),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10)
            };

            // Course 3 field
            lblCourse3Label = new Label
            {
                Text = "Course 3",
                Location = new Point(250, 260),
                Size = new Size(180, 20),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtCourse3 = new TextBox
            {
                Location = new Point(30, 260),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10)
            };

            // Add button
            btnAdd = new Button
            {
                Text = "Add",
                Location = new Point(150, 300),
                Size = new Size(150, 40),
                BackColor = Color.FromArgb(76, 175, 80),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.Click += BtnAdd_Click;

            // ListBox showing added students
            lstStudents = new ListBox
            {
                Location = new Point(30, 350),
                Size = new Size(390, 180),
                Font = new Font("Consolas", 9),
                BackColor = Color.FromArgb(250, 250, 250)
            };

            // Button Write to File
            btnWriteToFile = new Button
            {
                Text = "Write to a File",
                Location = new Point(30, 540),
                Size = new Size(390, 35),
                BackColor = Color.FromArgb(33, 150, 243),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnWriteToFile.FlatAppearance.BorderSize = 0;
            btnWriteToFile.Click += BtnWriteToFile_Click;

            // Add controls to Write Panel
            pnlWrite.Controls.AddRange(new Control[] {
                lblWriteTitle, lblName, txtName, lblID, txtID, lblPhone, txtPhone,
                lblCourse1Label, txtCourse1, lblCourse2Label, txtCourse2,
                lblCourse3Label, txtCourse3, btnAdd, lstStudents, btnWriteToFile
            });

            // ===== PANEL READ (RIGHT) =====
            pnlRead = new Panel
            {
                Location = new Point(490, 20),
                Size = new Size(480, 580),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            lblReadTitle = new Label
            {
                Text = "READ FROM FILE",
                Location = new Point(10, 10),
                Size = new Size(460, 30),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(244, 67, 54),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Button: Read from file
            btnReadFromFile = new Button
            {
                Text = "Button to read a File",
                Location = new Point(45, 60),
                Size = new Size(390, 40),
                BackColor = Color.FromArgb(33, 150, 243),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnReadFromFile.FlatAppearance.BorderSize = 0;
            btnReadFromFile.Click += BtnReadFromFile_Click;

            // Display Name
            lblDisplayName = new Label
            {
                Text = "Name",
                Location = new Point(360, 120),
                Size = new Size(100, 20),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtDisplayName = new TextBox
            {
                Location = new Point(45, 120),
                Size = new Size(300, 25),
                Font = new Font("Segoe UI", 10),
                ReadOnly = true,
                BackColor = Color.White
            };

            // Display ID
            lblDisplayID = new Label
            {
                Text = "ID",
                Location = new Point(360, 160),
                Size = new Size(100, 20),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtDisplayID = new TextBox
            {
                Location = new Point(45, 160),
                Size = new Size(300, 25),
                Font = new Font("Segoe UI", 10),
                ReadOnly = true,
                BackColor = Color.White
            };

            // Display Phone
            lblDisplayPhone = new Label
            {
                Text = "Phone",
                Location = new Point(360, 200),
                Size = new Size(100, 20),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtDisplayPhone = new TextBox
            {
                Location = new Point(45, 200),
                Size = new Size(300, 25),
                Font = new Font("Segoe UI", 10),
                ReadOnly = true,
                BackColor = Color.White
            };

            // Display Course
            lblDisplayCourse1 = new Label
            {
                Text = "Course 1",
                Location = new Point(360, 240),
                Size = new Size(100, 20),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtDisplayCourse1 = new TextBox
            {
                Location = new Point(45, 240),
                Size = new Size(300, 25),
                Font = new Font("Segoe UI", 10),
                ReadOnly = true,
                BackColor = Color.White
            };

        
            lblDisplayCourse2 = new Label
            {
                Text = "Course 2",
                Location = new Point(360, 280),
                Size = new Size(100, 20),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtDisplayCourse2 = new TextBox
            {
                Location = new Point(45, 280),
                Size = new Size(300, 25),
                Font = new Font("Segoe UI", 10),
                ReadOnly = true,
                BackColor = Color.White
            };

           
            lblDisplayCourse3 = new Label
            {
                Text = "Course 3",
                Location = new Point(360, 320),
                Size = new Size(100, 20),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtDisplayCourse3 = new TextBox
            {
                Location = new Point(45, 320),
                Size = new Size(300, 25),
                Font = new Font("Segoe UI", 10),
                ReadOnly = true,
                BackColor = Color.White
            };

            // Display Average
            lblDisplayAverage = new Label
            {
                Text = "Average",
                Location = new Point(360, 360),
                Size = new Size(100, 20),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtDisplayAverage = new TextBox
            {
                Location = new Point(45, 360),
                Size = new Size(300, 25),
                Font = new Font("Segoe UI", 10),
                ReadOnly = true,
                BackColor = Color.LightYellow
            };

            
            btnBack = new Button
            {
                Text = "Back",
                Location = new Point(45, 520),
                Size = new Size(120, 40),
                BackColor = Color.FromArgb(158, 158, 158),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.Click += BtnBack_Click;

            lblPageInfo = new Label
            {
                Text = "1",
                Location = new Point(175, 520),
                Size = new Size(130, 40),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(240, 240, 240)
            };

            btnNext = new Button
            {
                Text = "Next",
                Location = new Point(315, 520),
                Size = new Size(120, 40),
                BackColor = Color.FromArgb(158, 158, 158),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnNext.FlatAppearance.BorderSize = 0;
            btnNext.Click += BtnNext_Click;

            
            pnlRead.Controls.AddRange(new Control[] {
                lblReadTitle, btnReadFromFile,
                lblDisplayName, txtDisplayName, lblDisplayID, txtDisplayID,
                lblDisplayPhone, txtDisplayPhone, lblDisplayCourse1, txtDisplayCourse1,
                lblDisplayCourse2, txtDisplayCourse2, lblDisplayCourse3, txtDisplayCourse3,
                lblDisplayAverage, txtDisplayAverage,
                btnBack, lblPageInfo, btnNext
            });

            
            Controls.Add(pnlWrite);
            Controls.Add(pnlRead);
        }

        

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate name
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên sinh viên!", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtName.Focus();
                    return;
                }

                // Validate ID: must be an 8-digit integer
                if (!int.TryParse(txtID.Text, out int id) || txtID.Text.Length != 8)
                {
                    MessageBox.Show("MSSV phải là số có 8 chữ số!", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtID.Focus();
                    return;
                }

                // Validate phone: 10 digits and starts with 0
                if (txtPhone.Text.Length != 10 || !txtPhone.Text.StartsWith("0") || !txtPhone.Text.All(char.IsDigit))
                {
                    MessageBox.Show("Số điện thoại phải có 10 chữ số và bắt đầu bằng số 0!", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPhone.Focus();
                    return;
                }

                // Validate course score (0-10)
                if (!float.TryParse(txtCourse1.Text, out float course1) || course1 < 0 || course1 > 10)
                {
                    MessageBox.Show("Điểm môn 1 phải là số từ 0 đến 10!", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCourse1.Focus();
                    return;
                }

                
                if (!float.TryParse(txtCourse2.Text, out float course2) || course2 < 0 || course2 > 10)
                {
                    MessageBox.Show("Điểm môn 2 phải là số từ 0 đến 10!", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCourse2.Focus();
                    return;
                }

                if (!float.TryParse(txtCourse3.Text, out float course3) || course3 < 0 || course3 > 10)
                {
                    MessageBox.Show("Điểm môn 3 phải là số từ 0 đến 10!", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCourse3.Focus();
                    return;
                }

                
                Student student = new Student
                {
                    Name = txtName.Text.Trim(),
                    ID = id,
                    Phone = txtPhone.Text.Trim(),
                    Course1 = course1,
                    Course2 = course2,
                    Course3 = course3
                };

                // Add to list
                students.Add(student);

                // Add to ListBox
                lstStudents.Items.Add($"{student.Name} - ID: {student.ID}");

                // Clear input 
                txtName.Clear();
                txtID.Clear();
                txtPhone.Clear();
                txtCourse1.Clear();
                txtCourse2.Clear();
                txtCourse3.Clear();
                txtName.Focus();

                MessageBox.Show("Đã thêm sinh viên thành công!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnWriteToFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (students.Count == 0)
                {
                    MessageBox.Show("Chưa có sinh viên nào để ghi!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Serialize to JSON
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(students, options);

                // Write to file
                File.WriteAllText(INPUT_FILE, jsonString);

                MessageBox.Show($"Đã ghi {students.Count} sinh viên vào file {INPUT_FILE}!", "Thành công", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi ghi file: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnReadFromFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(INPUT_FILE))
                {
                    MessageBox.Show($"File {INPUT_FILE} không tồn tại!", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                
                string jsonString = File.ReadAllText(INPUT_FILE);
                loadedStudents = JsonSerializer.Deserialize<List<Student>>(jsonString);

                if (loadedStudents == null || loadedStudents.Count == 0)
                {
                    MessageBox.Show("File không có dữ liệu!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Calculate average for each student
                foreach (var student in loadedStudents)
                {
                    student.CalculateAverage();
                }

                // Write to output file
                var options = new JsonSerializerOptions { WriteIndented = true };
                string outputJson = JsonSerializer.Serialize(loadedStudents, options);
                File.WriteAllText(OUTPUT_FILE, outputJson);

                // Display first student
                currentIndex = 0;
                DisplayStudent(currentIndex);

                MessageBox.Show($"Đã đọc {loadedStudents.Count} sinh viên và ghi vào {OUTPUT_FILE}!", 
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đọc file: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayStudent(int index)
        {
            if (loadedStudents == null || loadedStudents.Count == 0)
            {
                ClearDisplay();
                return;
            }

            if (index < 0 || index >= loadedStudents.Count)
                return;

            Student student = loadedStudents[index];
            txtDisplayName.Text = student.Name;
            txtDisplayID.Text = student.ID.ToString();
            txtDisplayPhone.Text = student.Phone;
            txtDisplayCourse1.Text = student.Course1.ToString("0.##");
            txtDisplayCourse2.Text = student.Course2.ToString("0.##");
            txtDisplayCourse3.Text = student.Course3.ToString("0.##");
            txtDisplayAverage.Text = student.Average.ToString("0.##");

            lblPageInfo.Text = $"{index + 1}";
        }

        private void ClearDisplay()
        {
            txtDisplayName.Clear();
            txtDisplayID.Clear();
            txtDisplayPhone.Clear();
            txtDisplayCourse1.Clear();
            txtDisplayCourse2.Clear();
            txtDisplayCourse3.Clear();
            txtDisplayAverage.Clear();
            lblPageInfo.Text = "0";
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            if (loadedStudents == null || loadedStudents.Count == 0)
            {
                MessageBox.Show("Chưa có dữ liệu để hiển thị!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (currentIndex > 0)
            {
                currentIndex--;
                DisplayStudent(currentIndex);
            }
            else
            {
                MessageBox.Show("Đây là sinh viên đầu tiên!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (loadedStudents == null || loadedStudents.Count == 0)
            {
                MessageBox.Show("Chưa có dữ liệu để hiển thị!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (currentIndex < loadedStudents.Count - 1)
            {
                currentIndex++;
                DisplayStudent(currentIndex);
            }
            else
            {
                MessageBox.Show("Đây là sinh viên cuối cùng!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
