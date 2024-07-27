using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    PlayerInputHandler m_Input;
    Crosshair m_Crosshair;

    [SerializeField] GameObject m_LeftAttackPrefab;
    [SerializeField] GameObject m_RightAttackPrefab;
    private FireAttackPrefab m_FireAtk;

    float m_FireTime = 4f;
    float m_FireCd = 4f;
    float m_FireAltTime = 0f;
    float m_FireAltCd = 2f;

    void Awake() {
        m_Input = GetComponent<PlayerInputHandler>();
        m_Crosshair = GetComponent<Crosshair>();
        m_FireAtk = m_LeftAttackPrefab.GetComponent<FireAttackPrefab>();
    }


    void Update()
    {
        m_FireTime += m_FireTime > m_FireCd + 1f ? 0 : Time.deltaTime;
        m_FireAltTime += m_FireAltTime > m_FireAltCd + 1f ? 0 : Time.deltaTime;

        //Debug.Log(m_FireTime + " " + m_FireAltTime);

        if(m_Input.FireInput && m_FireTime > m_FireCd) {
            Debug.Log("Cast Left Hand");
            LeftAttack();
            m_FireTime = 0f;
        }
        if(m_Input.FireAltInput && m_FireAltTime > m_FireAltCd) {
            Debug.Log("Cast Right Hand");
            RightAttack();
            m_FireAltTime = 0;
        }
    }

    void LeftAttack() {
        GameObject projectile = (GameObject) Instantiate(m_LeftAttackPrefab, transform.position + Vector3.up * 3f, Quaternion.LookRotation(Camera.main.transform.forward));
        projectile.transform.rotation *= Quaternion.Euler(0, -90, 0);
    }

    void RightAttack() {
        GameObject projectile = (GameObject)Instantiate(m_RightAttackPrefab, transform.position + Vector3.up * 3f, Quaternion.LookRotation(Camera.main.transform.forward));
        projectile.transform.rotation *= Quaternion.Euler(0, -90, 0);
    }
}
