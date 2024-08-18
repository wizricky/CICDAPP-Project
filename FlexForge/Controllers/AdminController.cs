using FlexForge.Domain;
using FlexForge.Domain.Domain;
using FlexForge.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace FlexForge.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoriesService _categoriesService;

        public AdminController(ILogger<HomeController> logger, ICategoriesService categoriesService)
        {
            _logger = logger;
            _categoriesService = categoriesService;
        }

        public IActionResult Index()
        {
            var categories = _categoriesService.GetAllCategories(); // Fetch all categories
            var subCategories = _categoriesService.GetAllSubCategories();
            ViewBag.Categories = new SelectList(categories, "Id", "CategoryName"); // Populate ViewBag
            ViewBag.SubCategories = new SelectList(subCategories, "Id", "SubCategoryName");
            return View();
        }
    }
}
