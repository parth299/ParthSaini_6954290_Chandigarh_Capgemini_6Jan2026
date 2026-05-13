using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using BookStore.Web.Models.DTOs;

namespace BookStore.Web.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<ApiService> _logger;
        private const string BaseUrl = "http://localhost:5121/api";

        public ApiService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, ILogger<ApiService> logger)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _httpClient.BaseAddress = new Uri(BaseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private void SetAuthorizationHeader()
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("JWTToken");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        // Authentication Methods
        public async Task<LoginResponseDto?> LoginAsync(LoginViewModel model)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{BaseUrl}/auth/login", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var loginResponse = JsonSerializer.Deserialize<LoginResponseDto>(responseContent, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    // Store token in session
                    if (loginResponse != null && _httpContextAccessor.HttpContext != null)
                    {
                        _httpContextAccessor.HttpContext.Session.SetString("JWTToken", loginResponse.Token);
                        _httpContextAccessor.HttpContext.Session.SetString("UserName", loginResponse.User.FullName);
                        _httpContextAccessor.HttpContext.Session.SetString("UserEmail", loginResponse.User.Email);
                        _httpContextAccessor.HttpContext.Session.SetInt32("UserId", loginResponse.User.UserId);
                    }

                    return loginResponse;
                }
                else
                {
                    _logger.LogWarning($"Login failed: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login");
                return null;
            }
        }

        public async Task<bool> RegisterAsync(RegisterViewModel model)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{BaseUrl}/auth/register", content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during registration");
                return false;
            }
        }

        // Book Methods
        public async Task<List<BookDto>?> GetBooksAsync(string? search = null, int? categoryId = null, int page = 1, int pageSize = 12)
        {
            try
            {
                SetAuthorizationHeader();
                var queryParams = new List<string>();
                
                if (!string.IsNullOrEmpty(search))
                    queryParams.Add($"search={Uri.EscapeDataString(search)}");
                if (categoryId.HasValue)
                    queryParams.Add($"categoryId={categoryId.Value}");
                if (page > 1)
                    queryParams.Add($"page={page}");
                if (pageSize != 12)
                    queryParams.Add($"pageSize={pageSize}");

                var queryString = queryParams.Any() ? "?" + string.Join("&", queryParams) : "";
                var response = await _httpClient.GetAsync($"{BaseUrl}/books{queryString}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<BookDto>>(content, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
                else
                {
                    _logger.LogWarning($"Failed to get books: {response.StatusCode}");
                    return new List<BookDto>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting books");
                return new List<BookDto>();
            }
        }

        public async Task<BookDto?> GetBookByIdAsync(int id)
        {
            try
            {
                SetAuthorizationHeader();
                var response = await _httpClient.GetAsync($"{BaseUrl}/books/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<BookDto>(content, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
                else
                {
                    _logger.LogWarning($"Failed to get book {id}: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting book {id}");
                return null;
            }
        }

        public async Task<List<CategoryDto>?> GetCategoriesAsync()
        {
            try
            {
                SetAuthorizationHeader();
                var response = await _httpClient.GetAsync($"{BaseUrl}/categories");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<CategoryDto>>(content, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
                else
                {
                    _logger.LogWarning($"Failed to get categories: {response.StatusCode}");
                    return new List<CategoryDto>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting categories");
                return new List<CategoryDto>();
            }
        }

        // Cart Methods
        public async Task<List<CartItemDto>?> GetCartItemsAsync()
        {
            try
            {
                SetAuthorizationHeader();
                var response = await _httpClient.GetAsync($"{BaseUrl}/cart");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<CartItemDto>>(content, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
                else
                {
                    _logger.LogWarning($"Failed to get cart items: {response.StatusCode}");
                    return new List<CartItemDto>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting cart items");
                return new List<CartItemDto>();
            }
        }

        public async Task<bool> AddToCartAsync(int bookId, int quantity)
        {
            try
            {
                SetAuthorizationHeader();
                var cartItem = new { BookId = bookId, Quantity = quantity };
                var content = new StringContent(JsonSerializer.Serialize(cartItem), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{BaseUrl}/cart", content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error adding to cart: book {bookId}, quantity {quantity}");
                return false;
            }
        }

        public async Task<bool> UpdateCartItemAsync(int bookId, int quantity)
        {
            try
            {
                SetAuthorizationHeader();
                var cartItem = new { BookId = bookId, Quantity = quantity };
                var content = new StringContent(JsonSerializer.Serialize(cartItem), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{BaseUrl}/cart/{bookId}", content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating cart item: book {bookId}, quantity {quantity}");
                return false;
            }
        }

        public async Task<bool> RemoveFromCartAsync(int bookId)
        {
            try
            {
                SetAuthorizationHeader();
                var response = await _httpClient.DeleteAsync($"{BaseUrl}/cart/{bookId}");

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error removing from cart: book {bookId}");
                return false;
            }
        }

        // Order Methods
        public async Task<List<OrderDto>?> GetUserOrdersAsync()
        {
            try
            {
                SetAuthorizationHeader();
                var response = await _httpClient.GetAsync($"{BaseUrl}/orders");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<OrderDto>>(content, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
                else
                {
                    _logger.LogWarning($"Failed to get user orders: {response.StatusCode}");
                    return new List<OrderDto>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user orders");
                return new List<OrderDto>();
            }
        }

        public async Task<OrderDto?> GetOrderByIdAsync(int id)
        {
            try
            {
                SetAuthorizationHeader();
                var response = await _httpClient.GetAsync($"{BaseUrl}/orders/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<OrderDto>(content, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
                else
                {
                    _logger.LogWarning($"Failed to get order {id}: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting order {id}");
                return null;
            }
        }

        public async Task<OrderDto?> CreateOrderAsync(CreateOrderDto orderDto)
        {
            try
            {
                SetAuthorizationHeader();
                var content = new StringContent(JsonSerializer.Serialize(orderDto), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{BaseUrl}/orders", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<OrderDto>(responseContent, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
                else
                {
                    _logger.LogWarning($"Failed to create order: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating order");
                return null;
            }
        }

        // Review Methods
        public async Task<List<ReviewDto>?> GetBookReviewsAsync(int bookId)
        {
            try
            {
                SetAuthorizationHeader();
                var response = await _httpClient.GetAsync($"{BaseUrl}/books/{bookId}/reviews");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<ReviewDto>>(content, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
                else
                {
                    _logger.LogWarning($"Failed to get reviews for book {bookId}: {response.StatusCode}");
                    return new List<ReviewDto>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting reviews for book {bookId}");
                return new List<ReviewDto>();
            }
        }
    }
}
