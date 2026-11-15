using UnityEngine;

[RequireComponent(typeof(MachineController))]
public class MachineBoost : MonoBehaviour
{
    private MachineController controller;
    private BaseStats stats;
    private bool charging;

    //public float boostForce = 30f;
    public float chargeMeter = 0.0f;
    //public float chargeRate = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<MachineController>();
        stats = controller.BaseStats;
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.GetBrakeValue())
        {
            charging = true;
            if (chargeMeter <= 1f)
                chargeMeter += stats.chargeRate * Time.deltaTime;
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
            controller.Speed.CurrentSpeed += (stats.boostForce * chargeMeter);
        }
        charging = false;
        chargeMeter = 0.0f;
    } 
}
