using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace ADV_14
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private WalletView _walletView;

        [FormerlySerializedAs("_timerViewButtons")] [SerializeField]
        private TimerButtonsView _timerButtonsView;

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

            var wallet = new Wallet();
            var walletGame = new WalletGame(wallet);

            _walletView.Initialize(wallet);

            walletGame.Started += _walletView.Show;

            walletGame.Start();
        }

        private void TimerBoot()
        {
            if (_timerSliderView is null)
                throw new NullReferenceException("Timer slider viewer is not set");

            var timer = new Timer(this, _timer);

            _timerButtonsView.Initialize(timer);
            _timerSliderView.Initialize(timer);
            _timerImagasView.Initialize(timer);
        }
    }
}