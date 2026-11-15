using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UIElements;

public static class RigidBodyMovement
{
    public static void ForceAcceleration(Rigidbody rb, Speed speed,
        float xDrag = 0f,
        float yDrag = 0f,
        float zDrag = 0f,
        float forceMultiplier = 10f)
    {
        // Main forward movement
        Vector3 movement = rb.transform.forward * speed.CurrentSpeed;
        rb.AddForce(movement * forceMultiplier, ForceMode.Acceleration);
        // Drag force
        rb.AddForce(new Vector3(
            -rb.linearVelocity.x * xDrag,
            -rb.linearVelocity.y * yDrag,
            -rb.linearVelocity.z * zDrag));
    }

    public static void ForceAcceleration(Rigidbody rb, Speed speed,
        float dragForce)
    {
        ForceAcceleration(rb, speed, dragForce, dragForce, dragForce);
    }

    public static void DirectionalAcceleration(Rigidbody rb, Speed speed,
        Vector3 direction,
        float xDrag = 0f,
        float yDrag = 0f,
        float zDrag = 0f,
        float forceMultiplier = 10f)
    {
        // Main forward movement
        Vector3 movement = direction * speed.CurrentSpeed;
        rb.AddForce(movement * forceMultiplier, ForceMode.Acceleration);
        // Drag force
        rb.AddForce(new Vector3(
            -rb.linearVelocity.x * xDrag,
            -rb.linearVelocity.y * yDrag,
            -rb.linearVelocity.z * zDrag));
    }

    public static void DirectionalAcceleration(Rigidbody rb, Speed speed,
        Vector3 direction, 
        float dragForce)
    {
        DirectionalAcceleration(rb, speed, direction, dragForce, dragForce, dragForce);
    }
}