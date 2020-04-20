using NewsHeadlineApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsHeadlineApp.Models.ViewModels
{
   public class CreateCategoryVM
   {
      public ApplicationUser User { get; set; }
      public string[] NewsCategories { get; set; }
      public string Name { get; set; }
   }
}
