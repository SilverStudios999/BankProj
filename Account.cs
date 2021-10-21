using System;

namespace BankProg
{
    //Enum that dictates the account type the account is.
    public enum AccountType { Checking, InvestmentIndividual, InvestmentCorporate }

    /// <summary>
    /// Class that holds all account information and also functions to do things with said accounts
    /// </summary>
    internal class Account
    {
        public string owner { get; set; }
        public decimal balance { get; set; }
        public AccountType accountType { get; set; }

        //Holds the sum of withdrawals for the limit check (I'm just going to treat this strictly for withdrawals,  theoretically could also be added for transfers... but eh)
        private decimal withdrawalSum;

        /// <summary>
        /// Function with a simple check about the amount to be deposited and adds to the accounts balance
        /// </summary>
        /// <param name="amountToDeposit">The amount to be deposited (must be greater than 0)</param>
        public void Deposit(decimal amountToDeposit)
        {
            if (amountToDeposit > 0)
            {
                balance += amountToDeposit;
                Console.WriteLine($@"Deposited: {amountToDeposit}");
                Console.WriteLine($@"Balance: {balance}");
            }
            else
                Console.WriteLine("You must deposit more than $0.");
        }

        /// <summary>
        /// Function that subtracts the balance from the amount to withdraw,  sever checks are done here to make sure the withdrawal is allowed (funds are present).
        /// </summary>
        /// <param name="amountToWithdraw">The amount to withdraw, must be greater than 0 and less than or equal to the balance of the account.</param>
        public void Withdraw(decimal amountToWithdraw)
        {
            if (amountToWithdraw <= 0)
            {
                Console.WriteLine("You must withdraw more than $0.");                
            }
            else if (amountToWithdraw > balance)
            {
                Console.WriteLine($@"Your current Balance is {balance}.  You cannot withdraw greater than your balance (Attempted to Withdraw {amountToWithdraw}).");                
            }
            else if (accountType == AccountType.InvestmentIndividual && withdrawalSum + amountToWithdraw > 500)
            {
                Console.WriteLine($@"You cannot withdraw {amountToWithdraw} because you would exceed your account limit of $500.");                
            }
            else
            {
                balance -= amountToWithdraw;
                withdrawalSum += amountToWithdraw;
                Console.WriteLine($@"Withdrew: {amountToWithdraw}");
                Console.WriteLine($@"Balance: {balance}");             
            }
        }

        /// <summary>
        /// Function to handle transfers of balance from one account to another.
        /// </summary>
        /// <param name="amountToTransfer">The amount to transfer from the origin account (the one that calls this function).  Must be >0 and <= the account balance.</param>
        /// <param name="Destination">The account that will recieve the funds</param>
        public void Transfer(decimal amountToTransfer, Account Destination)
        {
            if(amountToTransfer <= 0)
            {
                Console.WriteLine($@"You must transfer more than $0.");                
            }
            else if(amountToTransfer > balance)
            {
                Console.WriteLine($@"Your transfer exceeds your balance (which is {balance}).  Please deposit more money or transfer less money.");
            }
            else
            {
                Console.WriteLine($@"You are going to transfer {amountToTransfer}. Current Balance {balance}  Destination Balance {Destination.balance}");
                Destination.balance += amountToTransfer;
                balance -= amountToTransfer;
                Console.WriteLine($@"Transfer Complete. Current Balance {balance}  Destination Balance {Destination.balance}");
            }

        }
    }


}