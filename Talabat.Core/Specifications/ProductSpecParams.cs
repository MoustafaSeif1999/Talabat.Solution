using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specifications
{
    public class ProductSpecParams
    {

        private const int Max_PageSize = 10 ;

        private int pageSize = 5 ;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > Max_PageSize? Max_PageSize : value ; }
        }

        public int PageIndex { get; set; } = 1 ;


        private string search;

        public string Search
        {
            get { return search; }
            set { search = value.ToLower(); }
        }


        public string Sort { get; set; }

        public int? ProductBrandId { get; set; }

        public int? ProductTypeId { get; set; }



    }
}
