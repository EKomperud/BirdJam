using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public string levelName;
    public float timer = 0f;
	
	void Update () {
        if (timer <= 2f)
            timer += Time.deltaTime;
        else if (Input.anyKey)
            SceneManager.LoadScene("TempMainMenu");
    }
}
