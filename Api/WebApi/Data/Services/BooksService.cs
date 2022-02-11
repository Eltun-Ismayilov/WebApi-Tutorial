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
        public async void AddBook(BookVM book)
        {
            var _book = new Book()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DataRead = book.IsRead ? book.DataRead.Value : null,
                Rate = book.Rate,
                Genre = book.Genre,
                Author = book.Author,
                CoverUrl = book.CoverUrl,
                DateAdded = DateTime.Now
            };

            db.Books.Add(_book);
            await db.SaveChangesAsync();
        }
        public async Task<List<Book>> GetAllBook() => await db.Books.ToListAsync();
        public async Task<Book> GetBookId(int bookId)
        {

            var data = await db.Books.FirstOrDefaultAsync(b => b.Id == bookId);

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
                data.Author = book.Author;
                data.CoverUrl = book.CoverUrl;
                data.DateAdded = DateTime.Now;
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
