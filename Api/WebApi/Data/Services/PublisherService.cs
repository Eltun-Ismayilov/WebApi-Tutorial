using System;
using System.Linq;
using System.Text.RegularExpressions;
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

        public  Publisher AddPublis(PublisherVM publisher)
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
                BookAuthors = n.Books.Select(n => new BookAuthorVM() {

                    Authors=n.Book_Authors.Select(x=>x.Author.FullName).ToList(),
                    BookName=n.Title


                }).ToList()
            }).FirstOrDefault();

            return author;
        }

        public void delete(int id)
        {
            var data = db.Publishers.FirstOrDefault(x => x.Id == id);

            if (data!=null)
            {
                db.Publishers.Remove(data);
                db.SaveChanges();
            }
        }


        private bool StringStartsWithNumber(string name) => (Regex.IsMatch(name, @"^\d"));
    }
}
