using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DASH_BOOKING.Models
{
    public class EventSubmissionModel
    {
        [Required(ErrorMessage = "Event Name is required.")]
        public string EventName { get; set; }

        [Required(ErrorMessage = "Event Description is required.")]
        public string EventDescription { get; set; }

        [Required(ErrorMessage = "Organizer Name is required.")]
        public string OrganizerName { get; set; }

        [Required(ErrorMessage = "Organizer Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string OrganizerEmail { get; set; }

        [Required(ErrorMessage = "Event Category is required.")]
        public string EventCategory { get; set; }

        /*[Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }
        */
    }
}