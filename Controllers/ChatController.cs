using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using DemoBIDMeet.Hubs;

namespace BIDMeet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IHubContext<WebRTCHub> _hubContext;

        public ChatController(IHubContext<WebRTCHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendMessage([FromBody] dynamic message)
        {
            if (message == null || message.User == null || message.Text == null)
            {
                return BadRequest("Invalid data.");
            }

            string user = message.User;
            string text = message.Text;

            // Broadcast the message to connected clients
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", user, text);

            return Ok();
        }
    }
}
