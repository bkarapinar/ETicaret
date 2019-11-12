using Project.DAL.Context;
using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Strategy
{
    public class MyInit : CreateDatabaseIfNotExists<MyContext>
    {
        protected override void Seed(MyContext context)
        {
            #region user-userDetail ekleme yapmıyor

            AppUser ap1 = new AppUser
            {
                ID=1,
                UserName = "admin",
                Password = "123456789",
                Email = "birsen7@gmail.com",
                Role = MODEL.Enums.UserRole.Admin,
                IsBanned = false,
                IsActive = true,
                Status=MODEL.Enums.DataStatus.Inserted,
                CreatedDate=DateTime.Now

            };

            context.AppUsers.Add(ap1);
            context.SaveChanges();


            AppUserDetail apd1 = new AppUserDetail
            {               
                FirstName = "Emel",
                LastName = "Cebecioğlu",
                ID = ap1.ID,
                CreatedDate=DateTime.Now,
                Status=MODEL.Enums.DataStatus.Inserted

            };

            context.AppUserDetails.Add(apd1);
            context.SaveChanges();

            #endregion


            Category c = new Category
            {
                ID = 1,
                CategoryName = "Soyut"
            };
            context.Categories.Add(c);
            context.SaveChanges();

            Category c2 = new Category
            {
                ID = 2,
                CategoryName = "Portre"
            };
            context.Categories.Add(c2);
            context.SaveChanges();

            Category c3 = new Category
            {
                ID = 3,
                CategoryName = "Natürmort"
            };
            context.Categories.Add(c3);
            context.SaveChanges();

            Category c4 = new Category
            {
                ID = 4,
                CategoryName = "Janr"
            };
            context.Categories.Add(c4);
            context.SaveChanges();

            Category c5 = new Category
            {
                ID = 5,
                CategoryName = "Figür"
            };
            context.Categories.Add(c5);
            context.SaveChanges();

            Category c6 = new Category
            {
                ID = 6,
                CategoryName = "Enteriyör"
            };
            context.Categories.Add(c6);
            context.SaveChanges();

            Category c7 = new Category
            {
                ID = 7,
                CategoryName = "Teknik Resim"
            };
            context.Categories.Add(c7);
            context.SaveChanges();

            Category c8 = new Category
            {
                ID = 8,
                CategoryName = "Peyzaj"
            };
            context.Categories.Add(c8);
            context.SaveChanges();


            Product p = new Product
            {
                ID = 1,
                ProductName = "Mavi Pembe",
                UnitInStock = 1,
                UnitPrice = 475,
                ProductSize = "100x75",
                Description = "Tuval, Yağlı Boya, Cam Boyası",
                ImagePath = "/Pictures/1MaviPembe.jpg",
                CategoryID = c.ID
            };

            c.Products.Add(p);
            context.Products.Add(p);
            context.SaveChanges();

            Product p2 = new Product
            {
                ID = 2,
                ProductName = "Dinamik Aşk",
                UnitInStock = 1,
                UnitPrice = 375,
                ProductSize = "75x75",
                Description = "Tuval,Chalky, Yağlı Boya, Akrilik",
                ImagePath = "/Pictures/2DinamikAsk.jpg",
                CategoryID = c.ID
            };

            c.Products.Add(p2);
            context.Products.Add(p2);
            context.SaveChanges();

            Product p3 = new Product
            {
                ID = 3,
                ProductName = "Zaman Zaman",
                UnitInStock = 1,
                UnitPrice = 550,
                ProductSize = "80x80",
                Description = "Tuval, Yağlı Boya, Cam Boyası, Akrilik",
                ImagePath = "/Pictures/3ZamanZaman.jpg",
                CategoryID = c.ID
            };

            c.Products.Add(p3);
            context.Products.Add(p3);
            context.SaveChanges();

            Product p4 = new Product
            {
                ID = 4,
                ProductName = "Hareket",
                UnitInStock = 1,
                UnitPrice = 300,
                ProductSize = "80x80",
                Description = "Tuval, Yağlı Boya, Akrilik",
                ImagePath = "/Pictures/4Hareket.jpg",
                CategoryID = c.ID
            };

            c.Products.Add(p4);
            context.Products.Add(p4);
            context.SaveChanges();

            Product p5 = new Product
            {
                ID = 5,
                ProductName = "Okyanus Güneşi",
                UnitInStock = 1,
                UnitPrice = 350,
                ProductSize = "80x80",
                Description = "Tuval, Yağlı Boya, Akrilik",
                ImagePath = "/Pictures/5OkyonusGunesi.jpg",
                CategoryID = c.ID
            };

            c.Products.Add(p5);
            context.Products.Add(p5);
            context.SaveChanges();

            Product p6 = new Product
            {
                ID = 6,
                ProductName = "Özleyiş",
                UnitInStock = 1,
                UnitPrice = 600,
                ProductSize = "90x90",
                Description = "Yağlı Boya, Cam Boyası, Akrilik",
                ImagePath = "/Pictures/6Ozleyis.jpg",
                CategoryID = c.ID
            };

            c.Products.Add(p6);
            context.Products.Add(p6);
            context.SaveChanges();

            Product p7 = new Product
            {
                ID = 7,
                ProductName = "Kasırga",
                UnitInStock = 1,
                UnitPrice = 400,
                ProductSize = "70x100",
                Description = "Tuval, Yağlı Boya, Akrilik",
                ImagePath = "/Pictures/7Kasirga.jpg",
                CategoryID = c.ID
            };

            c.Products.Add(p7);
            context.Products.Add(p7);
            context.SaveChanges();

            Product p8 = new Product
            {
                ID = 8,
                ProductName = "Mavi",
                UnitInStock = 1,
                UnitPrice = 400,
                ProductSize = "70x105",
                Description = "Tuval, Yağlı Boya",
                ImagePath = "/Pictures/8Mavi.jpg",
                CategoryID = c.ID
            };

            c.Products.Add(p8);
            context.Products.Add(p8);
            context.SaveChanges();



        }
    }
}
