# BookStore Pro - API Documentation

## Base URL
```
https://localhost:5001/api/v1
```

## Authentication
Include JWT token in header for protected endpoints:
```
Authorization: Bearer <token>
```

---

## Authentication Endpoints

### Register User
- **URL**: `/auth/register`
- **Method**: `POST`
- **Auth**: None
- **Request Body**:
```json
{
  "fullName": "string (3-100 chars)",
  "email": "string (valid email)",
  "password": "string (8-50 chars, must include uppercase, lowercase, number, special char)",
  "confirmPassword": "string (must match password)",
  "phone": "string (10-15 digits)"
}
```
- **Response** (201):
```json
{
  "success": true,
  "message": "Registration successful",
  "token": "jwt_token_string",
  "user": {
    "userId": 1,
    "fullName": "John Doe",
    "email": "john@example.com",
    "phone": "9876543210",
    "roleId": 2,
    "roleName": "Customer"
  }
}
```

### Login User
- **URL**: `/auth/login`
- **Method**: `POST`
- **Auth**: None
- **Request Body**:
```json
{
  "email": "string",
  "password": "string"
}
```
- **Response** (200):
```json
{
  "success": true,
  "message": "Login successful",
  "token": "jwt_token_string",
  "user": {
    "userId": 1,
    "fullName": "John Doe",
    "email": "john@example.com",
    "phone": "9876543210",
    "roleId": 2,
    "roleName": "Customer"
  }
}
```

---

## Books Endpoints

### Get All Books
- **URL**: `/books`
- **Method**: `GET`
- **Auth**: None
- **Query Parameters**: None
- **Response** (200):
```json
[
  {
    "bookId": 1,
    "title": "The Shining",
    "isbn": "978-0-385-33312-0",
    "price": 15.99,
    "stock": 50,
    "imageUrl": "https://example.com/cover.jpg",
    "categoryId": 1,
    "categoryName": "Fiction",
    "authorId": 1,
    "authorName": "Stephen King",
    "publisherId": 1,
    "publisherName": "Penguin Books"
  }
]
```

### Get Book by ID
- **URL**: `/books/{id}`
- **Method**: `GET`
- **Auth**: None
- **URL Parameters**: 
  - `id`: Integer (Book ID)
- **Response** (200): Single book object (same as Get All Books)

### Get Books by Category
- **URL**: `/books/category/{categoryId}`
- **Method**: `GET`
- **Auth**: None
- **URL Parameters**:
  - `categoryId`: Integer
- **Response** (200): Array of book objects

### Search Books
- **URL**: `/books/search/{keyword}`
- **Method**: `GET`
- **Auth**: None
- **URL Parameters**:
  - `keyword`: String (searches in title and ISBN)
- **Response** (200): Array of matching book objects

### Create Book
- **URL**: `/books`
- **Method**: `POST`
- **Auth**: Required (Admin role)
- **Request Body**:
```json
{
  "title": "string (3-200 chars, required)",
  "isbn": "string (10-20 chars, required, must be unique)",
  "price": "decimal (> 0, required)",
  "stock": "integer (>= 0, required)",
  "imageUrl": "string (optional)",
  "categoryId": "integer (required)",
  "authorId": "integer (required)",
  "publisherId": "integer (required)"
}
```
- **Response** (201): Created book object
- **Errors**:
  - 400: Validation failed
  - 401: Unauthorized
  - 403: Forbidden (not admin)

### Update Book
- **URL**: `/books/{id}`
- **Method**: `PUT`
- **Auth**: Required (Admin role)
- **URL Parameters**:
  - `id`: Integer (Book ID)
- **Request Body**: Same as Create Book + bookId field
- **Response** (204): No content
- **Errors**:
  - 400: ID mismatch or validation failed
  - 401: Unauthorized
  - 403: Forbidden (not admin)

### Delete Book
- **URL**: `/books/{id}`
- **Method**: `DELETE`
- **Auth**: Required (Admin role)
- **URL Parameters**:
  - `id`: Integer (Book ID)
