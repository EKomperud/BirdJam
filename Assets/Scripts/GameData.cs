using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

[CreateAssetMenu(fileName = "GameData")]
public class GameData : ScriptableObject {

    public float poopSize;
    public float minPoopSize;
    public float maxPoopSize;
    public int score;
    public Quaternion angle;
    public Transform target;
    public float defaultCamDistance;
    public float maxCamDistance;
    public float camAcceleration;
    public Stopwatch clock;
    public int pooHits;
    public bool letterCount;
    public List<GameObject> spawnPoints;

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

    //public void updateAngle(float newAngle)
    //{
    //    angle = newAngle;
    //}

    public void updateLetterCount(bool have)
    {
        letterCount = have;
    }

    //Time controls
    public void createTimer()
    {
        clock = new Stopwatch();
    }

    public void startTimer()
    {
        clock.Start();
    }

    public void stopTimer()
    {
        clock.Stop();
    }
}
