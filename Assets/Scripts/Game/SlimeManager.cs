using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Stage {
    Idle,
    StageOne,
    StageTwo,
    End,
}

public class SlimeManager : MonoBehaviour
{
    [SerializeField]
    GameObject m_SlimeS1Prefab;
    [SerializeField]
    GameObject m_SlimeS2Prefab;

    GameObject m_SlimeS1;
    GameObject m_SlimeS2;

    bool SS1Defeated = false;
    bool SS2Defeated = false;

    Stage m_CurrentStage = Stage.Idle;

    float time;

    void Start() {
        LoadPrefabs();
    }

    void Update() {
        time += Time.deltaTime;

        StageHandle();

        if(time > 6f && m_SlimeS1) {
            Destroy(m_SlimeS1);
            SS1Defeated = true;
        }
    }

    void StageHandle() {
        switch(m_CurrentStage) {
            case Stage.Idle:
                Idle();
                break;
            case Stage.StageOne:
                StageOne();
                break;
            case Stage.StageTwo:
                StageTwo();
                break;
            case Stage.End:
                End();
                break;
            default:

                break;
        }
    }

    void Idle() {
        m_CurrentStage = Stage.StageOne;
    }
    void StageOne() {
        if(m_SlimeS1 == null) {
            m_SlimeS1 = Instantiate(m_SlimeS1Prefab);
        }
        if(m_SlimeS1 == null && SS1Defeated) {
            m_CurrentStage = Stage.StageTwo;
        }
    }
    void StageTwo() {
        if(m_SlimeS2 == null) {
            m_SlimeS2 = Instantiate(m_SlimeS2Prefab);
        }
        if(m_SlimeS2 == null && SS2Defeated) {
            m_CurrentStage = Stage.End;
        }
    }
    void End() {
        
    }

    void LoadPrefabs() {

    }
}
