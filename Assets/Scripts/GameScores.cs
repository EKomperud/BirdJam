using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScores : MonoBehaviour {

    public int[] valuesHighscoresDelivery;
    public int[] valuesHighscoresPooper;
    public string[] namesHighscoresDelivery;
    public string[] namesHighscoresPooper;
    // Use this for initialization
    void Start () {
        valuesHighscoresDelivery = new int[5];
        valuesHighscoresPooper = new int[5];
        namesHighscoresDelivery = new string[5];
        namesHighscoresPooper = new string[5];
        namesHighscoresPooper[0] = "None";
        namesHighscoresPooper[1] = "None";
        namesHighscoresPooper[2] = "None";
        namesHighscoresPooper[3] = "None";
        namesHighscoresPooper[4] = "None";

        namesHighscoresDelivery[0] = "None";
        namesHighscoresDelivery[1] = "None";
        namesHighscoresDelivery[2] = "None";
        namesHighscoresDelivery[3] = "None";
        namesHighscoresDelivery[4] = "None";
    }

}
