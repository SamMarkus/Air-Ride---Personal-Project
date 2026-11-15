using Unity;
using UnityEngine;

public class MachineTurning : MonoBehaviour
{
    private MachineController controller;
    private Rigidbody rb;

    //public float brakeTurnMultiplier = 1f; 

    private void Start()
    {
        controller = GetComponent<MachineController>();
        rb = controller.Rigidbody;
    }

    private void FixedUpdate()
    {
        Turning();
    }

    private void Turning()
    {
        // Turn vehicle around Y axis using X input. 
        float turn = controller.BaseStats.turnSpeed;
        if (controller.GetBrakeValue())
            turn *= controller.BaseStats.brakeTurnMultiplier;

        float xValue = controller.GetInputValue().x;
        Quaternion deltaRotation = Quaternion.Euler(xValue *
            Time.fixedDeltaTime * turn * transform.up);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}