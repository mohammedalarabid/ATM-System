using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ATMSystem
{
    public class BankAccount
    {
        public int Id {  get; set; }
        public string AccountNumber { get; set; }
        public decimal Balnce {  get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<Transaction> transactions { get; set; } = new();
    }
}
