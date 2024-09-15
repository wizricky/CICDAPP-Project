using FlexForge.Domain.Domain;
using FlexForge.Service.Interface;
using FlexForge.Services.Implementation;
using FlexForge.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FlexForge.Web.Controllers
{
    public class CafeBarProductController : Controller
    {
        private readonly IProductService _productService;


        public CafeBarProductController(IProductService productService)
        {
            _productService = productService;
        }
        // GET: CafeBarProduct/Index
        public IActionResult Index()
        {
            List<CafeBarProduct> products = _productService.GetAllCafeBarProducts();
            return View(products);
        }
    }
}
