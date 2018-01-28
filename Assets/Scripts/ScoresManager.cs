using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoresManager : MonoBehaviour {

    private GameScores allScores;
    private string playerName;
    public InputField nameField;
    public Button submitButton;
    public GameData gData;
    public GameObject mesgLeaderboards;
    public GameObject pooperLeaderboards;
    public Text[] topMesgNames;
    public Text[] topMesgScores;
    public Text[] topPooperNames;
    public Text[] topPooperScores;


    // Use this for initialization
    void Start () {
        //topMesgNames = new Text[5];
        //topMesgScores = new Text[5];
        //topPooperNames = new Text[5];
        //topPooperScores = new Text[5];
        mesgLeaderboards.SetActive(false);
        pooperLeaderboards.SetActive(false);

        submitButton.enabled = false;
        nameField.enabled = false;

        LoadPlayerScores();

        if (gData.pooHits > allScores.valuesHighscoresPooper[4] || gData.score > allScores.valuesHighscoresDelivery[4])
        //if (gData.pooHits > 5 || gData.score > 5)
        {
                submitButton.enabled = true;
            nameField.enabled = true;
            nameField.text = "Your Name";
        }
        else
        {
            mesgLeaderboards.SetActive(true);
            pooperLeaderboards.SetActive(true);
            SetText(topMesgNames.Length);

        }
        
	}
	


	// Update is called once per frame
	void Update () {
		
	}

    //Set Text to all names from list
    public void SetText(int topScoreSize)
    {
        for (int i = 0; i < topScoreSize; i++)
        {
            topMesgNames[i].text = allScores.namesHighscoresDelivery[i];
            topMesgScores[i].text = allScores.valuesHighscoresDelivery[i].ToString();
            topPooperNames[i].text = allScores.namesHighscoresPooper[i];
            topPooperScores[i].text = allScores.valuesHighscoresPooper[i].ToString();
        }
    }

    public void SubmitScore()
    {
        SetPlayerName();

        if(gData.pooHits > allScores.valuesHighscoresPooper[4])
        {
            SubmitBadScore(gData.pooHits, playerName);
        }
        if(gData.score > allScores.valuesHighscoresDelivery[4])
        {
            SubmitGoodScore(gData.score,playerName);
        }
        mesgLeaderboards.SetActive(true);
        pooperLeaderboards.SetActive(true);
        SetText(topMesgNames.Length);


    }

    private void SetPlayerName()
    {
        playerName = nameField.text;
    }

    private void LoadPlayerScores()
    {
        allScores = new GameScores();
        allScores.Start();
        LoadGoodPlayerScores();
        LoadBadPlayerScores();
    }

    public void SubmitGoodScore(int newScore,string playerName)
    {
        if(newScore>allScores.valuesHighscoresDelivery[0])
        {
            allScores.valuesHighscoresDelivery[4] = allScores.valuesHighscoresDelivery[3];
            allScores.valuesHighscoresDelivery[3] = allScores.valuesHighscoresDelivery[2];
            allScores.valuesHighscoresDelivery[2] = allScores.valuesHighscoresDelivery[1];
            allScores.valuesHighscoresDelivery[1] = allScores.valuesHighscoresDelivery[0];
            allScores.valuesHighscoresDelivery[0] = newScore;

            allScores.namesHighscoresDelivery[4] = allScores.namesHighscoresDelivery[3];
            allScores.namesHighscoresDelivery[3] = allScores.namesHighscoresDelivery[2];
            allScores.namesHighscoresDelivery[2] = allScores.namesHighscoresDelivery[1];
            allScores.namesHighscoresDelivery[1] = allScores.namesHighscoresDelivery[0];
            allScores.namesHighscoresDelivery[0] = playerName;
        }
        else if(newScore > allScores.valuesHighscoresDelivery[1])
        {
            allScores.valuesHighscoresDelivery[4] = allScores.valuesHighscoresDelivery[3];
            allScores.valuesHighscoresDelivery[3] = allScores.valuesHighscoresDelivery[2];
            allScores.valuesHighscoresDelivery[2] = allScores.valuesHighscoresDelivery[1];
            allScores.valuesHighscoresDelivery[1] = newScore;

            allScores.namesHighscoresDelivery[4] = allScores.namesHighscoresDelivery[3];
            allScores.namesHighscoresDelivery[3] = allScores.namesHighscoresDelivery[2];
            allScores.namesHighscoresDelivery[2] = allScores.namesHighscoresDelivery[1];
            allScores.namesHighscoresDelivery[1] = playerName;
        }
        else if (newScore > allScores.valuesHighscoresDelivery[2])
        {
            allScores.valuesHighscoresDelivery[4] = allScores.valuesHighscoresDelivery[3];
            allScores.valuesHighscoresDelivery[3] = allScores.valuesHighscoresDelivery[2];
            allScores.valuesHighscoresDelivery[2] = newScore;

            allScores.namesHighscoresDelivery[4] = allScores.namesHighscoresDelivery[3];
            allScores.namesHighscoresDelivery[3] = allScores.namesHighscoresDelivery[2];
            allScores.namesHighscoresDelivery[2] = playerName;
        }
        else if (newScore > allScores.valuesHighscoresDelivery[3])
        {
            allScores.valuesHighscoresDelivery[4] = allScores.valuesHighscoresDelivery[3];
            allScores.valuesHighscoresDelivery[3] = newScore;

            allScores.namesHighscoresDelivery[4] = allScores.namesHighscoresDelivery[3];
            allScores.namesHighscoresDelivery[3] = playerName;
        }
        else if (newScore > allScores.valuesHighscoresDelivery[4])
        {
            allScores.valuesHighscoresDelivery[4] = newScore;

            allScores.namesHighscoresDelivery[4] = playerName;
        }
    }

    public void SubmitBadScore(int newScore, string playerName)
    {
        if (newScore > allScores.valuesHighscoresPooper[0])
        {
            allScores.valuesHighscoresPooper[4] = allScores.valuesHighscoresPooper[3];
            allScores.valuesHighscoresPooper[3] = allScores.valuesHighscoresPooper[2];
            allScores.valuesHighscoresPooper[2] = allScores.valuesHighscoresPooper[1];
            allScores.valuesHighscoresPooper[1] = allScores.valuesHighscoresPooper[0];
            allScores.valuesHighscoresPooper[0] = newScore;

            allScores.namesHighscoresPooper[4] = allScores.namesHighscoresPooper[3];
            allScores.namesHighscoresPooper[3] = allScores.namesHighscoresPooper[2];
            allScores.namesHighscoresPooper[2] = allScores.namesHighscoresPooper[1];
            allScores.namesHighscoresPooper[1] = allScores.namesHighscoresPooper[0];
            allScores.namesHighscoresPooper[0] = playerName;
        }
        else if (newScore > allScores.valuesHighscoresPooper[1])
        {
            allScores.valuesHighscoresPooper[4] = allScores.valuesHighscoresPooper[3];
            allScores.valuesHighscoresPooper[3] = allScores.valuesHighscoresPooper[2];
            allScores.valuesHighscoresPooper[2] = allScores.valuesHighscoresPooper[1];
            allScores.valuesHighscoresPooper[1] = newScore;

            allScores.namesHighscoresPooper[4] = allScores.namesHighscoresPooper[3];
            allScores.namesHighscoresPooper[3] = allScores.namesHighscoresPooper[2];
            allScores.namesHighscoresPooper[2] = allScores.namesHighscoresPooper[1];
            allScores.namesHighscoresPooper[1] = playerName;
        }
        else if (newScore > allScores.valuesHighscoresPooper[2])
        {
            allScores.valuesHighscoresPooper[4] = allScores.valuesHighscoresPooper[3];
            allScores.valuesHighscoresPooper[3] = allScores.valuesHighscoresPooper[2];
            allScores.valuesHighscoresPooper[2] = newScore;

            allScores.namesHighscoresPooper[4] = allScores.namesHighscoresPooper[3];
            allScores.namesHighscoresPooper[3] = allScores.namesHighscoresPooper[2];
            allScores.namesHighscoresPooper[2] = playerName;
        }
        else if (newScore > allScores.valuesHighscoresPooper[3])
        {
            allScores.valuesHighscoresPooper[4] = allScores.valuesHighscoresPooper[3];
            allScores.valuesHighscoresPooper[3] = newScore;

            allScores.namesHighscoresPooper[4] = allScores.namesHighscoresPooper[3];
            allScores.namesHighscoresPooper[3] = playerName;
        }
        else if (newScore > allScores.valuesHighscoresPooper[4])
        {
            allScores.valuesHighscoresPooper[4] = newScore;

            allScores.namesHighscoresPooper[4] = playerName;
        }
    }

    private void LoadBadPlayerScores()
    {
        if (PlayerPrefs.HasKey("BadScore1"))
        {
            allScores.valuesHighscoresPooper[0] = PlayerPrefs.GetInt("BadScore1");
            allScores.namesHighscoresPooper[0] = PlayerPrefs.GetString("BadScore1Name");
        }
        if (PlayerPrefs.HasKey("BadScore2"))
        {
            allScores.valuesHighscoresPooper[1] = PlayerPrefs.GetInt("BadScore2");
            allScores.namesHighscoresPooper[1] = PlayerPrefs.GetString("BadScore2Name");
        }
        if (PlayerPrefs.HasKey("BadScore3"))
        {
            allScores.valuesHighscoresPooper[2] = PlayerPrefs.GetInt("BadScore3");
            allScores.namesHighscoresPooper[2] = PlayerPrefs.GetString("BadScore3Name");
        }
        if (PlayerPrefs.HasKey("BadScore4"))
        {
            allScores.valuesHighscoresPooper[3] = PlayerPrefs.GetInt("BadScore4");
            allScores.namesHighscoresPooper[3] = PlayerPrefs.GetString("BadScore4Name");
        }
        if (PlayerPrefs.HasKey("BadScore5"))
        {
            allScores.valuesHighscoresPooper[4] = PlayerPrefs.GetInt("BadScore5");
            allScores.namesHighscoresPooper[4] = PlayerPrefs.GetString("BadScore5Name");
        }
    }

    private void LoadGoodPlayerScores()
    {
        if (PlayerPrefs.HasKey("GoodScore1"))
        {
            allScores.valuesHighscoresDelivery[0] = PlayerPrefs.GetInt("GoodScore1");
            allScores.namesHighscoresDelivery[0] = PlayerPrefs.GetString("GoodScore1Name");
        }
        if (PlayerPrefs.HasKey("GoodScore2"))
        {
            allScores.valuesHighscoresDelivery[1] = PlayerPrefs.GetInt("GoodScore2");
            allScores.namesHighscoresDelivery[1] = PlayerPrefs.GetString("GoodScore2Name");
        }
        if (PlayerPrefs.HasKey("GoodScore3"))
        {
            allScores.valuesHighscoresDelivery[2] = PlayerPrefs.GetInt("GoodScore3");
            allScores.namesHighscoresDelivery[2] = PlayerPrefs.GetString("GoodScore3Name");
        }
        if (PlayerPrefs.HasKey("GoodScore4"))
        {
            allScores.valuesHighscoresDelivery[3] = PlayerPrefs.GetInt("GoodScore4");
            allScores.namesHighscoresDelivery[3] = PlayerPrefs.GetString("GoodScore4Name");
        }
        if (PlayerPrefs.HasKey("GoodScore5"))
        {
            allScores.valuesHighscoresDelivery[4] = PlayerPrefs.GetInt("GoodScore5");
            allScores.namesHighscoresDelivery[4] = PlayerPrefs.GetString("GoodScore5Name");
        }
    }

    private void SavePlayerScores()
    {
        SaveGoodPlayerScores();
        SaveBadPlayerScores();
    }

    private void SaveBadPlayerScores()
    {
        PlayerPrefs.SetInt("BadScore1", allScores.valuesHighscoresPooper[0]);
        PlayerPrefs.SetString("BadScore1Name", allScores.namesHighscoresPooper[0]);
        PlayerPrefs.SetInt("BadScore2", allScores.valuesHighscoresPooper[1]);
        PlayerPrefs.SetString("BadScore2Name", allScores.namesHighscoresPooper[1]);
        PlayerPrefs.SetInt("BadScore3", allScores.valuesHighscoresPooper[2]);
        PlayerPrefs.SetString("BadScore3Name", allScores.namesHighscoresPooper[2]);
        PlayerPrefs.SetInt("BadScore4", allScores.valuesHighscoresPooper[3]);
        PlayerPrefs.SetString("BadScore4Name", allScores.namesHighscoresPooper[3]);
        PlayerPrefs.SetInt("BadScore5", allScores.valuesHighscoresPooper[4]);
        PlayerPrefs.SetString("BadScore5Name", allScores.namesHighscoresPooper[4]);
    }

    private void SaveGoodPlayerScores()
    {
        PlayerPrefs.SetInt("GoodScore1", allScores.valuesHighscoresDelivery[0]);
        PlayerPrefs.SetString("GoodScore1Name", allScores.namesHighscoresDelivery[0]);
        PlayerPrefs.SetInt("GoodScore2", allScores.valuesHighscoresDelivery[1]);
        PlayerPrefs.SetString("GoodScore2Name", allScores.namesHighscoresDelivery[1]);
        PlayerPrefs.SetInt("GoodScore3", allScores.valuesHighscoresDelivery[2]);
        PlayerPrefs.SetString("GoodScore3Name", allScores.namesHighscoresDelivery[2]);
        PlayerPrefs.SetInt("GoodScore4", allScores.valuesHighscoresDelivery[3]);
        PlayerPrefs.SetString("GoodScore4Name", allScores.namesHighscoresDelivery[3]);
        PlayerPrefs.SetInt("GoodScore5", allScores.valuesHighscoresDelivery[4]);
        PlayerPrefs.SetString("GoodScore5Name", allScores.namesHighscoresDelivery[4]);
    }

    //loads inputted level
    public void LoadLevel(string level)
    {
        //Application.LoadLevel(level);
        SceneManager.LoadScene(level);
    }

    public void QuitGame()
    {
        SavePlayerScores();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
