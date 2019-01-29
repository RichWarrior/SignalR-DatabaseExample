using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using SharedLayer;

namespace Web.Hubs
{
    public class EmployeeHub : Hub
    {
        public void GetLastData(List<EmployeeModel> dataModel)
        {
            Clients.All.GetData(dataModel);
        }
    }
}