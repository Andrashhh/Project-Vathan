using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument m_Document;
    private Button m_StartButton, m_ExitButton;
    private AudioSource m_Audio;
    private List<Button> m_AllMenuButton = new List<Button>();

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
    
    void OnExitClick(ClickEvent evt) {
        Application.Quit();
    }

    void OnStartClick(ClickEvent evt) {
        SceneManager.LoadScene("Game");
    }

    void OnAllButtonClick(ClickEvent evt) {
        Debug.Log("Played audio");
        m_Audio.Play();
    }



    #region MISCS
    void RegisterClickEventCallbacks() {
        m_StartButton.RegisterCallback<ClickEvent>(OnStartClick);
        m_ExitButton.RegisterCallback<ClickEvent>(OnExitClick);
    }
    void UnRegisterClickEventCallbacks() {
        m_StartButton.UnregisterCallback<ClickEvent>(OnStartClick);
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
        m_Document = GetComponent<UIDocument>();
        m_Audio = GetComponent<AudioSource>();
    }
    void QueryButtons(UIDocument doc) {
        m_AllMenuButton = doc.rootVisualElement.Query<Button>().ToList();

        m_StartButton = doc.rootVisualElement.Q("StartButton") as Button;
        m_ExitButton = doc.rootVisualElement.Q("ExitButton") as Button;
    }

    #endregion
}
