using Project___Intro_To_Computer_Networking.Dal;
using Project___Intro_To_Computer_Networking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Project___Intro_To_Computer_Networking.Controllers
{
    public class authenticateController : Controller
    {
        public ActionResult sign_in()
        {
            return View("sign_in");
        }

        public ActionResult authenticate()
        {
            string email = Request.Form["IV_Email"];
            string password = Request.Form["IV_Password"];

            cls_user currentUser = new clsUsersDal().users.ToList<cls_user>().Find(x => x.email.Equals(email) && x.password.Equals(password));
            if (currentUser != null)
            {
                Session["currentUser"] = currentUser;
                return RedirectToAction("index", "home");
            }
            TempData["errorMsg"] = "Incorrect username or password";
            return RedirectToAction("sign_in");
        }

        public ActionResult sign_up()
        {
            cls_user currentInput = new cls_user();
            return View("sign_up", currentInput);
        }

        public ActionResult submit()
        {
            clsUsersDal dal = new clsUsersDal();
            cls_user currentInput = getUser();
            if (ModelState.IsValid)
            {
                List<cls_user> users = dal.users.ToList<cls_user>();
                if (users.Find(x => x.email.Equals(currentInput.email)) != null)
                {
                    TempData["ErrorMsg"] = "The entered email is already registered";
                    return View("sign_up", currentInput);
                }
                dal.users.Add(currentInput);
                dal.SaveChanges();
                Session["currentUser"] = currentInput;
                return RedirectToAction("index", "home");
            }
            else
            {
                return View("sign_up", currentInput);
            }
        }

        private cls_user getUser()
        {
            return new cls_user()
            {
                firstName = Request.Form["firstName"],
                lastName = Request.Form["lastName"],
                password = Request.Form["password"],
                email = Request.Form["email"],
                is_admin = false
            };
        }
        public ActionResult sign_out()
        {
            Session["currentUser"] = null;
            Session["BUG"] = null;
            return RedirectToAction("index", "home");
        }
    }
}