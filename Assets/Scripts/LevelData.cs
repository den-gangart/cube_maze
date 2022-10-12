using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Maze/Level Data")]
public class LevelData : ScriptableObject
{
    public GameObject player;
    public GameObject finishZone;
    public GameObject wall;
    public GameObject losePoint;
}
