using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DAL.Entities
{
    public class Author
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }

        [Key]
        [Column("AuthorId")]
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        [MinLength(2)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(40)]
        [MinLength(2)]
        public string LastName { get; set; }

        public string Name {
            get { return FirstName + " " + LastName; }
                }
        public virtual ICollection<Book> Books { get; set; }
        public override string ToString()
        {
            return FirstName+ " "+LastName;
        }
    }
}