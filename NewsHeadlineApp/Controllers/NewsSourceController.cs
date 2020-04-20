using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NewsHeadlineApp.Models.ViewModels;
using NewsHeadlineApp.Services;

namespace NewsHeadlineApp.Controllers
{
   public class NewsSourceController : Controller
   {
      private readonly IUserRepository _userRepo;
      private readonly IConfiguration _configuration;
      private readonly NewsApi _newsApi;

      public NewsSourceController(IUserRepository userRepo, NewsApi newsApi, IConfiguration configuration)
      {
         _userRepo = userRepo;
         _configuration = configuration;
         _newsApi = newsApi;
      }

      public async Task<IActionResult> Index([Bind(Prefix = "id")]int newsCategoryId)
      {
         var user = await _userRepo.ReadAsync(User.Identity.Name);
         var newsCategory = user.GetNewsCategoryPreference(newsCategoryId);
         if (newsCategory == null)
         {
            return RedirectToAction("Manage", "Profile");
         }
         return View(newsCategory);
      }

      public async Task<IActionResult> Create([Bind(Prefix = "id")]int newsCategoryId)
      {
         var user = await _userRepo.ReadAsync(User.Identity.Name);
         var newsCategory = user.GetNewsCategoryPreference(newsCategoryId);
         if (newsCategory == null)
         {
            return RedirectToAction("Manage", "Profile");
         }
         var sourcesStr = await _newsApi.ReadSourcesAsync(
            newsCategory.Name, user.Language, user.Country);
         string[] sourceNames = sourcesStr.Split(",");
         string[] currentSourceNames = newsCategory.NewsSources.Select(ns => ns.Name).ToArray();
         var vm = new CreateNewsSourceVM
         {
            NewsCategory = newsCategory,
            SourceNameChoices = sourceNames.Except(currentSourceNames).ToArray()
         };
         return View(vm);
      }

      [HttpPost, ValidateAntiForgeryToken]
      public async Task<IActionResult> Create(int newsCategoryId, string Name)
      {
         await _userRepo.AddNewsSourceAsync(User.Identity.Name, newsCategoryId, Name);
         return RedirectToAction("Index", "NewsSource", new { id = newsCategoryId });
      }

      public async Task<IActionResult> Delete(
         [Bind(Prefix = "id")]int newsSourceId, [Bind(Prefix = "otherId")]int newsCategoryId)
      {
         var user = await _userRepo.ReadAsync(User.Identity.Name);
         var newsCategory = user.GetNewsCategoryPreference(newsCategoryId);
         if (newsCategory == null)
         {
            return RedirectToAction("Manage", "Profile");
         }
         var newsSource = newsCategory.GetNewsSource(newsSourceId);
         if (newsSource == null)
         {
            return RedirectToAction("Index", "NewsSource", new { id = newsCategoryId });
         }
         var vm = new RemoveSourceVM
         {
            NewsCategory = newsCategory,
            NewsSource = newsSource
         };
         return View(vm);
      }

      [HttpPost, ValidateAntiForgeryToken, ActionName("Delete")]
      public async Task<IActionResult> DeleteConfirmed(int newsSourceId, int newsCategoryId)
      {
         await _userRepo.RemoveNewsSourceAsync(User.Identity.Name, newsSourceId, newsCategoryId);
         return RedirectToAction("Index", "NewsSource", new { id = newsCategoryId });
      }
   }
}