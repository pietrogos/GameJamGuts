using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset; // initial offset
    public float followSpeed = 10f;
    public float zoomFactor = 1f; // adjust this to change how much the camera zooms out

    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset * target.localScale.x * zoomFactor;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}