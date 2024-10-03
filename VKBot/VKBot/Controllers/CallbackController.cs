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
        public IActionResult Callback(Updates msg)
        {
            _logger.LogInformation(msg.Type);
            _logger.LogInformation(_configuration["Config:Confirmation"]);
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
