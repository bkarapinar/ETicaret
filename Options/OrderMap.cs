using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    public class OrderMap : BaseMap<Order>
    {
        public OrderMap()
        {
            Property(x => x.ShippedAddress).HasMaxLength(500).IsOptional();
            Property(x => x.OrderDate).IsOptional();
            Property(x => x.ShippedDate).IsOptional();
            Property(x => x.AppUserID).IsOptional();
            Property(x => x.ShipperID).IsOptional();
           
        }
    }
}
