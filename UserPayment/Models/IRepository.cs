using System.Collections.Generic;

namespace UserPayment.Models
{
    public interface IRepository<T>
        where T : class
    {
        // получить список сущностей
        IEnumerable<T> GetItemList();
        // получить сущность по id
        T GetItem(int id);
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
