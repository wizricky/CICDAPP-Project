using FlexForge.Domain.Domain;
using FlexForge.Domain.DTO;
using FlexForge.Repository.Interface;
using FlexForge.Service.Interface;
using System.Text;

namespace FlexForge.Service.Implementation
{
    public class FavoriteProductsService : IFavoriteProductsService
    {
        private readonly IRepository<FavoriteProducts> _favoriteProductsRepository;
        private readonly IRepository<ProductInFavoriteProducts> _productInFavoriteProductsRepository;
        private readonly IUserRepository _userRepository;
        //private readonly IEmailService _emailService;


        public FavoriteProductsService(IUserRepository userRepository, IRepository<FavoriteProducts> favoriteProductsRepository, IRepository<ProductInFavoriteProducts> productInFavoriteProductsRepository)
        {
            _userRepository = userRepository;
            _favoriteProductsRepository = favoriteProductsRepository;
            _productInFavoriteProductsRepository = productInFavoriteProductsRepository;
            //_emailService = emailService;
        }
        public bool AddToShoppingConfirmed(ProductInFavoriteProducts model, string userId)
        {

            var loggedInUser = _userRepository.Get(userId);

            var userFavoriteProducts = loggedInUser.FavoriteProducts;

            if (userFavoriteProducts.ProductInFavorite == null)
                userFavoriteProducts.ProductInFavorite = new List<ProductInFavoriteProducts>(); ;

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

        public FavoriteProductsDto getFavoriteProductsInfo(string userId)
        {
            var loggedInUser = _userRepository.Get(userId);

            var userFavoriteProducts = loggedInUser?.FavoriteProducts;
            var allProduct = userFavoriteProducts?.ProductInFavorite?.ToList();

            FavoriteProductsDto dto = new FavoriteProductsDto
            {
                Products = allProduct
            };
            return dto;
        }

        public bool IsFavorite(string userId, Guid productId)
        {
            FavoriteProductsDto _favoriteProductsDto = this.getFavoriteProductsInfo(userId);
            if (_favoriteProductsDto != null && _favoriteProductsDto.Products != null)
            {
                return _favoriteProductsDto.Products.Any(p => p.ProductId == productId);
            }
            return false;
        }

    }
}

