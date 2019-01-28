using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcForum.Web.Controllers
{
    public class ChatController : Controller
    {
        // GET: Chat
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Chat()
        {
            //from here, get the user details then pass to view 
            string name = User.Identity.Name;// only takes 
            return View();
        }
    }
}