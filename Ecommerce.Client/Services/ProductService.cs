using Ecommerce.SharedLibrary.Contracts;
using Ecommerce.SharedLibrary.Models;
using Ecommerce.SharedLibrary.Response;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Ecommerce.Client.Services
{
    public class EndpointProductService(HttpClient httpClient) : IProduct
    {
        private readonly HttpClient _httpClient = httpClient;
        private const string _baseUrl = "api/product";

        private static string SerializeObj(object modelObject)
        {
            try
            {
                return JsonSerializer.Serialize(modelObject, JsonOptions());
            }
            catch (JsonException ex)
            {
                throw new Exception("Failed to serialize object", ex);
            }
        }
           
        private static T DeserializeJsonString<T>(string jsonString)
        {
            try
            {
                return JsonSerializer.Deserialize<T>(jsonString, JsonOptions())!;
            }
            catch (JsonException ex)
            {
                throw new Exception("Failed to deserialize JSON string", ex);
            }
        }
        
        private static StringContent GenerateStringContent(string serialiazedObj)
            => new(
                serialiazedObj,
                System.Text.Encoding.UTF8,
                "application/json"
                );
        private static IList<T> DeserializeJsonStringList<T>(string jsonString)
            => JsonSerializer.Deserialize<IList<T>>(jsonString, JsonOptions())!;
        private static JsonSerializerOptions JsonOptions()
        {
            return new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                UnmappedMemberHandling = JsonUnmappedMemberHandling.Skip
            };
        }


        public async Task<ServiceRepsonse> AddProduct(Product model)
        {
            var response = await _httpClient.PostAsync(_baseUrl, GenerateStringContent(SerializeObj(model)));

            if (!response.IsSuccessStatusCode)
            {
                return new ServiceRepsonse(false, "Error occour!. Try again later...");
            }

            var apiResponse = await response.Content.ReadAsStringAsync();
            return DeserializeJsonString<ServiceRepsonse>(apiResponse);
        }

        public async Task<List<Product>> GetAllProducts(bool IsFeaturedProduct)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}?IsFeaturedProduct={IsFeaturedProduct}");

            if (!response.IsSuccessStatusCode) return null!;

            var result = await response.Content.ReadAsStringAsync();
            return [.. DeserializeJsonStringList<Product>(result)];
        }
    }
}
