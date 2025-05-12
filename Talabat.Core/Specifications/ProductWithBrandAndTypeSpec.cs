using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductWithBrandAndTypeSpec : BaseSpecifications<Product>
    {
        public ProductWithBrandAndTypeSpec(ProductSpecParams specParams)
            : base(P =>
                    (string.IsNullOrEmpty(specParams.Search) || P.Name.Contains(specParams.Search) )
                    &&
                    (!specParams.ProductBrandId.HasValue || P.ProductBrandId == specParams.ProductBrandId.Value)
                     &&
                    (!specParams.ProductTypeId.HasValue || P.ProductTypeId == specParams.ProductTypeId.Value)
            )
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);

            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
                {
                    case "priceAs":
                        AddOrderByAs(P => P.Price);
                        break;

                    case "priceDes":
                        AddOrderByDes(P => P.Price);
                        break;

                    default:
                        AddOrderByAs(P => P.Name);
                        break;
                }
            }


            // Total Product =  18
            // page_Size     =  5
            // page_Index    =  2

            ApplyPagination((specParams.PageSize) * (specParams.PageIndex - 1), specParams.PageSize);




        }

        public ProductWithBrandAndTypeSpec(int ID) : base(P => P.Id == ID)
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);
        }

    }
}
