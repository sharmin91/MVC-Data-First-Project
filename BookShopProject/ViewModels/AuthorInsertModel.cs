using BookShopProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookShopProject.ViewModels
{
    public class AuthorInsertModel
    {
        public int AuthorId { get; set; }
        [Required, StringLength(40)]
        public string AuthorName { get; set; }
        [Required]
        public int Age { get; set; }
        [Required, EnumDataType(typeof(gender))]
        public gender Gender { get; set; }
        [Required, StringLength(40), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, StringLength(40)]
        public string Phone { get; set; }
        [Required]
        public HttpPostedFileBase Picture { get; set; }
        [Required, ForeignKey("Publisher")]
        public int PublisherId { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual List<Book> Books { get; set; } = new List<Book>();
    }
}