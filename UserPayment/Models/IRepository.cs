using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserPayment.Models
{
    public interface IRepository<T> : IDisposable
    {
        // получить список сущностей
        List<T> GetItemList();
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
