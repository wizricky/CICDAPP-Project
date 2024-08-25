using FlexForge.Domain.Domain;
using FlexForge.Domain.Identity;
using FlexForge.Repository.Interface;
using FlexForge.Service.Interface;

namespace FlexForge.Service.Implementation
{
    public class FavoriteProductsService : IFavoriteProductsService
    {
        private readonly IRepository<FavoriteProducts> _favoriteProductsRepository;
        private readonly IRepository<ProductInFavoriteProducts> _productInFavoriteProductsRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<Product> _productRepository;
        public FavoriteProductsService(IUserRepository userRepository, IRepository<FavoriteProducts> favoriteProductsRepository, IRepository<ProductInFavoriteProducts> productInFavoriteProductsRepository, IRepository<Product> productRepository)
        {
            _userRepository = userRepository;
            _favoriteProductsRepository = favoriteProductsRepository;
            _productInFavoriteProductsRepository = productInFavoriteProductsRepository;
            _productRepository = productRepository;
        }
        public bool AddToFavoriteProductsConfirmed(ProductInFavoriteProducts model, string userId)
        {
            var loggedInUser = _userRepository.Get(userId);
            var userFavoriteProducts = loggedInUser.FavoriteProducts;
            if (userFavoriteProducts.ProductInFavorite == null)
            {
                userFavoriteProducts.ProductInFavorite = new List<ProductInFavoriteProducts>();
            }
            userFavoriteProducts.ProductInFavorite.Add(model);
            _favoriteProductsRepository.Update(userFavoriteProducts);
            return true;
        }

        public bool deleteProductFromFavoriteProducts(string userId, Guid productId)
        {
            if (productId != null)
            {
                var loggedInUser = _userRepository.Get(userId);
                var userFavoriteProducts = loggedInUser.FavoriteProducts;
                var product = userFavoriteProducts.ProductInFavorite.Where(x => x.ProductId == productId).FirstOrDefault();
                userFavoriteProducts.ProductInFavorite.Remove(product);
                _favoriteProductsRepository.Update(userFavoriteProducts);
                return true;
            }
            return false;

        }

        public List<Product> getFavoriteProductsInfo(string userId)
        {
            FlexForgeApplicationUser user = _userRepository.Get(userId);

            if (user == null || user.FavoriteProducts == null)
                return new List<Product>();

            var favoriteProductsId = user.FavoriteProducts.Id;

            // Retrieve the list of product IDs from the favorite products
            var productIds = _productInFavoriteProductsRepository
                                .GetAll()
                                .Where(x => x.FavoriteProductsId == favoriteProductsId)
                                .Select(x => x.ProductId)
                                .ToList();

            // Retrieve products based on the list of product IDs
            var products = _productRepository
                                .GetAll()
                                .Where(p => productIds.Contains(p.Id))
                                .ToList();

            return products;
        }

        public bool IsFavorite(string userId, Guid? productId)
        {
            FlexForgeApplicationUser user = _userRepository.Get(userId);
            if (user == null || user.FavoriteProducts == null || productId == null)
                return false;

            FavoriteProducts favoriteProducts = user.FavoriteProducts;
            var productInFavorites = _productInFavoriteProductsRepository
                                     .GetAll()
                                     .Where(x => x.FavoriteProductsId == favoriteProducts.Id);

            foreach (ProductInFavoriteProducts pfp in productInFavorites)
            {
                if (pfp.ProductId == productId)
                    return true;
            }
            return false;
        }

    }
}

