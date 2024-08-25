using FlexForge.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FlexForge.Web.Controllers
{
    [Authorize]
    public class FavoriteProductsController : Controller
    {
        private readonly IFavoriteProductsService _favoriteProductsService;
        private readonly IProductService _productService;

        public FavoriteProductsController(IFavoriteProductsService favoriteProductsService, IProductService productService)
        {
            this._favoriteProductsService = favoriteProductsService;
            this._productService = productService;
        }

        // GET: FavoriteProducts
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var products = _favoriteProductsService.getFavoriteProductsInfo(userId);
            return View(products);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id == null)
            {
                return NotFound();
            }

            var product = _productService.GetDetailsForProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.IsFavoriteProduct = _favoriteProductsService.IsFavorite(userId, id);
            return View(product);
        }
    }
}
