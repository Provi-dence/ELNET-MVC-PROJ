using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace DASH_BOOKING.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DASH_BOOKINGContext")
        {

        }

        public DbSet<Event> Events { get; set; }
    }
}