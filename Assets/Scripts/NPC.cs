using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {
    public GameData data;
    int direction;
	// Use this for initialization
	void Start () {
        System.Random random = new System.Random();
        direction = random.Next(0, 359);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    //Spawns normal NPCs
    public NPC(Vector3 position)
    {

    }
    //true = pick up, false = drop off
    public NPC(bool pickup, Vector3 position)
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
