using FlexForge.Domain.Domain;
using FlexForge.Repository.Interface;
using FlexForge.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexForge.Service.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductInShoppingCart> _productInShoppingCartRepository;
        private readonly IUserRepository _userRepository;

        public ProductService(IRepository<Product> productRepository, IRepository<ProductInShoppingCart> productInShoppingCartRepository, IUserRepository userRepository)
        {
            _productRepository = productRepository;
            _productInShoppingCartRepository = productInShoppingCartRepository;
            _userRepository = userRepository;
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

        public List<Product> getProductsByCategory(Guid categoryId)
        {
            var products = this.GetAllProducts();
            var filteredProducts = products
                .Where(p => p.CategoryId == categoryId)
                .ToList();

            return filteredProducts;
        }

        public List<Product> getProductsByCategoryAndSubCategory(Guid categoryId, Guid subCategoryId)
        {
            var products = this.GetAllProducts();
            var filteredProducts = products
                .Where(p => p.SubCategoryId == subCategoryId)
                .ToList();

            return filteredProducts;
        }

        public List<Product> getProductsBySubCategory(Guid subCategoryId)
        {
            List<Product> products = this.GetAllProducts();
            List<Product> filteredProducts = new List<Product>();
            foreach (Product product in products)
            {
                if (product.SubCategoryId == subCategoryId)
                    filteredProducts.Add(product);
            }
            return filteredProducts;
        }

        public void UpdateExistingProduct(Product p)
        {
            _productRepository.Update(p);
        }

    }
}
