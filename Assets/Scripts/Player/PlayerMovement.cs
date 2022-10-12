using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveDelay;
    private NavMeshAgent _navMeshAgent;
    private Vector3 _target;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        StartCoroutine(StartMoveRoutine());
    }

    private IEnumerator StartMoveRoutine()
    {
        yield return new WaitForSeconds(_moveDelay);

        if(_target != null)
        {
            _navMeshAgent.SetDestination(_target);
        }
    }

    public void SetTarget(Vector3 target)
    {
        _target = target;
    }
}
