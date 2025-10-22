using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace WindowsFormsApp
{
    public class Bai3Form : Form
    {
        private Label lblInput;
        private TextBox txtInput;
        private Button btnChonFile;
        private Button btnDoc;
        private Button btnXoa;
        private Button btnThoat;
        private Label lblResult;
        private TextBox txtResult;
        private Label lblFilePath;
        private TextBox txtFilePath;
        private string selectedFilePath = "";

        public Bai3Form()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Text = "Bai3: Đọc và Ghi file và tính toán";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(550, 500);
            
            // Label và TextBox hiển thị đường dẫn file
            lblFilePath = new Label();
            lblFilePath.Text = "Đường dẫn file:";
            lblFilePath.Location = new Point(20, 20);
            lblFilePath.Size = new Size(100, 20);
            lblFilePath.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            
            txtFilePath = new TextBox();
            txtFilePath.Location = new Point(120, 18);
            txtFilePath.Size = new Size(300, 25);
            txtFilePath.ReadOnly = true;
            txtFilePath.BackColor = Color.White;
            txtFilePath.Font = new Font("Segoe UI", 9F);
            
            // Button chọn file
            btnChonFile = new Button();
            btnChonFile.Text = "Chọn File";
            btnChonFile.Location = new Point(430, 15);
            btnChonFile.Size = new Size(90, 30);
            btnChonFile.BackColor = Color.FromArgb(33, 150, 243);
            btnChonFile.ForeColor = Color.White;
            btnChonFile.FlatStyle = FlatStyle.Flat;
            btnChonFile.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnChonFile.Cursor = Cursors.Hand;
            btnChonFile.Click += BtnChonFile_Click;
            
            lblInput = new Label();
            lblInput.Text = "Nội dung File Input";
            lblInput.Location = new Point(20, 60);
            lblInput.Size = new Size(180, 20);
            lblInput.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            
            txtInput = new TextBox();
            txtInput.Location = new Point(20, 85);
            txtInput.Size = new Size(500, 120);
            txtInput.Multiline = true;
            txtInput.ScrollBars = ScrollBars.Both;
            txtInput.Font = new Font("Consolas", 10F);
            txtInput.BackColor = Color.FromArgb(250, 250, 250);
            
            btnDoc = new Button();
            btnDoc.Text = "Đọc và Tính";
            btnDoc.Location = new Point(20, 215);
            btnDoc.Size = new Size(120, 40);
            btnDoc.BackColor = Color.FromArgb(76, 175, 80);
            btnDoc.ForeColor = Color.White;
            btnDoc.FlatStyle = FlatStyle.Flat;
            btnDoc.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnDoc.Cursor = Cursors.Hand;
            btnDoc.Click += BtnDoc_Click;
            
            btnDoc.MouseEnter += (s, e) => btnDoc.BackColor = Color.FromArgb(67, 160, 71);
            btnDoc.MouseLeave += (s, e) => btnDoc.BackColor = Color.FromArgb(76, 175, 80);
            
            btnXoa = new Button();
            btnXoa.Text = "Xóa";
            btnXoa.Location = new Point(150, 215);
            btnXoa.Size = new Size(120, 40);
            btnXoa.BackColor = Color.FromArgb(255, 152, 0);
            btnXoa.ForeColor = Color.White;
            btnXoa.FlatStyle = FlatStyle.Flat;
            btnXoa.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnXoa.Cursor = Cursors.Hand;
            btnXoa.Click += BtnXoa_Click;
            
            btnXoa.MouseEnter += (s, e) => btnXoa.BackColor = Color.FromArgb(245, 124, 0);
            btnXoa.MouseLeave += (s, e) => btnXoa.BackColor = Color.FromArgb(255, 152, 0);
            
            btnThoat = new Button();
            btnThoat.Text = "Thoát";
            btnThoat.Location = new Point(280, 215);
            btnThoat.Size = new Size(120, 40);
            btnThoat.BackColor = Color.FromArgb(244, 67, 54);
            btnThoat.ForeColor = Color.White;
            btnThoat.FlatStyle = FlatStyle.Flat;
            btnThoat.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnThoat.Cursor = Cursors.Hand;
            btnThoat.Click += BtnThoat_Click;
            
            btnThoat.MouseEnter += (s, e) => btnThoat.BackColor = Color.FromArgb(229, 57, 53);
            btnThoat.MouseLeave += (s, e) => btnThoat.BackColor = Color.FromArgb(244, 67, 54);
            
            lblResult = new Label();
            lblResult.Text = "Kết quả (sẽ ghi vào file output)";
            lblResult.Location = new Point(20, 270);
            lblResult.Size = new Size(250, 20);
            lblResult.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            
            txtResult = new TextBox();
            txtResult.Location = new Point(20, 295);
            txtResult.Size = new Size(500, 140);
            txtResult.Multiline = true;
            txtResult.ScrollBars = ScrollBars.Both;
            txtResult.ReadOnly = true;
            txtResult.Font = new Font("Consolas", 10F);
            txtResult.BackColor = Color.FromArgb(245, 245, 245);
            
            Controls.Add(lblFilePath);
            Controls.Add(txtFilePath);
            Controls.Add(btnChonFile);
            Controls.Add(lblInput);
            Controls.Add(txtInput);
            Controls.Add(btnDoc);
            Controls.Add(btnXoa);
            Controls.Add(btnThoat);
            Controls.Add(lblResult);
            Controls.Add(txtResult);
        }

        private void BtnChonFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Chọn file input để tính toán";
            ofd.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    selectedFilePath = ofd.FileName;
                    txtFilePath.Text = selectedFilePath;

                    // Đọc và hiển thị nội dung file
                    string content = File.ReadAllText(selectedFilePath, Encoding.UTF8);
                    txtInput.Text = content;

                    MessageBox.Show("Đã chọn file thành công!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi đọc file: " + ex.Message, "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnDoc_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(selectedFilePath))
                {
                    MessageBox.Show("Vui lòng chọn file input trước!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!File.Exists(selectedFilePath))
                {
                    MessageBox.Show("File không tồn tại!", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Đọc nội dung file
                string[] lines = File.ReadAllLines(selectedFilePath, Encoding.UTF8);

                StringBuilder output = new StringBuilder();
                
                foreach (string line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        try
                        {
                            double result = EvaluateExpression(line.Trim());
                            
                            // Định dạng kết quả - Hiển thị số thập phân đúng
                            string formattedResult = FormatResult(result);
                            
                            output.AppendLine($"{line.Trim()} = {formattedResult}");
                        }
                        catch (Exception ex)
                        {
                            output.AppendLine($"{line.Trim()} = Lỗi: {ex.Message}");
                        }
                    }
                }

                // Hiển thị kết quả
                txtResult.Text = output.ToString();

                // Tạo tên file output (cùng thư mục với file input)
                string directory = Path.GetDirectoryName(selectedFilePath);
                string outputFileName = Path.GetFileNameWithoutExtension(selectedFilePath) + "_output.txt";
                string outputPath = Path.Combine(directory, outputFileName);

                // Ghi kết quả ra file output
                File.WriteAllText(outputPath, output.ToString(), Encoding.UTF8);

                MessageBox.Show($"Đã tính toán và ghi kết quả vào:\n{outputPath}", "Thành công", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Hàm định dạng kết quả
        private string FormatResult(double result)
        {
            
            if (result == Math.Floor(result))
            {
                return result.ToString("0");
            }
            else
            {
                
                return result.ToString("0.##########", CultureInfo.InvariantCulture);
            }
        }

        
        private double EvaluateExpression(string expression)
        {
            
            expression = expression.Replace(" ", "");
            
            // Đảm bảo dùng dấu chấm cho số thập phân
            expression = expression.Replace(",", ".");

            // Xử lý ngoặc đơn
            while (expression.Contains("("))
            {
                int closePos = expression.IndexOf(')');
                if (closePos == -1)
                    throw new Exception("Thiếu dấu ngoặc đóng");
                    
                int openPos = expression.LastIndexOf('(', closePos);
                
                if (openPos == -1)
                    throw new Exception("Thiếu dấu ngoặc mở");
                
                string subExpr = expression.Substring(openPos + 1, closePos - openPos - 1);
                double subResult = EvaluateExpression(subExpr);
                
                
                expression = expression.Substring(0, openPos) + 
                            subResult.ToString(CultureInfo.InvariantCulture) + 
                            expression.Substring(closePos + 1);
            }

            // Tính toán nhân và chia trước
            expression = EvaluateMultiplicationAndDivision(expression);

            // Tính toán cộng và trừ
            return EvaluateAdditionAndSubtraction(expression);
        }

        // Kiểm tra ký tự có phải là phần của số không
        private bool IsPartOfNumber(char c)
        {
            return char.IsDigit(c) || c == '.';
        }

        // Xử lý phép nhân và chia
        private string EvaluateMultiplicationAndDivision(string expression)
        {
            while (expression.Contains("*") || expression.Contains("/"))
            {
                int mulPos = expression.IndexOf('*');
                int divPos = expression.IndexOf('/');
                
                int pos = -1;
                char op = ' ';
                
                if (mulPos != -1 && divPos != -1)
                {
                    if (mulPos < divPos)
                    {
                        pos = mulPos;
                        op = '*';
                    }
                    else
                    {
                        pos = divPos;
                        op = '/';
                    }
                }
                else if (mulPos != -1)
                {
                    pos = mulPos;
                    op = '*';
                }
                else
                {
                    pos = divPos;
                    op = '/';
                }

                
                int leftStart = pos - 1;
                while (leftStart > 0 && IsPartOfNumber(expression[leftStart - 1]))
                {
                    leftStart--;
                }
                
                // Xử lý số âm
                if (leftStart > 0 && expression[leftStart - 1] == '-')
                {
                    bool isNegative = true;
                    if (leftStart > 1 && IsPartOfNumber(expression[leftStart - 2]))
                    {
                        isNegative = false;
                    }
                    if (isNegative)
                    {
                        leftStart--;
                    }
                }

                string leftStr = expression.Substring(leftStart, pos - leftStart);
                double left = double.Parse(leftStr, CultureInfo.InvariantCulture);

                // Tìm số bên phải (bao gồm cả dấu chấm thập phân)
                int rightStart = pos + 1;
                int rightEnd = rightStart;
                
                // Xử lý số âm bên phải
                if (rightStart < expression.Length && expression[rightStart] == '-')
                {
                    rightEnd++;
                }
                
                while (rightEnd < expression.Length && IsPartOfNumber(expression[rightEnd]))
                {
                    rightEnd++;
                }

                string rightStr = expression.Substring(rightStart, rightEnd - rightStart);
                double right = double.Parse(rightStr, CultureInfo.InvariantCulture);

                // Tính toán
                double result = op == '*' ? left * right : left / right;

                // Thay thế trong biểu thức với dấu chấm
                expression = expression.Substring(0, leftStart) + 
                            result.ToString(CultureInfo.InvariantCulture) + 
                            expression.Substring(rightEnd);
            }

            return expression;
        }

        // Xử lý phép cộng và trừ
        private double EvaluateAdditionAndSubtraction(string expression)
        {
            List<double> numbers = new List<double>();
            List<char> operators = new List<char>();

            int i = 0;
            while (i < expression.Length)
            {
                // Đọc số (bao gồm cả dấu chấm thập phân)
                int start = i;
                
                // Xử lý dấu âm
                if (expression[i] == '-' || expression[i] == '+')
                {
                    if (i == 0 || expression[i - 1] == '+' || expression[i - 1] == '-')
                    {
                        i++;
                    }
                }

                while (i < expression.Length && IsPartOfNumber(expression[i]))
                {
                    i++;
                }

                string numStr = expression.Substring(start, i - start);
                numbers.Add(double.Parse(numStr, CultureInfo.InvariantCulture));

                // Đọc toán tử
                if (i < expression.Length)
                {
                    operators.Add(expression[i]);
                    i++;
                }
            }

        
            double result = numbers[0];
            for (int j = 0; j < operators.Count; j++)
            {
                if (operators[j] == '+')
                {
                    result += numbers[j + 1];
                }
                else if (operators[j] == '-')
                {
                    result -= numbers[j + 1];
                }
            }

            return result;
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            txtInput.Clear();
            txtResult.Clear();
            txtFilePath.Clear();
            selectedFilePath = "";
            txtInput.Focus();
        }

        private void BtnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
