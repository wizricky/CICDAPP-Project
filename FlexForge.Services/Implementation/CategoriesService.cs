using FlexForge.Domain.Domain;
using FlexForge.Repository.Interface;
using FlexForge.Services.Interface;
using System.Collections.Generic;

namespace FlexForge.Services.Implementation
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<SubCategory> _subCategoryRepository;

        public CategoriesService(IRepository<Category> categoryRepository, IRepository<SubCategory> subCategoryRepository) 
        {
            _categoryRepository = categoryRepository;
            _subCategoryRepository = subCategoryRepository;
    }
        public void CreateNewCategory(Category c)
        {
            _categoryRepository.Insert(c);
        }

        public void CreateNewSubCategory(SubCategory c)
        {
            _subCategoryRepository.Insert(c);
        }

        public void DeleteCategory(Guid id)
        {
            Category c = _categoryRepository.Get(id);
            _categoryRepository.Delete(c);
        }

        public void DeleteSubCategory(Guid id)
        {
            SubCategory c = _subCategoryRepository.Get(id);
            _subCategoryRepository.Delete(c);
        }

        public List<Category> GetAllCategories()
        {
            return _categoryRepository.GetAll().ToList();
        }

        public List<SubCategory> GetAllSubCategories()
        {
            var subCategories = _subCategoryRepository.GetAll();
            if (subCategories != null)
            {
                return subCategories.ToList();
            }
            return new List<SubCategory>();
        }


        public Category GetDetailsForCategory(Guid? id)
        {
            var category = _categoryRepository.Get(id);
            return category;
        }

        public SubCategory GetDetailsForSubCategory(Guid? id)
        {
            var subCategory = _subCategoryRepository.Get(id);
            return subCategory;
        }

        public List<SubCategory> GetSupportedSubCategoriesForCategory(Guid? id)
        {
            if (!id.HasValue)
            {
                return new List<SubCategory>();
            }

            List<SubCategory> allSubCategories = GetAllSubCategories();
            List<SubCategory> subCategories = new List<SubCategory>();

            foreach (SubCategory sc in allSubCategories)
            {
                if (sc.CategoryId == id.Value) // Use id.Value to get the actual Guid
                {
                    subCategories.Add(sc);
                }
            }

            return subCategories;
        }

        public void UpdateExistingCategory(Category c)
        {
            _categoryRepository.Update(c);
        }

        public void UpdateExistingSubCategory(SubCategory c)
        {
            _subCategoryRepository.Update(c);
        }
    }
}
