using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;

namespace VKBot.Filters
{
    public class ModifyRequestBodyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ModifyRequestBodyMiddleware> _logger;

        public ModifyRequestBodyMiddleware(RequestDelegate next, ILogger<ModifyRequestBodyMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Проверяем, что это запрос с JSON телом
            if (context.Request.ContentType != null && context.Request.ContentType.Contains("application/json"))
            {
                // Позволяет многократное чтение тела запроса
                context.Request.EnableBuffering();

                // Читаем тело запроса
                using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true))
                {
                    var body = await reader.ReadToEndAsync();
                    context.Request.Body.Position = 0; // Сбрасываем позицию тела для дальнейшего использования

                    // Парсим JSON
                    var jsonObject = JsonConvert.DeserializeObject<JObject>(body);
                    _logger.LogError("Middleware: " + jsonObject);
                }
            }

            await _next(context);
        }
    }

}
