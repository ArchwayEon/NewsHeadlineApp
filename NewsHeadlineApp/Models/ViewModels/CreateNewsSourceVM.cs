using NewsHeadlineApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsHeadlineApp.Models.ViewModels
{
   public class CreateNewsSourceVM
   {
      public NewsCategory NewsCategory { get; set; }
      public string Name { get; set; }
      public string[] SourceNameChoices { get; set; }
   }
}
