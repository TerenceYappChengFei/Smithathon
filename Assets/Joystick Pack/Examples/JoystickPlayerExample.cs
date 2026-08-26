using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
    public float speed;
    public float acceleration;
    public float deceleration;
    private Quaternion targetRotation;

    public VariableJoystick variableJoystick;
    public Rigidbody rb;

    private void Start()
    {
        // Initialize the target rotation to the current rotation of the Rigidbody
        targetRotation = rb.rotation;
    }

    public void FixedUpdate()
    {
        Vector3 direction = new Vector3(
            -variableJoystick.Horizontal,
            0f,
            -variableJoystick.Vertical
        );

        if (direction.magnitude > 1f)
        {
            direction.Normalize();
        }

        Vector3 targetVelocity = direction * speed;

        float movementRate;

        if (direction.magnitude > 0.1f)
        {
            movementRate = acceleration;
        }
        else
        {
            movementRate = deceleration;
        }

        rb.linearVelocity = Vector3.MoveTowards(
            rb.linearVelocity,
            targetVelocity,
            movementRate * Time.fixedDeltaTime
        );

        // Rotate the player to face the direction of movement
        if (direction.magnitude > 0.1f)
        {
            // Find the movement direction,
            //convert to Y rotation and 90 degrees to face the direction of movement
            //compensate for model's innate orientation
            float targetYRotation = Quaternion.LookRotation(direction).eulerAngles.y + 90f;
            // Add 90 degrees to face the direction of movement

            // Create a new rotation that only affects the Y-axis (up axis) while not changing the X and Z rotations 
            targetRotation = Quaternion.Euler(
            rb.rotation.eulerAngles.x,
            targetYRotation,
            rb.rotation.eulerAngles.z
            );

        }
        rb.angularVelocity = Vector3.zero;
        rb.MoveRotation(targetRotation);
    }
}