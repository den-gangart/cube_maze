using Unity.AI.Navigation;
using UnityEngine;

[RequireComponent(typeof(Maze))]
[RequireComponent(typeof(LosePointSpawner))]
public class GameField : MonoBehaviour
{
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _finishPoint;
    [SerializeField] private LevelData _levelData;
    [SerializeField] private NavMeshSurface _navMeshSurface;

    private Maze _maze;
    private LosePointSpawner _losePointSpawner;
    private GameObject _activePlayer;

    private void Awake()
    {
        _maze = GetComponent<Maze>();
        _losePointSpawner = GetComponent<LosePointSpawner>();
    }

    private void Start()
    {
        CreateField();
        CreateLosePoints();
        UpdateNavMesh();
        SpawnPlayer();
        SpawnFinishZone();
    }

    private void OnEnable()
    {
        EventSystem.AddEventListener(ContentEventType.Restart, OnRestart);
    }

    private void OnDisable()
    {
        EventSystem.RemoveEventListener(ContentEventType.Restart, OnRestart);
    }

    private void OnRestart()
    {
        SpawnPlayer();
    }

    private void UpdateNavMesh()
    {
        _navMeshSurface.BuildNavMesh();
    }

    private void SpawnPlayer()
    {
        if(_activePlayer != null)
        {
            Destroy(_activePlayer);
        }

        _activePlayer = Instantiate(_levelData.player, _startPoint.position, Quaternion.identity);
        _activePlayer.GetComponent<PlayerMovement>().SetTarget(_finishPoint.position);
    }

    private void SpawnFinishZone()
    {
        Instantiate(_levelData.finishZone, _finishPoint.position, Quaternion.identity);
    }

    private void CreateField()
    {
        _maze.GenerateMaze();
    }

    private void CreateLosePoints()
    {
        _losePointSpawner.CreatePoints(_maze.GetCells());
    }
}