using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Web.Hubs
{
    public class EmployeeHub : Hub
    {
        public void Create(string name, string surname)
        {
            using (SignalR_ExampleEntities dbContext = new SignalR_ExampleEntities())
            {
                dbContext.Employee.Add(new Employee { Name = name, Surname = surname});

                dbContext.SaveChanges();

                Clients.Caller.GetProcessStatus(true);

                Clients.Others.GetValue(name,surname);
            }
        }
    }
}