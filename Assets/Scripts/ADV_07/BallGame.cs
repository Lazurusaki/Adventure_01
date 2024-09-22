using UnityEngine;

public class BallGame : MonoBehaviour
{
    [SerializeField] private Transform _ballStart;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private CameraRotator _cameraRotator;
    [SerializeField] private float _roundTimeSeconds = 60;
    [SerializeField] private Transform _coinsContainer;

    private InputDetector _inputDetector;
    private CoinCollector _coinCollector;
    private float _timer;
    private int _coinsCount;
    private bool _isRunning = false;

    private void Awake()
    {
        _inputDetector = new InputDetector();

        if (_playerController != null)
        {
            _playerController.transform.TryGetComponent(out _coinCollector);
            _playerController.SetInputDetector(_inputDetector);
            _cameraRotator.SetInputDetector(_inputDetector);
        }
        else
        {
            Debug.Log("PlayerController has not assigned");
        }
    }

    private void Start()
    {
        SetCoinsCount();
        StartNewGame();
    }

    private void Update()
    {
        _inputDetector.Update();

        if (_isRunning)
        {
            _timer -= Time.deltaTime;
            DisplayTimer();

            if (TryEndGame(out bool isWin))
            {
                if (isWin)
                {
                    DisplayWinMessage();
                }
                else
                {
                    DisplayLooseMessage();
                }

                PauseGame();
                DisplayRestartGameMessage();
            }
        }
        else if (_inputDetector.IsRestarting)
        {
            StartNewGame();
        }
    }

    private void LateUpdate()
    {
        _playerController.SetAxisDirection(_cameraRotator.GetCamraRotationY());
    }

    private bool CheckAllCoinsCollected()
    {
        return _coinCollector.Coins >= _coinsCount;
    }

    private void StartNewGame()
    {
        _timer = _roundTimeSeconds;
        _playerController.transform.position = _ballStart.position;
        EnableCoins();
        _coinCollector.Empty();
        _isRunning = true;
        _playerController.Enable();
        DisplayObjectiveMessage();
    }

    private void PauseGame()
    {
        _isRunning = false;
        _playerController.Disable();
    }

    private void DisplayLooseMessage()
    {
        Debug.Log("You Loose...");
    }

    private void DisplayObjectiveMessage()
    {
        Debug.Log($"Try to collect all coins for {_roundTimeSeconds} seconds.");
    }

    private void DisplayRestartGameMessage()
    {
        Debug.Log($"Press {_inputDetector.GetRestartKey()} button to try again.");
    }

    private void DisplayTimer()
    {
        Debug.Log($"Time: {_timer}");
    }

    private void DisplayWinMessage()
    {
        Debug.Log("You Win!");
    }

    private void EnableCoins()
    {
        foreach (Transform child in _coinsContainer)
        {
            child.gameObject.SetActive(true);
        }
    }

    private void SetCoinsCount()
    {
        foreach (Transform child in _coinsContainer)
            _coinsCount++;
    }

    private bool TryEndGame(out bool isWin)
    {
        isWin = false;

        if (CheckAllCoinsCollected())
        {
            isWin = true;
            return true;
        }

        if (_timer <= 0)
        {
            return true;
        }

        return false;
    }
}
