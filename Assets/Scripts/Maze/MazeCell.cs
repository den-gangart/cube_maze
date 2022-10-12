using UnityEngine;

public class MazeCell
{
    public const int DIRECTION_COUNT = 4;
    public const int WALL_LIMIT = 3;
    private static int HORIZONTAL_ROTATION_ANGLE = 0;
    private static int VERTICAL_ROTATION_ANGLE = 90;
    public Vector2 Position { get; private set; }

    private static Vector2Int[] _directionsAmount =
    {
        Vector2Int.left,
        Vector2Int.right,
        Vector2Int.up,
        Vector2Int.down,
    };

    private bool[] walls;
    public MazeCell(Vector2 position, float wallSpawnchance)
    {
        Position = position;
        FillWalls(wallSpawnchance);
    }

    private void FillWalls(float wallSpawnchance)
    {
        walls = new bool[DIRECTION_COUNT];
        for (int i = 0, wallCount = 0; i < walls.Length; i++)
        {
            if (wallCount == WALL_LIMIT) break;

            walls[i] = Random.value <= wallSpawnchance;
        }
    }

    public bool CheckWallExist(Direction direction)
    {
        return walls[(int)direction];
    }

    public void AddWall(Direction direction)
    {
        walls[(int)direction] = true;
    }

    public static Vector2Int DirectionToVector(Direction direction)
    {
        return _directionsAmount[(int)direction];
    }

    public static Quaternion RotationFromDirection(Direction direction)
    {
        float rotationAngle = isVerticalDirection(direction) ? HORIZONTAL_ROTATION_ANGLE : VERTICAL_ROTATION_ANGLE;
        return Quaternion.Euler(0, rotationAngle, 0);
    }

    public static bool isVerticalDirection(Direction direction)
    {
        return direction >= Direction.Up;
    }
}