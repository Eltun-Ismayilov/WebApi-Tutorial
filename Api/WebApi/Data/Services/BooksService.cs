using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data.Models;
using WebApi.Data.ViewModul;

namespace WebApi.Data.Services
{
    public class BooksService
    {
        readonly AppDbContext db;
        public BooksService(AppDbContext db)
        {
            this.db = db;
        }
        public  void AddBook(BookVM book)
        {
            var _book = new Book()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DataRead = book.IsRead ? book.DataRead.Value : null,
                Rate = book.Rate,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                DateAdded = DateTime.Now,
                PublisherId=book.PublisherId
            };

            db.Books.Add(_book);
            db.SaveChanges();

            foreach (var id in book.AuthorIds)
            {
                var book_author = new Book_Author()
                {
                    BookId = _book.Id,
                    AuthorId = id
                };

                db.Book_Authors.Add(book_author);
                db.SaveChanges();
            }
        }
        public async Task<List<Book>> GetAllBook() => await db.Books.ToListAsync();
        public async Task<BookAndAuthorVM> GetBookId(int bookId)
        {

            var data =  db.Books.Where(b => b.Id == bookId).Select(book=>new BookAndAuthorVM() {

                Title =book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DataRead = book.IsRead ? book.DataRead.Value : null,
                Rate = book.Rate,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                PublisherName = book.Publisher.Name,
                AuthorsName = book.Book_Authors.Select(n=>n.Author.FullName).ToList()

            }).FirstOrDefault(); 

            if (data==null)
            {
               throw new Exception("Data is not defient");
            }

            return data;

        }
        public async Task<Book> Update(int id,BookVM book)
        {
            var data = await db.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (data!=null)
            {
                data.Title = book.Title;
                data.Description = book.Description;
                data.IsRead = book.IsRead;
                data.DataRead = book.IsRead ? book.DataRead.Value : null;
                data.Rate = book.Rate;
                data.Genre = book.Genre;
                data.CoverUrl = book.CoverUrl;
                data.DateAdded = DateTime.Now;
                data.PublisherId = book.PublisherId;
                //data.Book_Authors.Select(x=>x.AuthorId).ToList()
                

                    
                await db.SaveChangesAsync();
            }
            return data;
        }
        public async Task Delete(int id)
        {
            var data = await db.Books.FirstOrDefaultAsync(b => b.Id == id);
            db.Books.Remove(data);
            await db.SaveChangesAsync();

        }
    }
}
