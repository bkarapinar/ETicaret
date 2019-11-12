using Project.BLL.RepositoryPattern.ConcRep;
using Project.MODEL.Entities;
using Project.MVCUI.Models.MyTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    public class MemberController : Controller
    {
        // GET: Member
        ProductRepository prep;

        OrderRepository orep;
        OrderDetailRepository odrep;

        public MemberController()
        {
            prep = new ProductRepository();

            orep = new OrderRepository();
            odrep = new OrderDetailRepository();

        }

        public ActionResult Index()
        {
            return View(prep.SelectActives().ToList());
        }
        public ActionResult AddToCart(int id)
        {
            if (Session["member"] == null)
            {
                TempData["uyeDegil"] = "Devam etmek için lütfen üye olun / Üyeliğiniz varsa giriş yapın ";
                Product bekleyenUrun = prep.GetByID(id);
                Session["bekleyenUrun"] = bekleyenUrun;
                return RedirectToAction("Register", "Home");

            }

            Cart c = Session["scart"] == null ? new Cart() : Session["scart"] as Cart;

            Product eklenecekUrun = prep.GetByID(id);

            if (eklenecekUrun.UnitInStock == 1)
            {
                CartItem ci = new CartItem();
                ci.ID = eklenecekUrun.ID;
                ci.Name = eklenecekUrun.ProductName;
                ci.Price = eklenecekUrun.UnitPrice;
                ci.ImagePath = eklenecekUrun.ImagePath;
                c.SepeteEkle(ci);
                eklenecekUrun.UnitInStock--;

            }
            else
            {
                TempData["stoktaYok"] = "Eser satılmıştır";

            }
            Session["scart"] = c;
            return RedirectToAction("Index");

        }

        public ActionResult CartPage()
        {
            if (Session["scart"] != null)
            {
                Cart c = Session["scart"] as Cart;
                return View(c);
            }
            else if (Session["member"] == null)
            {
                TempData["uyeDegil"] = "Devam etmek için lütfen giriş yapın ";
                return RedirectToAction("Login", "Home");
            }
           
            TempData["message"] = "Sepetinizde ürün bulunmamaktadır";
            return View();
        }


        public ActionResult ProductDetail(int id)
        {
            Product stokDurum = prep.GetByID(id);
            if (stokDurum.UnitInStock==0)
            {
                TempData["stoktaYok"] = "Eser satılmıştır";

            }

            return View(prep.GetByID(id));

        }

        public ActionResult Delete(int id)
        {
            if (Session["scart"] != null)
            {
                Cart c = Session["scart"] as Cart;

                c.SepettenSil(id);

                Product stokEkle = prep.GetByID(id);
                stokEkle.UnitInStock=1;


                if ((Session["scart"] as Cart).Sepetim.Count == 0)
                {
                    Session.Remove("scart");
                    TempData["sepetBos"] = "Sepetiniz boşalmıstır.";
                    return RedirectToAction("Index");
                }

                return RedirectToAction("CartPage");

            }

            TempData["silinecekYok"] = "Sepetiniz boş";
            return RedirectToAction("Index");
        }

        public ActionResult SiparisVer()
        {
            if (Session["scart"]!=null)
            {
                Cart c = Session["scart"] as Cart;
                return View(Tuple.Create(c, new Order())); //Sayfanın Post durumunda karşıladığı tipten farklı bir tip gönderdiği için hem Cart, hem Order tipinde veri gönderdik  
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult SiparisVer([Bind(Prefix ="Item2")] Order item)
        {
            //Order nesnesi buraya geldiği zaman ilgili kullanıcıyı Session veya TempData ile nesnenin KullanıcıID sini vermeliyiz. 
            //item.AppUserID = (Session["kullanici"] as AppUser).ID;
            //bu kısımda banka için api çağırılır.
            //bool confirm = Redirect("/.../.../"); false ise TempData(hata mesajı)
            //True ise devam gibi bir yol izlenebilir.


            orep.Add(item); //OrderID oluştu
            //ürünleri sepetten yakala
            Cart sepet = Session["scart"] as Cart; // UrunID leri sepet değişkeninde tutuluyor
            // Order Detail
            // 1     1
            // 1    2
            // 1   3

            foreach (CartItem  urun in sepet.Sepetim)
            {
                OrderDetail od = new OrderDetail();
                od.OrderID = item.ID;
                od.ProductID = urun.ID;
                od.Quantity = urun.Amount;
                od.TotalPrice = urun.Price;
                odrep.Add(od);
            }
            Session.Remove("cart");
            return RedirectToAction("Index");
            





        }



        #region Sipariş Onay / Tamamlama
        //public ActionResult SiparisOnayla()
        //{
        //    if (Session["member"] != null)
        //    {
        //        AppUser mevcutKullanici = Session["member"] as AppUser;
        //        return View(Tuple.Create(new Order(), new PaymentVM()));
        //    }
        //    TempData["message"] = "Sipariş vermek için üye olmalısınız..";
        //    return RedirectToAction("Index");

        //}

        //[HttpPost]
        //public ActionResult SiparisOnayla([Bind(Prefix ="Item1")] Order item, [Bind(Prefix ="Item2")] PaymentVM item2)
        //{
        //    //Burada artık bir client olarak Api'a istek göndermemiz lazım(Api consume)..Bunun icin WebApiClient package'ini Nuget'tan indiriyoruz..

        //    //Bir Api consume etme sürecinde actıgımız degişkenleri veya nesneler veya sürecleri ram'de cok uzun süre tutmamalıyız.

        //    bool result;
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("http://localhost:60402/api/");

        //        var postTask = client.PostAsJsonAsync("Payment/ReceivePayment",item2);
        //        //Burada item2 nesnesini Json olarak gönderiyoruz...Ve Base Adresimizin üzerine Payment/ReceivePayment'i ekliyoruz..
        //        var sonuc = postTask.Result;

        //        if (sonuc.IsSuccessStatusCode)
        //        {
        //            result = true;
        //        }
        //        else
        //        {
        //            result = false;
        //        }
        //    }
        //    if (result)
        //    {
        //        AppUser kullanici = Session["member"] as AppUser;
        //        item.AppUserID = kullanici.ID; //Order'in kim tarafından sipariş edildigini belirliyoruz.
        //        orep.Add(item); //save edildiginde Order nesnesinin ID'si olusacak..
        //        int sonSiparisID = item.ID;
        //        Cart sepet = Session["scart"] as Cart;

        //        foreach (CartItem urun in sepet.Sepetim)
        //        {
        //            OrderDetail od = new OrderDetail();
        //            od.OrderID = sonSiparisID;
        //            od.ProductID = urun.ID;
        //            od.TotalPrice = urun.SubTotal;
        //            od.Quantity = urun.Amount;
        //            odrep.Add(od);

        //        }
        //        TempData["odeme"] = "Siparişiniz bize ulasmıstır..Teşekkür ederiz";
        //        return RedirectToAction("Index");

        //    }
        //    else
        //    {
        //        TempData["odeme"] = "Odeme ile ilgili bir sıkıntı olustu.Lutfen banka ile iletişime geciniz";
        //        return RedirectToAction("Index");
        //    }
        //}

        #endregion

    }
}