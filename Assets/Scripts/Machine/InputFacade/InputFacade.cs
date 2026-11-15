using Assets.Scripts.Machine.InputFacade;
using UnityEngine;
using UnityEngine.InputSystem;
namespace Assets.Scripts.Machine.InputFacade
{
    public class MachineInputFacade : MonoBehaviour
    {
        public static MachineInputFacade Instance { get; private set; }

        [Header("Inputs")]
        public InputAction MoveAction;
        public InputAction BrakeAction;
        public Vector2 MoveValue
        {
            get { return MoveAction.ReadValue<Vector2>(); }
        }
        public bool BrakeValue
        {
            get { return BrakeAction.IsPressed(); }
        }


        [Header("Components")]
        public Rigidbody rigidBody;
        private MachineInputTurn _turn;

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
            _turn = new MachineInputTurn();
        }

        private void FixedUpdate()
        {
            _turn.MachineTurn(rigidBody, MoveValue.x, 100f);
        }
    }
}
