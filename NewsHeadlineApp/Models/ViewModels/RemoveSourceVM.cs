using NewsHeadlineApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsHeadlineApp.Models.ViewModels
{
   public class RemoveSourceVM
   {
      public NewsCategory NewsCategory { get; set; }
      public NewsSource NewsSource { get; set; }
   }
}
