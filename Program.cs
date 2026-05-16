using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ATMSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var services = new ServiceCollection();

            services.AddDbContext<ATMContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));

            var serviceProvider = services.BuildServiceProvider();

            using var context = serviceProvider.GetRequiredService<ATMContext>();


            Console.WriteLine("===== ATM SYSTEM =====");

            Console.WriteLine("1 - Register");
            Console.WriteLine("2 - Login");
            Console.WriteLine("3 - Exit");
                

            Console.Write("Choose Option: ");

            if (!int.TryParse(Console.ReadLine(), out int mainChoice))
            {
                Console.WriteLine("Invalid input! Please enter a number.");
                return;
            }


            // ================= REGISTER =================

            if (mainChoice == 1)
            {
                Console.Write("Enter Your Name: ");
                string name = Console.ReadLine()!;

                Console.Write("Create PIN: ");
                string newPin = Console.ReadLine()!;

                if (newPin.Length != 4 || !newPin.All(char.IsDigit))
                {
                    Console.WriteLine("PIN must be exactly 4 digits.");
                    return;
                }

                Console.Write("Enter Account Number: ");
                string newAccountNumber = Console.ReadLine()!;
                if (!newAccountNumber.StartsWith("ACC") || newAccountNumber.Length < 6)
                {
                    Console.WriteLine("Account Number must start with ACC and be at least 6 characters.");
                    return;
                }

                // نتأكد الحساب غير موجود
                var existingAccount = context.BankAccounts
                    .FirstOrDefault(a => a.AccountNumber == newAccountNumber);

                if (existingAccount != null)
                {
                    Console.WriteLine("Account Number Already Exists!");
                    return;
                }


                var newUser = new User
                {
                    Name = name,
                    Pin = newPin
                };

                var newAccount = new BankAccount
                {
                    AccountNumber = newAccountNumber,
                    Balnce = 0,
                    User = newUser
                };

                context.Users.Add(newUser);

                context.BankAccounts.Add(newAccount);

                context.SaveChanges();

                Console.WriteLine("Account Created Successfully!");
            }


            // ================= LOGIN =================

            else if (mainChoice == 2)
            {
                Console.Write("Enter Account Number: ");
                string accountNumber = Console.ReadLine()!;

                Console.Write("Enter PIN: ");
                string pin = Console.ReadLine()!;

                //////////////// Admin Panel  /////////////////////
                ///
                if (accountNumber == "ADMIN" && pin == "1234")
                {
                    while (true)
                    {
                        Console.WriteLine();
                        Console.WriteLine("===== ADMIN PANEL =====");
                        Console.WriteLine("1 - View All Users");
                        Console.WriteLine("2 - View All Accounts");
                        Console.WriteLine("3 - View All Transactions");
                        Console.WriteLine("4 - Deposit to Any Account");
                        Console.WriteLine("5 - Withdraw from Any Account");
                        Console.WriteLine("6 - Delete Any Account");
                        Console.WriteLine("7 - Edit Any User");
                        Console.WriteLine("8 - Export Transactions to CSV");
                        Console.WriteLine("9 - Search Account");
                        Console.WriteLine("10 - Unlock Account");
                        Console.WriteLine("11 - Logout");

                        Console.Write("Choose Option: ");

                        if (!int.TryParse(Console.ReadLine(), out int adminChoice))
                        {
                            Console.WriteLine("Invalid input! Please enter a number.");
                            continue;
                        }

                        // ================= VIEW ALL USERS =================
                        if (adminChoice == 1)
                        {
                            var users = context.Users.ToList();

                            Console.WriteLine("===== USERS =====");

                            foreach (var user in users)
                            {
                                Console.WriteLine(
                                    $"ID: {user.Id} | Name: {user.Name} | PIN: {user.Pin}");
                            }
                        }

                        // ================= VIEW ALL ACCOUNTS =================
                        else if (adminChoice == 2)
                        {
                            var accounts = context.BankAccounts
                                .Include(a => a.User)
                                .ToList();

                            Console.WriteLine("===== ACCOUNTS =====");

                            foreach (var account in accounts)
                            {
                                Console.WriteLine(
                                    $"Account: {account.AccountNumber} | Owner: {account.User.Name} | Balance: {account.Balnce}");
                            }
                        }

                        // ================= VIEW ALL TRANSACTIONS =================
                        else if (adminChoice == 3)
                        {
                            var transactions = context.Transactions
                                .Include(t => t.BankAccount)
                                .OrderByDescending(t => t.Date)
                                .ToList();

                            Console.WriteLine("===== ALL TRANSACTIONS =====");

                            foreach (var transaction in transactions)
                            {
                                Console.WriteLine(
                                    $"Account: {transaction.BankAccount.AccountNumber} | Type: {transaction.Type} | Amount: {transaction.Amount} | Date: {transaction.Date}");
                            }
                        }

                        // ================= Deposit to Any Account =================
                        else if (adminChoice == 4)
                        {
                            Console.Write("Enter Account Number: ");
                            string targetAccountNumber = Console.ReadLine()!;

                            var targetAccount = context.BankAccounts
                                .Include(a => a.User)
                                .FirstOrDefault(a => a.AccountNumber == targetAccountNumber);

                            if (targetAccount == null)
                            {
                                Console.WriteLine("Account not found.");
                                continue;
                            }

                            Console.Write("Enter Deposit Amount: ");

                            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
                            {
                                Console.WriteLine("Invalid amount.");
                                continue;
                            }

                            if (amount <= 0)
                            {
                                Console.WriteLine("Invalid Amount.");
                                continue;
                            }

                            targetAccount.Balnce += amount;

                            context.Transactions.Add(new Transaction
                            {
                                Amount = amount,
                                Type = "Admin Deposit",
                                Date = DateTime.Now,
                                BankAccount = targetAccount
                            });

                            context.SaveChanges();

                            Console.WriteLine("Deposit Successful!");
                        }

                        // ================= Withdraw from Any Account =================
                        else if (adminChoice == 5)
                        {
                            Console.Write("Enter Account Number: ");
                            string targetAccountNumber = Console.ReadLine()!;

                            var targetAccount = context.BankAccounts
                                .Include(a => a.User)
                                .FirstOrDefault(a => a.AccountNumber == targetAccountNumber);

                            if (targetAccount == null)
                            {
                                Console.WriteLine("Account not found.");
                                continue;
                            }

                            Console.Write("Enter Withdraw Amount: ");

                            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
                            {
                                Console.WriteLine("Invalid amount.");
                                continue;
                            }

                            if (amount <= 0)
                            {
                                Console.WriteLine("Invalid Amount.");
                                continue;
                            }

                            if (amount > targetAccount.Balnce)
                            {
                                Console.WriteLine("Insufficient Balance.");
                                continue;
                            }

                            targetAccount.Balnce -= amount;

                            context.Transactions.Add(new Transaction
                            {
                                Amount = amount,
                                Type = "Admin Withdraw",
                                Date = DateTime.Now,
                                BankAccount = targetAccount
                            });

                            context.SaveChanges();

                            Console.WriteLine("Withdraw Successful!");
                        }

                        // ================= Delete Any Account =================
                        else if (adminChoice == 6)
                        {
                            Console.Write("Enter Account Number to Delete: ");
                            string targetAccountNumber = Console.ReadLine()!;

                            var targetAccount = context.BankAccounts
                                .Include(a => a.User)
                                .FirstOrDefault(a => a.AccountNumber == targetAccountNumber);

                            if (targetAccount == null)
                            {
                                Console.WriteLine("Account not found.");
                                continue;
                            }

                            var transactions = context.Transactions
                                .Where(t => t.BankAccountId == targetAccount.Id)
                                .ToList();

                            context.Transactions.RemoveRange(transactions);
                            context.BankAccounts.Remove(targetAccount);
                            context.Users.Remove(targetAccount.User);

                            context.SaveChanges();

                            Console.WriteLine("Account Deleted Successfully!");
                        }

                        // ================= Edit Any User =================
                        else if (adminChoice == 7)
                        {
                            Console.Write("Enter Account Number: ");
                            string targetAccountNumber = Console.ReadLine()!;

                            var targetAccount = context.BankAccounts
                                .Include(a => a.User)
                                .FirstOrDefault(a => a.AccountNumber == targetAccountNumber);

                            if (targetAccount == null)
                            {
                                Console.WriteLine("Account not found.");
                                continue;
                            }

                            Console.WriteLine("1 - Change Name");
                            Console.WriteLine("2 - Change PIN");

                            Console.Write("Choose Option: ");

                            if (!int.TryParse(Console.ReadLine(), out int editChoice))
                            {
                                Console.WriteLine("Invalid input! Please enter a number.");
                                continue;
                            }

                            if (editChoice == 1)
                            {
                                Console.Write("Enter New Name: ");
                                string newName = Console.ReadLine()!;

                                if (string.IsNullOrWhiteSpace(newName))
                                {
                                    Console.WriteLine("Invalid Name.");
                                    continue;
                                }

                                targetAccount.User.Name = newName;
                            }
                            else if (editChoice == 2)
                            {
                                Console.Write("Enter New PIN: ");
                                string newPin = Console.ReadLine()!;

                                if (newPin.Length != 4 || !newPin.All(char.IsDigit))
                                {
                                    Console.WriteLine("PIN must be exactly 4 digits.");
                                    continue;
                                }

                                targetAccount.User.Pin = newPin;
                            }
                            else
                            {
                                Console.WriteLine("Invalid Option.");
                                continue;
                            }

                            context.SaveChanges();

                            Console.WriteLine("User Updated Successfully!");
                        }


                        // ================= EXPORT TRANSACTIONS TO CSV =================
                        else if (adminChoice == 8)
                        {
                            var transactions = context.Transactions
                                .Include(t => t.BankAccount)
                                .OrderByDescending(t => t.Date)
                                .ToList();

                            if (transactions.Count == 0)
                            {
                                Console.WriteLine("No transactions found.");
                                continue;
                            }

                            string fileName = "Transactions.csv";

                            using (var writer = new StreamWriter(fileName))
                            {
                                // Header
                                writer.WriteLine("AccountNumber,Type,Amount,Date");

                                // Data
                                foreach (var transaction in transactions)
                                {
                                    writer.WriteLine(
                                        $"{transaction.BankAccount.AccountNumber}," +
                                        $"{transaction.Type}," +
                                        $"{transaction.Amount}," +
                                        $"{transaction.Date}");
                                }
                            }

                            Console.WriteLine($"Transactions exported successfully to {fileName}");
                        }
                        // C:\Users\HP\source\repos\ATMSystem\bin\Debug\net8.0\

                        // ================= SEARCH ACCOUNT =================
                        else if (adminChoice == 9)
                        {
                            Console.Write("Enter Account Number to Search: ");
                            string searchAccountNumber = Console.ReadLine()!;

                            var account = context.BankAccounts
                                .Include(a => a.User)
                                .FirstOrDefault(a => a.AccountNumber == searchAccountNumber);

                            if (account == null)
                            {
                                Console.WriteLine("Account not found.");
                                continue;
                            }

                            Console.WriteLine("===== ACCOUNT DETAILS =====");
                            Console.WriteLine($"Owner Name: {account.User.Name}");
                            Console.WriteLine($"Account Number: {account.AccountNumber}");
                            Console.WriteLine($"PIN: {account.User.Pin}");
                            Console.WriteLine($"Balance: {account.Balnce}");
                        }
                        // ================= UNLOCK ACCOUNT =================

                        else if (adminChoice == 10)
                        {
                            Console.Write("Enter Account Number to Unlock: ");
                            string unlockAccountNumber = Console.ReadLine()!;

                            var accountToUnlock = context.BankAccounts
                                .Include(a => a.User)
                                .FirstOrDefault(a => a.AccountNumber == unlockAccountNumber);

                            if (accountToUnlock == null)
                            {
                                Console.WriteLine("Account not found.");
                                continue;
                            }

                            accountToUnlock.User.IsLocked = false;
                            accountToUnlock.User.FailedLoginAttempts = 0;

                            context.SaveChanges();

                            Console.WriteLine("Account unlocked successfully!");
                        }


                        // ================= LOGOUT =================

                        else if (adminChoice == 11)
                        {
                            Console.WriteLine("Admin Logged Out.");
                            break;
                        }

                        else
                        {
                            Console.WriteLine("Invalid Option!");
                        }
                    }

                    return;
                }
                ////////////////////////////////////


                // البحث عن الحساب باستخدام رقم الحساب فقط
                var existingAccount = context.BankAccounts
                    .Include(a => a.User)
                    .FirstOrDefault(a => a.AccountNumber == accountNumber);

                // التحقق من وجود الحساب
                if (existingAccount == null)
                {
                    Console.WriteLine("Account not found.");
                    return;
                }

                // التحقق إذا كان الحساب مقفلاً
                if (existingAccount.User.IsLocked)
                {
                    Console.WriteLine("Your account is locked due to 3 failed login attempts.");
                    return;
                }

                // التحقق من صحة الـ PIN
                if (existingAccount.User.Pin != pin)
                {
                    existingAccount.User.FailedLoginAttempts++;

                    // إذا وصل إلى 3 محاولات خاطئة
                    if (existingAccount.User.FailedLoginAttempts >= 3)
                    {
                        existingAccount.User.IsLocked = true;
                        Console.WriteLine("Account has been locked after 3 failed login attempts.");
                    }
                    else
                    {
                        Console.WriteLine(
                            $"Invalid PIN. Attempt {existingAccount.User.FailedLoginAttempts} of 3.");
                    }

                    context.SaveChanges();
                    return;
                }

                // تسجيل دخول ناجح
                existingAccount.User.FailedLoginAttempts = 0;
                context.SaveChanges();

                // استخدام الحساب كحساب مسجل دخول
                var loggedInAccount = existingAccount;


                if (loggedInAccount != null)
                {

                    while (true)
                    {

                        Console.WriteLine("Login Successful!");

                        Console.WriteLine($"Welcome {loggedInAccount.User.Name}");

                        Console.WriteLine($"Balance: {loggedInAccount.Balnce}");

                        Console.WriteLine("1 - Deposit");
                        Console.WriteLine("2 - Withdraw");
                        Console.WriteLine("3 - Transaction History");
                        Console.WriteLine("4 - Check Balance");
                        Console.WriteLine("5 - Transfer Money");
                        Console.WriteLine("6 - Delete Account");
                        Console.WriteLine("7 - Edit Profile");
                        Console.WriteLine("8 - Logout");



                        Console.Write("Choose Option: ");

                        if (!int.TryParse(Console.ReadLine(), out int choice))
                        {
                            Console.WriteLine("Invalid input! Please enter a number.");
                            continue;
                        }


                        // ================= DEPOSIT =================

                        if (choice == 1)
                        {
                            Console.Write("Enter Deposit Amount: ");

                            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
                            {
                                Console.WriteLine("Invalid amount.");
                                continue;
                            }

                            if (amount <= 0)
                            {
                                Console.WriteLine("Invalid Deposit Amount!");
                                return;
                            }

                            loggedInAccount.Balnce += amount;

                            var transaction = new Transaction
                            {
                                Amount = amount,
                                Type = "Deposit",
                                Date = DateTime.Now,
                                BankAccount = loggedInAccount
                            };

                            context.Transactions.Add(transaction);

                            context.SaveChanges();

                            Console.WriteLine("Deposit Successful!");

                            Console.WriteLine($"New Balance: {loggedInAccount.Balnce}");
                        }


                        // ================= WITHDRAW =================

                        else if (choice == 2)
                        {
                            Console.Write("Enter Withdraw Amount: ");

                            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
                            {
                                Console.WriteLine("Invalid amount.");
                                continue;
                            }

                            if (amount <= 0)
                            {
                                Console.WriteLine("Invalid Withdraw Amount!");
                                return;
                            }

                            if (amount <= loggedInAccount.Balnce)
                            {
                                loggedInAccount.Balnce -= amount;

                                var transaction = new Transaction
                                {
                                    Amount = amount,
                                    Type = "Withdraw",
                                    Date = DateTime.Now,
                                    BankAccount = loggedInAccount
                                };

                                context.Transactions.Add(transaction);

                                context.SaveChanges();

                                Console.WriteLine("Withdraw Successful!");

                                Console.WriteLine($"New Balance: {loggedInAccount.Balnce}");
                            }
                            else
                            {
                                Console.WriteLine("Insufficient Balance!");
                            }
                        }


                        // ================= TRANSACTION HISTORY =================

                        else if (choice == 3)
                        {
                            var transactions = context.Transactions
                                .Where(t => t.BankAccountId == loggedInAccount.Id)
                                .OrderByDescending(t => t.Date)
                                .ToList();

                            Console.WriteLine("===== Transaction History =====");

                            foreach (var transaction in transactions)
                            {
                                Console.WriteLine(
                                    $"Type: {transaction.Type} | Amount: {transaction.Amount} | Date: {transaction.Date}");
                            }
                        }


                        // ================= Check Balance =================


                        else if (choice == 4)
                        {
                            Console.WriteLine("===== ACCOUNT BALANCE =====");

                            Console.WriteLine($"Current Balance: {loggedInAccount.Balnce}");
                        }
                        // ================= Transfer Money =================

                        else if (choice == 5)
                        {
                            Console.Write("Enter Destination Account Number: ");
                            string destinationAccountNumber = Console.ReadLine()!;

                            // نمنع التحويل لنفس الحساب
                            if (destinationAccountNumber == loggedInAccount.AccountNumber)
                            {
                                Console.WriteLine("You cannot transfer to the same account.");
                                return;
                            }

                            // نبحث عن الحساب المستلم
                            var destinationAccount = context.BankAccounts
                                .Include(a => a.User)
                                .FirstOrDefault(a => a.AccountNumber == destinationAccountNumber);

                            // إذا الحساب غير موجود
                            if (destinationAccount == null)
                            {
                                Console.WriteLine("Destination account not found.");
                                return;
                            }

                            Console.Write("Enter Transfer Amount: ");
                            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
                            {
                                Console.WriteLine("Invalid amount.");
                                continue;
                            }

                            // تحقق من صحة المبلغ
                            if (amount <= 0)
                            {
                                Console.WriteLine("Invalid transfer amount.");
                                return;
                            }

                            // تحقق من الرصيد
                            if (amount > loggedInAccount.Balnce)
                            {
                                Console.WriteLine("Insufficient Balance!");
                                return;
                            }

                            // خصم من المرسل
                            loggedInAccount.Balnce -= amount;

                            // إضافة للمستلم
                            destinationAccount.Balnce += amount;

                            // عملية صادرة من حسابك
                            var senderTransaction = new Transaction
                            {
                                Amount = amount,
                                Type = "Transfer Sent",
                                Date = DateTime.Now,
                                BankAccount = loggedInAccount
                            };

                            // عملية واردة للحساب المستلم
                            var receiverTransaction = new Transaction
                            {
                                Amount = amount,
                                Type = "Transfer Received",
                                Date = DateTime.Now,
                                BankAccount = destinationAccount
                            };

                            context.Transactions.Add(senderTransaction);
                            context.Transactions.Add(receiverTransaction);

                            // حفظ كل التغييرات مرة واحدة
                            context.SaveChanges();

                            Console.WriteLine("Transfer Successful!");
                            Console.WriteLine($"Transferred {amount} to {destinationAccount.AccountNumber}");
                            Console.WriteLine($"Your New Balance: {loggedInAccount.Balnce}");
                        }
                        // ================= Delete Account =================

                        else if (choice == 6)
                        {
                            Console.Write("Are you sure you want to delete your account? (yes/no): ");
                            string confirmation = Console.ReadLine()!.ToLower();

                            if (confirmation == "yes")
                            {
                                // جلب جميع العمليات المرتبطة بالحساب
                                var transactions = context.Transactions
                                    .Where(t => t.BankAccountId == loggedInAccount.Id)
                                    .ToList();

                                // حذف العمليات
                                context.Transactions.RemoveRange(transactions);

                                // حذف الحساب البنكي
                                context.BankAccounts.Remove(loggedInAccount);

                                // حذف المستخدم
                                context.Users.Remove(loggedInAccount.User);

                                // حفظ التغييرات
                                context.SaveChanges();

                                Console.WriteLine("Account deleted successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Account deletion canceled.");
                            }
                        }

                        // ================= EDIT PROFILE =================


                        else if (choice == 7)
                        {
                            Console.WriteLine("===== EDIT PROFILE =====");
                            Console.WriteLine("1 - Change Name");
                            Console.WriteLine("2 - Change PIN");

                            Console.Write("Choose Option: ");

                            if (!int.TryParse(Console.ReadLine(), out int editChoice))
                            {
                                Console.WriteLine("Invalid input! Please enter a number.");
                                continue;
                            }

                            // تغيير الاسم
                            if (editChoice == 1)
                            {
                                Console.Write("Enter New Name: ");
                                string newName = Console.ReadLine()!;

                                if (string.IsNullOrWhiteSpace(newName))
                                {
                                    Console.WriteLine("Invalid Name!");
                                    return;
                                }

                                loggedInAccount.User.Name = newName;

                                context.SaveChanges();

                                Console.WriteLine("Name Updated Successfully!");
                            }

                            // تغيير الـ PIN
                            else if (editChoice == 2)
                            {
                                Console.Write("Enter New PIN: ");
                                string newPin = Console.ReadLine()!;

                                if (newPin.Length != 4 || !newPin.All(char.IsDigit))
                                {
                                    Console.WriteLine("PIN must be exactly 4 digits.");
                                    continue;
                                }

                               

                                loggedInAccount.User.Pin = newPin;

                                context.SaveChanges();

                                Console.WriteLine("PIN Updated Successfully!");
                            }
                            else
                            {
                                Console.WriteLine("Invalid Option!");
                            }
                        }

                        // ================= Logge Out =================


                        else if (choice == 8)
                        {
                            Console.WriteLine("Logged out successfully.");
                            break;
                        }
                        
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Account Number or PIN");
                }
            }


            // ================= EXIT =================

            else if (mainChoice == 3)
            {
                Console.WriteLine("Goodbye!");
            }


            // ================= INVALID OPTION =================

            else
            {
                Console.WriteLine("Invalid Option!");
            }
        }
    }
}