- **Response** (204): No content
- **Errors**:
  - 401: Unauthorized
  - 403: Forbidden (not admin)

---

## Orders Endpoints

### Get Order
- **URL**: `/orders/{id}`
- **Method**: `GET`
- **Auth**: Required
- **URL Parameters**:
  - `id`: Integer (Order ID)
- **Response** (200):
```json
{
  "orderId": 1,
  "userId": 1,
  "orderDate": "2024-01-15T10:30:00Z",
  "totalAmount": 31.98,
  "status": "Pending",
  "orderItems": [
    {
      "orderItemId": 1,
      "bookId": 1,
      "bookTitle": "The Shining",
      "qty": 2,
      "price": 15.99
    }
  ]
}
```

### Get User's Orders
- **URL**: `/orders/user/myorders`
- **Method**: `GET`
- **Auth**: Required
- **Response** (200): Array of order objects

### Create Order
- **URL**: `/orders`
- **Method**: `POST`
- **Auth**: Required
- **Request Body**:
```json
{
  "userId": "integer (auto-set from token)",
  "orderItems": [
    {
      "bookId": "integer (required)",
      "qty": "integer (> 0, required)"
    }
  ]
}
```
- **Response** (201): Created order object
- **Validations**:
  - Minimum 1 item required
  - Stock availability checked
  - Prices retrieved from current book data
- **Side Effects**:
  - Decreases book stock
  - Sends confirmation email
  - Logs email sent

### Update Order Status
- **URL**: `/orders/status`
- **Method**: `PUT`
- **Auth**: Required (Admin role)
- **Request Body**:
```json
{
  "orderId": "integer (required)",
  "status": "string (one of: Pending, Confirmed, Shipped, Delivered, Cancelled)"
}
```
- **Response** (204): No content
- **Errors**:
  - 400: Invalid status
  - 401: Unauthorized
  - 403: Forbidden (not admin)

---

## Categories Endpoints

### Get All Categories
- **URL**: `/categories`
- **Method**: `GET`
- **Auth**: None
- **Response** (200):
```json
[
  {
    "categoryId": 1,
    "name": "Fiction"
  }
]
```

### Get Category
- **URL**: `/categories/{id}`
- **Method**: `GET`
- **Auth**: None
- **Response** (200): Single category object

### Create Category
- **URL**: `/categories`
- **Method**: `POST`
- **Auth**: Required (Admin role)
- **Request Body**:
```json
{
  "name": "string (2-100 chars, required)"
}
```
- **Response** (201): Created category object

### Update Category
- **URL**: `/categories/{id}`
- **Method**: `PUT`
- **Auth**: Required (Admin role)
- **Request Body**: Same as Create Category
- **Response** (204): No content

### Delete Category
- **URL**: `/categories/{id}`
- **Method**: `DELETE`
- **Auth**: Required (Admin role)
- **Response** (204): No content

---

## Authors Endpoints

### Get All Authors
- **URL**: `/authors`
- **Method**: `GET`
- **Auth**: None
- **Response** (200):
```json
[
  {
    "authorId": 1,
    "name": "Stephen King"
  }
]
```

### Get Author
- **URL**: `/authors/{id}`
- **Method**: `GET`
- **Auth**: None
- **Response** (200): Single author object

### Create Author
- **URL**: `/authors`
- **Method**: `POST`
- **Auth**: Required (Admin role)
- **Request Body**:
```json
{
  "name": "string (3-100 chars, required)"
}
```
- **Response** (201): Created author object

### Update Author
- **URL**: `/authors/{id}`
- **Method**: `PUT`
- **Auth**: Required (Admin role)
- **Request Body**: Same as Create Author
- **Response** (204): No content

### Delete Author
- **URL**: `/authors/{id}`
- **Method**: `DELETE`
- **Auth**: Required (Admin role)
- **Response** (204): No content

---

## Publishers Endpoints

