using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared
{
    public class ProductQueryParams
    {
        public int ? BrandId { get; set; }
        public int ? TypeId { get; set; }
        public string ? Search { get; set; }
        public ProductSortingOptions Sort { get; set; }

        private const int  DefaultSize= 5;
        private const int  MaxSize= 10;
        private int _pagesize = DefaultSize;
        public int PageSize
        {
            get
            {
                return _pagesize;
            }

            set
            {
                if (value <= 0)
                    _pagesize = DefaultSize;
                else if (value > MaxSize)
                    _pagesize = MaxSize;
                else
                {
                    _pagesize = value;
                }
            }
        }


        private int _PageIndex = 1;
        public int PageIndex
        {
            get
            {
                return _PageIndex;
            }

            set
            {
                _PageIndex = (value <= 0) ? 1 : value;
            }
        }

    }
}
