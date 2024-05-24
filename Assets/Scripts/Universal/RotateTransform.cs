using UnityEngine;

public class RotateTransform : MonoBehaviour
{
    public enum RotationAxis { X, Y, Z }

    public RotationAxis axis = RotationAxis.Y;
    public bool rotatePositive = true;
    public float rotateSpeed = 1.0f;

    void Update()
    {
        Rotate();
    }

    void Rotate()
    {
        float rotationAmount = rotateSpeed * Time.deltaTime * (rotatePositive ? 1f : -1f);

        switch (axis)
        {
            case RotationAxis.X:
                transform.Rotate(Vector3.right, rotationAmount);
                break;
            case RotationAxis.Y:
                transform.Rotate(Vector3.up, rotationAmount);
                break;
            case RotationAxis.Z:
                transform.Rotate(Vector3.forward, rotationAmount);
                break;
        }
    }
}