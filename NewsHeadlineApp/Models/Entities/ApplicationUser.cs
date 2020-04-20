using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsHeadlineApp.Models.Entities
{
   public class ApplicationUser : IdentityUser
   {
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public string Language { get; set; } = "en";
      public string Country { get; set; } = "us";
      public ICollection<NewsCategory> NewsCategories { get; set; } 
         = new List<NewsCategory>();

      public NewsCategory GetNewsCategoryPreference(int newsCategoryId)
      {
         return NewsCategories.FirstOrDefault(nc => nc.Id == newsCategoryId);
      }

      public NewsCategory GetNewsCategoryByIndex(int ncIndex)
      {
         return NewsCategories.ElementAt(ncIndex);
      }
   }
}
