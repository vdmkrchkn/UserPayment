using NLog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UserPayment.Models.Services
{
    public class AccountService : IAccountService
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        #region Dependencies

        private readonly IRepository<Account> _accountRepo;
        private readonly IRepository<AccountStatus> _accountStatusRepo;
        private readonly IRepository<Wallet> _walletRepo;        
        public IValidationDictionary ModelState { get; set; }

        #endregion Dependencies

        #region Ctor

        public AccountService(IValidationDictionary modelState, IRepository<Account> accountRepo,
            IRepository<AccountStatus> accountStatusRepo,
            IRepository<Wallet> walletRepo)
        {
            ModelState = modelState;
            //               
            _accountRepo = accountRepo;
            _accountStatusRepo = accountStatusRepo;
            _walletRepo = walletRepo;
        }       

        #endregion Ctor

        #region IAccountService

        public IEnumerable<Account> GetAccounts() { return _accountRepo.GetItemList(); }

        public IEnumerable<AccountStatus> GetAccountStatuses()
        {
            return _accountStatusRepo.GetItemList();
        }

        public IEnumerable<Wallet> GetWallets()
        {
            return _walletRepo.GetItemList();
        }        

        public bool CreateAccount(Account account)
        {
            if (!ValidateAccount(account))
            {
                _logger.Error("попытка создания счёта с некорректными данными");
                return false;
            }

            try                
            {
                account.Date = DateTime.Today;
                _accountRepo.Create(account);

                _accountStatusRepo.Create(
                    new AccountStatus
                    {
                        AccountId = account.Id,
                        Status = Status.New
                    }
                );
            }            
            catch (Exception)
            {
                _logger.Error("неизвестная ошибка");
                return false;
            }            

            return true;
        }

        public void UpdateAccount(Account account) { _accountRepo.Update(account); }

        public void DeleteAccount(Account account) { _accountRepo.Delete(account.Id); }

        public bool AccountExists(Account account)
        {
            return GetAccounts().Any(e => e.Id == account.Id);
        }

    public IQueryable<Account> GetUserAccounts(IQueryable<Account> accountModel,
            string aUserLogin)
        {
            if (!string.IsNullOrEmpty(aUserLogin))
            {
                var userWallets = GetUserWallets(aUserLogin);

                return accountModel.Where(s => userWallets.Contains(s.SrcWalletId));
            }

            return accountModel;
        }

        public IQueryable<int> GetUserWallets(string aUserLogin)
        {
            return _walletRepo.GetItemList()
                .Where(w => w.User.Login.Equals(aUserLogin))
                .Select(w => w.Id).AsQueryable();
        }

        #endregion IAccountService

        #region Methods

        private bool ValidateAccount(Account account)
        {
            // проверка валидности создаваемого счёта
            if (!account.isValid())
            {
                ModelState.AddError("Account", "incorrect account data");
            }

            return ModelState.IsValid;
        }

        #endregion Methods
    }
}