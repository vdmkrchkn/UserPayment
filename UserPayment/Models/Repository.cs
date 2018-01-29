using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace UserPayment.Models
{
    public class Repository<T> : IRepository<T>, IDisposable
        where T : BaseEntity
    {
        private readonly EFDbContext _context = new EFDbContext();
        private IDbSet<T> entities;

        public Repository(string aContextName)
        {
            _context = new EFDbContext(aContextName);
        }

        public Repository() { }

        public void Create(T entity)
        {
            if(entity == null)
                throw new ArgumentNullException("entity");

            Entities.Add(entity);
            Save();
        }

        public void Delete(int id)
        {
            var entity = GetItemById(id);
            if (entity != null)
            {
                Entities.Remove(entity);
                Save();
            }
        }

        public T GetItemById(int id)
        {
            return Entities.Find(id);                
        }

        public IEnumerable<T> GetItemList()
        {            
            return Entities.AsEnumerable();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(T item)
        {            
            _context.Entry(item).State = EntityState.Modified;
            Save();
        }

        private IDbSet<T> Entities
        {
            get
            {
                if (entities == null)
                {
                    entities = _context.Set<T>();
                }

                return entities;
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~UserRepository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }   
}