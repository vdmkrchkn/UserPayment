using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UserPayment.Models
{
    public interface IRepository : IDisposable
    {
        // получить список сущностей
        List<Wallet> GetWalletList();
        // получить сущность по id
        Wallet GetWallet(int id);
        // создать сущность
        void Create(Wallet item);
        // обновить сущность
        void Update(Wallet item);
        // удалить сущность
        void Delete(int id);
        // сохранить все изменения контекста
        void Save();
    }
    //
    public class WalletRepository : IRepository
    {
        private readonly UserDBContext _context;

        public WalletRepository(string aContextName)
        {
            _context = new UserDBContext(aContextName);
        }

        public void Create(Wallet item)
        {
            _context.Wallet.Add(item);                        
        }

        public void Delete(int id)
        {
            var wallet = GetWallet(id);
            if (wallet != null)
            {
                _context.Wallet.Remove(wallet);
                Save();
            }
        }

        public Wallet GetWallet(int id)
        {
            return _context.Wallet
                .SingleOrDefault(m => m.Id == id);
        }

        public List<Wallet> GetWalletList()
        {
            var userWallets = _context.Wallet;//.Include(w => w.User);
            return userWallets.ToList();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Wallet item)
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
        // ~WalletRepository() {
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