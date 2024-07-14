using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameUIInput : MonoBehaviour
{
    [Header("Input Action Asset")]
    public InputActionAsset UIActionAsset;

    [Header("Action Map Name References")]
    [SerializeField] private string m_ActionMapName = "UI";

    [Header("Action Name References")]
    [SerializeField] private string m_MenuRef = "Menu";

    private InputAction m_MenuAction;

    public bool MenuKey { get; private set; }

    void OnEnable() {
        m_MenuAction.Enable();

    }
    void OnDisable() {
        m_MenuAction.Disable();
    }

    void Awake() {
        RegisterAction();
    }

    void Update() {
        MenuKey = m_MenuAction.triggered;
    }

    private void RegisterAction() {
        m_MenuAction = UIActionAsset.FindActionMap(m_ActionMapName).FindAction(m_MenuRef);

    }
}
