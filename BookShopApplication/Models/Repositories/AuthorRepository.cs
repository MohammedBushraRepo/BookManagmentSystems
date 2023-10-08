using BookShopApplication.Models;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Models.Repositories
{
    public class AuthorRepository : IBookStoreRepository<Author>
    {
        IList<Author> authors;

        public AuthorRepository()
        {
            authors = new List<Author>()
            {
                
               new Author  { Id = 1 , FullName = "khalid omer" } ,
                new Author { Id = 2 , FullName = "Hamid magboul" } ,
                new Author { Id = 3 , FullName = "husam khalil" }

                };





        }
        public void Add(Author entity)
        {
            //   throw new NotImplementedException();
            authors.Add(entity);
        }

        public void Delete(int id)
        {
            //throw new NotImplementedException();
            var author = Find(id);
            authors.Remove(author);
        }

        public Author Find(int id)
        {
            var author = authors.SingleOrDefault(a => a.Id == id);

            return author;
            //throw new NotImplementedException();
        }

        public IList<Author> List()
        {
            return authors;//throw new NotImplementedException();
        }

        public List<Author> Search(string term)
        {
            return authors.Where(a => a.FullName.Contains(term)).ToList();
        }

        public void Update(int id, Author newAuthor)
        {
            var author = Find(id);
           
            author.FullName = newAuthor.FullName;
            //throw new NotImplementedException();
            // newAuthor.Id = author.Id;
        }
    }
}
