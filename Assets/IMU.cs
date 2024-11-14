using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMUFollow : MonoBehaviour
{
    public GameObject target;
    public float orientationX, orientationY, orientationZ;
    public float accelerationX, accelerationY, accelerationZ;
    public float gyroscopeX, gyroscopeY, gyroscopeZ;
    private Rigidbody rb;

    void Start()
    {
        transform.SetParent(target.transform);
        rb = GetComponent<Rigidbody>();
        if (rb == null) Debug.LogError("Rigidbody not found!");
    }

    void FixedUpdate()
    {
        orientationX = transform.rotation.eulerAngles.x;
        orientationY = transform.rotation.eulerAngles.y;
        orientationZ = transform.rotation.eulerAngles.z;
        accelerationX = rb.velocity.x;
        accelerationY = rb.velocity.y;
        accelerationZ = rb.velocity.z;
        gyroscopeX = rb.angularVelocity.x;
        gyroscopeY = rb.angularVelocity.y;
        gyroscopeZ = rb.angularVelocity.z;
    }
}
