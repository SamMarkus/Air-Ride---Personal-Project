using UnityEngine;
using UnityEngine.Rendering;

public class BrakeMachineState : BaseMachineState
{
    private readonly Rigidbody rb;
    private readonly BaseStats stats;
    private readonly Speed speed;
    private Vector3 storedDirection; 

    // Boost
    private float chargeMeter;

    public BrakeMachineState(MachineStateManager.MStates key, 
        MachineController controller) : base(key, controller)
    {
        rb = _controller.Rigidbody;
        stats = _controller.BaseStats;
        speed = _controller.Speed;
    }

    public override void EnterState()
    {
        Debug.Log("Enter Brake State");
        if (!stats.updateForceWhenBraking)
            storedDirection = rb.transform.forward;
    }

    public override void ExitState()
    {
        ApplyBoost();
    }

    public override void FixedUpdateState()
    {
        
        // Determine how much deceleration is being applied
        if (_controller.GetInputValue().x != 0)
            speed.Deceleration = stats.turnBrakeRate;
        else
            speed.Deceleration = stats.brakeRate;
        speed.Decelerate();
        
        if (stats.updateForceWhenBraking)
            RigidBodyMovement.ForceAcceleration(rb, speed, stats.groundDrag);
        else
            RigidBodyMovement.DirectionalAcceleration(rb, speed, storedDirection,
                stats.groundDrag);

        // Blink effect
        if (rb.gameObject.TryGetComponent<ModelBlink>(out var blink))
        {
            blink.Blink();
        }

    }

    public override MachineStateManager.MStates ToStateTransition()
    {
        if (!_controller.GetBrakeValue())
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
        // Charge drift boost meter
        if (chargeMeter <= 1f)
            chargeMeter += stats.chargeRate * Time.deltaTime;
    }

    public void ApplyBoost()
    {
        if (chargeMeter > 0.4)
        {
            Vector3 direction = rb.transform.forward;
            speed.CurrentSpeed += (stats.boostForce * chargeMeter);
        }
        chargeMeter = 0.0f;
    }
}