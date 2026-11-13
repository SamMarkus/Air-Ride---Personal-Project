using Unity.VisualScripting;
using UnityEngine;

public class Speed
{
    private float _currentSpeed;
    public float CurrentSpeed
    {
        get { return _currentSpeed; }
        set { _currentSpeed = value; }

    }
    private float _topSpeed;
    public float TopSpeed
    {
        get { return _topSpeed; }
        set { _topSpeed = value; }
    }
    private float _acceleration;
    public float Acceleration
    {
        get { return _acceleration; }
        set { _acceleration = value; }
    }
    private float _deceleration;
    public float Deceleration
    {
        get { return _deceleration; }
        set { _deceleration = value; }
    }
    public Speed(float topSpeed = 0f, float acceleration = 0f,
        float decelerate = 0f) 
    {
        _topSpeed = topSpeed + 1;
        _acceleration = acceleration;
        _deceleration = decelerate;
    }

    public void UpdateSpeed()
    {
        if (_currentSpeed < _topSpeed)
            Accelerate();
        if (_currentSpeed > _topSpeed)
            Decelerate();
    }

    public void Accelerate()
    {
        _currentSpeed = Mathf.Lerp(_currentSpeed, _topSpeed, 
            (_acceleration * Time.fixedDeltaTime));
    }

    public void Decelerate()
    {
        _currentSpeed = Mathf.Lerp(_currentSpeed, 0.0f,
            (_deceleration * Time.fixedDeltaTime));
    }


}
