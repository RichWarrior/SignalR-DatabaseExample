using Microsoft.AspNet.SignalR.Client;
using SharedLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Dependency
    {
        private string _connectionString = "Data Source=192.168.2.162,1433;Initial Catalog=SignalR_Example;User Id=sa;Pwd=03102593";
        private SqlDependency _dependency;
        public Dependency()
        {
            SqlDependency.Start(_connectionString);
            StartListening();
        }

        public void StartListening()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                using (var cmd = con.CreateCommand())
                {

                    cmd.CommandText = "SELECT * FROM Employee";
                    cmd.Notification = null;
                    cmd.CommandType = CommandType.Text;

                    _dependency = new SqlDependency(cmd);

                    _dependency.OnChange += _dependency_OnChange;

                    if (con.State != System.Data.ConnectionState.Open)
                    {
                        con.Open();
                    }
                    cmd.ExecuteReader();
                }
            }       
            
        }

        private void _dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            Console.WriteLine("Update Data");
            var model = new List<EmployeeModel>();
            using (SignalR_ExampleEntities d = new SignalR_ExampleEntities())
            {
                var db = d.Employee.ToList();
                foreach (var item in db)
                {
                    model.Add(new EmployeeModel {
                         name = item.Name,
                          surname = item.Surname
                    });
                }
            }            
            Update(model);
            StartListening();
        }
        public void Update(List<EmployeeModel> model)
        {
            var hubConnection = new HubConnection("http://localhost:49246/");
            var hub = hubConnection.CreateHubProxy("EmployeeHub");
            hubConnection.Start().Wait();
            hub.Invoke("GetLastData", model);
        }
    }
}
