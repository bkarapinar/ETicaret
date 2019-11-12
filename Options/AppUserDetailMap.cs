using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    public class AppUserDetailMap : BaseMap<AppUserDetail>
    {
        public AppUserDetailMap()
        {
            Property(x => x.Address).HasMaxLength(500);
            Property(x => x.FirstName).HasMaxLength(50).IsRequired();
            Property(x => x.LastName).HasMaxLength(50).IsRequired();
            Property(x => x.TcKimlikNo).HasMaxLength(11);
        }
    }
}
