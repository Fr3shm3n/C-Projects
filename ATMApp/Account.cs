using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMApp
{
    class Account
    {
        private string firstName = "";
        private string lastName = "";
        private readonly int accountNumber;
        private double checkingsBalance;
        private double savingsBalance;

        public Account() {}

        public Account(string name, int accountNumber, double checkingsBalance, double savingsBalance=0.0)
        {
            SetName(name);
            this.AccountNumber = accountNumber;
            this.CheckingsBalance = checkingsBalance;
            this.SavingsBalance = savingsBalance;
        }

        public string GetName()
        {
            return firstName + " " + lastName;
        }
        public void SetName(string s)
        {
            int delimiterPos = s.LastIndexOf(" ");

            this.lastName = s.Substring(delimiterPos + 1);
            this.firstName = s.Substring(0, delimiterPos);
        }
        public int AccountNumber { get; init; }

        public double CheckingsBalance { get; set; }

        public double SavingsBalance { get; set; }
    }
}
