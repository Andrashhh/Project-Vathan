using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Damage : MonoBehaviour
{
    float m_DamageAmount;
    public float DamageAmount {
        get { return m_DamageAmount; }
        set { m_DamageAmount = value; }
    }

    public void SetDamage(float DamageAmount) {
        m_DamageAmount = DamageAmount;
    }
}
