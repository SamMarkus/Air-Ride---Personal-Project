using System.Runtime.CompilerServices;
using Unity;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[RequireComponent(typeof(MachineController))]
public class MachineTurning : MonoBehaviour
{
    public GameObject playerModel; 
    public MachineController controller;
    private Rigidbody rb;

    [Header("Tilt Settings")]
    public float tiltSpeed = 10f;
    public float maxTilt = 50f;
    public float tiltDeadzone = 0.5f;
    private float r;

    //public float brakeTurnMultiplier = 1f; 

    private void Start()
    {
        controller = GetComponent<MachineController>();
        rb = controller.Rigidbody;
    }

    private void FixedUpdate()
    {
        Turning();
        Tilting();
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

    private void Tilting()
    {

        float yValue = controller.GetInputValue().y;
        float Angle;
        if (yValue > tiltDeadzone || yValue < -tiltDeadzone)
        {
            Angle = Mathf.SmoothDampAngle(
                playerModel.transform.localEulerAngles.x,
                maxTilt * yValue,
                ref r,
                tiltSpeed);
        }
        else
        {
            Angle = Mathf.SmoothDampAngle(
                playerModel.transform.localEulerAngles.x,
                0f,
                ref r,
                tiltSpeed);
        }

        playerModel.transform.localEulerAngles = Vector3.right * Angle;
    }
    
}