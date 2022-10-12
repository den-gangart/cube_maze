using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonPause : MonoBehaviour
{
    [SerializeField] GameObject _pausePopup;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        EventSystem.AddEventListener(ContentEventType.Resume, OnResume);
    }

    private void OnDisable()
    {
        EventSystem.RemoveEventListener(ContentEventType.Resume, OnResume);
    }

    public void OnPauseClicked()
    {
        EventSystem.Broadcast(ContentEventType.Pause);
        Instantiate(_pausePopup);
        _button.interactable = false;
    }

    private void OnResume()
    {
        _button.interactable = true;
    }
}
