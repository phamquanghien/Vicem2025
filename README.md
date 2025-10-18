1. Môi trường lập trình
   - C#, .Net
   - IDE/Code Editor: Visual Studio Code/Visual Studio (C# Dev Kit, C#, IntelliCode for C# Dev Kit...)
   - Git + Github (git add, git commit, git push, git pull, git branch...)
2. Nội dung học
   - Giới thiệu về .NET và Quản lý mã nguồn với Git/GitHub
   - C# cơ bản, C# nâng cao
   - HTML, CSS
   - C# MVC, EntityFramework, LinQ
3. Tạo project trong .net:
   - Sử dụng lệnh "dotnet new list" để xem toàn bộ danh sách các mẫu có thể dùng với dotnet new
   - Sử dụng lệnh "dotnet new mvc -o PROJECT_NAME" để tạo mới dự án .Net MVC, trong đó PROJECT_NAME là tên của dự án.
   - Sử dụng lệnh "dotnet new mvc -h" để xem toàn bộ lựa chọn thêm cho mẫu của dự án .Net MVC
4. Triển khai project với docker
   - Tạo docker file
   - Tạo image: docker build -t demo-mvc-image:v1.0 .
   - Tạo container:  docker run -d -p 8080:80 --name mvc_container -e ASPNETCORE_URLS="http://+:80" demo-mvc-image:v1.0
5. Triển khai SQL Server với docker:
   - Tải về image: 'docker pull mcr.microsoft.com/mssql/server:2019-latest' hoặc 'docker pull mcr.microsoft.com/mssql/server:2022-latest'
   - Tạo container: 'docker run -d --name sql_server_container -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourStrong!Passw0rd" -p 1433:1433 mcr.microsoft.com/mssql/server:2019-latest' hoặc 'docker run -d --name sql_server_container -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourStrong!Passw0rd" -p 1433:1433 mcr.microsoft.com/mssql/server:2022-latest'
6. Điều hướng trong .Net MVC
   - Cấu hình trong file Program.cs
   - Sử dụng [Route("")] để điều hướng trong controller
7. Cấu hình kết nối với cơ sở dữ liệu: Program.cs, appsettings.json, ApplicationDbContext
8. Sử dụng migrations để quản lý các phiên bản của cơ sở dữ liệu với 2 lệnh sau:
   - Lưu thay đổi cấu trúc CSDL vào migrations: dotnet ef migrations add MIGRATION_NOTE
   - Lưu thay đổi migrations và CSDL: dotnet ef database update
9. Sinh mã CRUD dựa trên class model: dotnet aspnet-codegenerator controller -name CourseController -m Course -dc DemoMVC.Data.ApplicationDbContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries --databaseProvider sqlite
10. Bài toán quản lý học viên, nghiệp vụ bài toán như sau:
   - Bước 1: Tạo khoá học, một khoá học có thể có nhiều đợt học, một đợt học có nhiều buổi học
   - Bước 2: Nhập danh sách học viên tham gia khoá học
   - Bước 3: Điểm danh học viên theo các buổi học
   - Bước 4: Nhập kết quả khoá học
   - CSDL bao gồm các bảng: Course, Batch, Session, Trainee, Attendance, Registration và Evaluation
11. 