### Get All Publishers
- **URL**: `/publishers`
- **Method**: `GET`
- **Auth**: None
- **Response** (200):
```json
[
  {
    "publisherId": 1,
    "name": "Penguin Books"
  }
]
```

### Get Publisher
- **URL**: `/publishers/{id}`
- **Method**: `GET`
- **Auth**: None
- **Response** (200): Single publisher object

### Create Publisher
- **URL**: `/publishers`
- **Method**: `POST`
- **Auth**: Required (Admin role)
- **Request Body**:
```json
{
  "name": "string (2-100 chars, required)"
}
```
- **Response** (201): Created publisher object

### Update Publisher
- **URL**: `/publishers/{id}`
- **Method**: `PUT`
- **Auth**: Required (Admin role)
- **Request Body**: Same as Create Publisher
- **Response** (204): No content

### Delete Publisher
- **URL**: `/publishers/{id}`
- **Method**: `DELETE`
- **Auth**: Required (Admin role)
- **Response** (204): No content

---

## Reviews Endpoints

### Get Book Reviews
- **URL**: `/reviews/book/{bookId}`
- **Method**: `GET`
- **Auth**: None
- **URL Parameters**:
  - `bookId`: Integer
- **Response** (200):
```json
[
  {
    "reviewId": 1,
    "userId": 1,
    "bookId": 1,
    "rating": 5,
    "comment": "Excellent book!"
  }
]
```

### Create Review
- **URL**: `/reviews`
- **Method**: `POST`
- **Auth**: Required
- **Request Body**:
```json
{
  "bookId": "integer (required)",
  "rating": "integer (1-5, required)",
  "comment": "string (max 500 chars, optional)"
}
```
- **Response** (201): Created review object

### Delete Review
- **URL**: `/reviews/{id}`
- **Method**: `DELETE`
- **Auth**: Required
- **URL Parameters**:
  - `id`: Integer (Review ID)
- **Response** (204): No content

---

## Wishlist Endpoints

### Get User Wishlist
- **URL**: `/wishlist`
- **Method**: `GET`
- **Auth**: Required
- **Response** (200):
```json
[
  {
    "userId": 1,
    "bookId": 1,
    "bookTitle": "The Shining"
  }
]
```

### Add to Wishlist
- **URL**: `/wishlist`
- **Method**: `POST`
- **Auth**: Required
- **Request Body**:
```json
{
  "bookId": "integer (required)"
}
```
- **Response** (201): No content
- **Note**: Duplicate entries are ignored

### Remove from Wishlist
- **URL**: `/wishlist/{bookId}`
- **Method**: `DELETE`
- **Auth**: Required
- **URL Parameters**:
  - `bookId`: Integer
- **Response** (204): No content

---

## Error Responses

### Validation Error (400)
```json
{
  "errors": {
    "fieldName": ["Error message 1", "Error message 2"]
  }
}
```

### Unauthorized (401)
```json
{
  "message": "Unauthorized"
}
```

### Forbidden (403)
```json
{
  "message": "Forbidden"
}
```

### Not Found (404)
```json
{
  "message": "Resource not found"
}
```

### Server Error (500)
```json
{
  "status": 500,
  "message": "An internal server error occurred"
}
```

---

## Status Codes

| Code | Meaning |
|------|---------|
| 200 | OK |
| 201 | Created |
| 204 | No Content |
| 400 | Bad Request |
| 401 | Unauthorized |
| 403 | Forbidden |
| 404 | Not Found |
| 500 | Internal Server Error |

---

## Rate Limiting

Currently: **No rate limiting** (to be added in production)

---

## Pagination

Currently: **Not implemented** (returns all records)

To be added in future versions: `?page=1&pageSize=20`

---

## Filters & Sorting

Currently: **Not implemented** (can be added per requirement)

---

## API Versioning Strategy

- Current: **v1**
- Path: `/api/v1/{endpoint}`
- Future versions: `/api/v2/{endpoint}`
