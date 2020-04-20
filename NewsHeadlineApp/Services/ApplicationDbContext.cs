using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewsHeadlineApp.Models.Entities;

namespace NewsHeadlineApp.Services
{
   public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
   {
      public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options)
      {
      }

      public DbSet<NewsCategory> NewsCategories { get; set; }
      public DbSet<NewsSource> NewsSources { get; set; }
   }
}
