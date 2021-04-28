using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypeAndBrandSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypeAndBrandSpecification(int id): base( x => x.Id == id)
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);
        }
        public ProductsWithTypeAndBrandSpecification()
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);
        }
    }
}