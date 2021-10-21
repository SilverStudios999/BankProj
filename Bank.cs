using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProg
{
    /// <summary>
    /// Simple class that will hold bank information (and it's accounts)
    /// </summary>
    internal class Bank
    {
        public string name { get; set; }
        public List<Account> Accounts { get; set; } = new List<Account>();
    }    
}
