public abstract class BaseMachineState : BaseState<MachineStateManager.MStates>
{
    protected MachineController _controller;

    public BaseMachineState(MachineStateManager.MStates key,
        MachineController controller) : base(key)
    {
        _controller = controller;
    }

    public override MachineStateManager.MStates GetNextState()
    {
        // Any transitions
        // EXAMPLE: if (health == 0) then die



        // If no transitions, check To transitions
        return ToStateTransition();
    }

    public virtual MachineStateManager.MStates ToStateTransition()
    {
        // calling base.ToStateTransition() will return StateKey, use
        // that when you need to return the state by itself
        return StateKey;
    }
}
