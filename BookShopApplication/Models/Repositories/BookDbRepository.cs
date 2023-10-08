using BookStore.Models.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BookShopApplication.Models.Repositories
{
    public class BookDbRepository : IBookStoreRepository<Book>
    {
        private readonly BookStoreDbContext db;

        public BookDbRepository(BookStoreDbContext _db)
        {
            db = _db;
        }
        public void Add(Book entity)
        {
            db.Books.Add(entity);
            db.SaveChanges();
            //throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            //throw new NotImplementedException();
            var book = Find(id);    // books.SingleOrDefault(b => b.Id == id);

            db.Remove(book);
            db.SaveChanges();
        }

        public Book Find(int id)
        {
            var book = db.Books.Include(a => a.Author).SingleOrDefault(b => b.Id == id);
            return book;
            //throw new NotImplementedException();
        }

        public IList<Book> List()
        {
            return db.Books.Include(a => a.Author).ToList();
            // throw new NotImplementedException();
        }

        public void Update(int id, Book newBook)
        {

            db.Update(newBook);
            db.SaveChanges();
            //throw new NotImplementedException();
            //var book = Find(id);      //books.SingleOrDefault(b => b.Id == id);

            //book.Title = newBook.Title;

            //book.Description = newBook.Description;

            //book.Author = newBook.Author;

            //book.ImagUrl = newBook.ImagUrl;
        }

        public List<Book> Search(string term)
        {
            var result = db.Books.Include(a => a.Author)
                .Where(b => b.Title.Contains(term) ||
                  b.Description.Contains(term) ||
                  b.Author.FullName.Contains(term)).ToList();

            return result;
        }
    }
}
