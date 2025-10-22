using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Linq;

namespace WindowsFormsApp
{
    public partial class Bai1Form : Form
    {
        private Button btnDocFile;
        private Button btnGhiFile;
        private RichTextBox rtbNoiDung;
        private Label lblTieuDe;
        private Label lblHoTen;
        private Label lblMSSV;
        private Label lblLop;
        private Panel pnlHeader;
        private Panel pnlContent;
        private Panel pnlButtons;
        private Panel pnlInfo;
        private Label lblFileStatus;
        private PictureBox picIcon;
        private Label lblSelectedFile;
        private string selectedFilePath = null;

        public Bai1Form()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Thiết lập form chính
            this.Size = new Size(900, 650);
            this.Text = "File Reader & Writer";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.Font = new Font("Segoe UI", 9.5F);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Padding = new Padding(0);

            // Panel Header
            pnlHeader = new Panel();
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Height = 110;
            pnlHeader.BackColor = Color.White;
            pnlHeader.BorderStyle = BorderStyle.None;
            pnlHeader.Padding = new Padding(30, 20, 30, 20);

            // Icon
            picIcon = new PictureBox();
            picIcon.Size = new Size(50, 50);
            picIcon.Location = new Point(30, 25);
            picIcon.BackColor = Color.FromArgb(66, 133, 244);
            picIcon.SizeMode = PictureBoxSizeMode.CenterImage;
            
            // Label Tiêu đề
            lblTieuDe = new Label();
            lblTieuDe.Text = "FILE MANAGER";
            lblTieuDe.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTieuDe.ForeColor = Color.FromArgb(33, 33, 33);
            lblTieuDe.Location = new Point(90, 20);
            lblTieuDe.AutoSize = true;

            // File Status Label
            lblFileStatus = new Label();
            lblFileStatus.Text = "Ready to select file";
            lblFileStatus.Font = new Font("Segoe UI", 9F);
            lblFileStatus.ForeColor = Color.FromArgb(117, 117, 117);
            lblFileStatus.Location = new Point(92, 55);
            lblFileStatus.AutoSize = true;

            // Selected File Label
            lblSelectedFile = new Label();
            lblSelectedFile.Text = "No file selected";
            lblSelectedFile.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblSelectedFile.ForeColor = Color.FromArgb(158, 158, 158);
            lblSelectedFile.Location = new Point(92, 72);
            lblSelectedFile.AutoSize = true;
            lblSelectedFile.MaximumSize = new Size(500, 0);

            // Panel Info - thông tin sinh viên (bên phải header)
            pnlInfo = new Panel();
            pnlInfo.Size = new Size(250, 110);
            pnlInfo.Location = new Point(620, 0);
            pnlInfo.Dock = DockStyle.Right;
            pnlInfo.BackColor = Color.FromArgb(248, 249, 250);
            pnlInfo.BorderStyle = BorderStyle.None;
            pnlInfo.Padding = new Padding(20, 15, 20, 15);

            // Labels thông tin
            lblHoTen = new Label();
            lblHoTen.Text = "👤 Hồ Hoàng Tiến";
            lblHoTen.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            lblHoTen.ForeColor = Color.FromArgb(33, 33, 33);
            lblHoTen.Location = new Point(20, 22);
            lblHoTen.AutoSize = true;

            lblMSSV = new Label();
            lblMSSV.Text = "🆔 MSSV: 24251762";
            lblMSSV.Font = new Font("Segoe UI", 9.5F);
            lblMSSV.ForeColor = Color.FromArgb(95, 99, 104);
            lblMSSV.Location = new Point(20, 46);
            lblMSSV.AutoSize = true;

            
            lblLop = new Label();
            lblLop.Text = "📚 Class: MMTT";
            lblLop.Font = new Font("Segoe UI", 9.5F);
            lblLop.ForeColor = Color.FromArgb(95, 99, 104);
            lblLop.Location = new Point(20, 68);
            lblLop.AutoSize = true;
            // Panel Content chính
            pnlContent = new Panel();
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.BackColor = Color.FromArgb(240, 242, 245);
            pnlContent.Padding = new Padding(30, 20, 30, 20);

            // Container cho RichTextBox với border
            Panel rtbContainer = new Panel();
            rtbContainer.Dock = DockStyle.Fill;
            rtbContainer.BackColor = Color.White;
            rtbContainer.BorderStyle = BorderStyle.FixedSingle;
            rtbContainer.Padding = new Padding(1);

            // RichTextBox
            rtbNoiDung = new RichTextBox();
            rtbNoiDung.Dock = DockStyle.Fill;
            rtbNoiDung.Font = new Font("Consolas", 10F);
            rtbNoiDung.BackColor = Color.White;
            rtbNoiDung.ForeColor = Color.FromArgb(33, 33, 33);
            rtbNoiDung.BorderStyle = BorderStyle.None;
            rtbNoiDung.Text = "📄 File content will be displayed here...\n\nClick 'SELECT & READ FILE' button to choose any .txt file from your computer";
            rtbNoiDung.ScrollBars = RichTextBoxScrollBars.Vertical;
            rtbNoiDung.Padding = new Padding(15);

