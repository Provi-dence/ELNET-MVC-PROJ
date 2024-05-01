using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DASH_BOOKING.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        public string EventName { get; set; }

        [Required]
        public string EventDescription { get; set; }

        [Required]
        public string OrganizerName { get; set; }

        [Required]
        public string OrganizerEmail { get; set; }

        [Required]
        public string EventCategory { get; set; }
    }
}