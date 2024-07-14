using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    private GameUIInput m_Input;
    private GameState gameState;

    void InitializeComponents() {
        m_Input = gameObject.GetComponent<GameUIInput>();
    }

    void Awake() {
        InitializeComponents();
    }

    void Update() {
        ManageInputUI(m_Input);
    }

    void ManageInputUI(GameUIInput input) {
        if(input.MenuKey) {
            switch(gameState.CurrentState) {
                case GameStateEnum.Idle:
                    Debug.Log("Bing Chilling Idle State");
                    break;
                case GameStateEnum.Active:
                    SetToPause();
                    break;
                case GameStateEnum.Pause:
                    SetToResume();
                    break;
                default:
                    break;
            }
        }
    }

    void SetToPause() {
        gameState.PauseGame();
    }
    void SetToResume() {
        gameState.ResumeGame();
    }

    void OnValidate() {
        if(gameState == null) {
            gameState = gameObject.GetComponent<GameState>();
        }
    }
}
