using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace ADV_14
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private WalletView _walletView;
        [FormerlySerializedAs("_timerViewButtons")] [SerializeField] private TimerButtonsView _timerButtonsView;
        [SerializeField] private TimerSliderView _timerSliderView;

        [FormerlySerializedAs("_imagesSpawner")] [FormerlySerializedAs("_timerImagesView")] [SerializeField]
        private TimerImagasView _timerImagasView;

        [SerializeField, Range(0, 30)] private float _timer;

        private void Awake()
        {
            WalletBoot();
            TimerBoot();
        }

        private void WalletBoot()
        {
            if (_walletView is null)
                throw new NullReferenceException("Wallet viewer is not set");

            _walletView.Initialize();

            var wallet = new Wallet();
            var walletGame = new WalletGame(wallet);

            wallet.CurrencyBalances[Currencies.Coin].Changed +=
                (amount) => _walletView.SetBalance(Currencies.Coin, amount);
            wallet.CurrencyBalances[Currencies.Gem].Changed +=
                (amount) => _walletView.SetBalance(Currencies.Gem, amount);
            wallet.CurrencyBalances[Currencies.Energy].Changed +=
                (amount) => _walletView.SetBalance(Currencies.Energy, amount);


            walletGame.Started += _walletView.Show;

            walletGame.Start();
        }

        private void TimerBoot()
        {
            if (_timerSliderView is null)
                throw new NullReferenceException("Timer slider viewer is not set");

            var timer = new Timer(this, _timer);
            _timerButtonsView.Initialize(timer);

            _timerSliderView.SetMaxValue(_timer);
            timer.Started += _timerSliderView.Show;
            timer.Stopped += _timerSliderView.Hide;
            timer.Tick += _timerSliderView.SetValue;

            _timerImagasView.Initialize((int)_timer);
            timer.Tick += _timerImagasView.Update;
            timer.Stopped += _timerImagasView.Hide;
            timer.Started += _timerImagasView.Show;
        }
    }
}