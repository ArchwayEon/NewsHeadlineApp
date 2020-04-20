using NewsHeadlineApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewsHeadlineApp.Models.ViewModels
{
   public class ManageProfileVM
   {
      [Display(Name ="First Name")]
      public string FirstName { get; set; }
      [Display(Name = "Last Name")]
      public string LastName { get; set; }
      public string Language { get; set; }
      public string Country { get; set; }
      public ICollection<NewsCategory> NewsCategories { get; set; }
   }
}
