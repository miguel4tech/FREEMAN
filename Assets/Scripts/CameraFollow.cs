using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 cameraOffset = new Vector3(2.0f, 1.0f, -5.0f);
    public Transform targetPlayer;
 
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = targetPlayer.transform.position + cameraOffset;
    }
}
