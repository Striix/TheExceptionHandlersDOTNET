using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Hogent_Stages.Models;
using HoGent_Stages.Models.DAL;
using Hogent_Stages.Repository.Stages.DBContext;
using Hogent_Stages.Repository.Stages.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using HoGent_Stages.Models;


namespace Hogent_Stages.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        public ActionResult Manage()
        {
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                    if (IsValid(model.UserName, model.Password))
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, false);
                        using (var db = new stagesContext())
                        {
                            var user = db.User.FirstOrDefault(u => u.email == model.UserName);
                            switch (user.rol)
                            {
                                case "bedrijf":
                                    return RedirectToAction("Home","Bedrijf");
                                case "student":
                                    return RedirectToAction("Home", "Student");
                                case "stageAdministrator":
                                    return RedirectToAction("Home", "Admin");
                            }
                        }
                    }
                    else
                    {
                        return RedirectToAction("About", "Home");
                    }
  
                }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private bool IsValid(String email, String password)
        {
            bool IsValid = false;

            using (var db = new stagesContext())
            {
                var user = db.User.FirstOrDefault(u => u.email == email);

                if (user != null)
                {
                    if (user.wachtwoord == password)
                    {
                        IsValid = true;
                    }
                }
            }

            return IsValid;
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var user = new ApplicationUser() { UserName = model.UserName };
                //var result = await UserManager.CreateAsync(user, model.Password);
                //if (result.Succeeded)
                //{
                //    await SignInAsync(user, isPersistent: false);
                //    return RedirectToAction("Index", "Home");
                //}
                //else
                //{
                //    //AddErrors(result);
                //}
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/Disassociate


        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}