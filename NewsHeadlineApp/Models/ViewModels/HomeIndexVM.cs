using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsHeadlineApp.Models.ViewModels
{
   public class HomeIndexVM
   {
      public int NCIndex { get; set; }
      public int NSIndex { get; set; }
      public int NCNextIndex { get; set; }
      public int NSNextIndex { get; set; }
      public string NewsCategoryName { get; set; }
      public string NewsSourceName { get; set; }
      public ICollection<NewsArticleVM> Articles { get; set; }
   }
}
