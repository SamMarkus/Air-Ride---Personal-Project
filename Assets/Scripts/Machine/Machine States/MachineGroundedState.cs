using UnityEngine;
using UnityEngine.UIElements;

public class MachineGroundedState : MachineBaseState
{
    // Components
    private Transform transform;
    private Rigidbody rb;

    // Variables
    private float forceMultiplier = 10f;
    private Speed speed; 

    public MachineGroundedState(MachineController controller) : base(controller) {}

    public override void OnEnter()
    {
        transform = controller.gameObject.transform;
        rb = controller.Rigidbody;
        speed = controller.Speed;
    }

    public override void OnExit() {}

    public override void Update() { }
    public override void FixedUpdate()
    {
        Acceleration();
        GroundedMovement();
    }

    public void GroundedMovement()
    {
        Speed speed = controller.Speed;
        Vector3 groundMovement = transform.forward * speed.CurrentSpeed;
        rb.AddForce(groundMovement * forceMultiplier, ForceMode.Acceleration);
        rb.AddForce(-rb.linearVelocity * controller.groundDrag); // Drag force
    }

    private void Acceleration()
    {
        controller.Speed.Accelerate();
    }
}