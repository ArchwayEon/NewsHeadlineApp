using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsHeadlineApp.Models.Entities
{
   public class NewsSource
   {
      public int Id { get; set; }
      public string Name { get; set; }
      public NewsCategory NewsCategory { get; set; }
   }
}
