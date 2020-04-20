using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NewsHeadlineApp.Models.Entities;
using NewsHeadlineApp.Models.ViewModels;
using NewsHeadlineApp.Services;

namespace NewsHeadlineApp.Controllers
{
   public class NewsCategoryController : Controller
   {
      private readonly IUserRepository _userRepo;
      private readonly IConfiguration _configuration;

      public NewsCategoryController(IUserRepository userRepo, IConfiguration configuration)
      {
         _userRepo = userRepo;
         _configuration = configuration;
      }

      public async Task<IActionResult> Create()
      {
         var newsCategories = _configuration.GetValue<string>("NewsCategories");
         var user = await _userRepo.ReadAsync(User.Identity.Name);
         var userCategoryNames = user.NewsCategories.Select(nc => nc.Name).ToArray();
         var model = new CreateCategoryVM
         {
            User = user,
            NewsCategories = newsCategories.Split(',').Except(userCategoryNames).ToArray()
         };
         return View(model);
      }

      [HttpPost, ValidateAntiForgeryToken]
      public async Task<IActionResult> Create(CreateCategoryVM vm)
      {
         if (vm.Name != "Choose")
         {
            await _userRepo.AddNewsCategoryAsync(
               User.Identity.Name, new NewsCategory { Name = vm.Name });
         }
         return RedirectToAction("Manage", "Profile");
      }

      public async Task<IActionResult> Delete(int id)
      {
         var user = await _userRepo.ReadAsync(User.Identity.Name);
         var model = user.NewsCategories.FirstOrDefault(nc => nc.Id == id);
         if(model == null)
         {
            return RedirectToAction("Manage", "Profile");
         }
         return View(model);
      }

      [HttpPost, ValidateAntiForgeryToken, ActionName("Delete")]
      public async Task<IActionResult> DeleteConfirmed(int id)
      {
         await _userRepo.RemoveNewsCategoryAsync(User.Identity.Name, id);
         return RedirectToAction("Manage", "Profile");
      }


   }
}