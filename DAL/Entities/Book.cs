using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DAL.Entities
{
    public class Book
    {
        public Book()
        {
            Keepers = new HashSet<Keeper>();
        }

        [Key]
        [Column("BookId")]
        public int Id { get; set; }
        [Required]
        [MaxLength(100), MinLength(2)]
        [Display(Name = "Book")]
        public string BookTitle { get; set; }
        [MaxLength(50), MinLength(2)]
        public string Publisher { get; set; }
        [Range(1, 5000)]
        public int Pages { get; set; }
        [Range(1000, 2200)]
        [Display(Name = "Release Date")]
        public int Date { get; set; }
        [Display(Name ="ISBN")]
        [RegularExpression(@"(ISBN[-]*(1[03])*[ ]*(: ){0,1})*(([0-9Xx][- ]*){13}|([0-9Xx][- ]*){10})", ErrorMessage = "No Valid format ISBN Example: ISBN 978-5-496-02163-0!")]
        public string Isbn { get; set; }
        public int? AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public  virtual ICollection<Keeper>Keepers { get; set; }
        //

    }
}