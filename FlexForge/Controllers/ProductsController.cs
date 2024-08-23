
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using FlexForge.Domain.Domain;
using FlexForge.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using FlexForge.Services.Interface;
namespace FlexForge.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoriesService _categoriesService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IFavoriteProductsService _favoriteProductsService;

        public ProductsController(IProductService productService, IShoppingCartService shoppingCartService, IFavoriteProductsService favoriteProductsService, ICategoriesService categoriesService)
        {
            _productService = productService;
            _shoppingCartService = shoppingCartService;
            _favoriteProductsService = favoriteProductsService;
            _categoriesService = categoriesService;
        }

        // GET: Products/Index
        public IActionResult Index(Guid? categoryId, Guid? subCategoryId)
        {
            var categories = _categoriesService.GetAllCategories();
            var subCategories = _categoriesService.GetAllSubCategories();
            var products = _productService.GetAllProducts(); // Modify as needed for initial view

            if (categoryId.HasValue)
            {
                if (subCategoryId.HasValue)
                {
                    products = _productService.getProductsByCategoryAndSubCategory(categoryId.Value, subCategoryId.Value);
                }
                else
                {
                    products = _productService.getProductsByCategory(categoryId.Value);
                }
            }
            else if (subCategoryId.HasValue)
            {
                products = _productService.getProductsBySubCategory(subCategoryId.Value);
            }

            ViewBag.Categories = categories;
            ViewBag.SubCategories = subCategories;

            return View(products); // Pass filtered products to the view
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            var categories = _categoriesService.GetAllCategories();
            var subCategories = _categoriesService.GetAllSubCategories();
            if (id == null)
            {
                return NotFound();
            }

            var product = _productService.GetDetailsForProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.Categories = categories;
            ViewBag.SubCategories = subCategories;
            return View(product);
        }


        // GET: Products/SupportedSubCategories
        [Authorize(Roles = "Admin")]
        public IActionResult SupportedSubCategories(Guid categoryId)
        {
            var subCategories = _categoriesService.GetSupportedSubCategoriesForCategory(categoryId);
            return Json(subCategories);
        }

        // GET: Products/FilterByCategories
        public IActionResult FilterByCategories(Guid? categoryId, Guid? subCategoryId)
        {
            List<Product> filteredProducts = new List<Product>();

            if (categoryId.HasValue && subCategoryId.HasValue)
            {
                filteredProducts = _productService.getProductsByCategoryAndSubCategory(categoryId.Value, subCategoryId.Value);
            }
            else if (categoryId.HasValue)
            {
                filteredProducts = _productService.getProductsByCategory(categoryId.Value);
            }
            else if (subCategoryId.HasValue)
            {
                filteredProducts = _productService.getProductsBySubCategory(subCategoryId.Value);
            }

            TempData["FilteredProducts"] = filteredProducts;
            return RedirectToAction("Index");
        }


        // GET: Products/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var categories = _categoriesService.GetAllCategories(); // Fetch all categories
            var subCategories = _categoriesService.GetAllSubCategories();
            ViewBag.Categories = new SelectList(categories, "Id", "CategoryName"); // Populate ViewBag
            ViewBag.SubCategories = new SelectList(subCategories, "Id", "SubCategoryName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public IActionResult Create([Bind("Id,ProductName,ProductDescription,ProductImage,Price, CategoryId, AgeGroup, GenderType,SubCategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                Category c = _categoriesService.GetDetailsForCategory(product.CategoryId);
                SubCategory sc = null;
                if (product.SubCategoryId == null)
                {
                    sc = _categoriesService.GetDetailsForSubCategory(product.SubCategoryId);
                }
                product.Id = Guid.NewGuid();
                product.Category = c;
                product.SubCategory = sc;
                _productService.CreateNewProduct(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public IActionResult AddToCart(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _productService.GetDetailsForProduct(id);

            ProductInShoppingCart ps = new ProductInShoppingCart();

            if (product != null)
            {
                ps.ProductId = product.Id;
            }

            return View(ps);
        }


        [HttpPost]
        public IActionResult AddToCartConfirmed(ProductInShoppingCart model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _shoppingCartService.AddToShoppingConfirmed(model, userId);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult AddToProductsConfirmed(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            

            var product = _productService.GetDetailsForProduct(id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

           

            ProductInFavoriteProducts ps = new ProductInFavoriteProducts();

            if (product != null)
            {
                ps.ProductId = product.Id;
            }
            if (product != null && this.IsFavoriteProduct(product.Id))
            {
                _favoriteProductsService.deleteProductFromFavoriteProducts(userId, product.Id);
            }
            else
            {
                _favoriteProductsService.AddToShoppingConfirmed(ps, userId);
            }

            return View("Index", _productService.GetAllProducts());
        }

        public bool IsFavoriteProduct(Guid productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return _favoriteProductsService.IsFavorite(userId, productId);
        }


        // GET: Products/Edit/5
        [Authorize(Roles = "Admin")]

        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _productService.GetDetailsForProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            var categories = _categoriesService.GetAllCategories(); // Fetch all categories
            var subCategories = _categoriesService.GetAllSubCategories();
            ViewBag.Categories = new SelectList(categories, "Id", "CategoryName"); // Populate ViewBag
            ViewBag.SubCategories = new SelectList(subCategories, "Id", "SubCategoryName");
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public IActionResult Edit(Guid id, [Bind("Id,ProductName,ProductDescription,ProductImage,Price,CategoryId, AgeGroup, GenderType, SubCategoryId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Category c = _categoriesService.GetDetailsForCategory(product.CategoryId);
                    SubCategory sc = null;
                    if (product.SubCategoryId == null)
                    {
                        sc = _categoriesService.GetDetailsForSubCategory(product.SubCategoryId);
                    }
                    product.Category = c;
                    product.SubCategory = sc;
                    _productService.UpdateExistingProduct(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _productService.GetDetailsForProduct(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _productService.DeleteProduct(id);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult ImportProducts(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                TempData["Error"] = "Please upload a valid Excel file.";
                return RedirectToAction("Index");
            }

            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                _productService.ImportProductsFromExcel(stream);
            }

            TempData["Success"] = "Products imported successfully!";
            return RedirectToAction("Index");
        }


    }
}
