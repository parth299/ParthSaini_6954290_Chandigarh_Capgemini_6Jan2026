# Case Study 1 — Product Inventory API (.NET 10)

## Project Structure

```
InventoryAPI/
├── InventoryAPI.sln
├── README.md
│
├── src/
│   └── InventoryAPI/                          # ASP.NET Core Web API
│       ├── InventoryAPI.csproj
│       ├── Program.cs
│       ├── Controllers/
│       │   └── ProductController.cs           # GET /api/product/{id}
│       ├── Models/
│       │   └── Product.cs                     # Product entity
│       └── Services/
│           ├── IProductService.cs             # Abstraction (what we mock)
│           └── ProductService.cs              # In-memory implementation
│
└── tests/
    ├── InventoryAPI.Tests.xUnit/              # xUnit + Moq tests
    │   ├── InventoryAPI.Tests.xUnit.csproj
    │   └── ProductControllerTests.cs
    └── InventoryAPI.Tests.NUnit/              # NUnit + Moq tests
        ├── InventoryAPI.Tests.NUnit.csproj
        └── ProductControllerTests.cs
```

---

## Prerequisites

| Tool        | Version       |
|-------------|---------------|
| .NET SDK    | **10.0+**     |
| IDE         | VS 2022 / Rider / VS Code |

---

## How to Run

### 1. Restore & Build
```bash
cd InventoryAPI
dotnet restore
dotnet build
```

### 2. Run the API
```bash
dotnet run --project src/InventoryAPI
# Swagger UI → https://localhost:{port}/swagger
```

### 3. Run All Tests
```bash
dotnet test
```

### 4. Run Only xUnit Tests
```bash
dotnet test tests/InventoryAPI.Tests.xUnit
```

### 5. Run Only NUnit Tests
```bash
dotnet test tests/InventoryAPI.Tests.NUnit
```

---

## API Endpoint

| Method | Route                | Description           |
|--------|----------------------|-----------------------|
| GET    | `/api/product/{id}`  | Get product by ID     |

**Responses:**
- `200 OK` — Product found → returns product JSON
- `404 Not Found` — No product with that ID

**Example:**
```bash
curl https://localhost:5001/api/product/1    # 200 OK
curl https://localhost:5001/api/product/99   # 404 Not Found
```

---

## Test Cases

Both xUnit and NUnit suites cover the same scenarios:

| # | Test Name                                       | Input ID | Expected Result           |
|---|-------------------------------------------------|----------|---------------------------|
| 1 | `GetProduct_ExistingId_ReturnsOkWithProduct`    | 1        | 200 OK + correct product  |
| 2 | `GetProduct_NonExistingId_ReturnsNotFound`      | 99       | 404 Not Found             |
| 3 | `GetProduct_ReturnsExpectedStatusCode` (bonus)  | 1 / 99   | 200 / 404 (parameterised) |

---

## Key Concepts Demonstrated

### Dependency Injection
`ProductController` receives `IProductService` via constructor injection — it never creates the service itself.

### Mocking with Moq
```csharp
var mockService = new Mock<IProductService>();

// Stub: return a product for id=1
mockService.Setup(s => s.GetProductByIdAsync(1)).ReturnsAsync(product);

// Stub: return null for id=99
mockService.Setup(s => s.GetProductByIdAsync(99)).ReturnsAsync((Product?)null);

// Verify it was called
mockService.Verify(s => s.GetProductByIdAsync(1), Times.Once);
```

### xUnit vs NUnit Comparison

| Feature         | xUnit                  | NUnit                   |
|-----------------|------------------------|-------------------------|
| Test attribute  | `[Fact]`               | `[Test]`                |
| Setup method    | Constructor            | `[SetUp]` method        |
| Parameterised   | `[Theory] + [InlineData]` | `[TestCase]`         |
| Multiple asserts| Manual                 | `Assert.Multiple()`     |
| Assertion style | `Assert.IsType<T>(x)`  | `Assert.That(x, Is.InstanceOf<T>())` |
