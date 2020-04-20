using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsHeadlineApp.Models.Entities;
using NewsHeadlineApp.Models.ViewModels;
using NewsHeadlineApp.Services;

namespace NewsHeadlineApp.Controllers
{
   [Authorize]
   public class ProfileController : Controller
   {
      private IUserRepository _userRepo;

      public ProfileController(IUserRepository userRepo)
      {
         _userRepo = userRepo;
      }

      public async Task<IActionResult> Manage()
      {
         var user = await _userRepo.ReadAsync(User.Identity.Name);
         var profileVM = new ManageProfileVM
         {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Language = user.Language,
            Country = user.Country,
            NewsCategories = user.NewsCategories
         };
         return View(profileVM);
      }

      [HttpPost]
      public async Task<IActionResult> Manage(ManageProfileVM profileVM)
      {
         var user = await _userRepo.ReadAsync(User.Identity.Name);
         if (ModelState.IsValid)
         {
            var updatedUser = new ApplicationUser
            {
               FirstName = profileVM.FirstName,
               LastName = profileVM.LastName
            };
            await _userRepo.UpdateAsync(user.Id, updatedUser);
            return RedirectToAction("Manage");
         }
         var badProfileVM = new ManageProfileVM
         {
            FirstName = profileVM.FirstName,
            LastName = profileVM.LastName,
            Language = user.Language,
            Country = user.Country,
            NewsCategories = user.NewsCategories
         };
         return View(badProfileVM);
      }
   }
}