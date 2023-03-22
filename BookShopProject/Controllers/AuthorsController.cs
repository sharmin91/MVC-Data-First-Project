using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookShopProject.Models;
using BookShopProject.ViewModels;

namespace BookShopProject.Controllers
{
    public class AuthorsController : Controller
    {
        private BookDbContext db = new BookDbContext();

        // GET: Authors
        public ActionResult Index()
        {
            var authors = db.Authors.Include(a => a.Publisher);
            return View(authors.ToList());
        }


 
        public ActionResult Create()
        {
            ViewBag.Publisher = db.Publishers.ToList();
            return View();
        }
        [HttpPost]

        public ActionResult Create( AuthorInsertModel author)
        {
            if (ModelState.IsValid)
            {
                Author b = new Author
                {
                    AuthorName = author.AuthorName,
                    Age = author.Age,
                    Gender = author.Gender,
                    Email = author.Email,
                    Phone = author.Phone,
                    PublisherId = author.PublisherId
                };

                    if(author.Picture != null)
                {
                    string ext = Path.GetExtension(author.Picture.FileName);
                    string filename = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                    string savepath = Path.Combine(Server.MapPath("~/Uploads"), filename);
                    author.Picture.SaveAs(savepath);
                    b.Picture = filename;
                }
  
                db.Authors.Add(b);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(author);
        }
        public ActionResult Edit(int id)
        {
            var data = db.Authors.FirstOrDefault (x=>x.AuthorId== id);
            ViewBag.pic = data.Picture;
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            ViewBag.Publisher = db.Publishers.ToList();

            return View(author);
        }
        [HttpPost]

        public ActionResult Edit( AuthorEditModel author)
        {
            var data = db.Authors.FirstOrDefault(x=> x.AuthorId== author.AuthorId);
            if (ModelState.IsValid)
            {
                data.AuthorId = author.AuthorId;
                data.AuthorName = author.AuthorName;
                data.Age = author.Age;
                data.Gender = author.Gender;
                data.Email = author.Email;
                data.Phone = author.Phone;
                data.PublisherId = author.PublisherId;

                if (author.Picture != null)
                {
                    string ext = Path.GetExtension(author.Picture.FileName);
                    string filename = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                    string savepath = Path.Combine(Server.MapPath("~/Uploads"), filename);
                    author.Picture.SaveAs(savepath);
                    data.Picture = filename;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(author);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Author author = db.Authors.Find(id);
            db.Authors.Remove(author);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
