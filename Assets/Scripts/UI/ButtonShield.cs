using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
[RequireComponent(typeof(Animator))]
public class ButtonShield : MonoBehaviour
{
    private Animator _animator;
    private float _resetTime = 2f;
    private bool _isActive = false;
    private IEnumerator _resetButtonRoutine;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnShieldPressed()
    {
        ChangeButtonActive(true);
        _resetButtonRoutine = OnResetButtonRoutine();
        StartCoroutine(_resetButtonRoutine);
    }

    public void OnShieldUnpressed()
    {
        if(_isActive)
        {
            StopCoroutine(_resetButtonRoutine);
        }

        ChangeButtonActive(false);
    }

    private IEnumerator OnResetButtonRoutine()
    {
        yield return new WaitForSeconds(_resetTime);
        ChangeButtonActive(false);
    }

    private void ChangeButtonActive(bool _isActiveNow)
    {
        if(_isActiveNow  && !_isActive)
        {
            EventSystem.Broadcast(ContentEventType.ShieldActivated);
            SaveState(_isActiveNow);
        }
        else if(!_isActiveNow && _isActive)
        {
            EventSystem.Broadcast(ContentEventType.ShieldDeactivated);
            SaveState(_isActiveNow);
        }
    }

    private void SaveState(bool _isActiveNow)
    {
        _animator.SetBool("Pressed", _isActiveNow);
        _isActive = _isActiveNow;
    }
}
