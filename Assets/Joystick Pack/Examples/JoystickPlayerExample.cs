using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
    public float speed;
    public float acceleration;
    public float deceleration;
    public VariableJoystick variableJoystick;
    public Rigidbody rb;

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

    }


}