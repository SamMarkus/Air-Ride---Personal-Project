using UnityEngine;
using UnityEngine.InputSystem;

/**
 * @brief Machine Controller component that handles all stats and input
 *        tied to the machine. Machine components borrow from this script
 */

[RequireComponent(typeof(Rigidbody))]
public class MachineController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] 
    private BaseStats baseStats;
    public BaseStats BaseStats { get { return baseStats; } }
    private Speed speed;
    public Speed Speed { get { return speed; } }

    public bool logSpeedometer;

    [Header("Gravity")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    // Input system (x=left/right turn, y=up/down tilt) 
    [Header("Input")]
    public InputAction MoveAction;
    public InputAction BrakeAction;
    private Vector2 moveValue;
    private bool brakeValue;

    [Header("Components")]
    private Rigidbody _rb; 
    public Rigidbody Rigidbody
    {
        get { return _rb; }
    }

    private void OnEnable()
    {
        MoveAction.Enable();
        BrakeAction.Enable(); 
    }
    private void OnDisable()
    {
        MoveAction.Disable();
        BrakeAction.Disable(); 
    }

    // Awake is called so necessary objects are referenced or made
    // before anything else
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        speed = new Speed(BaseStats.topSpeed,
            BaseStats.boostAcceleration,
            BaseStats.brakeRate);
    }
    private void Update()
    {
        moveValue = MoveAction.ReadValue<Vector2>();
        brakeValue = BrakeAction.IsPressed();

        if (logSpeedometer)
            DisplaySpeedometer();
    }

    // Get Input values
    public Vector2 GetInputValue() { return moveValue; }
    public bool GetBrakeValue() { return brakeValue; }

    // Gravity
    public bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundDistance,
            groundMask);
    }

    // Speedometer
    public void DisplaySpeedometer()
    {
        int speedometer = (int)Vector3.Magnitude(Rigidbody.linearVelocity);
        Debug.Log(speedometer);
    }
}
