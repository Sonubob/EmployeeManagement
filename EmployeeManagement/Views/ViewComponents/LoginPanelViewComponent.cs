﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers.ViewComponents
{
    public class LoginPanelViewComponent : ViewComponent
    {
        private byte[] byteValue;

        public IViewComponentResult Invoke()
        {
            // Check the login state here 
            bool isLoggedIn = false;
             HttpContext.Session.TryGetValue("IsAuthenticated",out byteValue);
            isLoggedIn = BitConverter.ToBoolean(byteValue??new byte[1]);
            // Pass the login state to the view
            return View( isLoggedIn );
        }
    }
}
