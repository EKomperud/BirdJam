using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameData data;
  
	// Use this for initialization
	void Start () {
        data.createTimer();
        //load map
        //create npcs/update npc count
        //create first pickup in visible area (probably same area each time)
	}
	
	// Update is called once per frame
	void Update () {
		//have arrow point at dest.
	}

    void createNPC(int amount)
    {

    }

    //updates arrow direction
    void updateArrow(GameObject npc)
    {

    }

    void createPickUp()
    {
        //create 
    }
}
