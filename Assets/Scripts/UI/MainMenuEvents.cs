using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument m_Document;
    private Button m_StartButton;

    private AudioSource m_Audio;

    private List<Button> m_AllMenuButton = new List<Button>();

    void Awake() {
        InitComponents();

        m_StartButton = m_Document.rootVisualElement.Q("StartButton") as Button;
        m_AllMenuButton = m_Document.rootVisualElement.Query<Button>().ToList();
        
        m_StartButton.RegisterCallback<ClickEvent>(OnClick);
    }

    void OnEnable() {
        RegisterAllMenuButton();
    }
    void OnDisable() {
        m_StartButton.UnregisterCallback<ClickEvent>(OnClick);

        UnRegisterAllMenuButton();
    }

    void OnClick(ClickEvent evt) {

        Debug.Log("Game started!");
    }

    void OnAllButtonClick(ClickEvent evt) {
        Debug.Log("Played audio");
        m_Audio.Play();
    }

    #region MISCS
    void InitComponents() {
        m_Document = GetComponent<UIDocument>();
        m_Audio = GetComponent<AudioSource>();
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
    #endregion
}
