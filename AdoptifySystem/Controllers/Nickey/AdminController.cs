using AdoptifySystem.Models.nickeymodel;
using Google.Authenticator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using System.Web.Security;
using AdoptifySystem;

namespace AdoptifySystem.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        private const string key = "qaz123!@@)(*";//this needs to be generated for each person so that it is unique barcode
        /* any 10-12 char string for use as private key in google authenticator
        use later for generate Google authenticator code.*/

        //this is the Db that i will be unstatnitatiung to use thought the whole controller
        Wollies_ShelterEntities db = new Wollies_ShelterEntities();
        public static Flexible flex = new Flexible();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User_ login)
        {

            //bool status = false;
            Wollies_ShelterEntities db = new Wollies_ShelterEntities();
            List<User_> Users;
            try
            {
                Users = db.User_.ToList();
            }
            catch (Exception e)
            {
                ViewBag.err = e.Message;
                throw;
            }

            foreach (var item in Users)
            {
                if (item.Username == login.Username && item.Password == login.Password)
                {
                    Session["Username"] = login.Username;
                    flex.currentuser = item;

                    //2FA Setup
                    TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
                    string UserUniqueKey = (login.Username + key);
                    Session["UserUniqueKey"] = UserUniqueKey;
                    //var setupInfo = tfa.GenerateSetupCode("Wollies Shelter", login.Username, UserUniqueKey, 300, 300);
                    //ViewBag.BarcodeImageUrl = setupInfo.QrCodeSetupImageUrl;
                    //ViewBag.SetupCode = setupInfo.ManualEntryKey;
                    //message = "Credentials are correct";
                    return View("Authorize", flex);
                }
            }
            return View();
        }

        public ActionResult Authorize()
        {
            return View();
        }
        //authorized user will be redirected to after successful login
        public ActionResult MyProfile()
        {
            if (Session["Username"] == null || Session["IsValid2FA"] == null || !(bool)Session["IsValid2FA"])
            {
                return RedirectToAction("Login");
            }

            ViewBag.Message = "Welcome " + Session["Username"].ToString();
            return View();
        }
        [HttpPost]
        //here this is where we go and authorize the code that was genereated on the user mobile application
        public ActionResult Verify2FA(string token)
        {
            //var token = Request["passcode"];
            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            string UserUniqueKey = Session["UserUniqueKey"].ToString();
            bool isValid = tfa.ValidateTwoFactorPIN(UserUniqueKey, token);
            if (isValid)
            {
                Session["IsValid2FA"] = true;
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Login", "Admin");
        }
        public ActionResult Logout()
        {
            return Redirect("Login");
        }
        public ActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ResetPassword(string EmailID)
        {
            //Verify Email ID
            //Generate Reset password link 
            //Send Email 
            string message = "";
            //bool status = false;

            using (Wollies_ShelterEntities dc = new Wollies_ShelterEntities())
            {

                //adding a employee
                //Employee emp = new Employee();
                //emp.Emp_Name = "jem";
                //emp.Emp_Email = "jemimakola99@gmail.com";
                //dc.Employees.Add(emp);
                //dc.SaveChanges();

                //User_ useremp = new User_();
                //useremp.Emp_ID = 1;
                //useremp.Username = "nick";
                //useremp.Password = "john";
                //dc.User_.Add(useremp);
                //dc.SaveChanges();
                var account = dc.Employees.Where(a => a.Emp_Email == EmailID).FirstOrDefault();
                if (account != null)
                {
                    var user = dc.User_.Where(a => a.Emp_ID == account.Emp_ID).FirstOrDefault();
                    if (user != null)
                    {
                        //Send email for reset password
                        string resetCode = Membership.GeneratePassword(12, 1);
                        SendVerificationLinkEmail(account.Emp_Email, resetCode, "ResetPassword");
                        user.Password = resetCode;
                        //This line I have added here to avoid confirm password not match issue , as we had added a confirm password property 
                        //in our model class in part 1
                        dc.Configuration.ValidateOnSaveEnabled = false;
                        dc.SaveChanges();
                        message = "Reset password link has been sent to your email id.";
                    }
                    else
                    {
                        message = "Account not found";
                    }
                }
                else
                {
                    message = "Account not found";
                }

            }
            ViewBag.Message = message;
            return View();
        }
        public ActionResult Checkin()
        {
            List<Employee> emp = new List<Employee>();
            emp = db.Employees.Where(z => z.Employee_Status.Employee_Status_Name == "Checked-out").ToList();
            flex.employeelist = emp;
            flex.employee = null;
            return View(flex);
        }
        [HttpPost]
        public ActionResult Checkin(int? id)
        {
            id++;
            DateTime datenow = DateTime.Now;
            TimeSheet time = new TimeSheet();
            time.Emp_ID = flex.employee.Emp_ID;
            time.Check_in = datenow;

            db.TimeSheets.Add(time);
            db.SaveChanges();
            var emp = db.Employees.Where(z => z.Emp_ID == flex.employee.Emp_ID).FirstOrDefault();
            var empold = db.Employees.Where(z => z.Emp_ID == flex.employee.Emp_ID).FirstOrDefault();
            if (emp == null)
            {
                ViewBag.err = "Employee is not found";
                return View();
            }
            empold.Employee_Status_ID = 1;
            db.Entry(emp).CurrentValues.SetValues(empold);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");


        }
        [HttpPost]
        public ActionResult GetUserDetails(int? id, string button)
        {
            //int userId = Convert.ToInt32(Request.Form["id"]);
            //fetch the data by userId and assign in a variable, for ex: myUser
            //Flexible myUser = new Flexible();
            if (id == null)
            {
                return RedirectToAction("Checkin");
            }
            id++;
            var emp = flex.employeelist.Where(z => z.Emp_ID == id).FirstOrDefault();
            if (id == null)
            {
                ViewBag.err = "Employee not found";
                return RedirectToAction("Checkin");
            }
            flex.employee = emp;
            if (button == "checkout")
            {
                return View("Checkout", flex);
            }
            if (button == "checkin")
            {
                return View("Checkin", flex);
            }
            return View("Search");

        }
        public ActionResult Checkout()
        {
            return View();
        }
        public ActionResult Checkout(int? id)
        {
            id++;
            DateTime datenow = DateTime.Now;
            TimeSheet time = new TimeSheet();
            time.Emp_ID = flex.employee.Emp_ID;
            time.Check_out = datenow;
            //find the timesheet
            var timesheet = db.TimeSheets.Where(z => z.Emp_ID == time.Emp_ID && time.Check_in.Equals(datenow.Date)).FirstOrDefault();
            var oldtimesheet = db.TimeSheets.Where(z => z.Emp_ID == time.Emp_ID && time.Check_in.Equals(datenow.Date)).FirstOrDefault();
            if (timesheet == null)
            {
                ViewBag.err = "Timesheet is not found";
                return View();
            }
            timesheet.Check_out = time.Check_out;
            db.Entry(oldtimesheet).CurrentValues.SetValues(timesheet);
            db.SaveChanges();
            var emp = db.Employees.Where(z => z.Emp_ID == flex.employee.Emp_ID).FirstOrDefault();
            var empold = db.Employees.Where(z => z.Emp_ID == flex.employee.Emp_ID).FirstOrDefault();
            if (emp == null)
            {
                ViewBag.err = "Employee is not found";
                return View();
            }
            empold.Employee_Status_ID = 2;
            db.Entry(emp).CurrentValues.SetValues(empold);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        public ActionResult AddUserRole()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddUserRole(Role_ role, string button)
        {
            ViewBag.errorMessage = "";
            //Donation_Type asd = new Donation_Type();
            if (button == "Save")
            {
                try
                {

                    List<Role_> Roles = new List<Role_>();
                    Roles = db.Role_.ToList();
                    if (Roles.Count != 0)
                    {
                        int count = 0;
                        foreach (var item in Roles)
                        {
                            if (item.Role_Name == role.Role_Name)
                            {
                                count++;
                                ViewBag.errorMessage = "There is a duplicate Donation Type Already";
                                return View();
                            }

                        }
                        if (count == 0)
                        {
                            db.Role_.Add(role);
                            db.SaveChanges();
                        }
                    }
                    else
                    {

                        db.Role_.Add(role);
                        db.SaveChanges();


                    }

                }
                catch (Exception e)
                {
                    ViewBag.errorMessage = "There was an Error with network please try again: " + e.Message;
                    return View();
                }

            }
            else if (button == "Cancel")
            {

                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SearchUserRole()
        {

            ViewBag.errormessage = "";
            List<Role_> roles = new List<Role_>();
            try
            {
                roles = db.Role_.ToList();
                if (roles.Count == 0)
                {

                }
                return View(roles);
            }
            catch (Exception e)
            {
                ViewBag.errormessage = "there was a network error: " + e.Message;
                return View();
            }

        }

        public ActionResult MaintainUserRole(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role_ role = db.Role_.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }
        [HttpPost]
        public ActionResult MaintainUserRole(Role_ role)
        {

            return View();
        }

        [NonAction]
        public void SendVerificationLinkEmail(string emailID, string activationCode, string emailFor = "VerifyAccount")
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("u17136319@tuks.co.za");
                mail.To.Add(emailID);
                mail.Subject = "Wollies Animal Shelter Passeword Reset Code";
                mail.Body = "<h1>Hello There!</h1><br><h3>Please Use the New Password Below to Login:<br>" + "  " + activationCode + "</h3>";
                mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment("C:\\file.zip"));

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("u17136319@tuks.co.za", "Urahara123");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
            //var smtp = new SmtpClient
            //{
            //    Host = "smtp.gmail.com",
            //    Port = 587,
            //    EnableSsl = true,
            //    DeliveryMethod = SmtpDeliveryMethod.Network,
            //    UseDefaultCredentials = false,
            //    Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            //};

            //using (var message = new MailMessage(fromEmail, toEmail)
            //{
            //    Subject = subject,
            //    Body = body,
            //    IsBodyHtml = true
            //})
            //    smtp.Send(message);
        }
    }
}