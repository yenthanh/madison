# Veterinary API - Main Project

## Overview
This is the main API project that serves as the entry point for the Veterinary Products Management System. It implements the API layer of Clean Architecture.

## Architecture Role
- **API Layer**: HTTP endpoints, controllers, middleware
- **Configuration**: Dependency injection, CORS, Swagger
- **Entry Point**: Application startup and configuration

## Key Components

### Controllers
- `ProductsController`: REST API endpoints for product management
  - `GET /api/products/active` - Get active products
  - `GET /api/products/dangerous-drugs` - Get dangerous drugs
  - `PUT /api/products/update-description` - Update product description
  - `GET /api/products/{id}` - Get product by ID

### Configuration
- **MediatR**: CQRS pattern implementation
- **Entity Framework**: Database context registration
- **Swagger**: API documentation
- **CORS**: Cross-origin resource sharing
- **Dependency Injection**: Service registration

## Dependencies
- `VeterinaryAPI.Application`: Use cases, DTOs, interfaces
- `VeterinaryAPI.Infrastructure`: Data access, repositories

## How to Run
```bash
cd VeterinaryAPI
dotnet restore
dotnet build
dotnet run
```

## API Documentation
- Swagger UI: `https://localhost:7001/swagger`
- Health Check: `https://localhost:7001/health`

## Environment Variables
- `ConnectionStrings:DefaultConnection`: SQL Server connection string
- `Logging:LogLevel`: Logging configuration
- `AllowedHosts`: CORS configuration 
