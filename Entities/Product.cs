using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MODEL.Entities
{
    public class Product : BaseEntity
    {
        [MaxLength(500, ErrorMessage = "En fazla 500 karakter girebilirsiniz!"),Required(ErrorMessage ="Ürün ismi girmek zorunludur")]
        public string ProductName { get; set; }
        public int UnitInStock { get; set; }
        public decimal UnitPrice { get; set; }

        [MaxLength(15, ErrorMessage = "En fazla 15 karakter girebilirsiniz!")]
        public string ProductSize { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int? CategoryID { get; set; }

        //Relational Properties
        public virtual Category Category { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
        public Product()
        {
            UnitInStock = 1;
        }

    }
}