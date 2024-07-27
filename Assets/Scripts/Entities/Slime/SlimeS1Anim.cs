using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

enum State {
    moving,
    jumpAttack,
    dashAttack,
    idle,
}

public class SlimeS1Anim : MonoBehaviour
{
    State m_CurrentState = State.idle;

    Animator m_Anim;
    Rigidbody m_Rb;
    GameObject m_Target;
    Damage m_Dmg;

    Vector3 m_TargetDirection;
    float m_TargetDistance;
    Vector3 m_JumpForce;

    string m_DashAnim = "dash";
    string m_JumpAnim = "jump";

    int rand;
    float time;
    bool attackTick;

    void Awake() {
        m_Anim = GetComponentInChildren<Animator>();
        m_Rb = GetComponent<Rigidbody>();
        m_Dmg = GetComponent<Damage>();
        m_Target = GameObject.FindGameObjectWithTag("Player");
    }

    void Start() {
        rand = UnityEngine.Random.Range(1, 3);
    }

    void Update() {
        m_TargetDirection = m_Target.transform.position - transform.position;
        m_TargetDistance = Vector3.Distance(m_Target.transform.position, transform.position);
        m_JumpForce = (m_TargetDirection + (Vector3.up * m_TargetDistance));


        CurrentState(m_CurrentState);
        RotationToTarget();
        UpdateTime();
        attackTick = false;
        if(time > 3.2f) {
            attackTick = true;
            ResetTime();
        }
    }

    void DashAttack() {
        m_Rb.AddForce((m_TargetDirection.normalized * 10f) + Vector3.up * 3f , ForceMode.VelocityChange);
        PlayAnim(m_DashAnim);
    }
    void MoveAttack() {
        m_Rb.AddForce(m_JumpForce.normalized * 10f, ForceMode.VelocityChange);
    }
    void JumpAttack() {
        m_Rb.AddForce(m_JumpForce * 20f, ForceMode.Impulse);
        PlayAnim(m_JumpAnim);
    }


    void RotationToTarget() {
        transform.rotation = Quaternion.LookRotation(new Vector3(m_TargetDirection.x, 0, m_TargetDirection.z), Vector3.up);
    }

    void UpdateTime() {
        time += Time.deltaTime;
    }
    void ResetTime() {
        time = 0f;
    }

    void CurrentState(State state) {
        switch(state) {
            case State.moving:
                MoveAttack();
                m_Dmg.SetDamage(10);
                m_CurrentState = State.idle;
                break;

            case State.jumpAttack:
                m_Dmg.SetDamage(50);
                JumpAttack();
                m_CurrentState = State.idle;
                break;

            case State.dashAttack:
                m_Dmg.SetDamage(30);
                DashAttack();
                m_CurrentState = State.idle;
                break;

            case State.idle:
                if(attackTick && GroundCheck()) {
                    m_Dmg.SetDamage(0);
                    if(m_TargetDistance > 20) {
                        m_CurrentState = State.moving;
                        if(m_TargetDistance > 25) {
                            m_CurrentState = State.jumpAttack;
                        }
                    }
                    else if(m_TargetDistance < 20) {
                        if(m_TargetDistance > 15) {
                            m_CurrentState = State.dashAttack;
                            return;
                        }
                        m_CurrentState = State.moving;
                    }
                }
                break;

            default:
                break;
        }
    }


    bool GroundCheck() {
        return Physics.CheckSphere(transform.position, 0.3f);
    }

    void OnDrawGizmos() {
        Gizmos.DrawSphere(transform.position, 0.3f);
    }

    void PlayAnim(string name) {
        m_Anim.SetTrigger(name);
    }
}
