using UnityEngine;

public class Logger : MonoBehaviour
{
    public IMUFollow imu;
    public PlayAnims playAnims;

    private string filePath;

    void Start()
    {
        filePath = System.IO.Path.Combine(Application.persistentDataPath, "imu_data.csv");
        if (System.IO.File.Exists(filePath)) System.IO.File.Delete(filePath);
        string header = "Set,AnimIndex,OrientationX,OrientationY,OrientationZ,AccelerationX,AccelerationY,AccelerationZ,GyroscopeX,GyroscopeY,GyroscopeZ";
        System.IO.File.WriteAllText(filePath, header + System.Environment.NewLine);
    }

    void Update()
    {
        string dataLine = string.Format(System.Globalization.CultureInfo.InvariantCulture,
            "{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}",
            playAnims.currentSet,
            playAnims.currentAnimIndex,
            imu.orientationX,
            imu.orientationY,
            imu.orientationZ,
            imu.accelerationX,
            imu.accelerationY,
            imu.accelerationZ,
            imu.gyroscopeX,
            imu.gyroscopeY,
            imu.gyroscopeZ);
        System.IO.File.AppendAllText(filePath, dataLine + System.Environment.NewLine);
    }
}
