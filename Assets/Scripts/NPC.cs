using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {
    public GameData data;
    public GameManager gm;
    public float speed;
    Vector3 posDir;
    bool isSpecialNPC;
    Transform gameObjTrans;
    Target t;
    System.Random random;
    // Use this for initialization
    void Start () {
        GameManager.TryGetInstance(out gm);
        random = new System.Random();
        posDir = new Vector3(0, 0, 1);
        //speed = .5f;

    }
	
	// Update is called once per frame
	void Update () {
        if (!isSpecialNPC)
        {
            transform.position += posDir * Time.deltaTime * speed;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(posDir), Time.deltaTime*5f);
        }
	}
    //Spawns normal NPCs
    public void spawnNPC(Vector3 position)
    {
        gameObjTrans = this.transform;
        gameObjTrans.position = position;
    }
    //true = pick up, false = drop off
    public void spawnSpecial(bool pickup, Vector3 position)
    {
        gameObjTrans = this.transform;
        gameObjTrans.position = position;
        isSpecialNPC = true;
        t = GetComponentInChildren<Target>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("NPC Collision");
        if(!isSpecialNPC)
        {
            posDir.x = random.Next(-1, 1);
            posDir.z = random.Next(-1, 1);
            while (posDir.x == 0 && posDir.z == 0)
            {
                posDir.x = random.Next(-1, 1);
                posDir.z = random.Next(-1, 1);
            }
            if(other.gameObject.tag == "Killzone")
            {
                gm.despawnNPC();
                Destroy(this.gameObject);
            }
        }
        
    }
}
