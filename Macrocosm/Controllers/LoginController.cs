﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Macrocosm.Filter;
using Microsoft.AspNetCore.Mvc;

namespace Macrocosm.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        [SystemFiltercs]
        public IActionResult ALL()
        {
            return View();
        }
    }
}