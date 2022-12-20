using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 cameraOffset = new Vector3(2.0f, 1.0f,-5.0f);
    public Transform targetPlayer;

    //Some cool skybox rotation
    public float rotateSpeed = 1.2f;
    void Start()
    {
        targetPlayer = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = targetPlayer.transform.position + cameraOffset;
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotateSpeed);
    }
}
