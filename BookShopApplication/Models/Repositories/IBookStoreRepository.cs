using BookShopApplication.Models;
using System.Collections.Generic;

namespace BookStore.Models.Repositories
{
    public interface IBookStoreRepository <TEntity>
    {
        // جلب جميع العناصر
        IList <TEntity> List();
        // البحث عن عنصر معين
        TEntity Find(int id);

        void Add(TEntity entity);

        void Update(int id ,TEntity entity);

        void Delete(int id);

        List<TEntity> Search(string term);



    }

}
