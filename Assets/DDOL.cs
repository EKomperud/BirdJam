using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDOL : MonoBehaviour {

    [SerializeField] GameData data;

	void Start () {
        DontDestroyOnLoad(gameObject);
	}
}
