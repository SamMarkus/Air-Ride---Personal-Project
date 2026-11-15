using UnityEngine;

public class IdleMachineState : BaseMachineState
{
    public IdleMachineState(MachineStateManager.MStates key,
        MachineController controller) : base(key, controller)
    {
    }

    public override void EnterState()
    {
        //noop
    }

    public override void ExitState()
    {
        //noop
    }

    public override void FixedUpdateState()
    {
        //noop
    }

    public override MachineStateManager.MStates ToStateTransition()
    {
        return MachineStateManager.MStates.Drive;
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
