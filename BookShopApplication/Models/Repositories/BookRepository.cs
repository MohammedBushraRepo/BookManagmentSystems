using BookShopApplication.Models;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Models.Repositories
{
    public class BookRepository : IBookStoreRepository<Book>
    {
        IList<Book> books;
        public BookRepository()
        {
            books = new List<Book>() {

            new Book 
            {
                 Id = 1 ,
                Description = "no description" ,
                Title = "birds" ,
                ImagUrl = "book1.jpg",
                Author = new Author { Id = 2} },
             new Book 
             {
                 Id = 2,
                 Description = "no description",
                 Title = "animals" ,
                 ImagUrl = "book2.jpg",
                 Author = new Author { Id = 3}  },
              new Book 
              { 
                  Id = 3,
                  Description = "no description",
                  Title = "fish" ,
                  ImagUrl = "book3.jpg",
                  Author = new Author () }

              };

        }

        public void Add(Book entity)
        {
            books.Add(entity);
            //throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            //throw new NotImplementedException();
            var book = Find(id);    // books.SingleOrDefault(b => b.Id == id);

            books.Remove(book);
        }

        public Book Find(int id)
        {
            var book = books.SingleOrDefault(b => b.Id == id);
            return book;
            //throw new NotImplementedException();
        }   

        public IList<Book> List()
        {
            return books;
           // throw new NotImplementedException();
        }

        public List<Book> Search(string term)
        {
            var result = books
                .Where(b => b.Title.Contains(term) ||
                  b.Description.Contains(term) ||
                  b.Author.FullName.Contains(term)).ToList();
            return result;

        }

        public void Update(int id, Book newBook)
        {
            //throw new NotImplementedException();
            var book = Find(id);      //books.SingleOrDefault(b => b.Id == id);

            book.Title = newBook.Title;

            book.Description = newBook.Description;

            book.Author = newBook.Author;

            book.ImagUrl = newBook.ImagUrl;
        }
    }
}
