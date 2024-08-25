using FlexForge.Domain.Domain;
using FlexForge.Domain.Enum;
using FlexForge.Repository.Interface;
using FlexForge.Service.Interface;
using OfficeOpenXml;


namespace FlexForge.Service.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductInShoppingCart> _productInShoppingCartRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<Category> _categoryRepository;
        public ProductService(IRepository<Product> productRepository, IRepository<ProductInShoppingCart> productInShoppingCartRepository, IUserRepository userRepository, IRepository<Category> categoryRepository)
        {
            _productRepository = productRepository;
            _productInShoppingCartRepository = productInShoppingCartRepository;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
        }

        public void CreateNewProduct(Product p)
        {
            _productRepository.Insert(p);
        }

        public void DeleteProduct(Guid id)
        {
            var product = _productRepository.Get(id);
            _productRepository.Delete(product);
        }

        public List<Product> GetAllProducts()
        {
            return _productRepository.GetAll().ToList();
        }

        public Product GetDetailsForProduct(Guid? id)
        {
            var product = _productRepository.Get(id);
            return product;
        }

        public void UpdateExistingProduct(Product p)
        {
            _productRepository.Update(p);
        }
        public void ImportProductsFromExcel(Stream fileStream)
        {
            // Check if the file stream is not null
            if (fileStream == null || fileStream.Length == 0)
                throw new ArgumentException("File stream cannot be null or empty.");

            // Initialize EPPlus package
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(fileStream))
            {
                // Get the first worksheet
                var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                    throw new InvalidOperationException("The Excel file does not contain any worksheets.");

                // Define the start row and end row (assuming the first row is headers)
                int startRow = 2;
                int endRow = worksheet.Dimension.End.Row;

                // Iterate through rows in the worksheet
                for (int row = startRow; row <= endRow; row++)
                {
                    // Read data from the Excel row
                    string productName = worksheet.Cells[row, 1].Text;
                    string productDescription = worksheet.Cells[row, 2].Text;
                    string productImage = worksheet.Cells[row, 3].Text;
                    int price = int.TryParse(worksheet.Cells[row, 4].Text, out var parsedPrice) ? parsedPrice : 0;
                    int rating = int.TryParse(worksheet.Cells[row, 5].Text, out var parsedRating) ? parsedRating : 0;
                    string ageGroupStr= worksheet.Cells[row, 6].Text;
                    string categoryName = worksheet.Cells[row, 7].Text;
                    string genderTypeStr = worksheet.Cells[row, 8].Text;
                    if (!Enum.TryParse<GenderType>(genderTypeStr, true, out var genderType))
                    {
                        throw new InvalidOperationException($"Invalid GenderType value '{genderTypeStr}' at row {row}. Ensure it matches one of the enum values.");
                    }

                    // Parse AgeGroup enum
                    if (!Enum.TryParse<AgeGroup>(ageGroupStr, true, out var ageGroup))
                    {
                        throw new InvalidOperationException($"Invalid AgeGroup value '{ageGroupStr}' at row {row}. Ensure it matches one of the enum values.");
                    }
                    var category = _categoryRepository.GetAll().FirstOrDefault(c => c.CategoryName.Equals(categoryName, StringComparison.OrdinalIgnoreCase));

                    // Handle case where category is not found
                    if (category == null)
                    {
                        throw new InvalidOperationException($"Category '{categoryName}' not found for product '{productName}' at row {row}.");
                    }
                    // Create a new product object
                    var product = new Product
                    {
                        ProductName = productName,
                        ProductDescription = productDescription,
                        ProductImage = productImage,
                        Price = price,
                        Rating = rating,
                        AgeGroup = ageGroup,
                        Category = category,
                        GenderType = genderType,


                    };

                    // Insert the product into the database
                    _productRepository.Insert(product);
                }
            }
        }

    }
}
