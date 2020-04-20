using NewsHeadlineApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsHeadlineApp.Services
{
   public interface IUserRepository
   {
      Task<ApplicationUser> ReadAsync(string userName);
      Task UpdateAsync(string id, ApplicationUser user);
      Task<NewsCategory> AddNewsCategoryAsync(string userName, NewsCategory newsCategory);

      Task RemoveNewsCategoryAsync(string userName, int newsCategoryId);

      Task<NewsSource> AddNewsSourceAsync(string userName, int newsCategoryId, string sourceName);

      Task RemoveNewsSourceAsync(string userName, int newsSourceId, int newsCategoryId);
   }

}
