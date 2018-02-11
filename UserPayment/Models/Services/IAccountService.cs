using System.Collections.Generic;
using System.Linq;

namespace UserPayment.Models
{
    public interface IAccountService
    {
        IEnumerable<Account> GetAccounts();

        IEnumerable<AccountStatus> GetAccountStatuses();

        IEnumerable<Wallet> GetWallets();        

        bool CreateAccount(Account account);

        void UpdateAccount(Account account);

        void DeleteAccount(Account account);

        bool AccountExists(Account account);        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountModel"></param>
        /// <param name="aUserLogin"></param>
        /// <returns></returns>
        IQueryable<Account> GetUserAccounts(IQueryable<Account> accountModel,
            string aUserLogin);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="aUserLogin"></param>
        /// <returns></returns>
        IQueryable<int> GetUserWallets(string aUserLogin);
    }
}
