using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Text;
using VKBot.Models;
using VkNet.Abstractions;
using VkNet.Enums.StringEnums;
using VkNet.Model;
using VkNet.Utils;

namespace VKBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallbackController : Controller
    {
        private readonly ILogger<CallbackController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IVkApi _vkApi;

        public CallbackController(IVkApi vkApi, IConfiguration configuration, ILogger<CallbackController> logger)
        {
            _vkApi = vkApi;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Callback([ModelBinder(BinderType = typeof(VkMessageModelBinder))][FromBody] Message msg)
        {
            using (var reader = new StreamReader(Request.Body))
            {
                var requestBody = await reader.ReadToEndAsync();
                _logger.LogError("Callback function" + requestBody);
                _logger.LogError(msg.PeerId.ToString());
            }
           

            //_logger.LogError("PEEEEEEEEEEEEEEEEEEER: " + msg.PeerId);

            //switch (msg.Type)
            //{
            //    case "confirmation":

            //        return Ok(_configuration["Config:Confirmation"]);
            //        /*case "message_new":
            //            {
            //                _vkApi.Messages.Send(new MessagesSendParams
            //                {
            //                    RandomId = new DateTime().Millisecond,
            //                    PeerId = msg.PeerId.Value,
            //                    Message = msg.Text
            //                });
            //                break;
            //            }*/
            //}
            
            _vkApi.Messages.Send(new MessagesSendParams
            {
                RandomId = new DateTime().Millisecond,
                PeerId = 651565729,
                Message = "Лох",
                
            });
            return Ok("ok");
        }
    }
}
