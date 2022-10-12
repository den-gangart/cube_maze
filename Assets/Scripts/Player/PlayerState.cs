using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(MeshRenderer))]
public class PlayerState : MonoBehaviour
{
    [SerializeField] private GameObject _winParticles;
    [SerializeField] private GameObject _loseParticles;
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private Material _shieldMaterial;

    private NavMeshAgent _navMeshAgent;
    private MeshRenderer _meshRenderer;
    private bool _canLose = true;
    private bool _isAlive = true;

    private void OnEnable()
    {
        EventSystem.AddEventListener(ContentEventType.ShieldActivated, OnShieldActivated);
        EventSystem.AddEventListener(ContentEventType.ShieldDeactivated, OnShieldDeactivated);
    }

    private void OnDisable()
    {
        EventSystem.RemoveEventListener(ContentEventType.ShieldActivated, OnShieldActivated);
        EventSystem.RemoveEventListener(ContentEventType.ShieldDeactivated, OnShieldDeactivated);
    }

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void Kill()
    {
        if(_canLose == false || _isAlive == false)
        {
            return;
        }

        _navMeshAgent.isStopped = true;
        _meshRenderer.enabled = false;
        _loseParticles.SetActive(true);
        _isAlive = false;
        EventSystem.Broadcast(ContentEventType.Lose);
    }

    public void Win()
    {
        _winParticles.SetActive(true);
        EventSystem.Broadcast(ContentEventType.Win);
    }

    private void OnShieldActivated()
    {
        _canLose = false;
        SwitchMaterial();
    }

    private void OnShieldDeactivated()
    {
        _canLose = true;
        SwitchMaterial();
    }

    private void SwitchMaterial()
    {
        if(_meshRenderer.enabled == false)
        {
            return;
        }

        _meshRenderer.material = _canLose ? _defaultMaterial : _shieldMaterial;
    }
}
