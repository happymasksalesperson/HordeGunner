using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    public float rotationSpeed = 50f; // Adjust this to change the rotation speed

    void Update()
    {
        float rotationAmount = 0f;

        // Check for input
        if (Input.GetKey(KeyCode.A))
        {
            rotationAmount = -1f; // Rotate left
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rotationAmount = 1f; // Rotate right
        }

        // Apply rotation
        transform.Rotate(Vector3.up, rotationAmount * rotationSpeed * Time.deltaTime);
    }
}
