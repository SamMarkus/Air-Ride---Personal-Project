using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
[RequireComponent(typeof(Rigidbody))]

[RequireComponent(typeof(MachineController))]
public class MachineMovement : MonoBehaviour
{
    private MachineController controller;
    private Rigidbody rb;

    [Header("Speed")]
    private float forceMultiplier = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<MachineController>();
        rb = controller.Rigidbody;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!controller.GetBrakeValue())
            controller.Speed.UpdateSpeed();
        Movement();
    }

    private void Movement()
    {
        // Move the vehicle automatically. Air Ride machines do not stop.
        if (controller.IsGrounded()) // Ground control
        {
            GroundedMovement();
        }
        else // Flight control
        {
            FlightMovement();
        }
        DisplaySpeedometer();
    }

    public void GroundedMovement()
    {
        Speed speed = controller.Speed;
        Vector3 groundMovement = transform.forward * speed.CurrentSpeed;
        rb.AddForce(groundMovement * forceMultiplier, ForceMode.Acceleration);
        rb.AddForce(-rb.linearVelocity * controller.groundDrag); // Drag force
    }

    public void FlightMovement()
    {
        Speed speed = controller.Speed;
        // Main forward movement
        Vector3 airMovement = transform.forward * speed.CurrentSpeed;
        rb.AddForce(airMovement * forceMultiplier, ForceMode.Acceleration);
        // Drag force
        rb.AddForce(new Vector3(
            -rb.linearVelocity.x * controller.airDrag,
            -rb.linearVelocity.y * controller.floatiness,
            -rb.linearVelocity.z * controller.airDrag));
    }

    public void DisplaySpeedometer()
    {
        int speedometer = (int)Vector3.Magnitude(rb.linearVelocity);
        Debug.Log(speedometer);
    }

    private void Acceleration()
    {
        controller.Speed.Accelerate();
    }
    
    public void Decelerate()
    {
        controller.Speed.Decelerate();
    }
}