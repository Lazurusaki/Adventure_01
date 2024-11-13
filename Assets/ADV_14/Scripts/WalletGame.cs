using System;

namespace ADV_14
{
    public class WalletGame
    {
        private readonly Wallet _wallet;

        public event Action Started;

        public WalletGame(Wallet wallet)
        {
            _wallet = wallet;
        }

        public void Start()
        {
            Started?.Invoke();
            Test_01();
        }

        private void Test_01()
        {
            _wallet.Deposit(Currencies.Coin, 100);
            _wallet.Deposit(Currencies.Gem, 100);
            _wallet.Deposit(Currencies.Energy, 100);

            _wallet.Withdraw(Currencies.Coin, 99);
            _wallet.Withdraw(Currencies.Gem, 1);
            //_wallet.Withdraw(Currencies.Energy,-1);
        }
    }
}