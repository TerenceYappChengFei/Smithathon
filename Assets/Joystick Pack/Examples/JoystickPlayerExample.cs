using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
    public float speed;
    public float acceleration;
    public float deceleration;
    private Quaternion targetRotation;
    private float startingXRotation;
    private float startingZRotation;

    public VariableJoystick variableJoystick;
    public Rigidbody rb;
    public Animator animator;


    private void Start()
    {
        // Store the initial X and Z rotation values of the Rigidbody so player won't change rotation when colliding with walls or other objects
        startingXRotation = rb.rotation.eulerAngles.x;
        startingZRotation = rb.rotation.eulerAngles.z;

        // Initialize the target rotation to the current rotation of the Rigidbody
        targetRotation = rb.rotation;
    }

    public void FixedUpdate()
    {
        Vector3 actualMovement = new Vector3(
            rb.linearVelocity.x,
            0f,
            rb.linearVelocity.z
        );


        Vector3 direction = new Vector3(
            -variableJoystick.Horizontal,
            0f,
            -variableJoystick.Vertical
        );
        // Check if the player is moving based on the joystick input
        bool isMoving = direction.magnitude > 0.1f;
        animator.SetBool("IsMoving", isMoving);


        if (isMoving)
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
            float targetYRotation =
            Quaternion.LookRotation(direction).eulerAngles.y;
            // Add 90 degrees to face the direction of movement

            // Create a new rotation that only affects the Y-axis (up axis) while not changing the X and Z rotations 
            targetRotation = Quaternion.Euler(
            startingXRotation,
            targetYRotation,
            startingZRotation
            );

        }
        rb.angularVelocity = Vector3.zero;
        rb.MoveRotation(targetRotation);
    }
}