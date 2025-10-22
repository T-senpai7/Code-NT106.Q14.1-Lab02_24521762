using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp
{
    public partial class Bai2Form : Form
    {
        private Button btnReadFile;
        private Label lblFileName;
        private Label lblSize;
        private Label lblUrl;
        private Label lblLineCount;
        private Label lblWordsCount;
        private Label lblCharacterCount;
        private TextBox txtFileName;
        private TextBox txtSize;
        private TextBox txtUrl;
        private TextBox txtLineCount;
        private TextBox txtWordsCount;
        private TextBox txtCharacterCount;
        private RichTextBox rtbContent;
        private Panel pnlMain;
        private Panel pnlFileInfo;
        private Panel pnlContent;
        private Panel pnlButtons;
        private Panel pnlHeader;
        private Label lblTieuDe;

        public Bai2Form()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Thiết lập form chính
            this.Size = new Size(700, 650);
            this.Text = "Bai2";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 10F);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Panel Header
            pnlHeader = new Panel();
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.BackColor = Color.FromArgb(32, 32, 32);
            pnlHeader.Height = 80;

            // Tiêu đề
            lblTieuDe = new Label();
            lblTieuDe.Text = "Read from File";
            lblTieuDe.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTieuDe.ForeColor = Color.White;
            lblTieuDe.AutoSize = false;
            lblTieuDe.Size = new Size(650, 40);
            lblTieuDe.Location = new Point(25, 20);
            lblTieuDe.TextAlign = ContentAlignment.MiddleCenter;

            // Panel chính
            pnlMain = new Panel();
            pnlMain.Size = new Size(650, 530);
            pnlMain.Location = new Point(25, 100);
            pnlMain.BackColor = Color.White;

            // Panel thông tin file (bên trái)
            pnlFileInfo = new Panel();
            pnlFileInfo.Size = new Size(240, 440);
            pnlFileInfo.Location = new Point(10, 10);
            pnlFileInfo.BackColor = Color.FromArgb(248, 249, 250);
            pnlFileInfo.BorderStyle = BorderStyle.FixedSingle;

            // File name
            lblFileName = new Label();
            lblFileName.Text = "File name";
            lblFileName.Size = new Size(220, 25);
            lblFileName.Location = new Point(10, 20);
            lblFileName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblFileName.ForeColor = Color.FromArgb(33, 150, 243);

            txtFileName = new TextBox();
            txtFileName.Size = new Size(220, 30);
            txtFileName.Location = new Point(10, 50);
            txtFileName.Font = new Font("Segoe UI", 10F);
            txtFileName.BorderStyle = BorderStyle.FixedSingle;
            txtFileName.BackColor = Color.White;
            txtFileName.ReadOnly = true;

            // Size
            lblSize = new Label();
            lblSize.Text = "Size";
            lblSize.Size = new Size(220, 25);
            lblSize.Location = new Point(10, 90);
            lblSize.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSize.ForeColor = Color.FromArgb(33, 150, 243);

            txtSize = new TextBox();
            txtSize.Size = new Size(220, 30);
            txtSize.Location = new Point(10, 120);
            txtSize.Font = new Font("Segoe UI", 10F);
            txtSize.BorderStyle = BorderStyle.FixedSingle;
            txtSize.BackColor = Color.White;
            txtSize.ReadOnly = true;

            // URL
            lblUrl = new Label();
            lblUrl.Text = "URL";
            lblUrl.Size = new Size(220, 25);
            lblUrl.Location = new Point(10, 160);
            lblUrl.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUrl.ForeColor = Color.FromArgb(33, 150, 243);

            txtUrl = new TextBox();
            txtUrl.Size = new Size(220, 30);
            txtUrl.Location = new Point(10, 190);
            txtUrl.Font = new Font("Segoe UI", 9F);
            txtUrl.BorderStyle = BorderStyle.FixedSingle;
            txtUrl.BackColor = Color.White;
            txtUrl.ReadOnly = true;

            // Line count
            lblLineCount = new Label();
            lblLineCount.Text = "Line count";
            lblLineCount.Size = new Size(220, 25);
            lblLineCount.Location = new Point(10, 230);
            lblLineCount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblLineCount.ForeColor = Color.FromArgb(33, 150, 243);

            txtLineCount = new TextBox();
            txtLineCount.Size = new Size(220, 30);
            txtLineCount.Location = new Point(10, 260);
            txtLineCount.Font = new Font("Segoe UI", 10F);
            txtLineCount.BorderStyle = BorderStyle.FixedSingle;
            txtLineCount.BackColor = Color.White;
            txtLineCount.ReadOnly = true;

            // Words count
            lblWordsCount = new Label();
            lblWordsCount.Text = "Words count";
            lblWordsCount.Size = new Size(220, 25);
            lblWordsCount.Location = new Point(10, 300);
            lblWordsCount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblWordsCount.ForeColor = Color.FromArgb(33, 150, 243);

            txtWordsCount = new TextBox();
            txtWordsCount.Size = new Size(220, 30);
            txtWordsCount.Location = new Point(10, 330);
            txtWordsCount.Font = new Font("Segoe UI", 10F);
            txtWordsCount.BorderStyle = BorderStyle.FixedSingle;
            txtWordsCount.BackColor = Color.White;
            txtWordsCount.ReadOnly = true;

            // Character count
            lblCharacterCount = new Label();
            lblCharacterCount.Text = "Character count";
            lblCharacterCount.Size = new Size(220, 25);
            lblCharacterCount.Location = new Point(10, 370);
            lblCharacterCount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCharacterCount.ForeColor = Color.FromArgb(33, 150, 243);

            txtCharacterCount = new TextBox();
            txtCharacterCount.Size = new Size(220, 30);
            txtCharacterCount.Location = new Point(10, 400);
            txtCharacterCount.Font = new Font("Segoe UI", 10F);
            txtCharacterCount.BorderStyle = BorderStyle.FixedSingle;
            txtCharacterCount.BackColor = Color.White;
            txtCharacterCount.ReadOnly = true;

            // Thêm controls vào panel file info
            pnlFileInfo.Controls.Add(lblFileName);
            pnlFileInfo.Controls.Add(txtFileName);
            pnlFileInfo.Controls.Add(lblSize);
            pnlFileInfo.Controls.Add(txtSize);
            pnlFileInfo.Controls.Add(lblUrl);
            pnlFileInfo.Controls.Add(txtUrl);
            pnlFileInfo.Controls.Add(lblLineCount);
            pnlFileInfo.Controls.Add(txtLineCount);
            pnlFileInfo.Controls.Add(lblWordsCount);
            pnlFileInfo.Controls.Add(txtWordsCount);
            pnlFileInfo.Controls.Add(lblCharacterCount);
            pnlFileInfo.Controls.Add(txtCharacterCount);

            
            pnlContent = new Panel();
            pnlContent.Size = new Size(380, 440);
            pnlContent.Location = new Point(260, 10);
            pnlContent.BackColor = Color.FromArgb(32, 32, 32);
            pnlContent.BorderStyle = BorderStyle.FixedSingle;

            
            rtbContent = new RichTextBox();
            rtbContent.Size = new Size(378, 438);
            rtbContent.Location = new Point(0, 0);
            rtbContent.Font = new Font("Consolas", 10F);
            rtbContent.BackColor = Color.FromArgb(32, 32, 32);
            rtbContent.ForeColor = Color.FromArgb(33, 150, 243);
            rtbContent.BorderStyle = BorderStyle.None;
            rtbContent.ReadOnly = true;
            rtbContent.WordWrap = false;
            rtbContent.ScrollBars = RichTextBoxScrollBars.Both;

            pnlContent.Controls.Add(rtbContent);

            
            pnlButtons = new Panel();
            pnlButtons.Size = new Size(630, 60);
            pnlButtons.Location = new Point(10, 460);
            pnlButtons.BackColor = Color.Transparent;

            // Button Chọn File (đổi từ Exit)
            btnReadFile = new Button();
            btnReadFile.Text = "Chọn File";
            btnReadFile.Size = new Size(620, 45);
            btnReadFile.Location = new Point(5, 10);
            btnReadFile.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnReadFile.BackColor = Color.FromArgb(76, 175, 80);
            btnReadFile.ForeColor = Color.White;
            btnReadFile.FlatStyle = FlatStyle.Flat;
            btnReadFile.FlatAppearance.BorderSize = 0;
            btnReadFile.Cursor = Cursors.Hand;
            btnReadFile.Click += BtnReadFile_Click;

            
            btnReadFile.MouseEnter += (s, e) => {
                btnReadFile.BackColor = Color.FromArgb(67, 160, 71);
            };
            btnReadFile.MouseLeave += (s, e) => {
                btnReadFile.BackColor = Color.FromArgb(76, 175, 80);
            };

            pnlButtons.Controls.Add(btnReadFile);

            
            pnlMain.Controls.Add(pnlFileInfo);
            pnlMain.Controls.Add(pnlContent);
            pnlMain.Controls.Add(pnlButtons);
            pnlHeader.Controls.Add(lblTieuDe);
            this.Controls.Add(pnlMain);
            this.Controls.Add(pnlHeader);
        }

        private void BtnReadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Chọn file để đọc";
            ofd.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            ofd.FilterIndex = 1;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Lấy tên file sử dụng SafeFileName Property
                    string name = ofd.SafeFileName;
                    txtFileName.Text = name;

                    // Đọc file bằng FileStream và StreamReader
                    using (FileStream fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read))
                    {
                        // Lấy URL (đường dẫn) sử dụng Name Property của FileStream
                        string url = fs.Name;
                        txtUrl.Text = url;

                        
                        long sizeInBytes = fs.Length;
                        txtSize.Text = sizeInBytes + " bytes";

                        // Đọc nội dung file bằng StreamReader
                        using (StreamReader sr = new StreamReader(fs))
                        {
                            string content = sr.ReadToEnd();
                            rtbContent.Text = content;

                            // Đếm số dòng
                            string[] lines = content.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                            int lineCount = lines.Length;
                            txtLineCount.Text = lineCount.ToString();

                            // Đếm số từ
                            string[] words = content.Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                            int wordCount = words.Length;
                            txtWordsCount.Text = wordCount.ToString();

                            // Đếm số ký tự
                            int charCount = content.Length;
                            txtCharacterCount.Text = charCount.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi đọc file: {ex.Message}", 
                                  "Lỗi", 
                                  MessageBoxButtons.OK, 
                                  MessageBoxIcon.Error);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
