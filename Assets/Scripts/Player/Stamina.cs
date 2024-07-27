using Ricochet.Kinematic;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    Ricochet.Kinematic.CharacterController cc;
    PlayerInputHandler m_Input;

    private float m_CurrentStamina;
    private float m_MaxStamina;
    
    private int stamCd;

    public bool CanDodge { get; private set; }

    public event Action<float> OnStaminaChange;

    public float CurrentStamina {
        get { return m_CurrentStamina; }
        private set {
            m_CurrentStamina = value > m_MaxStamina ? m_MaxStamina : m_CurrentStamina;
        }
    }

    public float MaxStamina {
        get { return m_MaxStamina; }
        private set { m_MaxStamina = value; }
    }

    void Start() {
        cc = GetComponent<Ricochet.Kinematic.CharacterController>();
        m_Input = GetComponent<PlayerInputHandler>();

        m_MaxStamina = GetComponent<EntityPropertyHandler>().MaxStamina;
        m_CurrentStamina = m_MaxStamina;
    }

    void Update() {
        GainStamina();
        HasEnoughStamina();
        OnDodge();

        OnStaminaChange?.Invoke((m_CurrentStamina / m_MaxStamina) * 100f);
    }
    
    void HasEnoughStamina() {
        if(m_CurrentStamina >= 25f) {
            CanDodge = true;
            return;
        }
        CanDodge = false;
    }

    void GainStamina() {
        if(cc.Velocity.magnitude > 2f && m_CurrentStamina < m_MaxStamina) {
            m_CurrentStamina += Time.deltaTime * 5f;
        }
    }

    void OnDodge() {
        if(!cc.DodgeAvailable && stamCd == 0) {
            m_CurrentStamina -= 25f;
            stamCd = 1;
        }
        if(cc.DodgeAvailable) {
            stamCd = 0;
        }
    }
}
