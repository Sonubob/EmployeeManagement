using EmployeeManagement.Models;
using EmployeeManagement.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    /// <summary>
    /// Objective: Model binding in MVC
    /// </summary>
    public class AccountController : Controller
    {
        private readonly IMockEmployeeRepository _repo;
        public AccountController(IMockEmployeeRepository repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public IActionResult Login()
        {
            HttpContext.Session.Remove("IsAuthenticated");
            return View();
        }
        /// <summary>
        /// To validate the user using model binding and take them to landing page
        /// </summary>
        /// <param name="loginDetails"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Login(LoginDetails loginDetails)
        {
            // Simulate authentication 
            var employeeList = IsUserExist(loginDetails.Email, loginDetails.Password);

            if (employeeList?.Name != null)
            {
                // Set a session variable or cookie to indicate that the user is authenticated
                HttpContext.Session.SetString("IsAuthenticated", "true");

                // Redirect to a protected page after successful login
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Display an error message for invalid credentials
                ModelState.AddModelError(string.Empty, "Invalid username or password");
                return View();
            }
        }

        /// <summary>
        /// User registration method
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        private Employee IsUserExist(string email, string password)
        {
            return _repo.GetEmployeeByNameAndPassword(email, password);
        }

        /// <summary>
        /// Create new user from the form details
        /// </summary>
        /// <param name="newUserDetails"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(Employee newUserDetails)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid details provided");
                return View();
            }
            var user = IsUserExist(newUserDetails.Email, newUserDetails.Password);
            if (user != null)
            {
                ModelState.AddModelError(string.Empty, "User already exists");
                return View();
            }
            var result = _repo.Add(newUserDetails);
            if(result?.Id != 0)
            {
                // Set a session variable or cookie to indicate that the user is authenticated
                HttpContext.Session.SetString("IsAuthenticated", "true");

                // Redirect to a protected page after successful login
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Create new employee failed");
                return View();
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("IsAuthenticated");
            return Json(new { url = "/Account/Login" });
        }

    }
}
