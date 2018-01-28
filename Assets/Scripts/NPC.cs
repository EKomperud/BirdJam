using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {
    public GameData data;
    public float speed;
    Vector3 direction;
    float yDir;
    bool isSpecialNPC;
    Transform parentTrans;
    System.Random random;

    // Use this for initialization
    void Start () {
        random = new System.Random();
        yDir = random.Next(0, 359);
        direction = new Vector3(0, yDir, 0);
        speed = .5f;
    }
	
	// Update is called once per frame
	void Update () {
        if (!isSpecialNPC)
        {
            parentTrans.position += Vector3.left * Time.deltaTime * speed;
        }
	}
    //Spawns normal NPCs
    public void spawnNPC(Vector3 position)
    {
        parentTrans = this.transform;
        parentTrans.position = position;
    }
    //true = pick up, false = drop off
    public void spawnSpecial(bool pickup, Vector3 position)
    {
        parentTrans = this.transform;
        parentTrans.position = position;
        isSpecialNPC = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("HI");
        if(!isSpecialNPC)
        {
            parentTrans.Rotate(direction);
            yDir = random.Next(0, 359);
            direction = new Vector3(0, yDir, 0);
        }
        
    }
}
