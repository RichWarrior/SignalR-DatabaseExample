using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            var result = new List<EmployeeViewModel>();
            using (SignalR_ExampleEntities dbContext = new SignalR_ExampleEntities())
            {
                var db = dbContext.Employee.ToList();
                foreach (var item in db)
                {
                    result.Add(new EmployeeViewModel {
                        Id = item.Id,
                        Name = item.Name,
                        Surname = item.Surname
                    });
                }
            }
            return View(result);
        }
        public ActionResult Create()
        {
            return View();
        }        
    }
}