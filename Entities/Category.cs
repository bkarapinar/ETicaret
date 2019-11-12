using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MODEL.Entities
{
    public class Category : BaseEntity
    {
        [MaxLength(50,ErrorMessage ="Kategori İsmi en Fazla 50 karakterden oluşabilir!"),Required(ErrorMessage ="Kategori ismi zorunludur")]
        public string CategoryName { get; set; }


        //Relational Properties

        public virtual List<Product> Products { get; set; }
        public Category()
        {
            Products = new List<Product>();
        }
    }
}
