using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameData data;
    private int NPCCount;
    public PlayerController player;
    private static GameManager instance = null;
    [SerializeField] private Camera cam;

    [SerializeField] private Transform target;
    private float npcX;
    private float npcZ;

    System.Random random;
    int randomIndex;
    int seconds;
    [SerializeField] private Transform NPCPrefab;

    // Use this for initialization
    void Start() {

        if (instance != null && instance != this)
            Destroy(gameObject);
        instance = this;

        data.clock = new Stopwatch();
        data.clock.Start();

        data.spawnPoints = new List<GameObject>();
        updateNPCCount(1);
        random = new System.Random();


        data.target = target;
        npcX = target.position.x;
        npcZ = target.position.z;
        //load map
        //create npcs/update npc count
        //create first pickup in visible area (probably same area each time)
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
            data.poopSize += (Time.deltaTime * player.GetSpeed()) / 2f;
        }
    }

    private void endGame()
    {
        //player.control = false;
        data.clock.Stop();
        //tell ui to pop up game over screen
    }


    public static bool TryGetInstance(out GameManager gm)
    {
        gm = instance;
        if (instance == null)
            return false;
        else
            return true;
    }

    //NPCS

    //creates a pickup NPC
    void createPickUp()
    {
        randomIndex = random.Next(0, data.spawnPoints.Count);
        Transform n = Instantiate(NPCPrefab) as Transform;
        Vector3 tempPos = data.spawnPoints[randomIndex].transform.position;
        NPC npc = n.GetComponent<NPC>();
        npc.spawnSpecial(true, tempPos);
        npcX = tempPos.x;
        npcZ = tempPos.y;
    }

    //creates a dropoff NPC
    void createDropOff()
    {
        randomIndex = random.Next(0, data.spawnPoints.Count);
        Transform n = Instantiate(NPCPrefab) as Transform;
        Vector3 tempPos = data.spawnPoints[randomIndex].transform.position;
        NPC npc = n.GetComponent<NPC>();
        npc.spawnSpecial(false, tempPos);
        npcX = tempPos.x;
        npcZ = tempPos.y;
    }

    //creates normies
    void createNPC(int amount)
    {
        randomIndex = random.Next(0, data.spawnPoints.Count);
        Transform n = Instantiate(NPCPrefab) as Transform;
        Vector3 tempPos = data.spawnPoints[randomIndex].transform.position;
        //NPC npc = new NPC(tempPos);
        NPC npc = n.GetComponent<NPC>();
        npc.spawnNPC(tempPos);
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

    public float AttemptPoop()
    {
        if (data.poopSize >= data.minPoopSize)
        {
            float poopSize = data.poopSize;
            data.poopSize = 0f;
            return poopSize;
        }
        return 0f;
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

    public void PlayerDash(bool dashing)
    {
        //if (dashing && !cam.shaking)
        //{
        //    cam.shaking = true;
        //    cam.StartShake();
        //}
        //else
        //    cam.shaking = false;
        if (dashing)
            cam.fieldOfView = cam.fieldOfView < data.maxCamDistance ? cam.fieldOfView += data.camAcceleration : data.maxCamDistance;
        else
            cam.fieldOfView = cam.fieldOfView > data.defaultCamDistance ? cam.fieldOfView -= (data.camAcceleration*2) : data.defaultCamDistance;
    }

}
