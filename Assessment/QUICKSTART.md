# BookStore Pro - Quick Start Guide

## 5-Minute Setup

### Prerequisites
- .NET 8 SDK installed
- SQL Server installed locally or accessible remotely
- Visual Studio, Visual Studio Code, or Rider

### Step 1: Configure Database
Edit `BookStore.API/appsettings.json`:
```json
"ConnectionStrings": {
  "BookStoreConnection": "Server=(local);Database=BookStoreDb;Trusted_Connection=true;TrustServerCertificate=true;"
}
```

### Step 2: Apply Migrations
```bash
cd BookStoreSolution
dotnet ef database update -p BookStore.Infrastructure -s BookStore.API
```

### Step 3: Run the API
```bash
cd BookStore.API
dotnet run
```

API starts at `https://localhost:5001`

### Step 4: Access Swagger UI
Open browser: `https://localhost:5001/swagger`

---

## Testing the API

### 1. Register a User
```bash
POST https://localhost:5001/api/v1/auth/register
Content-Type: application/json

{
  "fullName": "John Doe",
  "email": "john@example.com",
  "password": "Password@123",
  "confirmPassword": "Password@123",
  "phone": "9876543210"
}
```

Response includes JWT token:
```json
{
  "success": true,
  "message": "Registration successful",
  "token": "eyJhbGciOiJIUzI1NiIs...",
  "user": {
    "userId": 1,
    "fullName": "John Doe",
    "email": "john@example.com",
    "roleId": 2,
    "roleName": "Customer"
  }
}
```

### 2. Login User
```bash
POST https://localhost:5001/api/v1/auth/login
Content-Type: application/json

{
  "email": "john@example.com",
  "password": "Password@123"
}
```

### 3. Get Books (No Auth Required)
```bash
GET https://localhost:5001/api/v1/books
```

### 4. Create a Category (Admin Only)
```bash
POST https://localhost:5001/api/v1/categories
Authorization: Bearer <YOUR_TOKEN>
Content-Type: application/json

{
  "name": "Fiction"
}
```

### 5. Create an Author
```bash
POST https://localhost:5001/api/v1/authors
Authorization: Bearer <YOUR_TOKEN>
Content-Type: application/json

{
  "name": "Stephen King"
}
```

### 6. Create a Publisher
```bash
POST https://localhost:5001/api/v1/publishers
Authorization: Bearer <YOUR_TOKEN>
Content-Type: application/json

{
  "name": "Penguin Books"
}
```

### 7. Create a Book (Admin Only)
```bash
POST https://localhost:5001/api/v1/books
Authorization: Bearer <YOUR_TOKEN>
Content-Type: application/json

{
  "title": "The Shining",
  "isbn": "978-0-385-33312-0",
  "price": 15.99,
  "stock": 50,
  "categoryId": 1,
  "authorId": 1,
  "publisherId": 1,
  "imageUrl": "https://example.com/cover.jpg"
}
```

### 8. Create an Order
```bash
POST https://localhost:5001/api/v1/orders
Authorization: Bearer <YOUR_TOKEN>
Content-Type: application/json

{
  "userId": 1,
  "orderItems": [
    {
      "bookId": 1,
      "qty": 2
    }
  ]
}
```

### 9. Get User Orders
```bash
GET https://localhost:5001/api/v1/orders/user/myorders
Authorization: Bearer <YOUR_TOKEN>
```

### 10. Add Book to Wishlist
```bash
POST https://localhost:5001/api/v1/wishlist
Authorization: Bearer <YOUR_TOKEN>
Content-Type: application/json

{
  "bookId": 1
}
```

### 11. Get Wishlist
```bash
GET https://localhost:5001/api/v1/wishlist
Authorization: Bearer <YOUR_TOKEN>
```

### 12. Add Review
```bash
POST https://localhost:5001/api/v1/reviews
Authorization: Bearer <YOUR_TOKEN>
Content-Type: application/json

{
  "bookId": 1,
  "rating": 5,
  "comment": "Excellent book, highly recommend!"
}
```

---

## Database Seed Script

Create roles in database (if not present):

```bash
dotnet ef migrations add AddDefaultRoles -p BookStore.Infrastructure -s BookStore.API
```

Edit the migration file and add to `Up()` method:
```csharp
migrationBuilder.InsertData(
    table: "Roles",
    columns: new[] { "RoleName" },
    values: new object[] { "Admin" });

migrationBuilder.InsertData(
    table: "Roles",
    columns: new[] { "RoleName" },
    values: new object[] { "Customer" });
```

Then:
```bash
dotnet ef database update -p BookStore.Infrastructure -s BookStore.API
```

---

## Project Structure Reference

| Project | Purpose |
|---------|---------|
| BookStore.Domain | Entity models |
| BookStore.Application | Business logic, DTOs, Validators |
| BookStore.Infrastructure | Data access, Repositories, EF Core |
| BookStore.API | ASP.NET Core API, Controllers |
| BookStore.Web | MVC Frontend (coming soon) |
| BookStore.Shared | Shared utilities |

---

## Troubleshooting

### Connection String Error
- Verify SQL Server is running
- Check server name matches your setup
- For Azure/remote: Use `Server=tcp:servername.database.windows.net,1433`

### JWT Token Errors
- Token might be expired
- Check secret key in appsettings.json is same everywhere
- Re-register/login for new token

### Validation Errors
- Check password meets requirements: 8 chars, uppercase, lowercase, number, special char
- Ensure email format is correct
- Phone should be 10-15 digits

### Database Migration Fails
- Delete `bin` and `obj` folders
- Run `dotnet restore`
- Try migration again

---

## Common Commands

```bash
# Build solution
dotnet build

# Run tests
dotnet test

# Create migration
dotnet ef migrations add MigrationName -p BookStore.Infrastructure -s BookStore.API

# Update database
dotnet ef database update -p BookStore.Infrastructure -s BookStore.API

# Remove last migration
dotnet ef migrations remove -p BookStore.Infrastructure -s BookStore.API

# View migration SQL
dotnet ef migrations script -p BookStore.Infrastructure -s BookStore.API
```

---

## Authentication Token Usage

All protected endpoints require:
```
Authorization: Bearer <JWT_TOKEN>
```

Example in cURL:
```bash
curl -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIs..." \
     https://localhost:5001/api/v1/books
```

---

## API Versioning

Current API version: **v1**

Path: `/api/v1/{resource}`

Future versions will be: `/api/v2/{resource}`

---

## Production Checklist

- [ ] Change JWT secret key to strong random string
- [ ] Update connection string to production database
- [ ] Enable HTTPS
- [ ] Disable Swagger UI in production
- [ ] Implement actual email service
- [ ] Add request logging
- [ ] Add API rate limiting
- [ ] Configure CORS for specific origins
- [ ] Set up database backups
- [ ] Monitor error logs
- [ ] Test disaster recovery

---

## Support

For issues, check:
1. Database is running
2. Connection string is correct
3. Migrations are applied
4. All dependencies installed
5. .NET 8 is installed
