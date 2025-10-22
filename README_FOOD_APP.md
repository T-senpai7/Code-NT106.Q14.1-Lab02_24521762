
## Hướng dẫn truy cập và xem dữ liệu SQLite (FOOD APP)

### 1) Vị trí file database thực tế
- **Tên file**: `food_database.db`
- **Đường dẫn thực tế**: `bin/Debug/net8.0-windows/food_database.db`
- Đây là file được app cập nhật khi chạy, KHÔNG phải file ở ngoài thư mục gốc.

### 2) Mở và xem dữ liệu bằng công cụ GUI
- **DB Browser for SQLite**:
  1. Tải từ [DB Browser for SQLite](https://sqlitebrowser.org/)
  2. Mở ứng dụng → Open Database → chọn file: `bin/Debug/net8.0-windows/food_database.db`
  3. Tab "Browse Data" để xem dữ liệu bảng; tab "Execute SQL" để chạy truy vấn.

- **VS Code + SQLite Viewer**:
  1. Cài extension "SQLite Viewer" hoặc "SQLTools" từ Marketplace
  2. Mở VS Code tại thư mục `D:\lAB2`
  3. Mở file `bin/Debug/net8.0-windows/food_database.db` để duyệt bảng và chạy SQL.

### 3) Dùng dòng lệnh (sqlite3 CLI)
- Cài `sqlite3` (Windows: `choco install sqlite` hoặc tải binary từ trang SQLite)
- Mở PowerShell tại thư mục gốc dự án và chạy lệnh sau để truy cập đúng database:

```pwsh
sqlite3 .\bin\Debug\net8.0-windows\food_database.db
```


### 4) Một số lệnh SQL hữu ích trong shell `sqlite3`:
```sql
-- Liệt kê bảng
.tables

-- Xem cấu trúc bảng
.schema NguoiDung
.schema MonAn

-- Đếm số dòng
SELECT COUNT(*) FROM NguoiDung;
SELECT COUNT(*) FROM MonAn;

-- Xem dữ liệu
SELECT * FROM NguoiDung ORDER BY HoVaTen;
SELECT * FROM MonAn ORDER BY TenMonAn;

-- Join để xem đầy đủ thông tin món + người đóng góp
SELECT m.IDMA, m.TenMonAn, m.HinhAnh, n.HoVaTen, n.QuyenHan
FROM MonAn m
LEFT JOIN NguoiDung n ON m.IDNCC = n.IDNCC
ORDER BY m.TenMonAn;

-- Tìm kiếm nhanh theo tên món ăn
SELECT * FROM MonAn WHERE TenMonAn LIKE '%bún%';

-- Đếm số món ăn theo từng người đóng góp
SELECT n.HoVaTen, COUNT(m.IDMA) AS SoMon
FROM NguoiDung n
LEFT JOIN MonAn m ON n.IDNCC = m.IDNCC
GROUP BY n.HoVaTen;
