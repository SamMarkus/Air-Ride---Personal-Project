using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    // Vehicle Stats (editable in inspector)
    [Header("Speed")]
    public float topSpeed = 20.0f;
    public float currentSpeed = 0f;
    private float forceMultiplier = 10f;

    [Header("Acceleration")]
    public float boostAcceleration = 1f;

    [Header("Braking")]
    public float brakeRate = 1f;

    [Header("Turn")]
    public float turnSpeed = 100.0f;
    public float groundDrag = 10f;
    public float airDrag = 10f;

    [Header("Flight")]
    public float floatiness = 0f; // 0 = affected by gravity

    // Components
    private Rigidbody rb;

    // Input system (x=left/right turn, y=up/down tilt) 
    public InputAction MoveAction;
    public InputAction BrakeAction; 
    private Vector2 inputValue;

    // Gravity
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;


    void OnEnable()
    {
        MoveAction.Enable();
        BrakeAction.Enable(); 
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Read 2D vector from MoveAction
        inputValue = MoveAction.ReadValue<Vector2>();

        // Brake if BrakeAction is pressed
        if (BrakeAction.IsPressed())
        {
            MachineBraking();
        }
        else if (currentSpeed < topSpeed) // Accelerate otherwise
        {
            MachineAcceleration();
        }

        MachineMovement();
        MachineTurning();
    }

    private void MachineMovement()
    {
        // Move the vehicle automatically. Air Ride machines do not stop.

        if (IsGrounded()) // Ground control
        {
            Vector3 groundMovement = transform.forward * currentSpeed;
            rb.AddForce(groundMovement * forceMultiplier, ForceMode.Acceleration);
            rb.AddForce(-rb.linearVelocity * groundDrag); // Drag force
        }
        else // Flight control
        {
            Vector3 airMovement = transform.forward * currentSpeed;
            rb.AddForce(airMovement * forceMultiplier, ForceMode.Acceleration);
            rb.AddForce(new Vector3(
                -rb.linearVelocity.x * airDrag,
                -rb.linearVelocity.y * floatiness,
                -rb.linearVelocity.z * airDrag));
        }

        Debug.Log(Vector3.Magnitude(rb.linearVelocity));
    }

    private void MachineTurning()
    {
        // Turn vehicle around Y axis using X input. 
        Quaternion deltaRotation = Quaternion.Euler(inputValue.x *
            Time.fixedDeltaTime * turnSpeed * transform.up);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }

    private void MachineBraking()
    {
        if (IsGrounded())
        {
            // Braking deacceleration
            if (currentSpeed > 0f)
                currentSpeed -= brakeRate * Time.fixedDeltaTime;
            else
                currentSpeed = 0; // Prevent speed from going negative

            // Blink effect
            if (gameObject.TryGetComponent<ModelBlink>(out var blink))
            {
                blink.Blink();
            }
        }
        else
        {
            // Plummet downwards
            currentSpeed = 0f;
            float plummetSpeed = 50f; 
            Vector3 plummet = transform.up * -plummetSpeed;
            rb.linearVelocity = plummet;
        }
    }

    private void MachineAcceleration()
    {
        currentSpeed += boostAcceleration * Time.fixedDeltaTime;
    }

    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundDistance, 
            groundMask);
    }
}
