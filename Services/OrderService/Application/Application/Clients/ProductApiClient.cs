using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class ProductApiClient
{
    private readonly HttpClient _httpClient;

    public ProductApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> ReduceProductStockAsync(int productId, int quantityToReduce)
    {
        var content = new StringContent(quantityToReduce.ToString(), Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync($"http://localhost:5233/api/products/{productId}/reduce-stock", content);

        return response.IsSuccessStatusCode;
    }
}
