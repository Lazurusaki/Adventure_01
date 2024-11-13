using System;
using System.Collections.Generic;
using System.Linq;

namespace ADV_14
{
    public class Wallet
    {
        private readonly Dictionary<Currencies, Stat<int>> _currencyBalances = new()
        {
            { Currencies.Coin, new Stat<int>() },
            { Currencies.Gem, new Stat<int>() },
            { Currencies.Energy, new Stat<int>() }
        };

        public IReadOnlyDictionary<Currencies, IReadOnlyStat<int>> CurrencyBalances =>
            _currencyBalances.ToDictionary(pair => pair.Key, pair => (IReadOnlyStat<int>)pair.Value);

        private bool HaveEnoughBalance(Currencies currency, int amount)
        {
            if (amount > _currencyBalances[currency].Value)
                throw new ArgumentOutOfRangeException(nameof(amount), "Not enough currency on balance");

            return true;
        }

        private void ValidateAmount(int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount cannot be negative");
        }

        private void ValidateCurrency(Currencies currency)
        {
            if (_currencyBalances.ContainsKey(currency) == false)
                throw new ArgumentException("Currency does not exist in the wallet");
        }

        private void UpdateBalance(Currencies currency, int amount)
        {
            ValidateCurrency(currency);
            _currencyBalances[currency].Value += amount;
        }

        public void Deposit(Currencies currency, int amount)
        {
            ValidateAmount(amount);
            UpdateBalance(currency, amount);
        }

        public void Withdraw(Currencies currency, int amount)
        {
            ValidateAmount(amount);

            if (HaveEnoughBalance(currency, amount))
                UpdateBalance(currency, -amount);
        }
    }
}