using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAttackPrefab : MonoBehaviour
{
    void Start() {
        Destroy(gameObject, 5f);
    }

    void Update() {
        transform.position += transform.right * 15f * Time.deltaTime;
    }

}
