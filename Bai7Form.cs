using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public class Bai7Form : Form
    {
        private Panel panelHeader;
        private Label lblTitle;
        private Label lblSubtitle;
        private Panel panelNavigation;
        private Label lblCurrentPath;
        private TextBox txtCurrentPath;
        private Button btnBack;
        private Button btnRefresh;
        private Button btnHome;
        private SplitContainer splitContainer;
        private TreeView treeViewFolders;
        private ListView listViewFiles;
        private Panel panelPreview;
        private Label lblPreviewTitle;
        private RichTextBox txtPreview;
        private PictureBox picturePreview;
        private ImageList imageListSmall;
        private ImageList imageListLarge;
        private string currentPath;

        public Bai7Form()
        {
            InitializeComponent();
            LoadDrives();
        }

        private void InitializeComponent()
        {
            Text = "Bài 7 - Trình duyệt file";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(1200, 800);
            BackColor = Color.FromArgb(240, 242, 245);
            Padding = new Padding(15);

            // Panel tiêu đề
            panelHeader = new Panel
            {
                Location = new Point(15, 15),
                Size = new Size(1150, 80),
                BackColor = Color.FromArgb(0, 120, 215)
            };

            lblTitle = new Label
            {
                Text = "TRÌNH DUYỆT FILE",
                Location = new Point(20, 15),
                Size = new Size(1110, 35),
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter
            };

            lblSubtitle = new Label
            {
                Text = "Duyệt file và thư mục trên máy tính",
                Location = new Point(20, 50),
                Size = new Size(1110, 20),
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.FromArgb(230, 240, 255),
                TextAlign = ContentAlignment.MiddleCenter
            };

            panelHeader.Controls.Add(lblTitle);
            panelHeader.Controls.Add(lblSubtitle);

            // Panel điều hướng
            panelNavigation = new Panel
            {
                Location = new Point(15, 105),
                Size = new Size(1150, 90),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            lblCurrentPath = new Label
            {
                Text = "Đường dẫn hiện tại:",
                Location = new Point(15, 15),
                Size = new Size(1120, 20),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(70, 70, 70)
            };

            txtCurrentPath = new TextBox
            {
                Location = new Point(15, 40),
                Size = new Size(780, 30),
                Font = new Font("Segoe UI", 10),
                ReadOnly = true,
                BackColor = Color.FromArgb(250, 250, 250),
                BorderStyle = BorderStyle.FixedSingle
            };

            btnBack = new Button
            {
                Text = "← Quay lại",
                Location = new Point(810, 38),
                Size = new Size(100, 35),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                BackColor = Color.FromArgb(100, 100, 100),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.Click += BtnBack_Click;

            btnHome = new Button
            {
                Text = "Home",
                Location = new Point(920, 38),
                Size = new Size(100, 35),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnHome.FlatAppearance.BorderSize = 0;
            btnHome.Click += BtnHome_Click;

            btnRefresh = new Button
            {
                Text = "Làm mới",
                Location = new Point(1030, 38),
                Size = new Size(100, 35),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 150, 136),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.Click += BtnRefresh_Click;

            panelNavigation.Controls.Add(lblCurrentPath);
            panelNavigation.Controls.Add(txtCurrentPath);
            panelNavigation.Controls.Add(btnBack);
            panelNavigation.Controls.Add(btnHome);
            panelNavigation.Controls.Add(btnRefresh);

            // Danh sách ảnh
            imageListSmall = new ImageList { ImageSize = new Size(16, 16) };
            imageListLarge = new ImageList { ImageSize = new Size(32, 32) };

            // SplitContainer
            splitContainer = new SplitContainer
            {
                Location = new Point(15, 205),
                Size = new Size(1150, 560),
                Orientation = Orientation.Horizontal,
                SplitterDistance = 280,
                BorderStyle = BorderStyle.FixedSingle
            };

            // SplitContainer trên (TreeView và ListView)
            SplitContainer topSplitContainer = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Vertical,
                SplitterDistance = 280,
                BorderStyle = BorderStyle.None
            };

            // TreeView cho thư mục
            treeViewFolders = new TreeView
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None
            };
            treeViewFolders.AfterSelect += TreeViewFolders_AfterSelect;

            // ListView cho file
            listViewFiles = new ListView
            {
                Dock = DockStyle.Fill,
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                Font = new Font("Segoe UI", 9),
                BackColor = Color.White,
                SmallImageList = imageListSmall,
                LargeImageList = imageListLarge
            };
            listViewFiles.Columns.Add("Tên", 300);
            listViewFiles.Columns.Add("Loại", 120);
            listViewFiles.Columns.Add("Kích thước", 100);
            listViewFiles.Columns.Add("Ngày sửa đổi", 150);
            listViewFiles.DoubleClick += ListViewFiles_DoubleClick;
            listViewFiles.SelectedIndexChanged += ListViewFiles_SelectedIndexChanged;

            topSplitContainer.Panel1.Controls.Add(treeViewFolders);
            topSplitContainer.Panel2.Controls.Add(listViewFiles);

            splitContainer.Panel1.Controls.Add(topSplitContainer);

            // Panel xem trước
            panelPreview = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(10)
            };

            lblPreviewTitle = new Label
            {
                Text = "Xem trước nội dung:",
                Location = new Point(10, 10),
                Size = new Size(1130, 25),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 50, 50)
            };

            txtPreview = new RichTextBox
            {
                Location = new Point(10, 40),
                Size = new Size(1125, 220),
                Font = new Font("Consolas", 9),
                ReadOnly = true,
                BackColor = Color.FromArgb(250, 250, 250),
                BorderStyle = BorderStyle.None,
                ScrollBars = RichTextBoxScrollBars.Vertical
            };

            picturePreview = new PictureBox
            {
                Location = new Point(10, 40),
                Size = new Size(1125, 220),
                BackColor = Color.FromArgb(240, 240, 240),
                SizeMode = PictureBoxSizeMode.Zoom,
                Visible = false
            };

            panelPreview.Controls.Add(lblPreviewTitle);
            panelPreview.Controls.Add(txtPreview);
            panelPreview.Controls.Add(picturePreview);

            splitContainer.Panel2.Controls.Add(panelPreview);

            Controls.Add(panelHeader);
            Controls.Add(panelNavigation);
            Controls.Add(splitContainer);

            currentPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            txtPreview.Text = "Chào mừng đến với Trình duyệt file!\n\nChọn một file để xem nội dung.";
        }

        private void LoadDrives()
        {
            treeViewFolders.Nodes.Clear();

            TreeNode computerNode = new TreeNode("Máy tính của tôi");
            treeViewFolders.Nodes.Add(computerNode);

            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                {
                    TreeNode driveNode = new TreeNode($"{drive.Name} ({drive.DriveType})")
                    {
                        Tag = drive.RootDirectory.FullName
                    };
                    computerNode.Nodes.Add(driveNode);
                    driveNode.Nodes.Add("");
                }
            }

            computerNode.Expand();
        }

        private void TreeViewFolders_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag == null) return;

            string path = e.Node.Tag.ToString();
            LoadDirectory(path);
        }

        private void LoadDirectory(string path)
        {
            try
            {
                currentPath = path;
                txtCurrentPath.Text = path;
                listViewFiles.Items.Clear();

                DirectoryInfo dirInfo = new DirectoryInfo(path);

                foreach (DirectoryInfo dir in dirInfo.GetDirectories())
                {
                    try
                    {
                        ListViewItem item = new ListViewItem(dir.Name);
                        item.SubItems.Add("Thư mục");
                        item.SubItems.Add("");
                        item.SubItems.Add(dir.LastWriteTime.ToString("dd/MM/yyyy HH:mm"));
                        item.Tag = dir.FullName;
                        item.ImageIndex = 0;
                        listViewFiles.Items.Add(item);
                    }
                    catch { }
                }

                foreach (FileInfo file in dirInfo.GetFiles())
                {
                    try
                    {
                        ListViewItem item = new ListViewItem(file.Name);
                        item.SubItems.Add(GetFileType(file.Extension));
                        item.SubItems.Add(FormatFileSize(file.Length));
                        item.SubItems.Add(file.LastWriteTime.ToString("dd/MM/yyyy HH:mm"));
                        item.Tag = file.FullName;
                        item.ImageIndex = 1;
                        listViewFiles.Items.Add(item);
                    }
                    catch { }
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Không có quyền truy cập thư mục này!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ListViewFiles_DoubleClick(object sender, EventArgs e)
        {
            if (listViewFiles.SelectedItems.Count == 0) return;

            string path = listViewFiles.SelectedItems[0].Tag.ToString();

            if (Directory.Exists(path))
            {
                LoadDirectory(path);
            }
            else if (File.Exists(path))
            {
                try
                {
                    var psi = new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = path,
                        UseShellExecute = true,
                        Verb = "open"
                    };
                    System.Diagnostics.Process.Start(psi);
                }
                catch
                {
                    MessageBox.Show("Không thể mở file này!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void ListViewFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewFiles.SelectedItems.Count == 0) return;

            string path = listViewFiles.SelectedItems[0].Tag.ToString();

            if (File.Exists(path))
            {
                PreviewFile(path);
            }
            else
            {
                txtPreview.Visible = true;
                picturePreview.Visible = false;
                txtPreview.Text = "Thư mục\n\nNhấp đúp để mở thư mục.";
            }
        }

        private void PreviewFile(string filePath)
        {
            try
            {
                string extension = Path.GetExtension(filePath).ToLower();
                FileInfo fileInfo = new FileInfo(filePath);

                if (extension == ".jpg" || extension == ".jpeg" || extension == ".png" ||
                    extension == ".bmp" || extension == ".gif")
                {
                    txtPreview.Visible = false;
                    picturePreview.Visible = true;
                    // Giải phóng ảnh trước đó để thả handle file
                    if (picturePreview.Image != null)
                    {
                        var old = picturePreview.Image;
                        picturePreview.Image = null;
                        old.Dispose();
                    }

                    // Tải ảnh vào bộ nhớ để tránh khoá file nguồn
                    using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (var ms = new MemoryStream())
                    {
                        fs.CopyTo(ms);
                        ms.Position = 0;
                        picturePreview.Image = Image.FromStream(ms);
                    }
                    lblPreviewTitle.Text = $"Xem trước: {fileInfo.Name} ({FormatFileSize(fileInfo.Length)})";
                }
                else if (extension == ".txt" || extension == ".cs" || extension == ".xml" ||
                         extension == ".json" || extension == ".html" || extension == ".css" ||
                         extension == ".js" || extension == ".log")
                {
                    picturePreview.Visible = false;
                    txtPreview.Visible = true;

                    if (fileInfo.Length > 1024 * 1024)
                    {
                        txtPreview.Text = $"File quá lớn ({FormatFileSize(fileInfo.Length)})\n\n" +
                                         "Chỉ hiển thị 1000 dòng đầu tiên...\n\n";
                        string[] lines = File.ReadLines(filePath).Take(1000).ToArray();
                        txtPreview.AppendText(string.Join("\n", lines));
                    }
                    else
                    {
                        txtPreview.Text = File.ReadAllText(filePath);
                    }

                    lblPreviewTitle.Text = $"Xem trước: {fileInfo.Name} ({FormatFileSize(fileInfo.Length)})";
                }
                else
                {
                    picturePreview.Visible = false;
                    txtPreview.Visible = true;
                    txtPreview.Text = $"{fileInfo.Name}\n\n" +
                                     $"Loại file: {GetFileType(extension)}\n" +
                                     $"Kích thước: {FormatFileSize(fileInfo.Length)}\n" +
                                     $"Ngày tạo: {fileInfo.CreationTime:dd/MM/yyyy HH:mm}\n" +
                                     $"Ngày sửa: {fileInfo.LastWriteTime:dd/MM/yyyy HH:mm}\n\n" +
                                     "Nhấp đúp để mở file với ứng dụng mặc định.";
                    lblPreviewTitle.Text = $"Thông tin file";
                }
            }
            catch (Exception ex)
            {
                txtPreview.Visible = true;
                picturePreview.Visible = false;
                txtPreview.Text = $"Không thể xem trước file\n\nLỗi: {ex.Message}";
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            try
            {
                DirectoryInfo parentDir = Directory.GetParent(currentPath);
                if (parentDir != null)
                {
                    LoadDirectory(parentDir.FullName);
                }
            }
            catch { }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadDirectory(currentPath);
        }

        private void BtnHome_Click(object sender, EventArgs e)
        {
            LoadDirectory(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
        }

        private string GetFileType(string extension)
        {
            switch (extension.ToLower())
            {
                case ".txt": return "Text File";
                case ".cs": return "C# File";
                case ".jpg":
                case ".jpeg":
                case ".png":
                case ".bmp":
                case ".gif": return "Image";
                case ".pdf": return "PDF";
                case ".doc":
                case ".docx": return "Word";
                case ".xls":
                case ".xlsx": return "Excel";
                case ".zip":
                case ".rar": return "Archive";
                case ".exe": return "Application";
                case ".mp3":
                case ".wav": return "Audio";
                case ".mp4":
                case ".avi": return "Video";
                default: return $"{extension} File";
            }
        }

        private string FormatFileSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }
            return $"{len:0.##} {sizes[order]}";
        }
    }
}
