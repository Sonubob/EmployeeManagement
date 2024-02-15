using EmployeeManagement.Models;
using EmployeeManagement.Repositories;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EmployeeManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMockEmployeeRepository _empRepo;
        private byte[] byteValue;

        public HomeController(IMockEmployeeRepository empRepo)
        {
            _empRepo = empRepo;
        }

        public IActionResult Index()
        {
            var model = _empRepo.GetAllEmployee();
           HttpContext.Session.TryGetValue("IsAuthenticated", out byteValue);
            var isUserAuthenticated = BitConverter.ToBoolean(byteValue ?? new byte[1]);
            if (isUserAuthenticated)
                return View(model);
            else
                return RedirectToAction("LogIn", "Account");
        }

        public ViewResult Details(int Id)
        {
            var model = _empRepo.GetEmployee(Id);

            
            var _empVM = new EmployeeViewModel()
            {
                Id = model.Id,
                Name = model.Name,
                Email = model.Email,
                Department = model.Department
            };
            
            return View(_empVM);
        }

        [HttpGet]
        public ViewResult Edit(int?Id)
        {
            if (Id == null)
            {
                return View(new Employee());
            }
            else
            {
                //get employee details
                var _empModel = _empRepo.GetEmployee(Id??0);
                var _empVM = new EmployeeViewModel()
                {
                    Id = _empModel.Id,
                    Name = _empModel.Name,
                    Email = _empModel.Email,
                    Department = _empModel.Department
                };
                ViewBag.EmployeeModel = _empVM;
                return View(_empModel);
            }
        }

        [HttpPost]
        public IActionResult Edit(Employee employeeModel)
        {
            var result = new Employee();
            if (ModelState.IsValid && employeeModel.Id > 0 )
            {
                //update employee
                 result = _empRepo.Update(employeeModel);
            }
            else
            {
                //add employee
                result = _empRepo.Add(employeeModel);
              
            }
            //if changes saved return to index
            if(result.Id > 0)
            {
                return RedirectToAction("Index");
            }
            //if changes failed return employ edit view
            else
            {
                return View(result);
            }
        }

        public IActionResult Remove(int Id)
        {
            //Remove Record

            //Save changes

            //Redirect to index
            return RedirectToAction("Index");
        }

        
    }
}
