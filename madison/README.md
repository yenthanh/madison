# Veterinary Products Management System

Hệ thống quản lý sản phẩm thú y với API .NET Core 8 và Frontend Angular 18.

## Cấu trúc Project

```
madison/
├── backend/                 # .NET Core 8 API
│   ├── VeterinaryAPI/      # API project
│   └── README.md           # Hướng dẫn backend
├── frontend/               # Angular 18 Frontend
│   ├── src/               # Source code Angular
│   ├── package.json       # Dependencies
│   └── README.md          # Hướng dẫn frontend
└── README.md              # File này
```

## Backend (.NET Core 8)

### Yêu cầu hệ thống
- .NET 8 SDK
- SQL Server Express 2022 (cho production)

### Chạy Backend
```bash
cd backend/VeterinaryAPI
dotnet restore
dotnet build
dotnet run
```

### API Endpoints
- `GET /api/products/active` - Lấy sản phẩm active
- `GET /api/products/dangerous-drugs` - Lấy thuốc nguy hiểm
- `PUT /api/products/update-description` - Cập nhật mô tả
- `GET /api/products/{id}` - Lấy sản phẩm theo ID

### Swagger UI
Truy cập: `http://localhost:5042/swagger`

## Frontend (Angular 18)

### Yêu cầu hệ thống
- Node.js 18+
- npm hoặc yarn

### Chạy Frontend
```bash
cd frontend
npm install
ng serve
```

### Truy cập ứng dụng
- Frontend: `http://localhost:4200`
- Backend API: `http://localhost:5042`

## Tính năng

- ✅ API .NET Core 8 với Swagger
- ✅ Frontend Angular 18 responsive
- ✅ Quản lý sản phẩm thú y
- ✅ Lọc sản phẩm active/inactive
- ✅ Lọc thuốc nguy hiểm
- ✅ Cập nhật mô tả sản phẩm
- ✅ CORS cấu hình cho Angular
- ✅ Giao diện đẹp và modern

## Tác giả

Yen Thanh - Veterinary Products Management System 
