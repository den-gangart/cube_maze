using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ScaleInterpolating))]
public class Shield : MonoBehaviour
{
    [SerializeField] private Vector3 _maxScale;
    [SerializeField] private Vector3 _minScale;

    private ScaleInterpolating _scaleInterpolating;

    private void Awake()
    {
        _scaleInterpolating = GetComponent<ScaleInterpolating>();
    }

    public void Appear()
    {
        _scaleInterpolating.SetTargetScale(_maxScale);
        _scaleInterpolating.StartScailing();
    }

    public void Disappear()
    {
        _scaleInterpolating.SetTargetScale(_minScale);
        _scaleInterpolating.StartScailing();
    }
}
