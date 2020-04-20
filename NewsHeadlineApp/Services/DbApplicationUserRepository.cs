using Microsoft.EntityFrameworkCore;
using NewsHeadlineApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsHeadlineApp.Services
{
   public class DbApplicationUserRepository : IUserRepository
   {
      private readonly ApplicationDbContext _db;

      public DbApplicationUserRepository(ApplicationDbContext db)
      {
         _db = db;
      }

      public async Task<NewsCategory> AddNewsCategoryAsync(string userName, NewsCategory newsCategory)
      {
         var userToUpdate = await ReadAsync(userName);
         var check = userToUpdate.NewsCategories.FirstOrDefault(nc => nc.Name == newsCategory.Name);
         if(check == null)
         {
            newsCategory.Id = 0;
            userToUpdate.NewsCategories.Add(newsCategory);
            await _db.SaveChangesAsync();
            return newsCategory;
         }
         return null;
      }

      public async Task<NewsSource> AddNewsSourceAsync(string userName, int newsCategoryId, string sourceName)
      {
         var userToUpdate = await ReadAsync(userName);
         var newsCategoryToUpdate = userToUpdate.GetNewsCategoryPreference(newsCategoryId);
         var check = newsCategoryToUpdate.NewsSources.FirstOrDefault(ns => ns.Name == sourceName);
         if(check == null)
         {
            var newsSource = new NewsSource
            {
               Id = 0,
               Name = sourceName,
               NewsCategory = newsCategoryToUpdate
            };
            newsCategoryToUpdate.NewsSources.Add(newsSource);
            await _db.SaveChangesAsync();
            return newsSource;
         }
         return null;
      }

      public async Task<ApplicationUser> ReadAsync(string userName)
      {
         return await _db.Users
            .Include(u => u.NewsCategories)
            .ThenInclude(nc => nc.NewsSources)
            .FirstOrDefaultAsync(u => u.UserName == userName);
      }

      public async Task RemoveNewsCategoryAsync(string userName, int newsCategoryId)
      {
         var userToUpdate = await ReadAsync(userName);
         if(userToUpdate.NewsCategories.Count > 1)
         {
            var ncToRemove = userToUpdate.NewsCategories.FirstOrDefault(nc => nc.Id == newsCategoryId);
            userToUpdate.NewsCategories.Remove(ncToRemove);
            await _db.SaveChangesAsync();
         }
      }

      public async Task RemoveNewsSourceAsync(string userName, int newsSourceId, int newsCategoryId)
      {
         var userToUpdate = await ReadAsync(userName);
         var ncToUpdate = userToUpdate.GetNewsCategoryPreference(newsCategoryId);
         var nsToRemove = ncToUpdate.GetNewsSource(newsSourceId);
         ncToUpdate.NewsSources.Remove(nsToRemove);
         await _db.SaveChangesAsync();
      }

      public async Task UpdateAsync(string id, ApplicationUser user)
      {
         var userToUpdate = _db.Users.Find(id);
         userToUpdate.FirstName = user.FirstName;
         userToUpdate.LastName = user.LastName;
         await _db.SaveChangesAsync();
      }
   }

}
