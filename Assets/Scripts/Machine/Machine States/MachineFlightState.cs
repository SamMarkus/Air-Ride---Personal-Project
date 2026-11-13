using System;
using UnityEngine;

public class MachineFlightState : MachineBaseState
{
    // Components
    private Transform transform;
    private Rigidbody rb;

    // Variables
    private float forceMultiplier = 10f;
    private Speed speed;

    public MachineFlightState(MachineController controller) : base(controller) { }

    public override void FixedUpdate()
    {
        Acceleration();
        FlightMovement();
    }

    public override void OnEnter()
    {
        transform = controller.gameObject.transform;
        rb = controller.Rigidbody;
        speed = controller.Speed;
    }

    public override void OnExit() { }

    public override void Update() { }

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

    private void Acceleration()
    {
        controller.Speed.Accelerate();
    }
}