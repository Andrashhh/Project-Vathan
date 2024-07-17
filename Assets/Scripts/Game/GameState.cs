using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public enum GameStateEnum {
    Idle,
    Active,
    Pause,
}

public class GameState : MonoBehaviour
{
    public GameStateEnum CurrentState { get; private set; } = GameStateEnum.Idle;

    public UIDocument GameUI;

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

        SetVisibleUI(false);
    }
    public void PauseGame() {
        CurrentState = GameStateEnum.Pause;
        SetCursorLocked(false);
        SetTimePause(true);
        OnGamePause?.Invoke();

        SetVisibleUI(true);
    }
    public void ResumeGame() {
        CurrentState = GameStateEnum.Active;
        SetCursorLocked(true);
        SetTimePause(false);
        OnGameResume?.Invoke();

        SetVisibleUI(false);
    }
    public void GameOver() {
        CurrentState = GameStateEnum.Idle;
        SetCursorLocked(false);
        SetTimePause(false);
        OnGameLost?.Invoke();

        // SetDeathUIVisible
    }
    public void ExitGame() {
        CurrentState = GameStateEnum.Idle;
        SetCursorLocked(false);
        SetTimePause(true);
        OnGameExit?.Invoke();

        // Do I even use this?
    }

    void SetCursorLocked(bool IsCursorLocked) {
        UnityEngine.Cursor.lockState = IsCursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
    }
    void SetTimePause(bool IsTimeStop) {
        Time.timeScale = IsTimeStop ? 0f : 1f;
    }
    void SetVisibleUI(bool IsUIVisible) {
        if(IsUIVisible) {
            GameUI.rootVisualElement.Q("Menu").style.display = DisplayStyle.Flex;
            GameUI.rootVisualElement.Q("HUD").style.opacity = 0.25f;
        }
        else {
            GameUI.rootVisualElement.Q("Menu").style.display = DisplayStyle.None;
            GameUI.rootVisualElement.Q("HUD").style.opacity = 1f;

        }
    }

    void OnDestroy() {
        OnGameTestStart = null;
        OnGamePause = null;
        OnGameResume = null;
        OnGameLost = null;
        OnGameExit = null;
    }
}
