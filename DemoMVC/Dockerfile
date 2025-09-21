# Giai đoạn 1: Xây dựng ứng dụng
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Sao chép tệp .csproj và khôi phục các gói NuGet
COPY *.csproj .
RUN dotnet restore

# Sao chép toàn bộ mã nguồn
COPY . .

# Publish ứng dụng vào một thư mục
RUN dotnet publish -c Release -o /app/publish

# Giai đoạn 2: Tạo ảnh cuối cùng để chạy
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Sao chép các tệp đã publish từ giai đoạn 'build'
COPY --from=build /app/publish .

# Mở cổng 80 cho web server
EXPOSE 80

# Đặt lệnh mặc định để khởi chạy ứng dụng
ENTRYPOINT ["dotnet", "DemoMVC.dll"]