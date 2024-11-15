using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using TMPro;
using UnityEngine;

namespace ADV_14
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private TMP_Text _coinBalance;
        [SerializeField] private TMP_Text _gemBalance;
        [SerializeField] private TMP_Text _energyBalance;

        private Dictionary<Currencies, TMP_Text> _currencyTextFields;

        private bool _isInitialized;

        public void Initialize(Wallet wallet)
        {
            CheckSerializefields();
            
            _currencyTextFields = new()
            {
                { Currencies.Coin, _coinBalance},
                { Currencies.Gem, _gemBalance},
                { Currencies.Energy, _energyBalance}
            };
            
            wallet.CurrencyBalances[Currencies.Coin].Changed +=
                (amount) => SetBalance(Currencies.Coin, amount);
            wallet.CurrencyBalances[Currencies.Gem].Changed +=
                (amount) => SetBalance(Currencies.Gem, amount);
            wallet.CurrencyBalances[Currencies.Energy].Changed +=
                (amount) => SetBalance(Currencies.Energy, amount);

            _isInitialized = true;
        }

        private void CheckSerializefields()
        {
            if (_canvas is null) throw new NullReferenceException("Canvas is not set"); 
            if (_coinBalance is null) throw new NullReferenceException("Coin balance text field is not set"); 
            if (_gemBalance is null) throw new NullReferenceException("Gem balance text field is not set"); 
            if (_energyBalance is null) throw new NullReferenceException("Energy balance text field is not set"); 
        }
        
        private void CheckInitialized()
        {
            if (_isInitialized == false)
                throw new CheckoutException("WalletViewer is not Initialized");
        }
        
        public void Show()
        {
            CheckInitialized();
            _canvas.enabled = true;
        }

        public void Hide()
        {
            CheckInitialized();
            _canvas.enabled = false;
        }
        
        public void SetBalance(Currencies currency, int amount)
        {
            CheckInitialized();
            _currencyTextFields[currency].text = amount.ToString();
        }
    }
}