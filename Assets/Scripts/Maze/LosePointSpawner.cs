using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LosePointSpawner : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float _spawnChance;
    [SerializeField] private Transform _pointsParent;
    [SerializeField] private LevelData _levelData;

    public void CreatePoints(List<MazeCell> cells)
    {
        foreach (var cell in cells)
        {
            if(Random.value < _spawnChance)
            {
                Instantiate(_levelData.losePoint, new Vector3(cell.Position.x, 0, cell.Position.y), Quaternion.identity, _pointsParent);
            }
        }
    }
}
