using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

[CreateAssetMenu(fileName = "GameData")]
public class GameData : ScriptableObject {

    public float poopSize { get; set; }
    public int score { get; set; }
    public float angle { get; set; }
    //public Stopwatch clock { get; set; }

    public bool letterCount { get; private set; }

    //Poop
    public void updatePoop(float size)
    {
        poopSize = size;
    }

    //UI stuff

    public void updateScore(int increment)
    {
        score += increment;
    }

    public void updateAngle(float newAngle)
    {
        angle = newAngle;
    }

    public void updateLetterCount(bool have)
    {
        letterCount = have;
    }

    //Time controls
    //public void createTimer()
    //{
    //    clock = new Stopwatch();
    //}

    //public void startTimer()
    //{
    //    clock.Start();
    //}

    //public void stopTimer()
    //{
    //    clock.Stop();
    //}
}
