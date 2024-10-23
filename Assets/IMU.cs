using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMUFollow : MonoBehaviour
{
    public GameObject target;
    private float orientationX, orientationY, orientationZ;
    private float accelerationX, accelerationY, accelerationZ;
    private float gyroscopeX, gyroscopeY, gyroscopeZ;

    void Start()
    {
        transform.SetParent(target.transform);
    }

    void FixedUpdate()
    {
        orientationX = transform.rotation.eulerAngles.x;
        orientationY = transform.rotation.eulerAngles.y;
        orientationZ = transform.rotation.eulerAngles.z;
        accelerationX = GetComponent<Rigidbody>().velocity.x;
        accelerationY = GetComponent<Rigidbody>().velocity.y;
        accelerationZ = GetComponent<Rigidbody>().velocity.z;
        gyroscopeX = GetComponent<Rigidbody>().angularVelocity.x;
        gyroscopeY = GetComponent<Rigidbody>().angularVelocity.y;
        gyroscopeZ = GetComponent<Rigidbody>().angularVelocity.z;
    }
}
