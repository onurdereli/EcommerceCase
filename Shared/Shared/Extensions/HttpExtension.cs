using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Shared.Extensions
{
    public static class HttpExtension
    {
        public static async Task<T> ReadFromJsonAsync<T>(this HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
            await response.Content.LoadIntoBufferAsync();
            return await response.Content.ReadFromJsonAsync<T>();
        }
    }
}
