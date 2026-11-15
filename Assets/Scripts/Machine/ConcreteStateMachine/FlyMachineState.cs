using UnityEngine;

public class FlyMachineState : BaseMachineState
{
    private readonly Rigidbody rb;
    private readonly BaseStats stats;
    private readonly Speed speed;
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
    }

    public override void ExitState()
    {
        //noop
    }

    public override void FixedUpdateState()
    {
        speed.UpdateSpeed();
        RigidBodyMovement.ForceAcceleration(rb, speed,
            stats.airDrag,
            stats.floatiness,
            stats.airDrag);
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