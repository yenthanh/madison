# Veterinary Products API

API .NET Core 8 để quản lý sản phẩm thú y.

## Cài đặt và chạy

1. **Yêu cầu hệ thống:**
   - .NET 8 SDK
   - SQL Server Express 2022 (cho production)

2. **Chạy API:**
   ```bash
   cd VeterinaryAPI
   dotnet restore
   dotnet build
   dotnet run
   ```

3. **Truy cập Swagger UI:**
   - Mở trình duyệt và truy cập: `https://localhost:7001` hoặc `http://localhost:5000`
   - Swagger UI sẽ hiển thị tại: `/swagger`

## API Endpoints

### 1. Lấy tất cả sản phẩm active
```
GET /api/products/active
```
- Trả về danh sách sản phẩm active và không bị xóa
- Sắp xếp theo thời gian tạo mới nhất

### 2. Lấy thuốc nguy hiểm
```
GET /api/products/dangerous-drugs
```
- Trả về danh sách thuốc nguy hiểm
- Loại trừ sản phẩm đã xóa hoặc không active

### 3. Cập nhật mô tả sản phẩm
```
PUT /api/products/update-description
```
Body:
```json
{
  "productId": 1,
  "description": "Mô tả mới"
}
```

### 4. Lấy sản phẩm theo ID
```
GET /api/products/{id}
```

## Cấu trúc dữ liệu

### Product Model
```json
{
  "id": 1,
  "name": "Tên sản phẩm",
  "description": "Mô tả sản phẩm",
  "category": "Danh mục",
  "price": 25.99,
  "isActive": true,
  "isDeleted": false,
  "isDangerousDrug": false,
  "createdAt": "2024-01-01T00:00:00Z",
  "updatedAt": "2024-01-01T00:00:00Z"
}
```

## Dữ liệu mẫu

API hiện tại sử dụng dữ liệu mẫu với 6 sản phẩm:
- Amoxicillin 250mg (Antibiotic)
- Morphine Sulfate 10mg (Thuốc nguy hiểm)
- Ivermectin 1% (Anti-parasitic)
- Ketamine 100mg/ml (Thuốc nguy hiểm)
- Vitamin B Complex (Không active)
- Expired Product (Đã xóa)

## CORS

API đã được cấu hình CORS để cho phép frontend Angular truy cập từ bất kỳ origin nào. 
