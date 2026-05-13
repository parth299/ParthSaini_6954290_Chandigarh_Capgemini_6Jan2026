# BookStore Pro Enterprise Solution - Backend API

A complete, production-ready ASP.NET Core 8 backend API for an enterprise bookstore system with a clean 3-tier architecture.

## Project Overview

**BookStore Pro Pvt Ltd** needed a modern, scalable online bookstore platform. This solution provides:
- Complete inventory management
- Order processing and tracking
- Customer reviews and wishlists
- Admin dashboard capabilities
- Secure JWT authentication
- RESTful API with versioning

## Solution Architecture (3-Tier)

```
Presentation Layer (BookStore.Web - MVC Frontend)
         ↓ HttpClient
Business Layer (BookStore.API + BookStore.Application)
         ↓
Data Access Layer (BookStore.Infrastructure + EF Core)
         ↓
SQL Server Database
```

## Project Structure

```
BookStoreSolution/
├── BookStore.Domain/                 # Entity definitions
│   └── Entities/
│       ├── Role.cs
│       ├── User.cs
│       ├── UserProfile.cs
│       ├── Book.cs
│       ├── Category.cs
│       ├── Author.cs
│       ├── Publisher.cs
│       ├── Order.cs
│       ├── OrderItem.cs
│       ├── Review.cs
│       ├── Wishlist.cs
│       └── EmailLog.cs
│
├── BookStore.Application/            # Business logic & DTOs
│   ├── DTOs/
│   │   ├── UserDtos.cs
│   │   ├── BookDtos.cs
│   │   ├── CatalogDtos.cs
│   │   ├── OrderDtos.cs
│   │   └── ReviewWishlistDtos.cs
│   ├── Services/
│   │   ├── AuthService.cs
│   │   ├── TokenService.cs
│   │   ├── BookService.cs
│   │   ├── OrderService.cs
│   │   ├── ReviewService.cs
│   │   ├── WishlistService.cs
│   │   ├── CatalogServices.cs
│   │   └── EmailService.cs
│   ├── Validators/
│   │   ├── UserValidators.cs
│   │   ├── CatalogValidators.cs
│   │   └── OrderReviewValidators.cs
│   ├── Interfaces/
│   │   ├── IGenericRepository.cs
│   │   ├── IRepositories.cs
│   │   └── IServices.cs
│   └── MappingProfiles/
│       └── MappingProfile.cs
│
├── BookStore.Infrastructure/         # Data access layer
│   ├── Data/
│   │   ├── BookStoreDbContext.cs
│   │   └── Migrations/
│   ├── Repositories/
│   │   ├── GenericRepository.cs
│   │   └── RepositoryImplementations.cs
│   └── Identity/
│
├── BookStore.API/                    # Web API
│   ├── Controllers/
│   │   ├── AuthController.cs
│   │   ├── BooksController.cs
│   │   ├── OrdersController.cs
│   │   ├── CategoriesController.cs
│   │   ├── AuthorsController.cs
│   │   ├── PublishersController.cs
│   │   ├── ReviewsController.cs
│   │   └── WishlistController.cs
│   ├── Middleware/
│   │   └── GlobalExceptionMiddleware.cs
│   ├── Extensions/
│   │   └── ServiceExtensions.cs
│   ├── Program.cs
│   ├── appsettings.json
│   └── appsettings.Development.json
│
├── BookStore.Web/                    # MVC Frontend (Pending)
│
└── BookStore.Shared/                 # Shared utilities
```

## Database Schema

### Tables:
- **Role** - User roles (Admin, Customer)
- **User** - Customer and admin accounts
- **UserProfile** - User address and contact info
- **Category** - Book categories
- **Author** - Author information
- **Publisher** - Publisher information
- **Book** - Book catalog
- **Order** - Customer orders
- **OrderItem** - Individual items in orders
- **Review** - Customer product reviews
- **Wishlist** - User wishlists
- **EmailLog** - Email tracking

### Relationships:
- **One-to-One**: User ↔ UserProfile
- **One-to-Many**: Role → Users, Category → Books, Author → Books, Publisher → Books, User → Orders, Order → OrderItems
- **Many-to-Many**: User ↔ Book (via Wishlist), User ↔ Book (via Review)

