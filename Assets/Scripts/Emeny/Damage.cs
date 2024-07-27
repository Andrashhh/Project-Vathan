using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float m_DamageAmount;
    float m_LastDamage;
    public float DamageAmount;

    void Update() {
        DamageAmount = m_DamageAmount;

    }

    public float SetDamage(float DamageAmount) {
        return m_DamageAmount = DamageAmount;
    }
}
