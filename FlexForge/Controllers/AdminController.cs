using FlexForge.Service.Interface;
using FlexForge.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace FlexForge.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoriesService _categoriesService;
        private readonly IProductService _productService;

        public AdminController(ILogger<HomeController> logger, ICategoriesService categoriesService, IProductService productService)
        {
            _logger = logger;
            _categoriesService = categoriesService;
            _productService = productService;
        }

        public IActionResult Index()
        {
            var categories = _categoriesService.GetAllCategories();
            var subCategories = _categoriesService.GetAllSubCategories();
            ViewBag.Categories = new SelectList(categories, "Id", "CategoryName");
            ViewBag.SubCategories = new SelectList(subCategories, "Id", "SubCategoryName");
            
            return View();
        }
      
    }
}
