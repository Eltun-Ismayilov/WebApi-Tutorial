using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Data.Models
{
    public class Book_Author
    {
        public int Id { get; set; }

        //Navigation Properties
        public int BookId { get; set; }
        public Book Book { get; set; }

        //Navigation Properties

        public int AuthorId { get; set; }
        public Author Author { get; set; }


    }
}
