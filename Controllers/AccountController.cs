using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AdvisementSys.Models;
using System.Data;
using AdvisementSys.Models.Request;

namespace AdvisementSys.Controllers
{
    public class AccountController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {
            Roles.CreateRole("Receptionist");
            Roles.CreateRole("Student Advisor");
            return View();
        }

        //
        // POST: /Account/LogOn
        /// <summary>
        /// Defualt Logon 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            try{
                if (ModelState.IsValid)
                {
                    if (Membership.ValidateUser(model.UserName, model.Password))
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);


                        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                            && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "The user name or password provided is incorrect.");
                    }
            }
            }
            catch (Exception ex)
            {
                return View(model);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff
        /// <summary>
        /// just logs user off
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            IEnumerable<faculty> faculty = db.faculties;
            List<String> data = new List<string>();
            foreach(faculty fac in faculty)
            {
                data.Add(fac.fname);
            }
            RegisterModel model = new RegisterModel() { facultyList = data };
            return View(model);
        }

        //
        // POST: /Account/Register
        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Entities db = new Entities();
                    IEnumerable<faculty> faculty = db.faculties;
                    List<String> data = new List<string>();
                    foreach (faculty fac in faculty)
                    {
                        data.Add(fac.fname);
                    }
                    model.facultyList = data;

                    // Attempt to register the user
                    MembershipCreateStatus createStatus;
                    Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);
                    employee employee_model = new employee() { employeeid = model.UserName, fname = model.fname, lname = model.lname, phonenum = model.phonenum, email = model.Email, faculty = model.faculty, role = model.position };
                    student student = db.students.SingleOrDefault(stud => stud.studentid ==  employee_model.employeeid);

                    if (student == null)
                    {
                        db.employees.AddObject(employee_model);
                        db.SaveChanges();

                        if (createStatus == MembershipCreateStatus.Success)
                        {
                            //Roles.
                            Roles.AddUserToRole(model.UserName, model.position);
                            FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError("", ErrorCodeToString(createStatus));
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("UserName", "Employee ID is already in ues.");
                    }
                }
            }
            catch (Exception ex)
            {
                return View(model);
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword
        /// <summary>
        /// Changes password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    // ChangePassword will throw an exception rather
                    // than return false in certain failure scenarios.
                    bool changePasswordSucceeded;
                    try
                    {
                        MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                        changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                    }
                    catch (Exception)
                    {
                        changePasswordSucceeded = false;
                    }

                    if (changePasswordSucceeded)
                    {
                        return RedirectToAction("Index", "Student");
                    }
                    else
                    {
                        ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                    }
                }
            }
            catch (Exception ex)
            {
                return View(model);
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        /// <summary>
        /// Populates the model with the appropriate fields 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult EditUser(String id)
        {
            IEnumerable<faculty> faculty = db.faculties;
            List<String> data = new List<string>();
            foreach (faculty fac in faculty)
            {
                data.Add(fac.fname);
            }
            EditUserModel model = new EditUserModel() { _employee = db.employees.Single(e => e.employeeid == id), faculty = data };
            return View(model);
        }
        /// <summary>
        /// Submits an edit user request to the database
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditUser(EditUserModel model, String id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.employees.Attach(model._employee);
                    db.ObjectStateManager.ChangeObjectState(model._employee, EntityState.Modified);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Student");
                }
            }
            catch (Exception ex)
            {
                return View(model);
            }
            return View(model);
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
