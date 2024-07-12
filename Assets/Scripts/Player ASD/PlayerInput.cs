using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public InputActionAsset inputActions;

    public InputAction Move { get; private set; }
    public bool Jump { get; private set; }


    void Awake() {
        Move = inputActions.FindActionMap("Player").FindAction("Move");

        inputActions.FindActionMap("Player").FindAction("Jump").started += OnJumpStarted;
        inputActions.FindActionMap("Player").FindAction("Jump").performed += OnJumpEnded;
    }

    void Update() {

    }

    private void OnJumpStarted(InputAction.CallbackContext context) {
        Jump = true;
    }
    private void OnJumpEnded(InputAction.CallbackContext context) {
        Jump = false;
    }

    void OnEnable() {
        inputActions.FindActionMap("Player").Enable();
    }

    void OnDisable() {
        inputActions.FindActionMap("Player").Disable();

    }
}
