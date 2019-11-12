using Project.BLL.RepositoryPattern.ConcRep;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        // GET: Admin/Order
        OrderRepository orep;
        OrderDetailRepository odrep;
        public OrderController()
        {
            orep = new OrderRepository();
            odrep = new OrderDetailRepository();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult OrderList()
        {
            return View(Tuple.Create(odrep.SelectActives().ToList(),orep.SelectActives().ToList()));
        }
        //Tamamlanacak
    }
}