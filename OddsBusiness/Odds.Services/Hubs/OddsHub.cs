using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using OddsBusiness.Models;

namespace OddsBusiness.Hubs
{
    public class OddsHub : Hub
    {
        [HubMethodName("broadcastData")]
        public static void BroadcastData(OddsModel model)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<OddsHub>();
            context.Clients.All.updatedData(model);
        }
    }
}