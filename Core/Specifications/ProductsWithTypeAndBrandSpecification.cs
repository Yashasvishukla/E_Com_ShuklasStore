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
        public ProductsWithTypeAndBrandSpecification(ProductSpecParams productSpecParams) :
            base (x => 
                (!productSpecParams.BrandId.HasValue || x.ProductBrandId == productSpecParams.BrandId) &&
                (!productSpecParams.TypeId.HasValue || x.ProductTypeId == productSpecParams.TypeId) &&
                (string.IsNullOrEmpty(productSpecParams.Search) || x.Name.ToLower().Contains(productSpecParams.Search) )
            )
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);
            AddOrderBy(x => x.Name);
            ApplyPagination(productSpecParams.pageSize * (productSpecParams.PageIndex - 1), productSpecParams.pageSize);
            if(!string.IsNullOrEmpty(productSpecParams.Sort))
            {
                switch(productSpecParams.Sort)
                {
                    case "priceAsc" : 
                    AddOrderBy(x => x.Price);
                    break;
                    case "priceDesc" :
                    AddOrderByDescending(x => x.Price);
                    break;
                    default :
                    AddOrderBy( x => x.Name);
                    break;
                }
            }

            
        }

    }
}