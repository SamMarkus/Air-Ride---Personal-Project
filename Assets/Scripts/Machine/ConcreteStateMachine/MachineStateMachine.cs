public class MachineStateManager : StateManager<MachineStateManager.MStates>
{
    public enum MStates
    {
        Idle,
        Drive,
        Fly,
        Brake,
        Spin,
    }

    public MachineController controller;

    void Start()
    {
        States.Add(MStates.Idle, new IdleMachineState(MStates.Idle, controller));
        States.Add(MStates.Drive, new DriveMachineState(MStates.Drive, controller));
        States.Add(MStates.Fly, new FlyMachineState(MStates.Fly, controller));
        States.Add(MStates.Brake, new BrakeMachineState(MStates.Brake, controller));
        //States.Add(MStates.Spin, new SpinMachineState(MStates.Spin, controller));

        CurrentState = States[MStates.Idle];
    }
}
