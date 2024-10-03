using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VkNet.Model;

namespace VKBot
{
    public class VkMessageModelBinder : IModelBinder
    {


        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {

                throw new ArgumentNullException(nameof(bindingContext));
            }

            // Получаем тело запроса
            using (var reader = new StreamReader(bindingContext.HttpContext.Request.Body))
            {
                var body = await reader.ReadToEndAsync();
                var jsonObject = JsonConvert.DeserializeObject<JObject>(body);
                Message message = new Message();
                if (jsonObject["object"]?["message"]?["out"] != null)
                {
                    message.PeerId = (long)Convert.ToDouble(jsonObject["object"]?["message"]?["peer_id"]?.ToString());
                }
                
                
                

                    //var message = JsonConvert.DeserializeObject<Message>(body);


                    bindingContext.Result = ModelBindingResult.Success(message);
            }
        }
    }
}
