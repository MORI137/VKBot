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
                    // Проверяем наличие ключа "out" и его значения
                    //if (jsonObject["object"]?["message"]?["out"] != null)
                    //{
                    //    // Заменяем 0 на "Received", а 1 на "Sended"
                    //    var outValue = jsonObject["object"]["message"]["out"].Value<int>();
                    //    if (outValue == 0)
                    //    {
                    //        jsonObject["object"]["message"]["out"] = "Received";
                    //    }
                    //    else if (outValue == 1)
                    //    {
                    //        jsonObject["object"]["message"]["out"] = "Sended";
                    //    }
                    //}

                    //// Преобразуем измененный объект обратно в строку
                    //var modifiedBody = JsonConvert.SerializeObject(jsonObject);

                    //// Создаем новый Stream с измененным телом
                    //var memoryStream = new MemoryStream();
                    //var writer = new StreamWriter(memoryStream);
                    //await writer.WriteAsync(modifiedBody);
                    //await writer.FlushAsync();
                    //memoryStream.Position = 0;
                    //_logger.LogError(modifiedBody.ToString());
                    //// Заменяем тело запроса новым Stream с измененным JSON
                    //context.Request.Body = memoryStream;
                }
            }


            // Передаем управление дальше
            await _next(context);
        }
    }

}
