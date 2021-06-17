using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControleer : MonoBehaviour
{
    public Transform takip;
    public float speed;
    public Vector3 cizgi;


    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,takip.position,speed)+cizgi;   
    }

}
