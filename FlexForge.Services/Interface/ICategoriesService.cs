using FlexForge.Domain.Domain;
namespace FlexForge.Services.Interface
{
    public interface ICategoriesService
    { 
        //categories
        List<Category> GetAllCategories();
        void CreateNewCategory(Category c);
        void UpdateExistingCategory(Category c);
        void DeleteCategory(Guid id);
        Category GetDetailsForCategory(Guid? id);

        //sub categories
        List<SubCategory> GetAllSubCategories();
        void CreateNewSubCategory(SubCategory c);
        void UpdateExistingSubCategory(SubCategory c);
        void DeleteSubCategory(Guid id);
        SubCategory GetDetailsForSubCategory(Guid? id);

        //Get supported SubCategories for Category
        List<SubCategory> GetSupportedSubCategoriesForCategory(Guid? id);
    }
}
