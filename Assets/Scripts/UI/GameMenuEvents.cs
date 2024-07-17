using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameMenuEvents : MonoBehaviour
{
    private UIDocument m_Document;
    private GameState m_GameState;

    private Button m_ResumeButton, m_ExitButton;
    private List<Button> m_AllMenuButton = new List<Button>();

    private AudioSource m_Audio;

    void OnEnable() {
        RegisterClickEventCallbacks();
        RegisterAllMenuButton();
    }

    void OnDisable() {
        UnRegisterClickEventCallbacks();
        UnRegisterAllMenuButton();
    }

    void Awake() {
        InitComponents();
        QueryButtons(m_Document);
    }

    void Update() {
    }

    void OnExitClick(ClickEvent evt) {
        Application.Quit();
    }

    void OnResumeClick(ClickEvent evt) {
        Debug.Log("Clicked");
        m_GameState.ResumeGame();
    }

    void OnAllButtonClick(ClickEvent evt) {
        Debug.Log("Played audio");
        //m_Audio.Play();
    }

    #region MISCS
    void RegisterClickEventCallbacks() {
        m_ResumeButton.RegisterCallback<ClickEvent>(OnResumeClick);
        m_ExitButton.RegisterCallback<ClickEvent>(OnExitClick);
    }
    void UnRegisterClickEventCallbacks() {
        m_ResumeButton.UnregisterCallback<ClickEvent>(OnResumeClick);
        m_ExitButton.UnregisterCallback<ClickEvent>(OnExitClick);
    }
    void RegisterAllMenuButton() {
        for(int i = 0; i < m_AllMenuButton.Count; i++) {
            m_AllMenuButton[i].RegisterCallback<ClickEvent>(OnAllButtonClick);
        }
    }
    void UnRegisterAllMenuButton() {
        for(int i = 0; i < m_AllMenuButton.Count; i++) {
            m_AllMenuButton[i].UnregisterCallback<ClickEvent>(OnAllButtonClick);
        }
    }
    void InitComponents() {
        m_GameState = gameObject.GetComponent<GameState>();
        m_Document = GetComponentInChildren<UIDocument>();
        m_Audio = GetComponent<AudioSource>();
    }
    void QueryButtons(UIDocument doc) {
        m_AllMenuButton = doc.rootVisualElement.Query<Button>().ToList();

        m_ResumeButton = doc.rootVisualElement.Q("ResumeButton") as Button;
        m_ExitButton = doc.rootVisualElement.Q("ExitButton") as Button;
    }
    #endregion
}
