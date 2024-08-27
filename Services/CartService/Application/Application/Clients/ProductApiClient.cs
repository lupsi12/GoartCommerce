using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class ProductApiClient
{
    private readonly HttpClient _httpClient;

    public ProductApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ProductDto> GetProductByIdAsync(int productId)
    {
        var response = await _httpClient.GetAsync($"/api/products/{productId}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ProductDto>(content);
        }

        return null;
    }
}

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}
