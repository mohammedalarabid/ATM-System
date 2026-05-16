using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMSystem
{
    public class ATMContext : DbContext
    {
        public ATMContext(DbContextOptions<ATMContext> options)
           : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

    }
}
