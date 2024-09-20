using UnityEngine;

public class BallGame : MonoBehaviour
{ 
    [SerializeField] private Transform _ballStart;
    [SerializeField] private BallController _ballController;
    [SerializeField] private CameraRotator _cameraRotator;
    [SerializeField] private float _roundTimeSeconds = 60;
    [SerializeField] private Transform _coinsContainer;

    private InputDetector _inputDetector;
    private float _timer;
    private bool _isRunning = false;

    private void Awake()
    {
        _inputDetector = new InputDetector();
        _ballController.SetInputDetector(_inputDetector);
        _cameraRotator.SetInputDetector(_inputDetector);
    }
    
    private void Start()
    {
        NewGame();
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
            NewGame();
        }
    }

    private bool CheckAllCoinsCollected()
    {
        foreach (Transform child in _coinsContainer)
        {
            if (child.gameObject.activeSelf)
                return false;
        }

        return true;
    }

    private void NewGame()
    {
        _timer = _roundTimeSeconds;   
        _ballController.transform.position = _ballStart.position;
        ResetCoins();
        _isRunning = true;
        _ballController.Unfreeze();
        DisplayObjectiveMessage();
    }

    private void PauseGame()
    {
        _isRunning = false;
        _ballController.Freeze();
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

    private void ResetCoins()
    {
        foreach (Transform child in _coinsContainer)
        {
            child.gameObject.SetActive(true);
        }
    }
}
