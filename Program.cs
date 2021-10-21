using System;
using System.Collections.Generic;
using System.Linq;

namespace BankProg
{
    class Program
    {
        //Random class used to generate random account types and balances
        static Random random = new Random();
        //Counter to give "unique" owner names
        static int counter = 0;
        static void Main(string[] args)
        {            
            Bank TestBank = new Bank
            {
                name = "Fidelity"                
            };

            //Generate 30 bank accounts randomly,  have them all deposit random amounts, withdraw random amounts... and transfer different amounts to the account created before them.
            while(TestBank.Accounts.Count < 30)
            {
                TestBank.Accounts.Add(GenerateAccount());                
            }            

            //Combining test for deposits and withdrawals
            foreach(Account account in TestBank.Accounts)
            {
                Console.WriteLine($@"Before Deposit Balance: {account.balance}");
                account.Deposit(random.Next(100));
                account.Withdraw(random.Next(10000));
            }

            //Get an account that is of type indivdual that has balance of greater than 500 and trigger the 500 check, test will be skipped if random account is not found.
            Account indAccount = TestBank.Accounts.Where(x => x.accountType == AccountType.InvestmentIndividual && x.balance > 500).FirstOrDefault();
            if (indAccount != null)
            {
                for (int x = 0; x < 505; x++)
                {
                    indAccount.Withdraw(1);
                }
            }

            //Transfers current account will transfer money to previous account, just a quick hackyish way to test transfers somewhat randomly.
            Account prevaccount = new Account();
            foreach (Account account in TestBank.Accounts)
            {
                if (prevaccount == null)
                    prevaccount = account;
                else
                {
                    account.Transfer(random.Next(1000), prevaccount);
                    prevaccount = account;
                }
                
            }
        }

        ///Function to generate a random account with a random account type and balance
        public static Account GenerateAccount()
        {
            Array values = Enum.GetValues(typeof(AccountType));
            counter += 1;
            return new Account
            {
                owner = $@"TestAccount{counter}",
                balance = decimal.Parse((random.NextDouble() * random.Next(10000)).ToString("0.00")),
                accountType = (AccountType)values.GetValue(random.Next(3))
            };
        }



    }
}
