using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace DemoBIDMeet.Hubs
{
    public class WebRTCHub : Hub
    {
        public async Task JoinRoom(string roomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
        }

        public async Task SendOffer(string roomId, string offer)
        {
            // Broadcast the offer to all clients in the room except the sender.
            await Clients.OthersInGroup(roomId).SendAsync("ReceiveOffer", offer);
        }

        public async Task SendAnswer(string roomId, string answer)
        {
            // Broadcast the answer to all clients in the room except the sender.
            await Clients.OthersInGroup(roomId).SendAsync("ReceiveAnswer", answer);
        }

        public async Task SendICECandidate(string roomId, string candidate)
        {
            // Broadcast the ICE candidate to all clients in the room except the sender.
            await Clients.OthersInGroup(roomId).SendAsync("ReceiveICECandidate", candidate);
        }
        public async Task SendMessage(string user, string message)
        {
            // Broadcast the message to all connected clients
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
