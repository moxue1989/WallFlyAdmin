using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage.Blob;
using WallFly.Models;

namespace WallFly.Controllers
{
    public class HomeController : Controller
    {
        public static string PASSWORD = "password";
        public static string ADMIN = "admin";
        public static string USER = "user";

        public IActionResult Index()
        {
            if (NotAdmin())
            {
                return View();
            }
            return RedirectToAction("Video");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.SetString(USER, "");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Index(string password)
        {
            if (password == PASSWORD)
            {
                HttpContext.Session.SetString(USER, ADMIN);
            }

            return RedirectToAction("Video");
        }

        public IActionResult Video()
        {
            if(NotAdmin())
            {
               return RedirectToAction("Index");
            }
            IEnumerable<IListBlobItem> videoBlobs = FileManager.GetVideoBlobs();
            return View(videoBlobs);
        }

        public IActionResult Audio()
        {
            if (NotAdmin())
            {
                return RedirectToAction("Index");
            }
            IEnumerable<IListBlobItem> audioBlobs = FileManager.GetAudioBlobs();
            return View(audioBlobs);
        }
  
        private bool NotAdmin()
        {
            return HttpContext.Session.GetString(USER) != ADMIN;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
