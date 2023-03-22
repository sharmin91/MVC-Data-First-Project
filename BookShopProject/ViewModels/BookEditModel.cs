using BookShopProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookShopProject.ViewModels
{
    public class BookEditModel
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
}