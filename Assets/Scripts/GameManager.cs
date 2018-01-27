using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameData data;
    private int NPCCount;
    public GameObject player;


    // Use this for initialization
    void Start () {
        data.createTimer();
        //load map
        //create npcs/update npc count
        //create first pickup in visible area (probably same area each time)
    }
	
	// Update is called once per frame
	void Update () {
        //updateArrow(GameObject npc);
        //update poop size
    }

    //UPDATE UI

    //updates arrow direction
    void updateArrow(GameObject npc)
    {
        float temp = (npc.transform.position.z - player.transform.position.z) / (npc.transform.position.x - player.transform.position.x);
        float angle = 1/Mathf.Tan(temp);
        data.updateAngle(angle);
    }

    //DECREMENT IS ALWAYS NEGATIVE
    void updatePoopSize(int decrement)
    {
        //get delta from timer
        //increase poop based of that
        //data.updatePoop(delta + decrement)
        //if poop size is certain size, drop automatically
    }

    //NPCS

    //creates a pickup NPC
    void createPickUp()
    {
        //specialNormies(bool pickup);
        updateNPCCount(1);
    }

    //creates a dropoff NPC
    void createDropOff()
    {
        //specialNormies(bool pickup);
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

}
