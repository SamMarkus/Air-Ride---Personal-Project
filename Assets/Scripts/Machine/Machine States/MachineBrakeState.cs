using UnityEngine;

public class MachineBrakeState : MachineBaseState
{

    public MachineBrakeState(MachineController controller) : base(controller) { }

    public override void FixedUpdate()
    {
        Brake();
    }

    public override void OnEnter()
    {
    }

    public override void OnExit()
    {
    }

    public override void Update()
    {
    }

    private void Brake()
    {
        // Braking deacceleration
        controller.Speed.Decelerate();

        // Blink effect
        if (controller.gameObject.TryGetComponent<ModelBlink>(out var blink))
        {
            blink.Blink();
        }
    }
}