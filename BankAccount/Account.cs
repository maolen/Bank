using System;
using System.Collections.Generic;
using System.Text;

namespace BankAccount
{
    public class Account
    {

        private object syncObject = new object();
        public int Balance { get; set; } = 10000;
        public Guid Id { get; set; } = Guid.NewGuid();
        public void ChangeBalance(object moneySum)
        {
            Balance += (int) moneySum;
        }
    }
}
