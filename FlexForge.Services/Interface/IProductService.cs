﻿
using FlexForge.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FlexForge.Service.Interface
{
    public interface IProductService
    {
        List<Product> GetAllProducts();
        Product GetDetailsForProduct(Guid? id);
        void CreateNewProduct(Product p);
        void UpdateExistingProduct(Product p);
        void DeleteProduct(Guid id);
        void ImportProductsFromExcel(Stream fileStream);
        List<Product> getProductsByCategory(Guid categoryId);
        List<Product> getProductsBySubCategory(Guid subCategoryId);
        List<Product> getProductsByCategoryAndSubCategory(Guid categoryId, Guid subCategoryId); 
    }
}
