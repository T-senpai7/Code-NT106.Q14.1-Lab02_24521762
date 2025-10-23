LAB 2 - File IO trong lập trình Winforms

**GIỚI THIỆU**

Dự án **Lab 2** ứng dụng Windows Forms được viết bằng C# .NET 8.0, bao gồm các bài tập thực hành từ Bài 1 đến Bài 7 

**YÊU CẦU HỆ THỐNG**

- **.NET 8.0 SDK** hoặc mới hơn
- **Windows 10/11** (vì sử dụng Windows Forms)
- **Visual Studio 2022** hoặc **Visual Studio Code** (khuyến nghị)
- **Git** (để clone repository)
 **CÀI ĐẶT VÀ CHẠY ỨNG DỤNG**

### **1. Clone Repository**
```bash
git clone <repository-url >
cd "lAB 2"
```

### **2. Kiểm tra .NET SDK**
```bash
dotnet --version
```
*Kết quả mong đợi: 8.0.x hoặc mới hơn*

### **3. Khôi phục Dependencies**
```bash
dotnet restore
```

### **4. Build Ứng Dụng**
```bash
# Build cho Debug mode
dotnet build
```

### **5. Chạy Ứng Dụng**
```bash
# Chạy trực tiếp từ source code
dotnet run
```

### **6. Chạy từ Executable**
```bash
# Sau khi build, chạy file .exe
cd bin\Debug\net8.0-windows
WindowsFormsApp.exe

# Hoặc từ Release build
cd bin\Release\net8.0-windows
WindowsFormsApp.exe
```
 **CÁC BÀI TẬP TRONG ỨNG DỤNG**

| Bài | Tên Form | Chức năng chính |
|-----|----------|-----------------|
| **Bài 1** | `Bai1Form.cs` | Đọc chép file |
| **Bài 2** | `Bai2Form.cs` | Đọc file nâng cao |
| **Bài 3** | `Bai3Form.cs` | Tínhftoans file input output thuật toán |
| **Bài 4** | `Bai4Form.cs` | Quản lý điểm |
| **Bài 5** | `Bai5Form.cs` | Ticket Seller ver 2|
| **Bài 6** | `Bai6Form.cs` | Tương tác đồ ăn ver 2  |
| **Bài 7** | `Bai7Form.cs` | Quản lý file tree |

 **CÁC LỆNH DOTNET HỮU ÍCH**

### **Build Commands**
```bash
# Build với thông tin chi tiết
dotnet build --verbosity normal

# Build và bỏ qua cảnh báo
dotnet build --verbosity quiet

# Build cho platform cụ thể
dotnet build --runtime win-x64

# Build và publish
dotnet publish --configuration Release --runtime win-x64 --self-contained true
```

### **Clean Commands**
```bash
# Xóa các file build
dotnet clean

# Xóa hoàn toàn (bao gồm obj, bin)
dotnet clean --verbosity detailed
```

### **Debug Commands**
```bash
# Chạy với debugger
dotnet run --configuration Debug

# Chạy với profiling
dotnet run --configuration Release --verbosity diagnostic

# Chạy với environment variables
dotnet run --environment Development
```

### **Lỗi thường gặp:**

#### **1. .NET SDK không tìm thấy**
```bash
# Cài đặt .NET 8.0 SDK từ: https://dotnet.microsoft.com/download
dotnet --info
```

#### **2. Lỗi build**
```bash
# Xóa cache và rebuild
dotnet clean
dotnet restore
dotnet build
```

#### **3. Lỗi Windows Forms không chạy**
```bash
# Kiểm tra target framework
dotnet --list-sdks
dotnet --list-runtimes
```

#### **4. Lỗi permissions**
```bash
# Chạy PowerShell as Administrator
Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser
```


_________________________________________________
## Hướng dẫn truy cập và xem dữ liệu SQLite (FOOD APP)

### 1) Vị trí file database
- **Tên file**: `s`
- **Đường dẫn**: cùng thư mục với solution/app (`D:\lAB2\food_database.db`)
- File sẽ được tạo tự động nếu chưa tồn tại khi app chạy lần đầu.

### 2) Mở và xem dữ liệu bằng công cụ GUI (khuyến nghị)
- **DB Browser for SQLite** (miễn phí, dễ dùng)
  1. Cài đặt từ trang chủ: [DB Browser for SQLite](https://sqlitebrowser.org/)
  2. Mở ứng dụng → Open Database → chọn `food_database.db`
  3. Tab "Browse Data" để xem dữ liệu bảng; tab "Execute SQL" để chạy truy vấn.

- **VS Code + SQLite Viewer** (nếu dùng VS Code)
  1. Cài tiện ích "SQLite Viewer" (hoặc "SQLite"/"SQLTools") từ Marketplace
  2. Mở VS Code tại thư mục `D:\lAB2`
  3. Mở file `food_database.db` → dùng viewer để duyệt bảng và chạy SQL.

### 3) Dùng dòng lệnh (sqlite3 CLI)
- Cài `sqlite3` (trên Windows có thể dùng `choco install sqlite` nếu có Chocolatey, hoặc tải binary từ trang SQLite).
- Mở PowerShell tại `D:\lAB2` và chạy:

```bash
sqlite3 .\food_database.db
(có 2 file nhưng file được cập nhật là file trong thư mục bin)
```

