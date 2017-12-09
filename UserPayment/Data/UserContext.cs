﻿using System;
using System.Data.Entity;

namespace UserPayment.Models
{
    public class UserDBContext : DbContext
    {
        public UserDBContext (string aContextName)
            : base(aContextName)
        {
            Console.WriteLine("Создание привязки к БД с названием {0}", aContextName);
        }

        public UserDBContext() : this("UserDBContext") { }

        public DbSet<User> User { get; set; }
        public DbSet<Wallet> Wallet { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<AccountStatus> AccountStatuses { get; set; }
    }
}
