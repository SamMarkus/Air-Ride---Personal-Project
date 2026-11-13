using UnityEngine;
using UnityEngine.InputSystem;

/**
 * @brief Machine Controller component that handles all stats and input
 *        tied to the machine. Machine components borrow from this script
 */

[RequireComponent(typeof(Rigidbody))]
public class MachineController : MonoBehaviour
{
    // Data Values 
    [Header("Speed")]
    private Speed speed;
    public Speed Speed { get { return speed; } }
    public float topSpeed = 20.0f;

    [Header("Acceleration")]
    public float boostAcceleration = 1f;
    public float brakeRate = 1f;

    [Header("Turn")]
    public float turnSpeed = 100.0f;

    [Header("Drag")]
    public float groundDrag = 10f;
    public float airDrag = 10f;

    [Header("Flight")]
    public float floatiness = 0f; // 0 = affected by gravity

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

    [Header("State Machine")]
    private StateMachine stateMachine; 

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

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        speed = new Speed(topSpeed, boostAcceleration, brakeRate);

        /*
        // State Machine
        stateMachine = new StateMachine();

        var groundState = new MachineGroundedState(this);
        var flightState = new MachineFlightState(this);
        var brakeState = new MachineBrakeState(this);

        // Transitions
        At(groundState, flightState, new FuncPredicate(() => !IsGrounded()));
        At(flightState, groundState, new FuncPredicate(() => IsGrounded()));
        At(groundState, brakeState, new FuncPredicate(() => GetBrakeValue()));
        At(brakeState, groundState, new FuncPredicate(() => !GetBrakeValue()));


        stateMachine.SetState(groundState);
        */
    }
    private void Update()
    {
        moveValue = MoveAction.ReadValue<Vector2>();
        brakeValue = BrakeAction.IsPressed();

        //stateMachine.Update();

    }

    private void FixedUpdate()
    {
        //stateMachine.FixedUpdate();
    }

    // Get Input values
    public Vector2 GetInputValue() { return moveValue; }
    public bool GetBrakeValue() { return brakeValue; }

    // State Machine helper
    void At(IState from, IState to, IPredicate condition)
        => stateMachine.AddTransition(from, to, condition);
    void Any(IState to, IPredicate condition) => 
        stateMachine.AddAnyTransition(to, condition);

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
