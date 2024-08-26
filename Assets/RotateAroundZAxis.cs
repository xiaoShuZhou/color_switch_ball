using UnityEngine;

public class RotateAroundZAxis : MonoBehaviour
{
    public float rotationSpeed = 100f;  // Rotation speed in degrees per second

    void Update()
    {
        // Rotate around the Z-axis every frame
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}