## API Endpoints

### Authentication (v1)
- `POST /api/v1/auth/login` - User login
- `POST /api/v1/auth/register` - User registration

### Books (v1)
- `GET /api/v1/books` - Get all books
- `GET /api/v1/books/{id}` - Get book details
- `GET /api/v1/books/category/{categoryId}` - Get books by category
- `GET /api/v1/books/search/{keyword}` - Search books
- `POST /api/v1/books` - Create book (Admin only)
- `PUT /api/v1/books/{id}` - Update book (Admin only)
- `DELETE /api/v1/books/{id}` - Delete book (Admin only)

### Orders (v1)
- `GET /api/v1/orders/{id}` - Get order details
- `GET /api/v1/orders/user/myorders` - Get user's orders
- `POST /api/v1/orders` - Create order
- `PUT /api/v1/orders/status` - Update order status (Admin only)

### Categories (v1)
- `GET /api/v1/categories` - Get all categories
- `GET /api/v1/categories/{id}` - Get category details
- `POST /api/v1/categories` - Create category (Admin only)
- `PUT /api/v1/categories/{id}` - Update category (Admin only)
- `DELETE /api/v1/categories/{id}` - Delete category (Admin only)

### Authors (v1)
- `GET /api/v1/authors` - Get all authors
- `GET /api/v1/authors/{id}` - Get author details
- `POST /api/v1/authors` - Create author (Admin only)
- `PUT /api/v1/authors/{id}` - Update author (Admin only)
- `DELETE /api/v1/authors/{id}` - Delete author (Admin only)

### Publishers (v1)
- `GET /api/v1/publishers` - Get all publishers
- `GET /api/v1/publishers/{id}` - Get publisher details
- `POST /api/v1/publishers` - Create publisher (Admin only)
- `PUT /api/v1/publishers/{id}` - Update publisher (Admin only)
- `DELETE /api/v1/publishers/{id}` - Delete publisher (Admin only)

### Reviews (v1)
- `GET /api/v1/reviews/book/{bookId}` - Get book reviews
- `POST /api/v1/reviews` - Create review
- `DELETE /api/v1/reviews/{id}` - Delete review

### Wishlist (v1)
- `GET /api/v1/wishlist` - Get user's wishlist
- `POST /api/v1/wishlist` - Add to wishlist
- `DELETE /api/v1/wishlist/{bookId}` - Remove from wishlist

## Key Technologies

- **Framework**: ASP.NET Core 8
- **ORM**: Entity Framework Core 8
- **Database**: SQL Server
- **Authentication**: JWT (JSON Web Tokens)
- **Validation**: FluentValidation
- **Mapping**: AutoMapper
- **Password Hashing**: BCrypt
- **Security**: Role-based authorization

## Key Features Implemented

### ✅ Authentication & Security
- JWT token generation and validation
- Role-based access control (Admin/Customer)
- Password hashing with BCrypt
- Secure claims-based authorization

### ✅ Validation
- Client-side validation DTOs
- Server-side FluentValidation rules
- Email format validation
- Password strength requirements
- Stock validation for orders

### ✅ Business Logic
- Order processing with automatic stock reduction
- Review and rating system
- Wishlist management
- Email notifications (placeholder)
- Generic repository pattern

### ✅ API Features
- RESTful endpoints
- Global exception handling middleware
- CORS enabled for frontend integration
- Automatic JSON response formatting
- Proper HTTP status codes

### ✅ Database
- Code-first migrations
- Fluent API configuration
- Relationship constraints
- Unique indices (Email, ISBN)
- Decimal precision for prices

## Setup Instructions

### Prerequisites
- .NET 8 SDK
- SQL Server (local or remote)
- Visual Studio or VS Code

### Installation Steps

1. **Clone or extract the project**
   ```bash
   cd BookStoreSolution
   ```

2. **Update database connection string**
   
   Edit `BookStore.API/appsettings.json`:
   ```json
   "ConnectionStrings": {
     "BookStoreConnection": "Server=YOUR_SERVER;Database=BookStoreDb;Trusted_Connection=true;TrustServerCertificate=true;"
   }
   ```

