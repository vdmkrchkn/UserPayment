//using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data.Entity;
using System.Linq;

namespace UserPayment.Models
{
    public class DBInitializer : DropCreateDatabaseAlways<UserDBContext>
    {
        protected override void Seed(UserDBContext context)
        // aspnet core only
         //public static void Init(IServiceProvider serviceProvider) 
        {
            //using (var context = new UserDBContext(
            //    serviceProvider.GetRequiredService<DbContextOptions<UserDBContext>>()))
            {
                // Look for any users.
                if (context.User.Any())
                {
                    Console.WriteLine("Не могу заполнить базу, поскольку в таблице уже есть данные");
                    return;   // DB has been seeded
                }

                context.User.Add(
                     new User
                     {
                         Login = "2-10-00-00",
                         Password = "0"
                     });
                context.User.Add(
                     new User
                     {
                         Login = "2-10-00-01",
                         Password = "1"
                     });
                context.User.Add(
                     new User
                     {
                         Login = "2-10-00-10",
                         Password = "2"
                     });
                context.User.Add(
                    new User
                    {
                        Login = "2-10-00-11",
                        Password = "3"
                    });
                context.SaveChanges();
                
                // добавление данных о кошельках
                Random rnd = new Random(42);

                foreach (var user in context.User)
                {
                    // check
                    context.Wallet.Add(new Wallet(user, Math.Round(rnd.NextDouble() * 1000, 2)));                    
                }
                
                context.SaveChanges();

                // добавление данных о счетах
                var firstWallet = context.Wallet.First();
                var lastWallet = context.Wallet.OrderByDescending(w => w.Id).First();
               
                context.Account.Add(new Account(firstWallet.Id, lastWallet.Id,
                        firstWallet.Balance, DateTime.Now, "test"));

                context.SaveChanges();

                foreach (var accnt in context.Account)
                {                    
                    context.AccountStatuses.Add(
                        new AccountStatus { AccountId = accnt.Id, Status = Status.New });
                }

                context.SaveChanges();                             
            }
        }
    }
}
