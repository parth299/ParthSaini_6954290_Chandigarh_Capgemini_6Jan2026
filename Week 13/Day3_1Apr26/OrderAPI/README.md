# Case Study 2 — Order Processing API (.NET 10)

## Project Structure

```
OrderAPI/
├── OrderAPI.sln
├── README.md
│
├── src/
│   └── OrderAPI/
│       ├── OrderAPI.csproj
│       ├── Program.cs
│       ├── Controllers/
│       │   └── OrderController.cs       # POST /api/order
│       ├── Models/
│       │   └── Order.cs                 # Order entity
│       └── Services/
│           ├── IOrderService.cs         # Interface (what we mock)
│           └── OrderService.cs          # In-memory implementation
│
└── tests/
    ├── OrderAPI.Tests.xUnit/            # xUnit + Moq
    │   ├── OrderAPI.Tests.xUnit.csproj
    │   └── OrderControllerTests.cs
    └── OrderAPI.Tests.NUnit/            # NUnit + Moq
        ├── OrderAPI.Tests.NUnit.csproj
        └── OrderControllerTests.cs
```

---

## Quick Start

```bash
# Build
dotnet restore && dotnet build

# Run API
dotnet run --project src/OrderAPI --urls "http://localhost:5050"
# Swagger → http://localhost:5050/swagger/index.html

# Run all tests
dotnet test

# Run individual test projects
dotnet test tests/OrderAPI.Tests.xUnit
dotnet test tests/OrderAPI.Tests.NUnit
```

---

## API Endpoint

| Method | Route        | Body         | Success | Failure |
|--------|--------------|--------------|---------|---------|
| POST   | `/api/order` | Order (JSON) | 201 Created | 400 Bad Request |

**Example request body:**
```json
{
  "customerId": 1,
  "productName": "Laptop",
  "quantity": 2,
  "totalAmount": 1999.98
}
```

---

## Test Cases (6 total — same in both xUnit and NUnit)

| # | Test | Input | Expected |
|---|------|-------|----------|
| 1 | `PlaceOrder_ValidOrder_ReturnsCreated`   | service returns `true`  | 201 Created     |
| 2 | `PlaceOrder_InvalidOrder_ReturnsBadRequest` | service returns `false` | 400 Bad Request |
| 3 | `PlaceOrder_ReturnsExpectedStatusCode(true, 201)`  | parameterised | 201 |
| 4 | `PlaceOrder_ReturnsExpectedStatusCode(false, 400)` | parameterised | 400 |

---

## Key Concepts

### Controller Logic
```csharp
var success = await _orderService.PlaceOrderAsync(order);

if (!success)
    return BadRequest();           // 400

return Created($"api/order/{order.Id}", order);  // 201
```

### Moq — Stubbing true/false
```csharp
// Simulate success
mockService.Setup(s => s.PlaceOrderAsync(order)).ReturnsAsync(true);

// Simulate failure
mockService.Setup(s => s.PlaceOrderAsync(order)).ReturnsAsync(false);
```

### xUnit vs NUnit

| Feature       | xUnit                  | NUnit                  |
|---------------|------------------------|------------------------|
| Test method   | `[Fact]`               | `[Test]`               |
| Setup         | Constructor            | `[SetUp]`              |
| Parameterised | `[Theory][InlineData]` | `[TestCase]`           |
| Assert result | `Assert.IsType<T>()`   | `Assert.That(x, Is.InstanceOf<T>())` |
