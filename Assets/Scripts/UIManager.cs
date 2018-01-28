using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Diagnostics;

public class UIManager : MonoBehaviour {

    //used https://www.sitepoint.com/building-a-dodger-game-clone-in-unity/
    GameObject[] pauseObjects;
    public GameObject poopFrameObj;
    public GameData gData;
    //public Text poopText;
    public Image poopAlertPanel;
    //temp until fused with game manager
    //public Stopwatch clock;
    public Slider poopSlider;
    public Slider timeSlider;
    public Image orbCounter;
    public int totalTime;
    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        poopFrameObj.SetActive(true);
        hidePaused();
        //gData.createTimer();
        //gData.startTimer();
        //setting alpha channel to 0
        SetTranspernecy(0);
        //gData.createTimer();
        //gData.startTimer();
        //clock = new Stopwatch();
        //clock.Start();
        poopSlider.value = gData.poopSize;
        timeSlider.value = totalTime;

        //orbCounter.color
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGUI();
        //uses the p button to pause and unpause the game
        if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) && SceneManager.GetActiveScene().name != "MainMenu")
        {
            if (Mathf.Equals(Time.timeScale, 1f))
            {
                Time.timeScale = 0;
                gData.clock.Stop();
                showPaused();
                poopFrameObj.SetActive(false);
            }
            else if (Time.timeScale == 0)
            {
                //Debug.Log("high");
                Time.timeScale = 1;
                gData.clock.Start();
                hidePaused();
                poopFrameObj.SetActive(true);
            }
        }
    }

 /*   //controls the pausing of the scene
    public void pauseControl()
    {
        if (Mathf.Equals(Time.timeScale, 1f))
        {
            Time.timeScale = 0;
            showPaused();
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            hidePaused();
        }
    }*/

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
        //float clocknumtest = clock.ElapsedMilliseconds / 10000.0f;
        //float clocknumtest = clock.ElapsedMilliseconds / 10000.0f;
        //poopText.text = ((int)(clock.ElapsedMilliseconds / 1000)).ToString();
        SetTranspernecy((float)gData.poopSize/6.0f);
        poopSlider.value = gData.poopSize;//((int)(clock.ElapsedMilliseconds / 100));
        timeSlider.value = totalTime - (int)(gData.clock.ElapsedMilliseconds / 1000);
        SetOrbColor((float)gData.pooHits * 0.1f, (float)gData.pooHits * 0.1f);
        //SetOrbColor(clocknumtest,clocknumtest);
    }

    public void SetOrbColor(float green,float blue)
    {
        Color colorHolder = orbCounter.color;
        //Debug.Log(colorHolder.a);
        /*colorHolder.g = green;
        colorHolder.b = blue;*/
        if (colorHolder.g > 0f)
        {
            colorHolder.g -= green;
        }
        if(colorHolder.b>0)
        {
           colorHolder.b -= blue;
        }
        orbCounter.color = colorHolder;
    }

    //Sets transpernacy level(alpha) for pooppanel
    public void SetTranspernecy(float transLevel)
    {
        Color colorHolder;
        colorHolder = poopAlertPanel.color;
        //Debug.Log(colorHolder.a);
        float clampalpah = Mathf.Clamp(transLevel, 0f, 1f);
        colorHolder.a = clampalpah;
        poopAlertPanel.color = colorHolder;
    }

    public void ShowInstruction()
    {

    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
