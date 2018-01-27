using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    //used https://www.sitepoint.com/building-a-dodger-game-clone-in-unity/
    GameObject[] pauseObjects;
    public GameData gData;
    public Text poopText;
    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        hidePaused();
        gData.createTimer();
        gData.startTimer();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGUI();
        //uses the p button to pause and unpause the game
        if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) && SceneManager.GetActiveScene().name != "MainMenu")
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                showPaused();
            }
            else if (Time.timeScale == 0)
            {
                Debug.Log("high");
                Time.timeScale = 1;
                hidePaused();
            }
        }
    }

    //controls the pausing of the scene
    public void pauseControl()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            showPaused();
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            hidePaused();
        }
    }

    //shows objects with ShowOnPause tag
    public void showPaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    //hides objects with ShowOnPause tag
    public void hidePaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }

    //loads inputted level
    public void LoadLevel(string level)
    {
        //Application.LoadLevel(level);
        SceneManager.LoadScene(level);
    }

    public void RestartLevel()
    {
        //Application.LoadLevel(Application.loadedLevel);
        Scene currentScene = SceneManager.GetActiveScene();
        LoadLevel(currentScene.name);
        //SceneManager.LoadScene(SceneManager.sce);
    }

    public void UpdateGUI()
    {
        //poopText.text = gData.poopSize.ToString() ;
        poopText.text = ((int)(gData.clock.ElapsedMilliseconds / 1000)).ToString();

    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
