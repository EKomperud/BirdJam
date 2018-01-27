using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

[CreateAssetMenu(fileName = "GameData")]
public class GameData : ScriptableObject {

    public int poopSize { get; set; }
    public int score { get; set; }
    public float angle { get; set; }
    public Stopwatch clock { get; set; }
    public int pooHits { get; set; }
    public bool letterCount { get; set; }
}