            // Panel Buttons
            pnlButtons = new Panel();
            pnlButtons.Dock = DockStyle.Bottom;
            pnlButtons.Height = 90;
            pnlButtons.BackColor = Color.White;
            pnlButtons.BorderStyle = BorderStyle.None;
            pnlButtons.Padding = new Padding(30, 20, 30, 20);

            // Button Đọc File
            btnDocFile = new Button();
            btnDocFile.Text = "📁 SELECT & READ FILE";
            btnDocFile.Size = new Size(220, 50);
            btnDocFile.Location = new Point(260, 20);
            btnDocFile.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnDocFile.BackColor = Color.FromArgb(66, 133, 244);
            btnDocFile.ForeColor = Color.White;
            btnDocFile.FlatStyle = FlatStyle.Flat;
            btnDocFile.FlatAppearance.BorderSize = 0;
            btnDocFile.FlatAppearance.MouseOverBackColor = Color.FromArgb(51, 103, 214);
            btnDocFile.Cursor = Cursors.Hand;
            btnDocFile.Click += BtnDocFile_Click;

            // Button Ghi File
            btnGhiFile = new Button();
            btnGhiFile.Text = "💾 SAVE FILE";
            btnGhiFile.Size = new Size(200, 50);
            btnGhiFile.Location = new Point(500, 20);
            btnGhiFile.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnGhiFile.BackColor = Color.FromArgb(52, 168, 83);
            btnGhiFile.ForeColor = Color.White;
            btnGhiFile.FlatStyle = FlatStyle.Flat;
            btnGhiFile.FlatAppearance.BorderSize = 0;
            btnGhiFile.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 138, 66);
            btnGhiFile.Cursor = Cursors.Hand;
            btnGhiFile.Click += BtnGhiFile_Click;

            // Thêm shadow effect cho buttons
            AddButtonEffects();

            // Xây dựng hierarchy
            pnlInfo.Controls.Add(lblLop);
            pnlInfo.Controls.Add(lblMSSV);
            pnlInfo.Controls.Add(lblHoTen);

            pnlHeader.Controls.Add(pnlInfo);
            pnlHeader.Controls.Add(lblSelectedFile);
            pnlHeader.Controls.Add(lblFileStatus);
            pnlHeader.Controls.Add(lblTieuDe);
            pnlHeader.Controls.Add(picIcon);

            rtbContainer.Controls.Add(rtbNoiDung);
            pnlContent.Controls.Add(rtbContainer);

            pnlButtons.Controls.Add(btnGhiFile);
            pnlButtons.Controls.Add(btnDocFile);

            // Thêm vào form
            this.Controls.Add(pnlContent);
            this.Controls.Add(pnlButtons);
            this.Controls.Add(pnlHeader);

