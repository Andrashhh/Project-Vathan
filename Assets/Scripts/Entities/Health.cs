using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Health : MonoBehaviour
{


    private float m_CurrentHealth;
    private float m_MaxHealth;

    public event Action<float> OnHealthChange;

    public float CurrentHealth {
        get { return m_CurrentHealth; }
        private set {
            m_CurrentHealth = value > m_MaxHealth ? m_MaxHealth : m_CurrentHealth;
        }
    }
    public float MaxHealth {
        get { return m_MaxHealth; }
        private set { m_MaxHealth = value; }
    }

    void Start() {
        m_MaxHealth = gameObject.GetComponent<EntityPropertyHandler>().MaxHealth;
        m_CurrentHealth = m_MaxHealth;
    }

    void Update() {
        
    }

    public void TakeDamage(float damageAmount) {
        if (m_CurrentHealth > 0.1f) {
            m_CurrentHealth -= damageAmount;
        }
        if(m_CurrentHealth <= 0f) {
            Death();
        }
    }
    public void TakeHealing(float healAmount) {
        if (m_CurrentHealth < m_MaxHealth) {
            m_CurrentHealth += healAmount;
        }
    }
    void Death() {
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other) {

    }
    void OnTriggerStay(Collider other) {

    }
    void OnCollisionEnter(Collision other) {
        if(other.gameObject.GetComponent<Damage>() != null) {
            var dmg = other.gameObject.GetComponent<Damage>();
            TakeDamage(dmg.DamageAmount);
            OnHealthChange?.Invoke((m_CurrentHealth / m_MaxHealth) * 100f);
            Debug.Log("AUCH " + m_CurrentHealth);
        }
    }
}
