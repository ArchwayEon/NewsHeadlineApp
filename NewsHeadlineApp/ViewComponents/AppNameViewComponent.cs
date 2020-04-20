using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NewsHeadlineApp.Models.ViewModels;

namespace NewsHeadlineApp.ViewComponents
{
   public class AppNameViewComponent : ViewComponent
   {
      private IConfiguration _config;

      public AppNameViewComponent(IConfiguration config)
      {
         _config = config;
      }

      public IViewComponentResult Invoke()
      {
         var model = new AppNameVM
         {
            AppName = _config.GetValue<string>("AppName")
         };
         return View(model);
      }
   }

}
