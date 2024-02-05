// See https://aka.ms/new-console-template for more information
namespace ATMApp
{
    public static class ATM
    {
        private static Account? currentAccount;


        public static void Main(string[] args)
        {
            int selection = 0;
            int accountNumber;

            try
            {
                Console.WriteLine("Welcome to CASHATM");
                while(currentAccount == null)
                {
                    Console.WriteLine("\nPlease enter your account number:");
                    accountNumber = int.Parse(Console.ReadLine());
                    currentAccount = Bank.GetAccountByAccountNumber(accountNumber);

                    if (currentAccount == null)
                    {
                        Console.WriteLine("\nCould not find a bank account associated" +
                            " with account #: {0}. Please try again", accountNumber);
                    }
                }

                foreach(Account a in Bank.GetAll())
                {
                    Console.WriteLine("{0}, {1}, {2}, {3}", a.GetName(), a.AccountNumber,
                        a.CheckingsBalance, a.SavingsBalance);
                }

                do
                {
                    MainMenu();
                    selection = int.Parse(Console.ReadLine());
                    double amount;

                    switch (selection)
                    {
                        case 1:
                            Console.Write("\nPlease enter the amount you would like to withdraw:\n$ ");
                            amount = double.Parse(Console.ReadLine());
                            Withdraw(amount);
                            PrintTransaction();
                            break;
                        case 2:
                            Console.Write("\nPlease enter the amount you would like to deposit:\n$ ");
                            amount = double.Parse(Console.ReadLine());
                            Deposit(amount);
                            PrintTransaction();
                            break;
                        case 3:
                            CheckBalances();
                            break;
                        case 4:
                            TransferMenu();
                            break;
                        case 5:
                            Bank.CloseAccount(currentAccount);
                            selection = 6;
                            break;
                    }
                } while (selection != 6);

                foreach (Account a in Bank.GetAll())
                {
                    Console.WriteLine("{0}, {1}, {2}, {3}", a.GetName(), a.AccountNumber,
                        a.CheckingsBalance, a.SavingsBalance);
                }
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void Withdraw(double amount)
        {
            if (amount < 0)
            {
                Console.WriteLine("Deposit amount must be greater than 0.");
                return;
            }
            if (currentAccount == null)
                return;
            if (currentAccount.CheckingsBalance < amount)
            {
                Console.WriteLine("Insufficient Funds.");
                return;
            }

            currentAccount.CheckingsBalance -= amount;
        }

        public static void Deposit(double amount)
        {
            if (amount < 0)
            {
                Console.WriteLine("Deposit amount must be greater than 0.");
                return;
            }
            if (currentAccount == null)
                return;

            currentAccount.CheckingsBalance += amount;
        }

        public static void CheckBalances()
        {
            if (currentAccount == null)
                return;

            Console.WriteLine("\nAccount Summary: ");
            Console.WriteLine("Checkings Balance:\t$ {0}", currentAccount.CheckingsBalance);
            Console.WriteLine("Savings Balance:\t$ {0}\n", currentAccount.SavingsBalance);
        }

        public static void Transfer()
        {
            double amount;

            Console.Write("\nPlease enter the amount you wish to deposit into savings:\n$");
            amount = double.Parse(Console.ReadLine());

            if (amount < 0)
            {
                Console.WriteLine("Deposit amount must be greater than 0.");
                return;
            } else if (currentAccount == null) {
                    return;
            }
            else if (currentAccount.CheckingsBalance < amount)
            {
                Console.WriteLine("Insufficient Funds.");
                return;
            }
            else
            {
                currentAccount.CheckingsBalance -= amount;
                currentAccount.SavingsBalance += amount;
            }
        }

        public static void TransferByAccountNumber(int accountNumber)
        {
            Account? toAccount = Bank.GetAccountByAccountNumber(accountNumber);
            double amount;

            if (toAccount == null)
            {
                Console.WriteLine("\nCould not find bank account associated with that account number.");
                return;
            }

            if (currentAccount == toAccount)
            {
                Transfer();
                return;
            }

            Console.Write("\nPlease enter the amount you wish to transfer to the account:\n$");
            amount = double.Parse(Console.ReadLine());

            if (amount < 0)
            {
                Console.WriteLine("\nTransfer amount must be greater than 0");
                return;
            }
            else if (currentAccount == null)
            {
                return;
            }
            else if (currentAccount.CheckingsBalance < amount)
            {
                Console.WriteLine("\nInsufficient funds.");
                return;
            }
            else
            {
                currentAccount.CheckingsBalance -= amount;
                toAccount.CheckingsBalance += amount;
                PrintTransaction();
            }
        }

        public static void TransferByName(string name)
        {
            Account? toAccount = Bank.GetAccountByName(name);
            if(toAccount == null)
            {
                Console.WriteLine("\nCould not locate any accounts associated with that name.");
                return;
            }

            TransferByAccountNumber(toAccount.AccountNumber);
        }

        public static void PrintTransaction()
        {
            if (currentAccount == null)
                return;

            Console.WriteLine("\nTransaction Summary: ");
            Console.WriteLine("{0}\n{1}\n", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString());
            Console.WriteLine("Checkings Balance:\t$ {0}", currentAccount.CheckingsBalance);
            Console.WriteLine("Savings Balance:\t$ {0}\n", currentAccount.SavingsBalance);
        }

        public static void MainMenu()
        {
            Console.WriteLine("\nPlease select from the following options:");
            Console.WriteLine("1) Withdrawal");
            Console.WriteLine("2) Deposit");
            Console.WriteLine("3) Check Account Balances");
            Console.WriteLine("4) Transfer");
            Console.WriteLine("5) Close Account");
            Console.WriteLine("6) Exit");
        }

        public static void TransferMenu()
        {
            int selection = 0;
            Console.WriteLine("\nPlease select from the following options:");
            Console.WriteLine("1) Transfer to personal savings account");
            Console.WriteLine("2) Transfer to Account Number");
            Console.WriteLine("3) Transfer by Name");
            Console.WriteLine("4) Return");
            try
            {
                selection = int.Parse(Console.ReadLine());

                switch (selection)
                {
                    case 1:
                        Transfer();
                        PrintTransaction();
                        break;
                    case 2:
                        Console.WriteLine("\nPlease enter the account number you wish to transfer funds to: ");
                        TransferByAccountNumber(int.Parse(Console.ReadLine()));
                        break;
                    case 3:
                        string toAccountName = "";
                        Console.WriteLine("\nPlease enter the name of the account holder to send funds to [First MI Last]: ");
                        toAccountName = Console.ReadLine();
                        if (toAccountName != null)
                        {
                            TransferByName(toAccountName);
                        }
                        break;
                    case 4:
                        return;

                }
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}