using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Linq;
namespace BankAccount
{
    class Program
    {
        static void Main(string[] args)
        {
            const int ACCOUNT_COUNT = 100;
            const int THREAD_COUNT = 1000;
            var accounts = new BlockingCollection<Account>();

            for (var i = 0; i < ACCOUNT_COUNT; i++)
            {
                var account = new Account();
                accounts.Add(account);
            }

            for (var i = 0; i < THREAD_COUNT; i++)
            {

                if (i % 2 == 0)
                {
                    var random = new Random();
                    var index = random.Next(0, accounts.Count);
                    var account = accounts.ElementAt(index);
                    lock (account)
                    {
                        ThreadPool.QueueUserWorkItem(account.ChangeBalance, 100);
                        Console.WriteLine($"Счёт пополнен на 100, баланс равен {accounts.ElementAt(index).Balance}");
                    }
                }
                else
                {
                    var random = new Random();
                    var index = random.Next(0, accounts.Count);
                    var account = accounts.ElementAt(index);
                    lock (account)
                    {
                        ThreadPool.QueueUserWorkItem(account.ChangeBalance, -100);
                        Console.WriteLine($"Со счёта снято 100, баланс равен {account.Balance}");
                    }
                }

            }
            foreach (var account in accounts)
            {
                Console.WriteLine(account.Id + " - " + account.Balance);
            }
        }
    }
}
