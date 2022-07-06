using Microsoft.AspNetCore.SignalR;

namespace WebApiStone.Hubs;

public class StatisticsHub : Hub, IStatisticsHub
{
    IHubContext<StatisticsHub> Hub;

    public StatisticsHub(IHubContext<StatisticsHub> StatisticsHub)
    {
        Hub = StatisticsHub;
    }

    public async Task NotifyAll()
    {
        await Hub.Clients.All.SendAsync("Notify");
    }
}
