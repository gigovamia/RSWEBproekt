using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using proekt.Areas.Identity.Data;
using proekt.Models;

namespace proekt.Data
{
    public class proektContext : IdentityDbContext<proektUser>
    {
        public proektContext (DbContextOptions<proektContext> options)
            : base(options)
        {
        }

        public DbSet<proekt.Models.Hotel> Hotel { get; set; } = default!;

        public DbSet<proekt.Models.Visit> Visit { get; set; }

        public DbSet<proekt.Models.GuestHouse> GuestHouse { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<proekt.Models.User> User { get; set; }
    }
}
