using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour {
    [Header("Input Action Asset")]
    public InputActionAsset m_PlayerActionAsset;

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

    public Vector2 moveInput { get; private set; }
    public Vector2 lookInput { get; private set; }
    public bool jumpInput { get; private set; }
    public bool crouchInput { get; private set; }
    public bool fireInput { get; private set; }
    public bool fireAltInput { get; private set; }

    [Header("Settings")]
    [Range(0f, 10f)]
    public float m_Sensitivity = 1f;

    void OnEnable() {
        m_MoveAction.Enable();
        m_LookAction.Enable();
        m_JumpAction.Enable();
        m_CrouchAction.Enable();
        m_FireAction.Enable();
        m_FireAltAction.Enable();
    }
    void OnDisable() {
        m_MoveAction.Disable();
        m_LookAction.Disable();
        m_JumpAction.Disable();
        m_CrouchAction.Disable();
        m_FireAction.Disable();
        m_FireAltAction.Disable();
    }

    void Awake() {

        RegisterAction();
        AddInputValue();
    }

    void Update() {

        AddTriggerInputValue();
    }

    private void RegisterAction() {
        m_MoveAction = m_PlayerActionAsset.FindActionMap(m_ActionMapName).FindAction(m_MoveRef);
        m_LookAction = m_PlayerActionAsset.FindActionMap(m_ActionMapName).FindAction(m_LookRef);
        m_JumpAction = m_PlayerActionAsset.FindActionMap(m_ActionMapName).FindAction(m_JumpRef);
        m_CrouchAction = m_PlayerActionAsset.FindActionMap(m_ActionMapName).FindAction(m_CrouchRef);
        m_FireAction = m_PlayerActionAsset.FindActionMap(m_ActionMapName).FindAction(m_FireRef);
        m_FireAltAction = m_PlayerActionAsset.FindActionMap(m_ActionMapName).FindAction(m_FireAltRef);
    }

    void AddTriggerInputValue() {
        jumpInput = m_JumpAction.triggered;
        fireInput = m_FireAction.triggered;
        fireAltInput = m_FireAltAction.triggered;
    }

    void AddInputValue() {
        m_MoveAction.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        m_MoveAction.canceled += ctx => moveInput = Vector2.zero;

        m_LookAction.performed += ctx => lookInput = ctx.ReadValue<Vector2>() * (m_Sensitivity * 0.1f);
        m_LookAction.canceled += ctx => lookInput = Vector2.zero;

        m_CrouchAction.performed += ctx => crouchInput = true;
        m_CrouchAction.canceled += ctx => crouchInput = false;

    }
}