            // Tab Order
            btnDocFile.TabIndex = 0;
            btnGhiFile.TabIndex = 1;
        }

        private void AddButtonEffects()
        {
            // Hover effect cho Read button
            btnDocFile.MouseEnter += (s, e) => {
                btnDocFile.BackColor = Color.FromArgb(51, 103, 214);
            };
            btnDocFile.MouseLeave += (s, e) => {
                btnDocFile.BackColor = Color.FromArgb(66, 133, 244);
            };

            // Hover effect cho Save button
            btnGhiFile.MouseEnter += (s, e) => {
                btnGhiFile.BackColor = Color.FromArgb(40, 138, 66);
            };
            btnGhiFile.MouseLeave += (s, e) => {
                btnGhiFile.BackColor = Color.FromArgb(52, 168, 83);
            };
        }

        private void BtnDocFile_Click(object sender, EventArgs e)
        {
            try
            {
                // Tạo OpenFileDialog - cho phép chọn file từ bất kỳ folder nào
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Title = "Select a Text File";
                    openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                    openFileDialog.FilterIndex = 1;
                    openFileDialog.RestoreDirectory = true;
                    openFileDialog.Multiselect = false;

                    lblFileStatus.Text = "🔍 Waiting for file selection...";
                    lblFileStatus.ForeColor = Color.FromArgb(251, 188, 5);

                    // Hiển thị dialog và kiểm tra kết quả
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        selectedFilePath = openFileDialog.FileName;

                        lblFileStatus.Text = "📖 Reading file...";
                        Application.DoEvents();

                        // Đọc nội dung file
                        using (StreamReader sr = new StreamReader(selectedFilePath))
                        {
                            string noiDung = sr.ReadToEnd();
                            rtbNoiDung.Text = noiDung;
                        }

                        // Cập nhật UI
                        string fileName = Path.GetFileName(selectedFilePath);
                        lblFileStatus.Text = $"✅ File loaded successfully";
                        lblFileStatus.ForeColor = Color.FromArgb(52, 168, 83);
                        lblSelectedFile.Text = $"📄 {fileName}";
                        lblSelectedFile.ForeColor = Color.FromArgb(66, 133, 244);

                        FileInfo fileInfo = new FileInfo(selectedFilePath);
                        MessageBox.Show($"✅ File loaded successfully!\n\n" +
                                      $"📄 Filename: {fileName}\n" +
                                      $"📍 Location: {Path.GetDirectoryName(selectedFilePath)}\n" +
                                      $"📊 Size: {fileInfo.Length} bytes\n" +
                                      $"📅 Last Modified: {fileInfo.LastWriteTime:yyyy-MM-dd HH:mm:ss}",
                                      "Success",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Information);
                    }
                    else
                    {
                        lblFileStatus.Text = "❌ File selection cancelled";
                        lblFileStatus.ForeColor = Color.FromArgb(158, 158, 158);
                    }
                }
            }
            catch (IOException ioEx)
            {
                lblFileStatus.Text = "❌ I/O Error occurred";
                lblFileStatus.ForeColor = Color.FromArgb(234, 67, 53);
                lblSelectedFile.Text = "Error reading file";
                lblSelectedFile.ForeColor = Color.FromArgb(234, 67, 53);
                MessageBox.Show($"❌ I/O Error: {ioEx.Message}",
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
            catch (UnauthorizedAccessException uaEx)
            {
                lblFileStatus.Text = "❌ Access denied";
                lblFileStatus.ForeColor = Color.FromArgb(234, 67, 53);
                MessageBox.Show($"❌ Access Denied: {uaEx.Message}\n\nPlease check file permissions.",
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                lblFileStatus.Text = "❌ Error occurred";
                lblFileStatus.ForeColor = Color.FromArgb(234, 67, 53);
                lblSelectedFile.Text = "Error";
                lblSelectedFile.ForeColor = Color.FromArgb(234, 67, 53);
                MessageBox.Show($"❌ Error: {ex.Message}",
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
        }

        private void BtnGhiFile_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem đã chọn file chưa
                if (selectedFilePath == null)
                {
                    MessageBox.Show("⚠️ No file has been selected!\n\nPlease select and read a file first.",
                                  "Warning",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(rtbNoiDung.Text))
                {
                    MessageBox.Show("⚠️ No content to save!\n\nPlease read a file first.",
                                  "Warning",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Warning);
                    return;
                }

                lblFileStatus.Text = "💾 Saving file...";
                lblFileStatus.ForeColor = Color.FromArgb(251, 188, 5);
                Application.DoEvents();

                // Tạo tên file output dựa trên file input
                string inputFileName = Path.GetFileNameWithoutExtension(selectedFilePath);
                string outputFileName = $"{inputFileName}_output.txt";
                string outputPath = Path.Combine(Path.GetDirectoryName(selectedFilePath), outputFileName);

                // Chuyển sang in hoa và ghi file
                string noiDungInHoa = rtbNoiDung.Text.ToUpper();

                using (StreamWriter sw = new StreamWriter(outputPath, false))
                {
                    sw.Write(noiDungInHoa);
                }

                lblFileStatus.Text = $"✅ Saved successfully";
                lblFileStatus.ForeColor = Color.FromArgb(52, 168, 83);

                rtbNoiDung.Text = noiDungInHoa;

                FileInfo fileInfo = new FileInfo(outputPath);
                MessageBox.Show($"✅ File saved successfully!\n\n" +
                              $"📄 Filename: {outputFileName}\n" +
                              $"📍 Location: {Path.GetDirectoryName(outputPath)}\n" +
                              $"📝 Content: Converted to UPPERCASE\n" +
                              $"📊 Size: {fileInfo.Length} bytes",
                              "Success",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information);
            }
            catch (IOException ioEx)
            {
                lblFileStatus.Text = "❌ Failed to save";
                lblFileStatus.ForeColor = Color.FromArgb(234, 67, 53);
                MessageBox.Show($"❌ I/O Error: {ioEx.Message}",
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
            catch (UnauthorizedAccessException uaEx)
            {
                lblFileStatus.Text = "❌ Access denied";
                lblFileStatus.ForeColor = Color.FromArgb(234, 67, 53);
                MessageBox.Show($"❌ Access Denied: {uaEx.Message}\n\nPlease check folder permissions.",
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                lblFileStatus.Text = "❌ Error occurred";
                lblFileStatus.ForeColor = Color.FromArgb(234, 67, 53);
                MessageBox.Show($"❌ Error: {ex.Message}",
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}