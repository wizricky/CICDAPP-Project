using FlexForge.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FlexForge.Web.Controllers
{
    public class FavoriteProductsController : Controller
    {
        private readonly IFavoriteProductsService _favoriteProductsService;

        public FavoriteProductsController(IFavoriteProductsService favoriteProductsService)
        {
            this._favoriteProductsService = favoriteProductsService;
        }

        // GET: FavoriteProducts
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var dto = _favoriteProductsService.getFavoriteProductsInfo(userId);
            return View(dto?.Products);
            //return (IActionResult)dto;
        }
        public IActionResult DeleteFromFavoriteProducts(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _favoriteProductsService.deleteProductFromFavoriteProducts(userId, id);

            return RedirectToAction("Index");

        }
    }
}
