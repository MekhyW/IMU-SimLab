using UnityEngine;

public class IMUFollow : MonoBehaviour
{
    public GameObject target;
    public float orientationX, orientationY, orientationZ;
    public float accelerationX, accelerationY, accelerationZ;
    public float gyroscopeX, gyroscopeY, gyroscopeZ;
    private Vector3 previousVelocity;
    private Vector3 previousPosition;
    private Quaternion previousRotation;
    private float deltaTime;

    void Start()
    {
        transform.SetParent(target.transform);
        previousPosition = transform.position;
        previousVelocity = Vector3.zero;
        previousRotation = transform.rotation;
    }

    void FixedUpdate()
    {
        deltaTime = Time.fixedDeltaTime;
        orientationX = transform.rotation.eulerAngles.x;
        orientationY = transform.rotation.eulerAngles.y;
        orientationZ = transform.rotation.eulerAngles.z;
        Vector3 currentVelocity = (transform.position - previousPosition) / deltaTime;
        Vector3 acceleration = (currentVelocity - previousVelocity) / deltaTime;
        accelerationX = acceleration.x;
        accelerationY = acceleration.y;
        accelerationZ = acceleration.z;
        Quaternion deltaRotation = transform.rotation * Quaternion.Inverse(previousRotation);
        Vector3 angularVelocity = new Vector3(
            Mathf.DeltaAngle(0, deltaRotation.eulerAngles.x),
            Mathf.DeltaAngle(0, deltaRotation.eulerAngles.y),
            Mathf.DeltaAngle(0, deltaRotation.eulerAngles.z)
        ) / deltaTime * Mathf.Deg2Rad;
        gyroscopeX = angularVelocity.x;
        gyroscopeY = angularVelocity.y;
        gyroscopeZ = angularVelocity.z;
        previousPosition = transform.position;
        previousVelocity = currentVelocity;
        previousRotation = transform.rotation;
    }
}
