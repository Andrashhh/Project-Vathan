using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    private float m_CurrentHealth;
    private float m_MaxHealth;

    public float CurrentHealth {
        get { return m_CurrentHealth; }
        private set { 
            m_CurrentHealth = value > m_MaxHealth? m_MaxHealth : m_CurrentHealth;
        }
    }
    public float MaxHealth {
        get { return m_MaxHealth; }
        private set { m_MaxHealth = value; }
    }

    void Awake() {

    }
    void Start() {
        m_MaxHealth = gameObject.GetComponent<EntityPropertyHandler>().MaxHealth;
        m_CurrentHealth = m_MaxHealth;
    }

    public void TakeDamage(float damageAmount) {
        if (m_CurrentHealth > 0) {
            m_CurrentHealth -= damageAmount;
        }
        else {
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
        if(other.gameObject.GetComponent<Damage>() !) {
            var dmg = other.gameObject.GetComponent<Damage>();
            dmg.SetDamage(100f);
            TakeDamage(dmg.DamageAmount);
            Debug.Log("AUCH");
        }
    }
}
