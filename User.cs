using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMSystem
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Pin { get; set; }
        public int FailedLoginAttempts { get; set; } = 0;
        public bool IsLocked { get; set; } = false;
        public List<BankAccount> Accounts { get; set; } = new();

    }
}
