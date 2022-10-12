using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private float _delayAfterWin;
    [SerializeField] private float _delayAfterLoose;
    [SerializeField] private GameObject _fadeScreen;
    [SerializeField] private GameObject _pausePopup;

    private float _fadeTime = 1.5f;
    private const float TIME_SCALE_ON_PAUSE = 0;
    private const float REGULAR_TIME_SCALE = 1;

    private void Start()
    {
        EventSystem.Broadcast(ContentEventType.LevelInitilize);
        base.OnAwake();
    }

    private void OnEnable()
    {
        EventSystem.AddEventListener(ContentEventType.Win, OnWin);
        EventSystem.AddEventListener(ContentEventType.Lose, OnLose);
        EventSystem.AddEventListener(ContentEventType.Pause, OnPause);
        EventSystem.AddEventListener(ContentEventType.Resume, OnResume);
    }

    private void OnDisable()
    {
        EventSystem.RemoveEventListener(ContentEventType.Win, OnWin);
        EventSystem.RemoveEventListener(ContentEventType.Lose, OnLose);
        EventSystem.RemoveEventListener(ContentEventType.Pause, OnPause);
        EventSystem.RemoveEventListener(ContentEventType.Resume, OnResume);
    }

    private void OnWin()
    {
        StartCoroutine(WinRoutine());
    }

    private IEnumerator WinRoutine()
    {
        yield return new WaitForSeconds(_delayAfterWin);
        Animator fadeAnimator = Instantiate(_fadeScreen, transform).GetComponent<Animator>();
        fadeAnimator.Play("Fade In");
        yield return new WaitForSeconds(_fadeTime);
        fadeAnimator.Play("Fade Out");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        yield return new WaitForSeconds(_fadeTime);
        Destroy(fadeAnimator.gameObject);
    }

    private void OnLose()
    {
        StartCoroutine(LoseRoutine());
    }

    private IEnumerator LoseRoutine()
    {
        yield return new WaitForSeconds(_delayAfterLoose);
        EventSystem.Broadcast(ContentEventType.Restart);
    }

    private void OnPause()
    {
        Time.timeScale = TIME_SCALE_ON_PAUSE;
    }

    private void OnResume()
    {
        Time.timeScale = REGULAR_TIME_SCALE;
    }
}
