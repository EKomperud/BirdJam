using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameData data;
    private int NPCCount;
    private int minPop;
    private float populationTimer;
    private int currentSpawn;
    public PlayerController player;
    private static GameManager instance = null;
    [SerializeField] private Camera cam;

    [SerializeField] private Transform target;

    [SerializeField] private Light dirLight;
    private float npcX;
    private float npcZ;

    System.Random random;
    int randomIndex;
    int seconds;
    Vector3 targetLightDir;
    [SerializeField] private Transform NPCPrefab;
    [SerializeField] private Transform NPC_DropOff;
    [SerializeField] private Transform NPC_PickUp;
    [SerializeField] private List<GameObject> spawnPoints;

    // Use this for initialization
    void Start() {
        data.score = 0;
        data.pooHits = 0;
        minPop = 20;
        populationTimer = 0f;
        currentSpawn = 0;
        if (instance != null && instance != this)
            Destroy(gameObject);
        instance = this;

        data.clock = new Stopwatch();
        data.clock.Start();

        updateNPCCount(1);
        random = new System.Random();
        createPickUp();

        data.target = target;
        npcX = target.position.x;
        npcZ = target.position.z;

        targetLightDir = new Vector3(0, 1, 1);
        //load map
        //create npcs/update npc count
        //create first pickup in visible area (probably same area each time)
    }

	
	// Update is called once per frame
	void Update () {
        //rotate light
        dirLight.transform.rotation = Quaternion.Slerp(dirLight.transform.rotation, Quaternion.LookRotation(targetLightDir), Time.deltaTime * .007f);
        //check if enough npcs
        if (NPCCount <= minPop)
        {
            if(currentSpawn > spawnPoints.Count - 1)
            {
                currentSpawn = 0;
            }
            createNPC(currentSpawn);
            currentSpawn++;
        }
        if (seconds > 150)
        {
            endGame();
        }
        else
        {
            seconds = (int)data.clock.ElapsedMilliseconds / 1000;
            data.poopSize += (Time.deltaTime * player.GetSpeed()) / 2f;
        }

        populationTimer += Time.deltaTime;
        if (populationTimer >= 5)
        {
            populationTimer = 0;
            minPop += 2;
        }
    }

    public void endGame()
    {
        //player.control = false;
        data.clock.Stop();
        //tell ui to pop up game over screen
        //SceneManager.LoadScene("GameOver");
        if (data.pooHits >= 5)
            SceneManager.LoadScene("BadEnding");
        else if (data.score >= 200)
            SceneManager.LoadScene("GoodEnding");
        else
            SceneManager.LoadScene("GameOver");
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
        randomIndex = random.Next(0,spawnPoints.Count);
        Transform n = Instantiate(NPC_PickUp) as Transform;
        Vector3 tempPos = spawnPoints[randomIndex].transform.position;
        NPC npc = n.GetComponent<NPC>();
        npc.spawnSpecial(true, tempPos);
        target = n;
        data.target = n;
        npcX = tempPos.x;
        npcZ = tempPos.y;
    }

    //creates a dropoff NPC
    void createDropOff()
    {
        randomIndex = random.Next(0, spawnPoints.Count);
        Transform n = Instantiate(NPC_DropOff) as Transform;
        Vector3 tempPos = spawnPoints[randomIndex].transform.position;
        NPC npc = n.GetComponent<NPC>();
        npc.spawnSpecial(false, tempPos);
        target = n;
        data.target = n;
        npcX = tempPos.x;
        npcZ = tempPos.y;
    }

    //creates normies
    void createNPC(int index)
    {
        Transform n = Instantiate(NPCPrefab) as Transform;
        Vector3 tempPos = spawnPoints[index].transform.position;
        tempPos.y = 0;
        NPC npc = n.GetComponent<NPC>();
        npc.spawnNPC(tempPos);
        updateNPCCount(1);
    }

    void updateNPCCount(int increment)
    {
        NPCCount += increment;
    }

    //COLLISIONS//


    //handles pickup collision
    public void collidedPickUp(GameObject person)
    {
        data.score += 50;
        Destroy(person);
        data.letterCount = true;
        createDropOff();
        player.Delivery(true);
    }

    //handles dropoff collision
    public void collidedDropOff(GameObject person)
    {
        data.score += 50;
        Destroy(person);
        data.letterCount = false;
        createPickUp();
        player.Delivery(false);
    }

    public void collidedPoop(GameObject person)
    {
        data.score -= 100;
        Destroy(person);
    }

    public void despawnNPC()
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

    public void collidePoopNPC(GameObject person, float droppedPoo)
    {
        //data.score += -droppedPoo * 10;
        data.pooHits++;
        Destroy(person);
    }

    void collidePoopSpecial(int droppedPoo)
    {
        //collidePoopNPC(droppedPoo);
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

    public void updateMinPop(int amount)
    {
        
    }

}
