using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePopup : MonoBehaviour
{
    [SerializeField] private Button _buttonContinue;
    [SerializeField] private Button _buttonExit;

    private void Start()
    {
        _buttonContinue.onClick.AddListener(OnContinue);
        _buttonExit.onClick.AddListener(OnExit);
    }

    private void OnContinue()
    {
        EventSystem.Broadcast(ContentEventType.Resume);
        Destroy(gameObject);
    }

    private void OnExit()
    {
        Application.Quit();
    }
}
