//using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data.Entity;
using System.Linq;

namespace UserPayment.Models
{
    public class DBInitializer : DropCreateDatabaseAlways<EFDbContext>
    {
        protected override void Seed(EFDbContext context)
        // aspnet core only
         //public static void Init(IServiceProvider serviceProvider) 
        {
            //using (var context = new UserDBContext(
            //    serviceProvider.GetRequiredService<DbContextOptions<UserDBContext>>()))
            {
                // Look for any users.
                if (context.Set<User>().Any())
                {
                    Console.WriteLine("Не могу заполнить базу, поскольку в таблице уже есть данные");
                    return;   // DB has been seeded
                }

                context.Set<User>().Add(
                     new User
                     {
                         Login = "2-10-00-00",
                         Password = "0"
                     });
                context.Set<User>().Add(
                     new User
                     {
                         Login = "2-10-00-01",
                         Password = "1"
                     });
                context.Set<User>().Add(
                     new User
                     {
                         Login = "2-10-00-10",
                         Password = "2"
                     });
                context.Set<User>().Add(
                    new User
                    {
                        Login = "2-10-00-11",
                        Password = "3"
                    });
                context.SaveChanges();
                
                // добавление данных о кошельках
                Random rnd = new Random(42);

                foreach (var user in context.Set<User>())
                {
                    // check
                    context.Set<Wallet>().Add(
                        new Wallet(user, Math.Round(rnd.NextDouble() * 1000, 2)));                    
                }
                
                context.SaveChanges();

                // добавление данных о счетах
                var firstWallet = context.Set<Wallet>().First();
                var lastWallet = context.Set<Wallet>().OrderByDescending(w => w.Id).First();
               
                context.Set<Account>().Add(new Account(firstWallet.Id, lastWallet.Id,
                        firstWallet.Balance, DateTime.Now, "test"));

                context.SaveChanges();

                foreach (var accnt in context.Set<Account>())
                {
                    accnt.Status = new AccountStatus { AccountId = accnt.Id, Status = Status.New };
                    context.Set<AccountStatus>().Add(accnt.Status);
                }

                context.SaveChanges();                             
            }
        }
    }
}
