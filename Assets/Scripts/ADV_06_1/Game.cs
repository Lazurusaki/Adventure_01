using System;
using UnityEngine;

public class Game : MonoBehaviour
{
    private const ConsoleKey RestartGameButton = ConsoleKey.R;

    [SerializeField] private Transform _playerDefaultTransform;
    [SerializeField] private Transform _enemyDefaultTransform;
    //[SerializeField] private BallController _ballController;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private float _timeToWinSeconds;
    [SerializeField] private float _timerRunDistance;

    private float _timer;
    private bool _isRunning;

    public bool IsTimerRunning { get; private set; } = false;

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (_isRunning == false)
        {
            if (Input.GetKeyDown(KeyCode.R))
                NewGame();
            return;
        }

        if (PlayerInZone())
        {
            if (IsTimerRunning == false)
                StartTimer();

            UpdateTimer();
        }

        if (TryEndGame(out bool isWon))
        {
            PauseGame();

            if (isWon)
                DisplayWinMessage();
            else
                DisplayLooseMessage();

            DisplayRestartGameMessage();
        }
    }

    private bool PlayerInZone()
    {
        //return Vector3.Distance(_player.transform.position, _enemy.transform.position) <= _timerRunDistance;
        return false;
    }

    private void DisplayLooseMessage()
    {
        Debug.Log("YOU LOOSE...");
    }

    private void DisplayWinMessage()
    {
        Debug.Log("YOU WIN!");
    }

    private void DisplayRestartGameMessage()
    {
        Debug.Log($"Press {RestartGameButton} button to try again.");
    }

    private void DisplayObjectiveMessage()
    {
        Debug.Log($"Try stay close to the enemy for {_timeToWinSeconds} seconds.");
    }

    private void NewGame()
    {
        //_player.transform.position = _playerDefaultTransform.position;
        _enemy.transform.position = _enemyDefaultTransform.position;
        IsTimerRunning = false;
        ResetTimer();
        _isRunning = true;
        _enemy.IsWorking = true;
       //_player.IsInputEnabled = true;
        DisplayObjectiveMessage();
    }

    private void PauseGame()
    {
        _isRunning = false;
        _enemy.IsWorking = false;
        _enemy.ResetInput();
        //_player.IsInputEnabled = false;
        //_player.ResetInput();
    }

    private void StartTimer()
    {
        IsTimerRunning = true;
    }

    private void ResetTimer()
    {
        _timer = 0;
    }

    private void UpdateTimer()
    {
        _timer += Time.deltaTime;
    }

    private bool TryEndGame(out bool isWon)
    {
        isWon = false;

        if (_timer >= _timeToWinSeconds)
        {
            isWon = true;
            return true;
        }

        if (PlayerInZone() == false && IsTimerRunning)
        {
            return true;
        }

        return false;
    }
}
