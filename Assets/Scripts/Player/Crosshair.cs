using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    Camera cm;
    GameObject vCam;

    public Vector3 CrosshairPoint { get; private set; }
    public Vector3 CrosshairDirection { get; private set; }


    void Awake() {
        cm = Camera.main;
        vCam = GameObject.FindGameObjectWithTag("VirtualCam");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var vCamPos = vCam.transform.position + Vector3.forward;

        if(Physics.Raycast(vCamPos, cm.transform.forward, out RaycastHit hit, float.MaxValue)) {
            CrosshairPoint = (hit.point - vCamPos);
            CrosshairDirection = CrosshairPoint + transform.position + Vector3.up * 3f;
            
            Debug.DrawRay(transform.position + (Vector3.up * 3f), CrosshairPoint, Color.yellow);
        }
    }
}
