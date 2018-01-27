using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameData data;
    private int NPCCount;
    public GameObject player;
    private float npcX;
    private float npcZ;
    int seconds;

    // Use this for initialization
    void Start () {
        data.clock = new Stopwatch();
        //load map
        //create npcs/update npc count
        //create first pickup in visible area (probably same area each time)
        updateNPCCount(1);
        data.clock.Start();
    }
	
	// Update is called once per frame
	void Update () {
        if (seconds > 299)
        {
            endGame();
        }
        else
        {
            seconds = (int)data.clock.ElapsedMilliseconds / 1000;
            updateArrow();
            updatePoopSize();
        }
    }

    private void endGame()
    {
        //player.control = false;
        data.clock.Stop();
        //tell ui to pop up game over screen
    }

    //UPDATE UI

    //updates arrow direction
    void updateArrow()
    {
        float temp = (npcZ - player.transform.position.z) / (npcX - player.transform.position.x);
        float angle = 1/Mathf.Tan(temp);
        data.angle = angle;
    }

    void updatePoopSize()
    {
        data.poopSize = 1 + data.poopSize;
        if (data.poopSize > 99)
        {
            //drop nuke
            data.poopSize = 0;
        }
    }

    void dropPoop()
    {
        data.poopSize = 0;
    }

    //NPCS

    //creates a pickup NPC
    void createPickUp()
    {
        //temp = specialNormies(bool pickup);
        //npcX = temp.transform.position.x;
        //npcZ = temp.transform.position.z;
    }

    //creates a dropoff NPC
    void createDropOff()
    {
        //temp = specialNormies(bool pickup);
        //npcX = temp.transform.position.x;
        //npcZ = temp.transform.position.z;
        //updates npc count;
    }

    //creates normies
    void createNPC(int amount)
    {
        //call NPC code to spawn npcs
        updateNPCCount(amount);
    }

    void updateNPCCount(int increment)
    {
        NPCCount += increment;
    }

    //COLLISIONS//


    //handles pickup collision
    void collidedPickUp()
    {
        data.score += 50;
        //remove pickup npc
        data.letterCount = true;
        //create a drop off npc

    }

    //handles dropoff collision
    void collidedDropOff()
    {
        data.score += 50;
        //remove dropoff npc
        data.letterCount = false;
        //create a pickup npc
    }

    void despawnNPC()
    {
        NPCCount--;
    }

    void collidePoopNPC(int droppedPoo)
    {
        data.score += -droppedPoo * 10;
        data.pooHits++;
    }
}
