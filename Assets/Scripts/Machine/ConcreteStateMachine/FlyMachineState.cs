using UnityEngine;
using UnityEngine.UIElements;

public class FlyMachineState : BaseMachineState
{
    private readonly Rigidbody rb;
    private readonly BaseStats stats;
    private readonly Speed speed;
    private const float plummetForce = 200f; 
    public FlyMachineState(MachineStateManager.MStates key, 
        MachineController controller) : base(key, controller)
    {
        rb = _controller.Rigidbody;
        stats = _controller.BaseStats;
        speed = _controller.Speed;
    }

    public override void EnterState()
    {
        Debug.Log("Entered Flight State");

        // Jump Force
        Vector3 jump = rb.transform.up * stats.jumpForce;
        rb.AddForce(jump, ForceMode.VelocityChange);
    }

    public override void ExitState()
    {
        //noop
    }

    public override void FixedUpdateState()
    {
        // Forward momentum
        speed.UpdateSpeed();
        RigidBodyMovement.ForceAcceleration(rb, speed,
            stats.airDrag,
            stats.floatiness,
            stats.airDrag);

        // Upward/Downward force
        float yValue = _controller.GetInputValue().y;
        Vector3 verticalForce = -rb.transform.up * 20f;
        rb.AddForce(verticalForce * yValue, ForceMode.Acceleration);

        // Brake plummet
        if (_controller.GetBrakeValue())
        {
            Vector3 plummet = -rb.transform.up * plummetForce;
            rb.AddForce(plummet, ForceMode.Acceleration);
        }

        
    }

    public override MachineStateManager.MStates ToStateTransition()
    {
        if (_controller.IsGrounded())
            return MachineStateManager.MStates.Drive;

        return base.ToStateTransition();
    }

    public override void OnTriggerEnter(Collider other)
    {
        //noop
    }

    public override void OnTriggerExit(Collider other)
    {
        //noop
    }

    public override void OnTriggerStay(Collider other)
    {
        //noop
    }

    public override void UpdateState()
    {
        //noop
    }
}