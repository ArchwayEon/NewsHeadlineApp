using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsHeadlineApp.Models.Entities
{
   public class NewsCategory
   {
      public int Id { get; set; }
      public string Name { get; set; }
      public string ApplicationUserId { get; set; }
      public ApplicationUser ApplicationUser { get; set; }
      public ICollection<NewsSource> NewsSources { get; set; } 
         = new List<NewsSource>();

      public NewsSource GetNewsSource(int newsSourceId)
      {
         return NewsSources.FirstOrDefault(ns => ns.Id == newsSourceId);
      }

      public NewsSource GetNewsSourceByIndex(int nsIndex)
      {
         if (NewsSources.Count == 0) return null;
         return NewsSources.ElementAt(nsIndex);
      }
   }
}
