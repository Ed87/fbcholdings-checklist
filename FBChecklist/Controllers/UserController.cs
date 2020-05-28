﻿using FBChecklist.Exceptions;
using FBChecklist.Services;
using FBChecklist.ViewModels;
using System;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Web.Mvc;

namespace FBChecklist.Controllers
{
    public class UserController : Controller
    {

        private AppEntities db = new AppEntities();
        private DomainControllerService domainControllerService;


        public UserController(DomainControllerService domainControllerService)
        {
            this.domainControllerService = domainControllerService;

        }
        public UserController() : this(new DomainControllerService())
        {
            //the framework calls this
        }


        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        // GET: User/Delete/5
        public ActionResult Login()
        {
            var model = new DomainControllerViewModel();
           // LoginViewModel model = new LoginViewModel();
            return View(model);
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Login(DomainControllerViewModel model)
        {
            try
            {

                PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "yyy.corp");

                // find a user
                UserPrincipal user = UserPrincipal.FindByIdentity(ctx, model.Username);

                if (user != null)
                {
                    // check user lockout state
                    if (user.IsAccountLockedOut())
                    {
                        ViewBag.Message = "Your account is locked out";
                    }
                    else
                    {
                        bool authentic = false;
                        try
                        {

                            DirectoryEntry entry = new DirectoryEntry("LDAP://XX.XX.XX.XX:111/OU=YYY,DC=yyy,DC=corp", model.Username, model.Password);
                            DirectoryEntry ldapConnection = new DirectoryEntry("yyy.corp");
                            ldapConnection.Path = "LDAP://";
                            ldapConnection.Username = "yyy";
                            ldapConnection.Password = "xxx";
                            ldapConnection.AuthenticationType = AuthenticationTypes.Secure;

                            //Login with user
                            object nativeObject = entry.NativeObject;
                            authentic = true;

                            if (authentic == true)
                            {

                                Session["Username"] = model.Username;
                                return View("Index");
                            }
                            else
                            {

                                ViewBag.Message = "FAILED TO LOGIN";
                            }

                        }
                        catch (Exception ex)
                        {
                            ExceptionLogger.SendErrorToText(ex);
                            ViewBag.ErrorMessage = Common.Helpers.Messages.GENERAL_ERROR;
                            return View();
                        }
                    }

                }
                else
                {
                   
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.SendErrorToText(ex);
                ViewBag.ErrorMessage = Common.Helpers.Messages.GENERAL_ERROR;
                return View();
            }
            return View();
        }


        public ActionResult Logout()
        {
            //killing sessions on logout
            Session.Clear();
            return RedirectToAction("Login");

        }
    }
}