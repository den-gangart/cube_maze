using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    [SerializeField] private Vector2Int _fieldSize;
    [SerializeField] private Vector2Int _playerZone;
    [SerializeField] private Vector2Int _finishZone;
    [SerializeField] private LevelData _levelData;
    [SerializeField] private Transform _obstacleParent;
    [SerializeField] private float _cellSize;
    [SerializeField] private float _borderOffset;
    [SerializeField, Range(0, 1)] private float _wallSpawnChance;

    private List<MazeCell> _mazeCells;
    private float _width;
    private float _height;

    private static int HALF_SIZE = 2;

    public void GenerateMaze()
    {
        CalculateSize();
        CreateMazeCells();
        SpawnWalls();
    }

    private void CalculateSize()
    {
        _width = _fieldSize.x * _cellSize;
        _height = _fieldSize.y * _cellSize;
    }

    private void CreateMazeCells()
    {
        _mazeCells = new List<MazeCell>();
        for (float x = 0; x < _width; x += _cellSize)
        {
            for (float y = 0; y < _height; y+= _cellSize)
            {
                if (x > _playerZone.x || y > _playerZone.y)
                {
                    _mazeCells.Add(new MazeCell(new Vector2(x - _width / HALF_SIZE, y - _height / HALF_SIZE), _wallSpawnChance));
                }
            }
        }
    }

    private void SpawnWalls()
    {
        PlaceBorderWalls();

        foreach (var cell in _mazeCells)
        {
            for (int i = 0; i < MazeCell.DIRECTION_COUNT; i++)
            {
                Direction direction = (Direction)i;
                if (cell.CheckWallExist(direction))
                {
                    PlaceWall(direction, cell);
                } 
            }
        }
    }

    private void PlaceWall(Direction direction, MazeCell cell)
    {
        Vector2Int positionOffset = MazeCell.DirectionToVector(direction);
        Vector3 position = new Vector3(cell.Position.x + (positionOffset.x * _cellSize / HALF_SIZE), 0, cell.Position.y + (positionOffset.y * _cellSize / HALF_SIZE));
        Quaternion rotation = MazeCell.RotationFromDirection(direction);
        Instantiate(_levelData.wall, position, rotation, _obstacleParent);
    }

    private void PlaceBorderWalls()
    {
        for(int i = 0; i < MazeCell.DIRECTION_COUNT; i++)
        {
            Direction direction = (Direction)i;
            Vector2 positionOffset = MazeCell.DirectionToVector(direction);
            positionOffset *= _borderOffset;

            Vector3 position = new Vector3((positionOffset.x * _width / HALF_SIZE), 0, (positionOffset.y * _height / HALF_SIZE));
            Quaternion rotation = MazeCell.RotationFromDirection(direction);

            GameObject wall = Instantiate(_levelData.wall, position, rotation, _obstacleParent);
            float wallScale = MazeCell.isVerticalDirection(direction) ? _width * _borderOffset : _height * _borderOffset;
            wall.transform.localScale = new Vector3(wallScale, wall.transform.localScale.y, wall.transform.localScale.z);
        }
    }

    public List<MazeCell> GetCells()
    {
        return _mazeCells;
    }
}
