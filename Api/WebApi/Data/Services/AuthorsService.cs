using Microsoft.EntityFrameworkCore;
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


        public async Task Delete(int id)
        {
            var data = await db.Authors.FirstOrDefaultAsync(b => b.Id == id);
            db.Authors.Remove(data);
            await db.SaveChangesAsync();

        }

        public List<Author> Get()
        {
            var data= db.Authors
                //.Include(x=>x.Book_Authors)
                //.ThenInclude(x=>x.Book)
                //.ThenInclude(x=>x.Publisher)
                .ToList(); 
            return data;
        }

        public async Task<Author> Update(int id, AuthorVM author)
        {
            var data = await db.Authors.FirstOrDefaultAsync(b => b.Id == id);

            if (data != null)
            {
                data.FullName = author.FullName;
            
                //data.Book_Authors.Select(x=>x.AuthorId).ToList()



                await db.SaveChangesAsync();
            }
            return data;
        }
    }
}
