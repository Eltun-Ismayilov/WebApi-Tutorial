using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebApi.Data.Models;
using WebApi.Data.ViewModul;
using WebApi.Exceptions;

namespace WebApi.Data.Services
{
    public class PublisherService
    {
        readonly AppDbContext db;
        public PublisherService(AppDbContext db)
        {
            this.db = db;
        }

        public Publisher AddPublis(PublisherVM publisher)
        {
            if (StringStartsWithNumber(publisher.Name)) throw new PublisherNameException($"Name starts with number {publisher.Name} ");


            var _publisher = new Publisher()
            {
                Name = publisher.Name,

            };

            db.Publishers.Add(_publisher);
            db.SaveChanges();

            return _publisher;
        }


        public PublisherWithBooksAndAuthorsVM Details(int id)
        {
            var author = db.Publishers.Where(n => n.Id == id).Select(n => new PublisherWithBooksAndAuthorsVM()
            {
                Name = n.Name,
                BookAuthors = n.Books.Select(n => new BookAuthorVM()
                {

                    Authors = n.Book_Authors.Select(x => x.Author.FullName).ToList(),
                    BookName = n.Title


                }).ToList()
            }).FirstOrDefault();

            return author;
        }

        public void delete(int id)
        {
            var data = db.Publishers.FirstOrDefault(x => x.Id == id);

            if (data != null)
            {
                db.Publishers.Remove(data);
                db.SaveChanges();
            }
        }

        public async Task<List<Publisher>> Get(string sortBy)
        {
            var publisher =await db.Publishers.OrderBy(n=>n.Name).ToListAsync();

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "name_desc":
                        publisher = publisher.OrderByDescending(n => n.Name).ToList();
                        break;
                    default:
                        break;
                }
            }

            return publisher;


        }



        private bool StringStartsWithNumber(string name) => (Regex.IsMatch(name, @"^\d"));
    }
}
