using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

[CreateAssetMenu(fileName = "GameData")]
public class GameData : ScriptableObject {

    public int poopSize;
    public int score;
    public float angle;
    public Stopwatch clock;
    public int pooHits;
    public bool letterCount;
    public List<GameObject> spawnPoints;
}
