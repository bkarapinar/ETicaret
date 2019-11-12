using Project.BLL.RepositoryPattern.ConcRep;
using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        CategoryRepository crep;
        public CategoryController()
        {
            crep = new CategoryRepository();
        }

        // GET: Admin/Category
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CategoryList()
        {
            return View(crep.SelectActives());
        }
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(Category item)
        {
            if (!ModelState.IsValid) //ServerSide Validation
            {
                return View();
            }

            crep.Add(item);
            return RedirectToAction("CategoryList");

        }
        public ActionResult DeleteCategory(int id)
        {
            crep.Delete(crep.GetByID(id));
            return RedirectToAction("CategoryList");
        }
        public ActionResult UpdateCategory(int id)
        {
            return View(crep.GetByID(id));
        }

        [HttpPost]
        public ActionResult UpdateCategory(Category item)
        {
            crep.Update(item);
            return RedirectToAction("CategoryList");

        }
    }
    }
