using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameData data;
    private int NPCCount;
    public GameObject player;
    private float npcX;
    private float npcZ;
    int seconds;
    int droppedPoo;

    // Use this for initialization
    void Start () {
        data.createTimer();
        //load map
        //create npcs/update npc count
        //create first pickup in visible area (probably same area each time)
        data.startTimer();
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
        data.stopTimer();
        //tell ui to pop up game over screen
    }

    //UPDATE UI

    //updates arrow direction
    void updateArrow()
    {
        float temp = (npcZ - player.transform.position.z) / (npcX - player.transform.position.x);
        float angle = 1/Mathf.Tan(temp);
        data.updateAngle(angle);
    }

    void updatePoopSize()
    {

        //get delta from timer
        //increase poop based of that
        //data.updatePoop(delta + decrement)
        //if poop size is certain size, drop automatically
        if (data.poopSize > 99)
        {
            //drop nuke
            droppedPoo = data.poopSize;
            data.updatePoop(0);
        }
    }

    void dropPoop()
    {
        data.updatePoop(0);
    }

    //NPCS

    //creates a pickup NPC
    void createPickUp()
    {
        //temp = specialNormies(bool pickup);
        //npcX = temp.transform.position.x;
        //npcZ = temp.transform.position.z;
        updateNPCCount(1);
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
        //change score
        //remove pickup npc
        //add a letter to player
        //create a drop off npc

    }

    //handles dropoff collision
    void collidedDropOff()
    {
        //change score
        //remove dropoff npc
        //remove letter from player
        //create a pickup npc
    }

    void despawnNPC()
    {
        NPCCount--;
    }

    void collidePoopNPC()
    {
        data.updateScore(-droppedPoo * 10);
    }
}
