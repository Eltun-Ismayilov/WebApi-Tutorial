using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data.Models;
using WebApi.Data.ViewModul;

namespace WebApi.Data.Services
{
    public class AuthorsService
    {
        readonly AppDbContext db;
        public AuthorsService(AppDbContext db)
        {
            this.db = db;
        }

        public  void AddBook(AuthorVM author)
        {
            var _author = new Author()
            {
                FullName = author.FullName,
               
              
            };

             db.Authors.Add(_author);
             db.SaveChanges();
        }


        public AuthorWithBooksVM Details(int id)
        {
            var author = db.Authors.Where(n => n.Id == id).Select(n => new AuthorWithBooksVM()
            {
                FullName = n.FullName,
                BookTitle = n.Book_Authors.Select(x => x.Book.Title).ToList()
            }).FirstOrDefault();

            return author;
        }

    }
}
