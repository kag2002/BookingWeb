using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace BookingWeb.SessionsDefine
{
    public static class SessionExtensions
    {
        public static async Task SetObjectAsync<T>(this ISession session, string key, T value)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true // Optional: To format JSON for better readability
            };

            using (var memoryStream = new MemoryStream())
            {
                await JsonSerializer.SerializeAsync(memoryStream, value, options);
                memoryStream.Seek(0, SeekOrigin.Begin);
                using (var reader = new StreamReader(memoryStream))
                {
                    var jsonValue = await reader.ReadToEndAsync();
                    session.SetString(key, jsonValue);
                }
            }
        }

        public static async Task<T> GetObjectAsync<T>(this ISession session, string key)
        {
            var jsonValue = session.GetString(key);
            if (jsonValue == null)
                return default;

            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new StreamWriter(memoryStream))
                {
                    await writer.WriteAsync(jsonValue);
                    await writer.FlushAsync();
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    return await JsonSerializer.DeserializeAsync<T>(memoryStream);
                }
            }
        }

        public static async Task RemoveAsync (this ISession session, string key)
        {
            await session.RemoveAsync(key);
        }

        public static async Task ClearAsync (this ISession session)
        {
            await session.ClearAsync();
        }


    }
}
