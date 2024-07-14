using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStateEnum {
    Idle,
    Active,
    Pause,
}

public class GameState : MonoBehaviour
{
    public GameStateEnum CurrentState { get; private set; } = GameStateEnum.Idle;

    public event Action OnGameTestStart;
    public event Action OnGamePause;
    public event Action OnGameResume;
    public event Action OnGameLost;
    public event Action OnGameExit;

    void Start() {
        GameTestStart();
    }
    public void GameTestStart() {
        CurrentState = GameStateEnum.Active;
        SetCursorLocked(true);
        SetTimePause(false);
        OnGameTestStart?.Invoke();
    }
    public void PauseGame() {
        CurrentState = GameStateEnum.Pause;
        SetCursorLocked(false);
        SetTimePause(true);
        OnGamePause?.Invoke();
    }
    public void ResumeGame() {
        CurrentState = GameStateEnum.Active;
        SetCursorLocked(true);
        SetTimePause(false);
        OnGameResume?.Invoke();
    }
    public void GameOver() {
        CurrentState = GameStateEnum.Idle;
        SetCursorLocked(false);
        SetTimePause(false);
        OnGameLost?.Invoke();
    }
    public void ExitGame() {
        CurrentState = GameStateEnum.Idle;
        SetCursorLocked(false);
        SetTimePause(true);
        OnGameExit?.Invoke();
    }

    void SetCursorLocked(bool IsCursorLocked) {
        Cursor.lockState = IsCursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
    }
    void SetTimePause(bool IsTimeStop) {
        Time.timeScale = IsTimeStop ? 0f : 1f;
    }

    void OnDestroy() {
        OnGameTestStart = null;
        OnGamePause = null;
        OnGameResume = null;
        OnGameLost = null;
        OnGameExit = null;
    }
}
