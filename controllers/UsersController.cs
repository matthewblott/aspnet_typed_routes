using System;
using Microsoft.AspNetCore.Mvc;
using aspnet_typed_routes.models;

namespace aspnet_typed_routes.controllers
{
  public class UsersController : Controller
  {
    public IActionResult Index()
    {
      var userViewModel = new UserViewModel();
      
      return View(userViewModel);
    }

    // [ModelBinder(BinderType = typeof(UserModelBinder))]
    public IActionResult New()
    {
      var model = new UserViewModel();
      
      return View(nameof(Index), model);
    }
    
    [HttpPost]
    public IActionResult Create(UserViewModel model)
    {
      return RedirectToAction(nameof(Index));
    }
    
  }
  
}