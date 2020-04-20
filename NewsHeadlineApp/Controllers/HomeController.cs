using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NewsHeadlineApp.Models;
using NewsHeadlineApp.Models.ViewModels;
using NewsHeadlineApp.Services;

namespace NewsHeadlineApp.Controllers
{
   [Authorize]
   public class HomeController : Controller
   {
      private readonly ILogger<HomeController> _logger;
      private readonly IUserRepository _userRepo;
      private readonly IConfiguration _configuration;
      private readonly NewsApi _newsApi;

      public HomeController(IUserRepository userRepo, IConfiguration configuration, NewsApi newsApi,
         ILogger<HomeController> logger)
      {
         _logger = logger;
         _userRepo = userRepo;
         _configuration = configuration;
         _newsApi = newsApi;
      }

      public async Task<IActionResult> Index(
         [Bind(Prefix="id")]int ncIndex = 0, [Bind(Prefix = "otherId")]int nsIndex = 0)
      {
         var user = await _userRepo.ReadAsync(User.Identity.Name);
         if (ncIndex < 0) ncIndex = 0;
         if (ncIndex >= user.NewsCategories.Count) ncIndex = user.NewsCategories.Count - 1;
         var newsCategory = user.GetNewsCategoryByIndex(ncIndex);

         if (nsIndex < 0) nsIndex = 0;
         if (nsIndex >= newsCategory.NewsSources.Count) nsIndex = newsCategory.NewsSources.Count - 1;
         var newsSource = newsCategory.GetNewsSourceByIndex(nsIndex);
         string newsSourceName = "No news source!";
         ICollection<NewsArticleVM> articles = new List<NewsArticleVM>();
         if(newsSource != null)
         {
            newsSourceName = newsSource.Name;
            articles = await _newsApi.ReadArticlesAsync(newsSourceName, user.Language);
         }

         int ncNextIndex = ncIndex + 1;
         if (ncNextIndex >= user.NewsCategories.Count) ncNextIndex = 0;
         int nsNextIndex = nsIndex + 1;
         if (nsNextIndex >= newsCategory.NewsSources.Count) nsNextIndex = 0;

         var vm = new HomeIndexVM
         {
            NCIndex = ncIndex,
            NSIndex = nsIndex,
            NCNextIndex = ncNextIndex,
            NSNextIndex = nsNextIndex,
            NewsCategoryName = newsCategory.Name,
            NewsSourceName = newsSourceName,
            Articles = articles
         };
         return View(vm);
      }

      public IActionResult Privacy()
      {
         return View();
      }

      [AllowAnonymous]
      public IActionResult About()
      {
         return View();
      }

      [AllowAnonymous]
      public IActionResult Developer()
      {
         return View();
      }

      [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
      public IActionResult Error()
      {
         return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
      }
   }
}
