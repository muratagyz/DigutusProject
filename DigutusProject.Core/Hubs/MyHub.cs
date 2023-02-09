using Microsoft.AspNetCore.SignalR;

namespace DigutusProject.Core.Hubs;

public class MyHub : Hub
{
    private static int ClientCount { get; set; } = 0;

    public async override Task OnConnectedAsync()
    {
        ClientCount++;
        await Clients.All.SendAsync("ReceiveClientCount", ClientCount);
        await base.OnConnectedAsync();
    }

    public async override Task OnDisconnectedAsync(Exception? exception)
    {
        ClientCount--;
        await Clients.All.SendAsync("ReceiveClientCount", ClientCount);
        await base.OnDisconnectedAsync(exception);
    }
}