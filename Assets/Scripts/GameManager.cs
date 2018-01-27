using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameData data;
    private int NPCCount;
    public GameObject player;
    private static GameManager instance = null;

    private float minPoopSize;

    private float npcX;
    private float npcZ;

    // Use this for initialization
    void Start () {
        //data.createTimer();

        if (instance != null && instance != this)
            Destroy(gameObject);

        instance = this;

        //load map
        //create npcs/update npc count
        //create first pickup in visible area (probably same area each time)
        data.startTimer();
    }
	
	// Update is called once per frame
	void Update () {
        updateArrow();
        int seconds = (int)data.clock.ElapsedMilliseconds / 1000;
        //update poop size
    }

    public static bool TryGetInstance(out GameManager gm)
    {
        gm = instance;
        if (instance == null)
            return false;
        else
            return true;
    }

    //UPDATE UI

    //updates arrow direction
    void updateArrow()
    {
        float temp = (npcZ - player.transform.position.z) / (npcX - player.transform.position.x);
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

    public bool TryPoop()
    {
        return true;

        //if (data.poopSize >= minPoopSize)
        //{
        //    data.updatePoop(0f);
        //    return true;
        //}
        //return false;
    }

}
