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
7. 