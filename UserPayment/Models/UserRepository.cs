using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace UserPayment.Models
{
    public class UserRepository : IRepository<User>, IDisposable
    {
        private readonly UserDBContext _context = new UserDBContext();

        public UserRepository(string aContextName)
        {
            _context = new UserDBContext(aContextName);
        }

        public UserRepository() { }

        public void Create(User item)
        {
            _context.User.Add(item);
        }

        public void Delete(int id)
        {
            var user = GetItem(id);
            if (user != null)
            {
                _context.User.Remove(user);
                Save();
            }
        }

        public User GetItem(int id)
        {
            return _context.User
                .SingleOrDefault(m => m.Id == id);
        }

        public IEnumerable<User> GetItemList()
        {            
            return _context.User.ToList();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(User item)
        {
            _context.Entry(item).State = EntityState.Modified;
            Save();
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