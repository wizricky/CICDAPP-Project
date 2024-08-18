using FlexForge.Domain.Identity;


namespace FlexForge.Domain.Domain
{
    public class FavoriteProducts: BaseEntity
    {
        public string? OwnerId { get; set; }
        public FlexForgeApplicationUser? Owner { get; set; }
        public virtual ICollection<ProductInFavoriteProducts>? ProductInFavorite { get; set; }
    }
}
