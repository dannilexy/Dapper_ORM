using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DapperORM2.Models;
using Microsoft.AspNetCore.Mvc;

namespace DapperORM2.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View(DapperORM.ReturnList<EmployeeModel>("EmployeeViewAll", null));
        }


        //Get AddOrEdit Action
        [HttpGet]
       public IActionResult AddOrEdit( int id=0)
        {
            if (id == 0)
            {
                return View();
            }
            else
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@EmployeeId", id);

                var rec = DapperORM.ReturnList<EmployeeModel>("EmployeeViewById", param).FirstOrDefault<EmployeeModel>();
                return View(rec);

            }
           

           
        }

        //Post AddOrEdit Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit(EmployeeModel emp)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@EmployeeId", emp.EmployeeId);
            param.Add("@Name", emp.Name);
            param.Add("@Position", emp.Position);
            param.Add("@Age", emp.Age);
            param.Add("@Salary", emp.Salary);
            DapperORM.ExecuteWithoutReturn("EmployeeAddOrEdit", param);
            return RedirectToAction("Index");
        }

       
        public ActionResult Delete(int id)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@EmployeeId", id);
            DapperORM.ExecuteWithoutReturn("EmployeeDeleteById", param);
             return RedirectToAction("Index");
        }
    }
}
