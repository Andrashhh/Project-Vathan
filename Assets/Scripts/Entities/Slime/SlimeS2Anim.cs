using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlimeS2Anim : MonoBehaviour
{
    Animator m_Anim;

    int rand;

    string[] m_ShortAttacks = { "atk3", "atk9", "combo0101" };
    string[] m_ShortToMediumAttacks = { "atk2", "atk6"};
    string[] m_Medium = { "atk1", "atk4", "atk7", "combo0102", "combo0103", "combo0104", "combo0105", "combo0302"};
    string[] m_MediumToLongAttacks = { "combo0201" };
    string[] m_ShorterLongAttacks = { "atk8" };
    string[] m_LongAttacks = { "combo0202", "combo0303" };

    

    float time;
    void Awake() {
        m_Anim = GetComponentInChildren<Animator>();
    }
    void Start() {
        rand = UnityEngine.Random.Range(1, m_ShortAttacks.Length);
    }
    void Update() {
        time += Time.deltaTime;

        if(time > 5f) {
            PlayAnim(m_ShortAttacks[UnityEngine.Random.Range(0, m_ShortAttacks.Length)]);
            ResetTime();
        }
    }

    void ResetTime() {
        time = 0f;
    }

    void PlayAnim(string name) {
        m_Anim.SetTrigger(name);
    }
}
