public abstract class MachineBaseState : IState
{
    protected readonly MachineController controller;
    // protected readonly Animator animator; 

    protected MachineBaseState(MachineController controller)
    {
        this.controller = controller;
    }

    public virtual void FixedUpdate()
    {
        //noop
    }

    public virtual void OnEnter()
    {
        //noop
    }

    public virtual void OnExit()
    {
        //noop
    }

    public virtual void Update()
    {
        //noop
    }
}
