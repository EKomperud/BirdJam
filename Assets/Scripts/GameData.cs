using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

[CreateAssetMenu(fileName = "GameData")]
public class GameData : ScriptableObject {

    public int poopSize { get; private set; }
    public int score { get; private set; }
    public Timer clock { get; private set; }
    public int npcCount { get; private set; }

    //Poop

    public void updatePoop(int size)
    {
        poopSize = size;
    }

    //change score
    public void updateScore(int increment)
    {
        score += increment;
    }

    //Time controls
    public void createTimer()
    {
        clock = new Timer();
    }

    public void startTimer()
    {
        clock.Start();
    }

    public void stopTimer()
    {
        clock.Stop();
    }

    //NPC data
    public void updateNPCCount(int increment)
    {
        npcCount += increment;
    }
}
