using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform ankerPos, ankerLookAtPos;
    public float posLerp;

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, ankerPos.position, posLerp);
        transform.LookAt(ankerLookAtPos);
    }
}