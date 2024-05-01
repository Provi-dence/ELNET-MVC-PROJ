using DASH_BOOKING.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DASH_BOOKING.Controllers
{
    public class ContactController : Controller
    {
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

                    return RedirectToAction("ThankYou");
                }
                catch (Exception ex)
                {
                    // Log the exception for debugging
                    System.Diagnostics.Debug.WriteLine("Error saving event: " + ex.Message);
                    ModelState.AddModelError("", "An error occurred while processing your request. Please try again later.");
                }
            }

            // If model state is not valid, return to the contact page with validation errors
            return View("Contact", model);
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

        public ActionResult ThankYou()
        {
            return View();
        }
    }
}