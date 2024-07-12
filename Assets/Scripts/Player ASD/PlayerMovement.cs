using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerInput m_PlayerInput;
    [SerializeField] private Rigidbody m_Rb;

    private Vector2 m_CurrentVelocity;

    void Awake() {
        InitializeComponents();
    }

    void Update() {

    }

    void FixedUpdate() {
        float forward = m_PlayerInput.Move.ReadValue<Vector2>().x;
        float right = m_PlayerInput.Move.ReadValue<Vector2>().y;

        m_CurrentVelocity = new Vector2(forward, right);
        // Work to be done in this sector
        MovePlayer(m_CurrentVelocity * 10f);
    }

    private void MovePlayer(Vector2 moveXY) {
        if(m_PlayerInput.Move.ReadValue<Vector2>() != null) {
            m_Rb.velocity = moveXY;
        }
        else {
            moveXY = new Vector2(0f, 0f);
            m_Rb.velocity = moveXY;
        }
    }

    void InitializeComponents() {
        m_PlayerInput = gameObject.GetComponent<PlayerInput>();
        m_Rb = gameObject.GetComponent<Rigidbody>();
    }
}
