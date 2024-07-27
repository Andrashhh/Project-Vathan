using Ricochet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator m_Anim;
    KinematicCharacterMotor m_Controller;

    void Awake() {
        m_Anim = GetComponentInChildren<Animator>();
        m_Controller = GetComponent<KinematicCharacterMotor>();
    }

    void Update() {
        m_Anim.SetFloat("Speed", m_Controller.BaseVelocity.magnitude);
    }
}
