﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using Omu.ValueInjecter;

namespace MVC5Course.Controllers
{
    public class ProductsController : Controller
    {
        private FabricsEntities db = new FabricsEntities();

        // GET: Products
        public ActionResult Index()
        {
            var data = db.Product
                .OrderByDescending(p => p.ProductId)
                .Take(10)
                .ToList();
            return View(data);
        }

        public ActionResult Index2()
        {
            var data = db.Product
                .Where(p => p.Active == true)
                .OrderByDescending(p => p.ProductId)
                .Take(10)
               .Select(p => new ProductViewModel()
               {
                   ProductName = p.ProductName,
                   ProductId = p.ProductId,
                   Price = p.Price,
                   Stock = p.Stock
               });
            return View(data);
        }

        public ActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(ProductViewModel data)
        {
            if (!ModelState.IsValid) //不有效的時候
            {
                return View();
            }

            var product = new Product()
            {
                ProductId = data.ProductId,
                Active = true,
                Price = data.Price,
                Stock = data.Stock,
                ProductName = data.ProductName
            };

            this.db.Product.Add(product);
            this.db.SaveChanges();

            return RedirectToAction("Index2");
        }

        public ActionResult EditProduct(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index2");
            }
            var data = db.Product.Find(id);
            return View(data);
        }

        [HttpPost]
        public ActionResult EditProduct(int id,ProductViewModel data)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            }

            var oneProduct = db.Product.Find(id);
            //oneProduct.Price = data.Price;
            //oneProduct.ProductName = data.ProductName;
            //oneProduct.Stock = data.Stock;
            oneProduct.InjectFrom(data);


            db.SaveChanges();


            return RedirectToAction("Index2");
        }

        public ActionResult DelProduct(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index2");
            }
            var data = db.Product.Find(id);
            if (data == null)
            {
                return RedirectToAction("Index2");
                //return HttpNotFound();
            }
            return View(data);
        }

        [HttpPost, ActionName("DelProduct")]
        public ActionResult DelProductConfirmed(int? id)
        {
            if(id==null)
            {
                return RedirectToAction("Index2");
            }
            var item = db.Product.Find(id);
            if(item==null)
            {
                return HttpNotFound();
            }

            db.Product.Remove(item);
            db.SaveChanges();

            return RedirectToAction("Index2");
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Product.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Product.Find(id);
            db.Product.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
