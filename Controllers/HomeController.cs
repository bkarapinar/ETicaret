using Project.BLL.RepositoryPattern.ConcRep;
using Project.COMMON.MyTools;
using Project.MODEL.Entities;
using Project.MVCUI.Models.MyTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    public class HomeController : Controller
    {

        AppUserRepository aprep;
        AppUserDetailRepository apdrep;

        public HomeController()
        {
            aprep = new AppUserRepository();
            apdrep = new AppUserDetailRepository();
        }
        // GET: Home
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register([Bind(Prefix = "Item1")]AppUser item, [Bind(Prefix = "Item2")]AppUserDetail item2)
        {
            //if (!ModelState.IsValid) //ServerSide Validation
            //{
            //    return View();
            //}

            if (aprep.Any(x => x.UserName == item.UserName))
            {
                ViewBag.ZatenVar = "Bu kullanıcı ismi kayıtlı. Lütfen başka bir isim giriniz";
                return View();
            }
            else if (aprep.Any(x => x.Email == item.Email))
            {
                ViewBag.ZatenVar = "Bu Email zaten bizde kayıtlıdır";
            }

            //register mail
            string gonderilecekMail = "Hesabınız oluşturulmuştur. Hesabınızı aktive etmek için http://localhost:60402/Home/Aktivasyon/" + item.ActivationCode + " linkine tıklayabilirsiniz.";


            MailSender.Send(item.Email, password: "birsen123", body: gonderilecekMail, subject: "Hesap Aktivasyon!");
            aprep.Add(item);
            item2.ID = item.ID; //appUsreDetail ile appUser ID lerini eşitledik
            apdrep.Add(item2);

            return View("RegisterOk");
            //return View("Index","Member");
        }

        public ActionResult RegisterOk()
        {
            return View();
        }
        public ActionResult Aktivasyon(Guid id)
        {
            if (id != null)
            {

                if (aprep.Any(x => x.ActivationCode == id))
                {
                    AppUser aktiveEdilecek = aprep.Default(x => x.ActivationCode == id);
                    aktiveEdilecek.IsActive = true;
                    aprep.Update(aktiveEdilecek);

                    TempData["HesapAktif"] = "Hesabınız aktif hale getirildi.";
                    Session["member"] = aktiveEdilecek;
                    if (Session["bekleyenUrun"] != null)
                    {
                        Product bekleyenUrun = Session["bekleyenUrun"] as Product;
                        CartItem ci = new CartItem();
                        ci.ID = bekleyenUrun.ID;
                        ci.Name = bekleyenUrun.ProductName;
                        ci.Price = bekleyenUrun.UnitPrice;
                        ci.ImagePath = bekleyenUrun.ImagePath;
                        Cart c = new Cart();
                        c.SepeteEkle(ci);

                        Session["scart"] = c;
                    }
                    return RedirectToAction("Index", "Member");
                }
            }

            TempData["HesapAktif"] = "Aktif edilecek hesap bulunamadı";
            return RedirectToAction("Register");
        }

        public ActionResult Login()
        {
            AppUser newUser = CheckCookie();
            if (newUser == null) return View();
            else return View(newUser); //eğer checkCookie metodu ile AppUser geliyorsa, model olarak view a gönderiyoruz. Sayfadaki alanlara bilgileri otomatik olarak yazması için. Sayfanın karşıladığı model ile gönderilen değerler aynı olduğu için bu işlem bize sorun yaratmayacaktır.
        }

        [HttpPost]
        public ActionResult Login(AppUser item, string Hatirla)
        {
            if (Hatirla == "true")// RememberMe işaretli ise, cookie oluşturuyoruz
            {
                HttpCookie girisIsim = new HttpCookie("userName");
                //cookie yi silmek istiyorsak süreyi kısa tutmalıyız.
                girisIsim.Expires = DateTime.Now.AddMinutes(10);
                girisIsim.Value = item.UserName;
                Response.Cookies.Add(girisIsim);

                HttpCookie girisSifre = new HttpCookie("password");
                girisIsim.Expires = DateTime.Now.AddMinutes(10);
                girisSifre.Value = item.Password;
                Response.Cookies.Add(girisSifre);

            }

            if (aprep.Any(

                x => x.UserName == item.UserName &&
                x.Password == item.Password &&
                x.Status != MODEL.Enums.DataStatus.Deleted &&
                x.IsBanned == false &&
                x.Role == MODEL.Enums.UserRole.Admin

                ))
            {
                AppUser girisYapan = aprep.Default(x => x.UserName == item.UserName && x.Password == item.Password);

                if (girisYapan.IsActive == false)
                {
                    ViewBag.AktifDegil = "Lütfen Hesabınızı aktif hale getiriniz.";
                    return View("RegisterOk");
                }
                Session["admin"] = girisYapan;
                return RedirectToAction("Index","Admin/HomeAdmin"); 

            }
            else if (aprep.Any
                (
                x => x.UserName == item.UserName &&
                x.Password == item.Password &&
                x.Status != MODEL.Enums.DataStatus.Deleted &&
                x.IsBanned == false &&
                x.Role == MODEL.Enums.UserRole.Member

                ))
            {
                AppUser girisYapan = aprep.Default(x => x.UserName == item.UserName && x.Password == item.Password);

                if (girisYapan.IsActive == false)
                {
                    ViewBag.AktifDegil = "Lütfen Hesabınızı aktif hale getiriniz.";
                    return View("Register");
                }

                Session["member"] = girisYapan;
                if (Session["bekleyenUrun"] != null)
                {
                    Product bekleyenUrun = Session["bekleyenUrun"] as Product;
                    CartItem ci = new CartItem();
                    ci.ID = bekleyenUrun.ID;
                    ci.Name = bekleyenUrun.ProductName;
                    ci.Price = bekleyenUrun.UnitPrice;
                    ci.ImagePath = bekleyenUrun.ImagePath;
                    Cart c = new Cart();
                    c.SepeteEkle(ci);
                    bekleyenUrun.UnitInStock--; //todo bu satırı sorun olursa sil
                    Session["scart"] = c;

                }
                return RedirectToAction("Index", "Member");

            }

            ViewBag.KullaniciBulunamadi = "Böyle bir kullanıcı yoktur";
            return View();
        }


        #region Cookie var mı yok mu kontrolü
        // Bu metod ile cookie yi kontrol ettik ve değerleri alıp kullanıcı tipine dönüştürdük.
        public AppUser CheckCookie()
        {
            string userName = string.Empty, password = string.Empty;

            AppUser cookiedeSaklanan = null;

            if (Request.Cookies["userName"] != null && Request.Cookies["password"] != null) //var olan cookie yi request ile kontrol ettik
            {
                userName = Request.Cookies["userName"].Value;
                password = Request.Cookies["password"].Value;
            }
            //boş veya null olmadığını garantiledik.
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                cookiedeSaklanan = new AppUser
                {
                    UserName = userName,
                    Password = password
                };
            }
            return cookiedeSaklanan;

        }

        #endregion

        #region ForgatPassword tamamlanacak

        //public ActionResult ForgatPassword()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult ForgatPassword([Bind(Prefix = "Item2")]string email)
        //{

        //    if (aprep.Any(x => x.Email == email))
        //    {                               
        //        AppUser guncellenecek = aprep.Default(x=>x.Email==email);
        //        Guid yeniAktivasyonKod = Guid.NewGuid();
        //        guncellenecek.ActivationCode = yeniAktivasyonKod;
        //        aprep.Update(guncellenecek);

        //        string gonderilecekMail = "Şifrenizi sıfırlamak için lütfen http://localhost:60402/Home/Aktivasyon/" +guncellenecek + " linkine tıklayabilirsiniz.";

        //        MailSender.Send(guncellenecek.Email, password: "birsen123", body: gonderilecekMail, subject: "Hesap Aktivasyon!");

        //    }

        //    return View("RegisterOk");
        //} 
        #endregion
    }
}