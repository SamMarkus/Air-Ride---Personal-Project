public abstract class BaseState : IState
{
    //protected readonly  brain;
    // protected readonly Animator animator; 

    protected BaseState()
    {
        //this.brain = brain;
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
