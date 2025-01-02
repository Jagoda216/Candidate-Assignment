using Microsoft.AspNetCore.SignalR;

namespace Server.Hubs
{
    /// <summary>
    /// SignalR hub for broadcasting quantity updates to web client
    /// </summary>
    public class QuantityUpdateHub : Hub
    {
        /// <summary>
        /// Sends updated quantity for specified user to web client
        /// </summary>
        /// <param name="name">User's name</param>
        /// <param name="quantity">User's quantity</param>
        /// <returns>A task that represents the asynchronous operation of broadcasting the updat</returns>
        public async Task SendUpdatedQuantity(string name, int quantity)
        {
            await Clients.All.SendAsync("RecivedUpdatedQuantity", name, quantity);
        }

    }
}
