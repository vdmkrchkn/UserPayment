using System.Collections.Generic;
using System.Linq;

namespace UserPayment.Models
{
    public interface IRepository<T>
        where T : class
    {
        // получить таблицу из запроса к БД
        IQueryable<T> TableNoTracking();
        // получить список сущностей
        IEnumerable<T> GetItemList();
        // получить сущность по id
        T GetItemById(int id);
        // создать сущность
        void Create(T item);
        // обновить сущность
        void Update(T item);
        // удалить сущность
        void Delete(int id);
        // сохранить все изменения контекста
        void Save();
    }
}
