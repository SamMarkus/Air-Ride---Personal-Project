using UnityEngine;
[RequireComponent(typeof(MachineController))]

[RequireComponent(typeof(MachineMovement))]
[RequireComponent(typeof(Rigidbody))]
public class MachineBrake : MonoBehaviour
{
    private MachineController controller;
    private MachineMovement movement;

    public float brakeDecayRate = 1f;
    public float brakeTurnDecayRate = 0.5f;

    void Start()
    {
        controller = GetComponent<MachineController>();
        movement = GetComponent<MachineMovement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Brake if BrakeAction is pressed
        if (controller.GetBrakeValue())
        {
            if (controller.GetInputValue().x != 0)
                controller.Speed.Deceleration = brakeTurnDecayRate;
            else
                controller.Speed.Deceleration = brakeDecayRate;
            Brake();
        }
        else
        {
            controller.Speed.Deceleration = controller.brakeRate;
        }
    }

    private void Brake()
    {
        if (controller.IsGrounded())
        {
            // Braking deacceleration
            movement.Decelerate();

            // Blink effect
            if (gameObject.TryGetComponent<ModelBlink>(out var blink))
            {
                blink.Blink();
            }
        }
        else
        {
            // Plummet downwards
            controller.Speed.CurrentSpeed = 0f;
            float plummetSpeed = 50f;
            Vector3 plummet = transform.up * -plummetSpeed;
            controller.Rigidbody.linearVelocity = plummet;
        }
    }

}
