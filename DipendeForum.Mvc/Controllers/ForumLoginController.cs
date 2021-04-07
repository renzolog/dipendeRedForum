using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DipendeForum.Mvc.Controllers
{
    public class ForumLoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
