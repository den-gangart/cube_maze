using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleInterpolating : MonoBehaviour
{
    [SerializeField] private float _transitionTime;

    private Vector3 _targetScale;
    private IEnumerator _lerpScaleRoutine;

    public void SetTargetScale(Vector3 targetScale)
    {
        _targetScale = targetScale;
    }

    public void StartScailing()
    {
        if (_lerpScaleRoutine != null)
        {
            StopCoroutine(_lerpScaleRoutine);
        }

        _lerpScaleRoutine = LerpScale();
        StartCoroutine(_lerpScaleRoutine);
    }

    private IEnumerator LerpScale()
    {
        float time = 0;
        Vector3 startScale = transform.localScale;

        while (time < _transitionTime)
        {
            transform.localScale = Vector3.Lerp(startScale, _targetScale, time / _transitionTime);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localScale = _targetScale;
    }
}
