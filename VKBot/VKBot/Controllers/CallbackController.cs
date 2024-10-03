using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Text;
using System.Text.Json;
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

        public CallbackController(IConfiguration configuration, ILogger<CallbackController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Callback([FromBody] JsonElement json)
        {
            if (json.TryGetProperty("type", out JsonElement typeElement))
            {
                var type = typeElement.GetString();
                if(type == "confirmation")
                {
                    return Ok(_configuration["Config:Confirmation"]);
                }
                else if(type == "message_new")
                {
                    return Method(ToObject<Models.Message>(json));
                }
                else if(type == "photo")
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        private IActionResult Method(Models.Message? url)
        {
            return Ok();
        }

        private T? ToObject<T>(JsonElement json)
        {
            return JsonSerializer.Deserialize<T>(json.ToString());
        }
    }
}
