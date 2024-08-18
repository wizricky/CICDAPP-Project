
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using FlexForge.Domain.Domain;
using FlexForge.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using FlexForge.Services.Interface;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace FlexForge.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SubCategoryController : Controller
    {
        private readonly ICategoriesService _categoriesService;
        public SubCategoryController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        // GET: Categories

        public IActionResult Index()
        {
            return View(_categoriesService.GetAllSubCategories());
        }

        /*

         // GET: Products/Details/5
         public async Task<IActionResult> Details(Guid? id)
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
        */

        // GET: Category/Create
        public IActionResult Create()
        {
            var categories = _categoriesService.GetAllCategories(); // Fetch all categories
            ViewBag.Categories = new SelectList(categories, "Id", "CategoryName"); // Populate ViewBag
            return View();
        }

        // POST: Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,SubCategoryName, CategoryId")] SubCategory subCategory)
        {
            subCategory.Id = Guid.NewGuid();
            Category c = _categoriesService.GetDetailsForCategory(subCategory.CategoryId);
            subCategory.Category = c;
            _categoriesService.CreateNewSubCategory(subCategory);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCategory = _categoriesService.GetDetailsForSubCategory(id);
            if (subCategory == null)
            {
                return NotFound();
            }
            var categories = _categoriesService.GetAllCategories(); // Fetch all categories
            ViewBag.Categories = new SelectList(categories, "Id", "CategoryName"); // Populate ViewBag
            return View(subCategory);
        }

        // POST: SubCategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(Guid id, [Bind("Id,SubCategoryName, CategoryId")] SubCategory subCategory)
        {
            if (id != subCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Category c = _categoriesService.GetDetailsForCategory(subCategory.CategoryId);
                    subCategory.Category = c;
                    _categoriesService.UpdateExistingSubCategory(subCategory);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(subCategory);
        }

        // GET: Products/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCategory = _categoriesService.GetDetailsForSubCategory(id);
            if (subCategory == null)
            {
                return NotFound();
            }

            return View(subCategory);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _categoriesService.DeleteSubCategory(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
