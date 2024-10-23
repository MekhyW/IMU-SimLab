using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.05f;
    private Vector3 locationOffset;
    private Vector3 rotationOffset;

    void Start()
    {
        locationOffset = transform.position - target.position;
        rotationOffset = transform.eulerAngles - target.eulerAngles;
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + locationOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        Quaternion desiredRotation = Quaternion.Euler(target.eulerAngles + rotationOffset);
        Quaternion smoothedRotation = Quaternion.Lerp(transform.rotation, desiredRotation, smoothSpeed);
        transform.rotation = smoothedRotation;

        transform.LookAt(target);
    }
}
