using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject player;
    float cameraDistance = 7.0f;
    float cameraHeight = 2f;

    //Follow player with offset
    void LateUpdate()
    {
        transform.position = player.transform.position - player.transform.forward * cameraDistance;
        transform.LookAt(player.transform.position);
        transform.position = new Vector3(transform.position.x, transform.position.y + cameraHeight, transform.position.z);
    }
}