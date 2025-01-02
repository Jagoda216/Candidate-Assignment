using Microsoft.AspNetCore.SignalR;

namespace Server.Hubs
{
    public class QuantityUpdateHub : Hub
    {
        public async Task SendUpdatedQuantity(string name, int quantity)
        {
            await Clients.All.SendAsync("RecivedUpdatedQuantity", name, quantity);
        }

    }
}
