using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour {
    [Header("Input Action Asset")]
    public InputActionAsset PlayerActionAsset;

    [Header("Action Map Name References")]
    [SerializeField] private string m_ActionMapName = "Player";

    [Header("Action Name References")]
    [SerializeField] private string m_MoveRef = "Move";
    [SerializeField] private string m_LookRef = "Look";
    [SerializeField] private string m_JumpRef = "Jump";
    [SerializeField] private string m_CrouchRef = "Crouch";
    [SerializeField] private string m_FireRef = "Fire";
    [SerializeField] private string m_FireAltRef = "FireAlt";

    private InputAction m_MoveAction;
    private InputAction m_LookAction;
    private InputAction m_JumpAction;
    private InputAction m_CrouchAction;
    private InputAction m_FireAction;
    private InputAction m_FireAltAction;

    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool JumpInput { get; private set; }
    public bool CrouchInput { get; private set; }
    public bool FireInput { get; private set; }
    public bool FireAltInput { get; private set; }

    [Header("Settings")]
    [Range(0f, 10f)]
    public float m_Sensitivity = 1f;

    [SerializeField] private GameState m_State;

    void OnEnable() {
        SubEvents();
    }

    void OnDisable() {
        UnSubEvents();
    }


    void Awake() {
        m_State = FindObjectOfType<GameState>();
        RegisterAction();
        AddInputValue();
    }

    void Update() {
        AddTriggerInputValue();
    }

    void SubEvents() {
        m_State.OnGameTestStart += ConnectInputs;
        m_State.OnGamePause += DisconnectInputs;
        m_State.OnGameResume += ConnectInputs;
        m_State.OnGameLost += DisconnectInputs;
    }
    void UnSubEvents() {
        m_State.OnGameTestStart -= ConnectInputs;
        m_State.OnGamePause -= DisconnectInputs;
        m_State.OnGameResume -= ConnectInputs;
    }

    void ConnectInputs() {
        m_MoveAction.Enable();
        m_LookAction.Enable();
        m_JumpAction.Enable();
        m_CrouchAction.Enable();
        m_FireAction.Enable();
        m_FireAltAction.Enable();
    }
    void DisconnectInputs() {
        m_MoveAction.Disable();
        m_LookAction.Disable();
        m_JumpAction.Disable();
        m_CrouchAction.Disable();
        m_FireAction.Disable();
        m_FireAltAction.Disable();
    }

    void RegisterAction() {
        m_MoveAction = PlayerActionAsset.FindActionMap(m_ActionMapName).FindAction(m_MoveRef);
        m_LookAction = PlayerActionAsset.FindActionMap(m_ActionMapName).FindAction(m_LookRef);
        m_JumpAction = PlayerActionAsset.FindActionMap(m_ActionMapName).FindAction(m_JumpRef);
        m_CrouchAction = PlayerActionAsset.FindActionMap(m_ActionMapName).FindAction(m_CrouchRef);
        m_FireAction = PlayerActionAsset.FindActionMap(m_ActionMapName).FindAction(m_FireRef);
        m_FireAltAction = PlayerActionAsset.FindActionMap(m_ActionMapName).FindAction(m_FireAltRef);
    }

    void AddTriggerInputValue() {
        JumpInput = m_JumpAction.triggered;
        FireInput = m_FireAction.triggered;
        FireAltInput = m_FireAltAction.triggered;
    }

    void AddInputValue() {
        m_MoveAction.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        m_MoveAction.canceled += ctx => MoveInput = Vector2.zero;

        m_LookAction.performed += ctx => LookInput = ctx.ReadValue<Vector2>() * (m_Sensitivity * 0.1f);
        m_LookAction.canceled += ctx => LookInput = Vector2.zero;

        m_CrouchAction.performed += ctx => CrouchInput = true;
        m_CrouchAction.canceled += ctx => CrouchInput = false;

    }
}