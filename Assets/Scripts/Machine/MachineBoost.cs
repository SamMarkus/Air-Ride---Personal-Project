using UnityEngine;

[RequireComponent(typeof(MachineController))]
public class MachineBoost : MonoBehaviour
{
    private MachineController controller;
    private bool charging;
    public float boostForce = 30f;
    public float chargeMeter = 0.0f;
    public float chargeRate = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<MachineController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.GetBrakeValue())
        {
            charging = true;
            if (chargeMeter <= 1f)
                chargeMeter += chargeRate * Time.deltaTime;
        }
        else
        {
            if (charging)
                ApplyBoost();
        }
    }

    public void ApplyBoost()
    {
        if (chargeMeter > 0.4)
        {
            Vector3 direction = transform.forward;
            controller.Speed.CurrentSpeed += (boostForce * chargeMeter);
        }
        charging = false;
        chargeMeter = 0.0f;
    } 
}
