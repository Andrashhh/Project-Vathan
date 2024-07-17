using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    private GameUIInput m_Input;
    private GameState m_GameState;

    void InitializeComponents() {
        m_Input = gameObject.GetComponent<GameUIInput>();
        m_GameState = gameObject.GetComponent<GameState>();
    }

    void Awake() {
        InitializeComponents();
    }

    void Update() {
        ManageInputUI(m_Input);
    }

    void ManageInputUI(GameUIInput input) {
        if(input.MenuKey) {
            switch(m_GameState.CurrentState) {
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
                    gameObject.GetComponent<MeshRenderer>().material.color = Color.magenta;
                    break;
            }
        }
    }

    void SetToPause() {
        m_GameState.PauseGame();
    }
    void SetToResume() {
        m_GameState.ResumeGame();
    }
}
