using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    public class ShipperMap : BaseMap<Shipper>
    {
        public ShipperMap()
        {
            Property(x => x.CompanyName).HasMaxLength(250);
            Property(x => x.Phone).HasMaxLength(20);
        }
    }
}
