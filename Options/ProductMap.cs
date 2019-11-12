using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    public class ProductMap : BaseMap<Product>
    {
        public ProductMap()
        {
            Property(x => x.ProductName).HasMaxLength(500).IsRequired();
            Property(x => x.ProductSize).HasMaxLength(15);
            Property(x => x.UnitInStock).IsOptional();
            Property(x => x.UnitPrice).IsOptional();

        }
    }
}
