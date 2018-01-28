using System.Collections;
using System.Collections.Generic;
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

    // Use this for initialization
    void Start () {

        if (instance != null && instance != this)
            Destroy(gameObject);
        instance = this;

        data.createTimer();
        data.startTimer();

        data.target = target;
        npcX = target.position.x;
        npcZ = target.position.z;
        //load map
        //create npcs/update npc count
        //create first pickup in visible area (probably same area each time)

    }

    // Update is called once per frame
    void Update () {
        updateArrow();

        int seconds = (int)data.clock.ElapsedMilliseconds / 1000;

        data.poopSize += (Time.deltaTime * player.GetSpeed())/2f;
    }

    public static bool TryGetInstance(out GameManager gm)
    {
        gm = instance;
        if (instance == null)
            return false;
        else
            return true;
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

    public float TryPoop()
    {
        if (data.poopSize >= data.minPoopSize)
        {
            float poopSize = data.poopSize;
            data.poopSize = 0f;
            return poopSize;
        }
        return 0f;
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
