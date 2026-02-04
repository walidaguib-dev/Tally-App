using Microsoft.AspNetCore.SignalR;

namespace API.Hubs
{
    public class NotificationsHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("ReceiveNotification");
        }

        
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
