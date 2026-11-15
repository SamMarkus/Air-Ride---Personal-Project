using UnityEngine;

[CreateAssetMenu(fileName = "BaseStats", menuName = "Stats/BaseStats")]
public class BaseStats : ScriptableObject
{
    [Header("Speed")]
    public float topSpeed = 30;

    [Header("Accerlation")]
    public float boostAcceleration = 1f;

    [Header("Brake")]
    public float brakeRate = 0.5f;
    public float turnBrakeRate = 0.2f;
    public bool updateForceWhenBraking = true;

    [Header("Turn")]
    public float turnSpeed = 50f;
    public float brakeTurnMultiplier = 2f;

    [Header("Charge")]
    public float boostForce = 20f;
    public float chargeRate = 0.5f;

    [Header("Drag")]
    public float groundDrag = 10f;
    public float airDrag = 10f;

    [Header("Flight")]
    public float floatiness = 0f;
}