3. **Update JWT Secret Key**
   
   Edit `BookStore.API/appsettings.json`:
   ```json
   "JwtSettings": {
     "SecretKey": "your-very-long-secret-key-at-least-32-characters"
   }
   ```

4. **Apply EF Core migrations**
   ```bash
   dotnet ef database update -p BookStore.Infrastructure -s BookStore.API
   ```

5. **Build the solution**
   ```bash
   dotnet build
   ```

6. **Run the API**
   ```bash
   cd BookStore.API
   dotnet run
   ```

   API will be available at: `https://localhost:5001` or `http://localhost:5000`

7. **View Swagger UI**
   
   Navigate to: `https://localhost:5001/swagger`

## Sample Data Initialization

To seed the database with initial roles:

```csharp
// Add to Program.cs before app.Run()
using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<BookStoreDbContext>();

// Add default roles if they don't exist
if (!context.Roles.Any())
{
    context.Roles.AddRange(
        new Role { RoleName = "Admin" },
        new Role { RoleName = "Customer" }
    );
    context.SaveChanges();
}
```

## Authentication Flow

1. User registers with email, password, and phone
2. Password is hashed with BCrypt
3. User login validates credentials
4. JWT token is generated with claims
5. Client sends token in Authorization header
6. API validates token and extracts claims
7. Role-based authorization checks

## Validation Rules

### Password Requirements
- Minimum 8 characters
- At least 1 uppercase letter
- At least 1 lowercase letter
- At least 1 number
- At least 1 special character (!@#$%^&*)

### Book Validation
- Title: Required, 3-200 characters
- ISBN: Required, 10-20 characters, unique
- Price: Required, > 0
- Stock: Required, >= 0
- Category, Author, Publisher: Required

### Order Validation
- Minimum 1 item required
- Sufficient stock available
- Valid user and book IDs

## Error Handling

All exceptions are caught by the global exception middleware which returns:
```json
{
  "status": 500,
  "message": "Error description"
}
```

Common status codes:
- 200 OK - Success
- 201 Created - Resource created
- 204 No Content - Success (no response body)
- 400 Bad Request - Validation error
- 401 Unauthorized - Invalid credentials
- 404 Not Found - Resource not found
- 500 Internal Server Error - Server error

## Service Layer

### AuthService
- User registration with validation
- User login with password verification
- Token generation

### BookService
- CRUD operations for books
- Search and filter functionality
- Category-based retrieval

### OrderService
- Order creation with stock management
- Order status updates
- User order history

### ReviewService
- Review creation and retrieval
- Rating calculations

### WishlistService
- Add/remove from wishlist
- Retrieve user wishlists

### CatalogServices
- Category, Author, Publisher management

### EmailService
- Order confirmation emails
- Invoice generation (placeholder)
- Stock alerts

## Repository Pattern

All data access follows the repository pattern:
- **IGenericRepository<T>** - Base interface for CRUD operations
- **Specific Repositories** - Inherit from generic repository with custom queries

## NuGet Packages

```
Microsoft.EntityFrameworkCore.SqlServer (8.0.6)
Microsoft.EntityFrameworkCore.Design (8.0.6)
System.IdentityModel.Tokens.Jwt (7.1.2)
Microsoft.AspNetCore.Authentication.JwtBearer (8.0.6)
AutoMapper (12.0.1)
AutoMapper.Extensions.Microsoft.DependencyInjection (12.0.1)
FluentValidation (11.9.2)
FluentValidation.AspNetCore (11.3.0)
BCrypt.Net-Next (4.0.3)
```

## Next Steps - Frontend Development

The MVC frontend (BookStore.Web) will include:
- Login/Register pages
- Book catalog with search
- Shopping cart functionality
- Checkout process
- Order history
- Review submission
- Wishlist management
- Admin dashboard

## Notes

- No cloud services are used (local SQL Server only)
- No integration testing is included
- Email service is a placeholder (not actually sending emails)
- Production deployment requires:
  - SSL certificate
  - Secure JWT secret management
  - Environment-specific configurations
  - Actual email service integration
  - Database backups
  - API rate limiting
  - Request logging
  - Monitoring and alerts

## License

Proprietary - BookStore Pro Pvt Ltd

## Support

For issues or questions, contact the development team.
