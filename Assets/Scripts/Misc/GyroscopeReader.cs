using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroscopeReader : MonoBehaviour
{
    // Angle to reach with the smartphone to get a tilt magnitude of 1
    [SerializeField] private const float maxAngle = 45;

    private const float normMaxAngle = maxAngle / 90;

    private void Start()
    {
        Input.gyro.enabled = true;
    }


    // Returns tilt vector indicating the direction in which the smartphone is tilted. Should not be greater than 1.
    public Vector2 getTilt()
    {
        Vector3 gravity = Input.gyro.gravity;

        Vector2 rotVec = new Vector2(Mathf.Clamp(gravity.x, -normMaxAngle, normMaxAngle), Mathf.Clamp(gravity.y, -normMaxAngle, normMaxAngle));

        return Vector2.ClampMagnitude(rotVec / normMaxAngle, 1);
    }

    private void OnDestroy()
    {
        Input.gyro.enabled = false;
    }
}
