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
        public ProductWithBrandAndTypeSpec(string sort)
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
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
        }

        public ProductWithBrandAndTypeSpec(int ID) : base(P => P.Id == ID)
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);
        }

    }
}
