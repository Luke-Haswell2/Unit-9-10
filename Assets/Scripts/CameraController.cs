using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float FollowSpeed = 1f;
    public float yOffset = 2.25f;
    public float yLevel = -0.59f;
    public Transform target;

    void LateUpdate()
    {
        Vector3 newPos = new Vector3(target.position.x, yLevel, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
    }
}
