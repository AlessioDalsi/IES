using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Assistenza.BufDalsi.Web.Models.ManutenzioneViewModels;
using Microsoft.AspNetCore.Identity;
using Assistenza.BufDalsi.Web.Data;

namespace Assistenza.BufDalsi.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }



        [Authorize(Roles = "Operator, Admin")]
        public IActionResult Operator()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdministratorControlPanel()
        {
            return View();
        }
    }
}
