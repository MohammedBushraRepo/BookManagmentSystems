using BookStore.Models.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace BookShopApplication.Models.Repositories
{
    public class AuthorDbRepository : IBookStoreRepository<Author>
    {
       private readonly BookStoreDbContext db;

        public AuthorDbRepository(BookStoreDbContext _db)
        {

            db = _db;

        }
        public void Add(Author entity)
        {
            //   throw new NotImplementedException();
            db.Authors.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            //throw new NotImplementedException();
            var author = Find(id);
            db.Authors.Remove(author);
            db.SaveChanges();
        }

        public Author Find(int id)
        {
            var author = db.Authors.SingleOrDefault(a => a.Id == id);

            return author;
            //throw new NotImplementedException();
        }

        public IList<Author> List()
        {
            return db.Authors.ToList();//throw new NotImplementedException();
        }

        public List<Author> Search(string term)
        {
            return db.Authors.Where(a => a.FullName.Contains(term)).ToList();
        }

        public void Update(int id, Author newAuthor)
        {
            db.Update(newAuthor);
            db.SaveChanges();
            //var author = Find(id);

          // author.FullName = newAuthor.FullName;
            //throw new NotImplementedException();
            // newAuthor.Id = author.Id;
        }

    }
}
