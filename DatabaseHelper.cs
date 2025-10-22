using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace WindowsFormsApp
{
    public class DatabaseHelper
    {
        private string connectionString;
        private string databaseFileName = "food_database.db";
        // Publicly accessible full path to the database file
        public string DatabaseFilePath { get; private set; }
        // Images folder under application base directory
        public string ImagesFolderPath { get; private set; }

        public DatabaseHelper()
        {
            // Store DB and Images under application base directory so paths are predictable
            var baseDir = AppContext.BaseDirectory;
            DatabaseFilePath = Path.Combine(baseDir, databaseFileName);
            ImagesFolderPath = Path.Combine(baseDir, "Images");

            // Ensure Images folder exists
            if (!Directory.Exists(ImagesFolderPath))
            {
                Directory.CreateDirectory(ImagesFolderPath);
            }

            connectionString = $"Data Source={DatabaseFilePath};Version=3;";
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            if (!File.Exists(DatabaseFilePath))
            {
                SQLiteConnection.CreateFile(DatabaseFilePath);
            }

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Tạo bảng NguoiDung
                string createNguoiDungTable = @"
                    CREATE TABLE IF NOT EXISTS NguoiDung (
                        IDNCC INTEGER PRIMARY KEY AUTOINCREMENT,
                        HoVaTen TEXT NOT NULL,
                        QuyenHan TEXT NOT NULL
                    )";

                // Tạo bảng MonAn
                string createMonAnTable = @"
                    CREATE TABLE IF NOT EXISTS MonAn (
                        IDMA INTEGER PRIMARY KEY AUTOINCREMENT,
                        TenMonAn TEXT NOT NULL,
                        HinhAnh TEXT,
                        IDNCC INTEGER,
                        FOREIGN KEY (IDNCC) REFERENCES NguoiDung(IDNCC)
                    )";

                using (var command = new SQLiteCommand(createNguoiDungTable, connection))
                {
                    command.ExecuteNonQuery();
                }

                using (var command = new SQLiteCommand(createMonAnTable, connection))
                {
                    command.ExecuteNonQuery();
                }

                // Sau tạo bảng, đảm bảo schema cập nhật (thêm cột mới, index) — migration an toàn
                EnsureSchemaUpToDate(connection);

                // Không tự động thêm dữ liệu mẫu - để người dùng tự thêm
            }
        }

        private void EnsureSchemaUpToDate(SQLiteConnection connection)
        {
            // helper to execute SQL safely and log failures
            void ExecuteNonQuerySafe(string sql)
            {
                try
                {
                    using (var cmd = new SQLiteCommand(sql, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    try
                    {
                        var logPath = Path.Combine(AppContext.BaseDirectory, "db_schema_errors.log");
                        string msg = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] SQL failed: {sql}\nException: {ex}\n\n";
                        File.AppendAllText(logPath, msg);
                    }
                    catch { /* swallow logging errors */ }
                }
            }

            // Helper to check column existence
            bool HasColumn(string table, string column)
            {
                using (var cmd = new SQLiteCommand($"PRAGMA table_info(\"{table}\");", connection))
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        var col = rd[1].ToString();
                        if (string.Equals(col, column, StringComparison.OrdinalIgnoreCase)) return true;
                    }
                }
                return false;
            }

            // NguoiDung: add Email, CreatedAt, UpdatedAt
            if (!HasColumn("NguoiDung", "Email"))
            {
                ExecuteNonQuerySafe("ALTER TABLE NguoiDung ADD COLUMN Email TEXT DEFAULT '';");
            }
            if (!HasColumn("NguoiDung", "CreatedAt"))
            {
                // SQLite does not allow adding a column with a non-constant default via ALTER TABLE.
                // Add the column without a default, then populate existing rows.
                ExecuteNonQuerySafe("ALTER TABLE NguoiDung ADD COLUMN CreatedAt DATETIME;");
                ExecuteNonQuerySafe("UPDATE NguoiDung SET CreatedAt = CURRENT_TIMESTAMP WHERE CreatedAt IS NULL;");
            }
            if (!HasColumn("NguoiDung", "UpdatedAt"))
            {
                ExecuteNonQuerySafe("ALTER TABLE NguoiDung ADD COLUMN UpdatedAt DATETIME;");
                ExecuteNonQuerySafe("UPDATE NguoiDung SET UpdatedAt = CURRENT_TIMESTAMP WHERE UpdatedAt IS NULL;");
            }

            // MonAn: add Description, ImagePath (full/relative), CreatedAt, UpdatedAt
            if (!HasColumn("MonAn", "Description"))
            {
                ExecuteNonQuerySafe("ALTER TABLE MonAn ADD COLUMN Description TEXT DEFAULT '';");
            }
            if (!HasColumn("MonAn", "ImagePath"))
            {
                ExecuteNonQuerySafe("ALTER TABLE MonAn ADD COLUMN ImagePath TEXT DEFAULT '';");
            }
            if (!HasColumn("MonAn", "CreatedAt"))
            {
                ExecuteNonQuerySafe("ALTER TABLE MonAn ADD COLUMN CreatedAt DATETIME;");
                ExecuteNonQuerySafe("UPDATE MonAn SET CreatedAt = CURRENT_TIMESTAMP WHERE CreatedAt IS NULL;");
            }
            if (!HasColumn("MonAn", "UpdatedAt"))
            {
                ExecuteNonQuerySafe("ALTER TABLE MonAn ADD COLUMN UpdatedAt DATETIME;");
                ExecuteNonQuerySafe("UPDATE MonAn SET UpdatedAt = CURRENT_TIMESTAMP WHERE UpdatedAt IS NULL;");
            }

            // Create helpful indexes if not exist
            ExecuteNonQuerySafe("CREATE INDEX IF NOT EXISTS idx_MonAn_TenMonAn ON MonAn(TenMonAn);");
            ExecuteNonQuerySafe("CREATE INDEX IF NOT EXISTS idx_NguoiDung_HoVaTen ON NguoiDung(HoVaTen);");
        }

        private void InsertSampleData(SQLiteConnection connection)
        {
            // Kiểm tra xem đã có dữ liệu chưa
            string checkData = "SELECT COUNT(*) FROM NguoiDung";
            using (var command = new SQLiteCommand(checkData, connection))
            {
                int count = Convert.ToInt32(command.ExecuteScalar());
                if (count > 0) return; // Đã có dữ liệu
            }

            // Thêm người dùng mẫu
            string[] sampleUsers = {
                "INSERT INTO NguoiDung (HoVaTen, QuyenHan) VALUES ('Nguyễn Văn A', 'Admin')",
                "INSERT INTO NguoiDung (HoVaTen, QuyenHan) VALUES ('Trần Thị B', 'User')",
                "INSERT INTO NguoiDung (HoVaTen, QuyenHan) VALUES ('Lê Văn C', 'User')"
            };

            foreach (string sql in sampleUsers)
            {
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
            }

            // Thêm món ăn mẫu
            string[] sampleFoods = {
                "INSERT INTO MonAn (TenMonAn, HinhAnh, IDNCC) VALUES ('Phở', 'pho.jpg', 1)",
                "INSERT INTO MonAn (TenMonAn, HinhAnh, IDNCC) VALUES ('Bún chả', 'bun_cha.jpg', 1)",
                "INSERT INTO MonAn (TenMonAn, HinhAnh, IDNCC) VALUES ('Cơm tấm', 'com_tam.jpg', 2)",
                "INSERT INTO MonAn (TenMonAn, HinhAnh, IDNCC) VALUES ('Bánh mì', 'banh_mi.jpg', 2)",
                "INSERT INTO MonAn (TenMonAn, HinhAnh, IDNCC) VALUES ('Mì Quảng', 'mi_quang.jpg', 3)",
                "INSERT INTO MonAn (TenMonAn, HinhAnh, IDNCC) VALUES ('Bún bò Huế', 'bun_bo_hue.jpg', 3)",
                "INSERT INTO MonAn (TenMonAn, HinhAnh, IDNCC) VALUES ('Cao lầu', 'cao_lau.jpg', 1)",
                "INSERT INTO MonAn (TenMonAn, HinhAnh, IDNCC) VALUES ('Hủ tiếu', 'hu_tieu.jpg', 2)",
                "INSERT INTO MonAn (TenMonAn, HinhAnh, IDNCC) VALUES ('Bánh xèo', 'banh_xeo.jpg', 3)",
                "INSERT INTO MonAn (TenMonAn, HinhAnh, IDNCC) VALUES ('Gỏi cuốn', 'goi_cuon.jpg', 1)"
            };

            foreach (string sql in sampleFoods)
            {
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<MonAnInfo> GetAllMonAn()
        {
            var monAnList = new List<MonAnInfo>();
            string sql = @"
                SELECT m.IDMA, m.TenMonAn, m.HinhAnh, m.IDNCC, n.HoVaTen, n.QuyenHan
                FROM MonAn m
                LEFT JOIN NguoiDung n ON m.IDNCC = n.IDNCC
                ORDER BY m.TenMonAn";

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(sql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            monAnList.Add(new MonAnInfo
                            {
                                IDMA = reader.GetInt32(0),
                                TenMonAn = reader.GetString(1),
                                HinhAnh = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                IDNCC = reader.IsDBNull(3) ? 0 : reader.GetInt32(3),
                                HoVaTen = reader.IsDBNull(4) ? "" : reader.GetString(4),
                                QuyenHan = reader.IsDBNull(5) ? "" : reader.GetString(5)
                            });
                        }
                    }
                }
            }

            return monAnList;
        }

        public List<NguoiDungInfo> GetAllNguoiDung()
        {
            var nguoiDungList = new List<NguoiDungInfo>();
            string sql = "SELECT IDNCC, HoVaTen, QuyenHan FROM NguoiDung ORDER BY HoVaTen";

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(sql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            nguoiDungList.Add(new NguoiDungInfo
                            {
                                IDNCC = reader.GetInt32(0),
                                HoVaTen = reader.GetString(1),
                                QuyenHan = reader.GetString(2)
                            });
                        }
                    }
                }
            }

            return nguoiDungList;
        }

        public bool AddMonAn(string tenMonAn, string hinhAnh, int idNCC)
        {
            string sql = "INSERT INTO MonAn (TenMonAn, HinhAnh, IDNCC) VALUES (@tenMonAn, @hinhAnh, @idNCC)";

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@tenMonAn", tenMonAn);
                    command.Parameters.AddWithValue("@hinhAnh", hinhAnh ?? "");
                    command.Parameters.AddWithValue("@idNCC", idNCC);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool AddNguoiDung(string hoVaTen, string quyenHan)
        {
            string sql = "INSERT INTO NguoiDung (HoVaTen, QuyenHan) VALUES (@hoVaTen, @quyenHan)";

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@hoVaTen", hoVaTen);
                    command.Parameters.AddWithValue("@quyenHan", quyenHan);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public void AddSampleData()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                InsertSampleData(connection);
            }
        }

        public MonAnInfo GetRandomMonAn()
        {
            string sql = @"
                SELECT m.IDMA, m.TenMonAn, m.HinhAnh, m.IDNCC, n.HoVaTen, n.QuyenHan
                FROM MonAn m
                LEFT JOIN NguoiDung n ON m.IDNCC = n.IDNCC
                ORDER BY RANDOM()
                LIMIT 1";

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(sql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new MonAnInfo
                            {
                                IDMA = reader.GetInt32(0),
                                TenMonAn = reader.GetString(1),
                                HinhAnh = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                IDNCC = reader.IsDBNull(3) ? 0 : reader.GetInt32(3),
                                HoVaTen = reader.IsDBNull(4) ? "" : reader.GetString(4),
                                QuyenHan = reader.IsDBNull(5) ? "" : reader.GetString(5)
                            };
                        }
                    }
                }
            }

            return null;
        }

        public bool DeleteMonAn(int idMA)
        {
            string sql = "DELETE FROM MonAn WHERE IDMA = @idMA";

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@idMA", idMA);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool UpdateMonAn(int idMA, string tenMonAn, string hinhAnh, int idNCC)
        {
            string sql = "UPDATE MonAn SET TenMonAn = @tenMonAn, HinhAnh = @hinhAnh, IDNCC = @idNCC WHERE IDMA = @idMA";

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@idMA", idMA);
                    command.Parameters.AddWithValue("@tenMonAn", tenMonAn);
                    command.Parameters.AddWithValue("@hinhAnh", hinhAnh ?? "");
                    command.Parameters.AddWithValue("@idNCC", idNCC);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }
    }

    public class MonAnInfo
    {
        public int IDMA { get; set; }
        public string TenMonAn { get; set; }
        public string HinhAnh { get; set; }
        public int IDNCC { get; set; }
        public string HoVaTen { get; set; }
        public string QuyenHan { get; set; }
    }

    public class NguoiDungInfo
    {
        public int IDNCC { get; set; }
        public string HoVaTen { get; set; }
        public string QuyenHan { get; set; }
    }
}
