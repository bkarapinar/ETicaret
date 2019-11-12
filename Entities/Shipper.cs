using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MODEL.Entities
{
    public class Shipper : BaseEntity
    {
        [MaxLength(250, ErrorMessage = "En fazla 250 karakter girebilirsiniz!")]
        public string CompanyName { get; set; }

        [MaxLength(20, ErrorMessage = "En fazla 20 karakter girebilirsiniz!")]
        public string Phone { get; set; }

        //Relational Properties
        public virtual List<Order> Orders { get; set; }

    }
}
