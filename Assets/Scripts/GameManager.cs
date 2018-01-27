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
    System.Random random;
    int randomIndex;
    int seconds;
    [SerializeField] private Transform NPCPrefab;

    // Use this for initialization
    void Start () {
        data.clock = new Stopwatch();
        data.spawnPoints = new List<GameObject>();
        //load map
        //create npcs/update npc count
        //create first pickup in visible area (probably same area each time)
        updateNPCCount(1);
        data.clock.Start();
        random = new System.Random();
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
        randomIndex = random.Next(0, data.spawnPoints.Count);
        Transform n = Instantiate(NPCPrefab) as Transform;
        Vector3 tempPos = data.spawnPoints[randomIndex].transform.position;
        NPC npc = new NPC(true, tempPos);
        npcX = tempPos.x;
        npcZ = tempPos.y;
    }

    //creates a dropoff NPC
    void createDropOff()
    {
        randomIndex = random.Next(0, data.spawnPoints.Count);
        Transform n = Instantiate(NPCPrefab) as Transform;
        Vector3 tempPos = data.spawnPoints[randomIndex].transform.position;
        NPC npc = new NPC(false, tempPos);
        npcX = tempPos.x;
        npcZ = tempPos.y;
    }

    //creates normies
    void createNPC(int amount)
    {
        randomIndex = random.Next(0, data.spawnPoints.Count);
        Transform n = Instantiate(NPCPrefab) as Transform;
        Vector3 tempPos = data.spawnPoints[randomIndex].transform.position;
        NPC npc = new NPC(tempPos);
        updateNPCCount(amount);
    }

    void updateNPCCount(int increment)
    {
        NPCCount += increment;
    }

    //COLLISIONS//


    //handles pickup collision
    void collidedPickUp(GameObject person)
    {
        data.score += 50;
        Destroy(person);
        data.letterCount = true;
        createDropOff();

    }

    //handles dropoff collision
    void collidedDropOff(GameObject person)
    {
        data.score += 50;
        Destroy(person);
        data.letterCount = false;
        createPickUp();
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

    void collidePoopSpecial(int droppedPoo)
    {
        collidePoopNPC(droppedPoo);
        //despawn special npc
        if(data.letterCount)
        {
            createDropOff();
        }
        else
        {
            createPickUp();
        }
    }
}
