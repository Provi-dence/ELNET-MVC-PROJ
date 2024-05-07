using DASH_BOOKING.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DASH_BOOKING.Controllers
{
    public class HomeController : Controller
    {


        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private readonly ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Admin
        public ActionResult Panel()
        {
            List<Event> eventsList = _context.Events.ToList();
            var events = db.Events.ToList();
            return View(eventsList);
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult ThankYou()
        {
            return View();
        }

        public ActionResult Running()
        {
            return View();
        }
        public ActionResult Biking()
        {
            return View();
        }
        public ActionResult Swimming()
        {
            return View();
        }
        public ActionResult UltraMarathon()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string txtUsername, string txtPassword)
        {
            if (IsValidAdmin(txtUsername, txtPassword))
            {
                // Admin credentials are valid, redirect to admin dashboard or another page
                return RedirectToAction("Panel", "Home");
            }
            else
            {
                ViewBag.Message = "Invalid username or password";
                return View("Login"); // Redirect back to the login page with an error message
            }
        }
        private bool IsValidAdmin(string username, string password)
        {
            // Implement your logic to validate admin credentials here
            // This could involve querying your database or any other validation method
            // For demonstration purposes, I'll just check hardcoded credentials
            return username == "admin" && password == "1234";
        }


        [HttpPost]
        public ActionResult UpdateEventStatus(int eventId, string status)
        {
            try
            {
                var eventToUpdate = db.Events.Find(eventId);
                if (eventToUpdate != null)
                {
                    eventToUpdate.Status = status;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Status updated successfully." });
                }
                else
                {
                    return Json(new { success = false, message = "Event not found or invalid Id." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error updating status: " + ex.Message });
            }
        }




        [HttpPost]
        public ActionResult SubmitEvent(EventSubmissionModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var dbContext = new ApplicationDbContext())
                    {
                        var newEvent = new Event
                        {
                            EventName = model.EventName,
                            EventDescription = model.EventDescription,
                            OrganizerName = model.OrganizerName,
                            OrganizerEmail = model.OrganizerEmail,
                            EventCategory = model.EventCategory
                        };

                        dbContext.Events.Add(newEvent);
                        dbContext.SaveChanges();

                        // Log success message for debugging
                        System.Diagnostics.Debug.WriteLine("Event saved successfully.");
                    }

                    SendConfirmationEmail(model.OrganizerEmail);
                    TempData["Message"] = "Thank you for registering your event. You will receive a confirmation email shortly.";

                    return RedirectToAction("ThankYou" ,"Home");
                }
                catch (Exception ex)
                {
                    // Log the exception for debugging
                    System.Diagnostics.Debug.WriteLine("Error saving event: " + ex.Message);
                    ModelState.AddModelError("", "An error occurred while processing your request. Please try again later.");
                }
            }

            // If model state is not valid, return to the contact page with validation errors
            return View("Contact", "Home", model);
        }

        // Method to send confirmation email
        public void SendConfirmationEmail(string emailAddress)
        {
            try
            {
                var message = new System.Net.Mail.MailMessage();
                message.To.Add(emailAddress);
                message.Subject = "Confirmation Email";
                message.Body = "Thank you for submitting your event. We have received your information.";

                var smtpClient = new System.Net.Mail.SmtpClient("smtp.ethereal.email", 587)
                {
                    Credentials = new System.Net.NetworkCredential("keely.lebsack@ethereal.email", "ECCdTNEeuP3cgaSsT2"),
                    EnableSsl = true
                };

                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                Console.WriteLine("Failed to send confirmation email: " + ex.Message);
            }
        }
    }
}