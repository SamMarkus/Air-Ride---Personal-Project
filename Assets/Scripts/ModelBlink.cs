using UnityEngine;
using UnityEditor;
using System.Runtime.CompilerServices;

public class ModelBlink : MonoBehaviour
{
    [SerializeField]
    private float blinkDecaySpeed = 1f;

    private Renderer[] _renderers;
    private MaterialPropertyBlock _propertyBlock;
    private float _blinkFactor;

    private void Start()
    {
        _renderers = GetComponentsInChildren<Renderer>();
        _propertyBlock = new MaterialPropertyBlock();
    }

    private void Update()
    {
        if (_blinkFactor <= 0f)
        {
            return;
        }

        _blinkFactor = Mathf.Lerp(_blinkFactor, 0f, Time.deltaTime * blinkDecaySpeed);
        if (_blinkFactor < 0.01f)
        {
            _blinkFactor = 0f;
        }

        ApplyBlinkFactor();
    }

    public void Blink()
    {
        _blinkFactor = 1f;
        ApplyBlinkFactor();
    }

    private void ApplyBlinkFactor()
    {
        foreach (var renderer in _renderers)
        {
            renderer.GetPropertyBlock(_propertyBlock);
            _propertyBlock.SetFloat(name: "_BlinkFactor", _blinkFactor);
            renderer.SetPropertyBlock(_propertyBlock);
        }
    }

}
