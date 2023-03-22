using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BookShopProject.Models
{
    public enum gender {Male=1, Female}
    public class Publisher
    {
        public int PublisherId { get; set; }
        [Required, StringLength(40)]
        public string PublisherName { get; set; }
        [Required, StringLength(40)]
        public string WebUrl { get; set; }
        [Required, StringLength(40)]
        public string Phone { get; set; }
        public virtual List<Author> Authors { get; set; } = new List<Author>();

    }
    public class Author
    {
        public int AuthorId { get; set; }
        [Required, StringLength(40)]
        public string AuthorName { get; set; }
        [Required] 
        public int Age { get; set; }
        [Required, EnumDataType(typeof(gender))]
        public gender  Gender { get; set; }
        [Required, StringLength(40),DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, StringLength(40)]
        public string Phone { get; set; }
        [Required, StringLength(40)]
        public string Picture { get; set; }
        [Required, ForeignKey("Publisher")]
        public int PublisherId { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual List<Book> Books { get; set; } = new List<Book>();
    }
    public class Book
    {
        public int BookId { get; set; }
        [Required, StringLength(40)]
        public string Title { get; set; }
        [Required, Column(TypeName = "date"), DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }
        public int TotalPage { get; set; }
        public bool AvaiableStock { get; set; }
        [Required, StringLength(50)]
        public string CoverPage { get; set; }
        [Required, ForeignKey("Author")]
        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }
        public virtual List<Order> Orders { get; set; } = new List<Order>();


    }
    public class Order
    {

        public int OrderId { get; set; }
        [Required]
        public int Quantity { get; set; }

        [Required, Column(TypeName = "money"), DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Required, Column(TypeName = "date"), DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
        [Required, ForeignKey("Book")]
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
    }
    public class BookDbContext : DbContext
    {
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Order> orders { get; set; }
    }
}