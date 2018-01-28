using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MMenuManager : MonoBehaviour {

    //used https://www.sitepoint.com/building-a-dodger-game-clone-in-unity/
    GameObject[] pauseObjects;
    GameObject[] instructionObjects;
    //public Text poopText;
    public Image poopAlertPanel;
    //temp until fused with game manager
    public Slider poopSlider;
    public Slider timeSlider;
    public Image orbCounter;
    // Use this for initialization
    void Start()
    {
        //Time.timeScale = 1;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        instructionObjects = GameObject.FindGameObjectsWithTag("ShowInstructions");

        //hidePaused();
        showPaused();
        //setting alpha channel to 0
        //SetTranspernecy(0);
        //gData.createTimer();
        //gData.startTimer();
        poopSlider.value = 40;
        timeSlider.value = 60;

        //orbCounter.color
    }

    // Update is called once per frame
    void Update()
    {

    }


    //shows objects with ShowOnPause tag
    public void showPaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }

        foreach (GameObject i in instructionObjects)
        {
            i.SetActive(false);
        }
    }

    //hides objects with ShowOnPause tag
    public void hidePaused()
    {
        foreach (GameObject i in instructionObjects)
        {
            i.SetActive(true);
        }

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

    public void SetOrbColor(float green, float blue)
    {
        Color colorHolder = orbCounter.color;
        //Debug.Log(colorHolder.a);
        /*colorHolder.g = green;
        colorHolder.b = blue;*/
        if (colorHolder.g > 0f)
        {
            colorHolder.g -= green;
        }
        if (colorHolder.b > 0)
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