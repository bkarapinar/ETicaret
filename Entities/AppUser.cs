using Project.MODEL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MODEL.Entities
{
    public class AppUser : BaseEntity
    {
        [Required(ErrorMessage = "Kullanıcı ismini girmek zorunludur."), MinLength(5, ErrorMessage = "En az 5 karakter girmelisiniz"), MaxLength(15, ErrorMessage = "15'den fazla karakter giremezsiniz!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Şifre girmek zorunludur."), MinLength(8, ErrorMessage = "En az 8 karakter girmelisiniz"), MaxLength(20, ErrorMessage = "20'den fazla karakter giremezsiniz!")]
        public string Password { get; set; }

        [Required(ErrorMessage ="Şifreyi tekrar giriniz"),Compare("Password", ErrorMessage = "Şifreler uyuşmuyor")]
        public string ConfirmPassword { get; set; }
        public Guid? ActivationCode { get; set; }
        public UserRole Role { get; set; }
        public bool IsBanned { get; set; }
        public bool IsActive { get; set; }

        [EmailAddress(ErrorMessage = "Lütfen Email formatına uygun doldurunuz")]
        public string Email { get; set; }
        public AppUser()
        {
            Role = UserRole.Member;
            ActivationCode = Guid.NewGuid();
            
        }

        //Relational Properties
        public virtual AppUserDetail AppUserDetail { get; set; }
        public virtual List<Order> Orders { get; set; }


    }
}