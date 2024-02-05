using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMApp
{
    internal static class Bank
    {

        private static List<Account> Accounts = new List<Account>() { new Account("Tony Stark", 123456, 200000.99D), new Account("Elon Musk", 101010, 1234567.00D, 500.00D),
                                                                      new Account("Richie $ Rich", 999999, 999999999.99D, 999999.99D), new Account("Bill Gates", 987654, 1357911.83D),
                                                                      new Account("Mark Cuban", 333999, 1000000000.00, 500000.00), new Account("Lebron James", 454545, 1250000.00, 750000.00)  };

        public static List<Account> GetAll()
        {
            return Accounts;
        }

        public static Account? GetAccountByAccountNumber(int accountNumber) 
        {
            return Accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
        }

        public static Account? GetAccountByName(string name)
        {
            return Accounts.FirstOrDefault(a => a.GetName() == name);
        }

        public static void SetAccount(Account account)
        {
            if(!account.Equals(null))
                Accounts.Add(account);
        }

        public static void CloseAccount(Account account)
        {
            Console.WriteLine("Closing CASHATM account:");
            Console.WriteLine("Checkings Balance:\t$ {0}", account.CheckingsBalance);
            Console.WriteLine("Savings Balance:\t$ {0}\n", account.SavingsBalance);
            Console.WriteLine("Thank you for banking with us and we hope you consider us in the future.\n");
            account.CheckingsBalance = account.SavingsBalance = 0;
            Accounts.Remove(account);
        }
    }
}
