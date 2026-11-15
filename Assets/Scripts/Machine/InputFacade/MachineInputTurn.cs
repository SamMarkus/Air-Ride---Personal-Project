using UnityEngine;

namespace Assets.Scripts.Machine.InputFacade
{     
    public class MachineInputTurn
    {
        public void MachineTurn(Rigidbody rb, float inputValue, float turnSpeed)
        {
            // Note: Include brake turning

            // Turn vehicle around Y axis using X input. 
            Quaternion deltaRotation = Quaternion.Euler(inputValue *
                Time.fixedDeltaTime * turnSpeed * rb.transform.up);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }

    }
}