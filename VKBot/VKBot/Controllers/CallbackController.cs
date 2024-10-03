using Microsoft.AspNetCore.Mvc;
using VKBot.Models;
using VkNet.Abstractions;
using VkNet.Model;
using VkNet.Utils;

namespace VKBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallbackController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IVkApi _vkApi;

        public CallbackController(IVkApi vkApi, IConfiguration configuration)
        {
            _vkApi = vkApi;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Callback(Updates msg)
        {
            Console.WriteLine(msg.Type);
            switch (msg.Type)
            {
                case "confirmation":

                    return Ok(_configuration["Config:Confirmation"]);
                /*case "message_new":
                    {
                        _vkApi.Messages.Send(new MessagesSendParams
                        {
                            RandomId = new DateTime().Millisecond,
                            PeerId = msg.PeerId.Value,
                            Message = msg.Text
                        });
                        break;
                    }*/
            }

            return Ok("ok");
        }
    }
}
