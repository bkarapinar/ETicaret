using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MODEL.Entities
{
    public class AppUserDetail : BaseEntity
    {
        [Required(ErrorMessage ="Lütfen isminizi girin!"),MaxLength(50, ErrorMessage = "En fazla 50 karakter girebilirsiniz!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage ="Lütfen soyisiminizi girin!"),MaxLength(50, ErrorMessage = "En fazla 50 karakter girebilirsiniz!")]
        public string LastName { get; set; }

        [MaxLength(500, ErrorMessage = "En fazla 500 karakter girebilirsiniz!")]
        public string Address { get; set; }

        [Phone(ErrorMessage = "Telefon formatında giriş yapınız!")]
        public string PhoneNumber { get; set; }
        //todo kontrol et
        [MinLength(11, ErrorMessage = "TC Kimlik No 11 haneli olmalıdır!"), MaxLength(11, ErrorMessage = "TC Kimlik No 11 haneli olmalıdır!")]
        public string TcKimlikNo { get; set; }
       

        //Relational Properties

        public virtual AppUser AppUser { get; set; }

    }
}
