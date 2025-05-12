using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductWithFiltersForCountSpec : BaseSpecifications<Product>
    {

        public ProductWithFiltersForCountSpec(ProductSpecParams specParams)
             : base(P =>
                    (string.IsNullOrEmpty(specParams.Search) || P.Name.Contains(specParams.Search))
                    &&
                    (!specParams.ProductBrandId.HasValue || P.ProductBrandId == specParams.ProductBrandId.Value)
                     &&
                    (!specParams.ProductTypeId.HasValue || P.ProductTypeId == specParams.ProductTypeId.Value)
            )

        {

        }
    }
}
