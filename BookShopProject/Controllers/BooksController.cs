using BookShopProject.Models;
using BookShopProject.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace BookShopProject.Controllers
{
    public class BooksController : Controller
    {
        // GET: Books
        BookDbContext db = new BookDbContext();
        public ActionResult Index(int page=1)
        {
            ViewBag.current = page;
            ViewBag.TotalPage = (int)Math.Ceiling((double)db.Books.Count() / 5);
            return View(db.Books.OrderBy(x => x.BookId).Skip((page - 1) * 5).Take(5).ToList());
        }
        public PartialViewResult IndexOrders(int page=1)
        {

            ViewBag.current = page;
            ViewBag.TotalPage = (int)Math.Ceiling((double)db.orders.Count() / 5);
            return PartialView(db.orders.OrderBy(x => x.BookId).Skip((page - 1) * 5).Take(5).ToList());
        }
        public ActionResult Create()
        {
            ViewBag.Author= db.Authors.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return Json(new { id = book.BookId });
            }
            return Json(null);
        }
        public PartialViewResult AddOrder(Book book = null)
        {
            if (book == null) book = new Book();
            book.Orders.Add(new Order());
            return PartialView(book);
        }
        public ActionResult Image(int id, Imageup img)
        {
            if (ModelState.IsValid)
            {
                if (img.CoverPage != null)
                {
                    Book b = db.Books.FirstOrDefault(x => x.BookId == id);
                    string ext = Path.GetExtension(img.CoverPage.FileName);
                    string filename = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                    string savepath = Path.Combine(Server.MapPath("~/Uploads"), filename);
                    img.CoverPage.SaveAs(savepath);
                    b.CoverPage = filename;
                    db.SaveChanges();
                    return Json(filename);
                }
            }
            return Json(null);
        }


        public ActionResult Edit(int id)
        {
            
            ViewBag.Author = db.Authors.ToList();
            var book = db.Books.FirstOrDefault(x=> x.BookId==id);
            ViewBag.image = book.CoverPage;
            return View(
                new BookEditModel
                {
                    BookId= book.BookId,
                    Title = book.Title,
                    PublishDate = book.PublishDate,
                    TotalPage= book.TotalPage,
                    AvaiableStock = book.AvaiableStock,
                    AuthorId= book.AuthorId,
                    CoverPage= book.CoverPage,
                    Orders = book.Orders,
                });
        }
        [HttpPost]
        public ActionResult Edit( BookEditModel book)
        {
            var data = db.Books.FirstOrDefault(x => x.BookId == book.BookId);
            if (ModelState.IsValid)
            {
                data.BookId= book.BookId;
                data.Title = book.Title;
                data.PublishDate = book.PublishDate;
                data.TotalPage = book.TotalPage;
                data.AuthorId= book.AuthorId;
                data.CoverPage= book.CoverPage;
                db.orders.RemoveRange(data.Orders.ToList());
                foreach (var o in book.Orders)
                {
                    o.BookId = data.BookId;
                    db.orders.Add(o);
                }

                db.SaveChanges();
                return Json(new { id = book.BookId });
            }
            return Json(null);

        }
        public PartialViewResult EditOrder(BookEditModel book, int? index = null)
        {

            if (index.HasValue)
            {
                if (index < book.Orders.Count)
                {
                    book.Orders.RemoveAt(index.Value);
                }
            }


            return PartialView("OrderEditform", book);
        }
        public PartialViewResult EdtiO(BookEditModel book, int? index = null)
        {
            if (book == null) book = new BookEditModel();
            if (index.HasValue)
            {
                if (index < book.Orders.Count)
                {
                    book.Orders.RemoveAt(index.Value);
                }
            }
            else
            {
                book.Orders.Add(new Order());
            }

            return PartialView("OrderEditform", book);
        }
        public ActionResult OrderEdit(int id)
        {
            var data = db.orders.FirstOrDefault(x => x.OrderId == id);
            ViewBag.Book = db.Books.ToList();

            return View(data);
        }
        [HttpPost]
        public ActionResult OrderEdit(Order order)
        {
            var data = db.orders.FirstOrDefault(x => x.OrderId == 0);

            if (ModelState.IsValid)
            {
                db.Entry(order).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult DeleteBook(int id)
        {
            var book = new Book { BookId = id };
            db.Entry(book).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
            //var existing = db.Books.FirstOrDefault(c => c.BookId == id);

            //    db.Books.Remove(existing);
            //    db.SaveChanges();
            //    return Json(existing.BookId);

        }
        public ActionResult Deleteorder(int id)
        {
            var o = new Order { OrderId = id };
            db.Entry(o).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
            //var existing = db.Books.FirstOrDefault(c => c.BookId == id);

            //    db.Books.Remove(existing);
            //    db.SaveChanges();
            //    return Json(existing.BookId);

        }
    }
}