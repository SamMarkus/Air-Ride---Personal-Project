using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerController : MonoBehaviour
{
    // Vehicle Stats (editable in inspector)
    [Header("Speed")]
    public float topSpeed = 20.0f;
    public float currentSpeed = 0f;

    [Header("Acceleration")]
    public float boostAcceleration = 1f;

    [Header("Braking")]
    public float brakeRate = 1f;

    [Header("Turn")]
    public float turnSpeed = 100.0f;

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

        Debug.Log(IsGrounded());
    }

    private void MachineMovement()
    {
        // Move the vehicle automatically. Air Ride machines do not stop. 
        Vector3 movement = transform.forward * currentSpeed;
        rb.linearVelocity = new Vector3(
                movement.x,
                rb.linearVelocity.y,
                movement.z
            );
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
