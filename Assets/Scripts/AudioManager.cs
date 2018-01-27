using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    static AudioManager _instance;


    public AudioData ambience;
    public AudioData pigeonSound;
    public AudioData poopSound;
    public AudioData swearSound;
    AudioSource audioSource;
    public static AudioManager instance { get { return _instance; }}
    // Use this for initialization
    void Awake () {
        if (_instance == null)
            _instance = this;
        if (_instance != this)
            Destroy(this);

        audioSource = GetComponent<AudioSource>();
	}
	
    void Start(){
        audioSource.clip = ambience.sounds[0];
        //audioSource.clip = poopSound.sounds[3];
        //audioSource.Play();
    }

    public void PlayAmbience(){
        audioSource.Play();
    }

    public void PlayPoopSound(AudioSource source, int index){
        source.clip = poopSound.sounds[index];
        source.Play();
    }

    public void PlayPigeonSound(AudioSource source, bool happy){
        if (happy)
            source.clip = pigeonSound.sounds[Random.Range(0, 1)];
        else
            source.clip = poopSound.sounds[2];
        source.Play();

    }

    public void PlaySwearSound(AudioSource source){
        source.clip = swearSound.sounds[Random.Range(0, swearSound.sounds.Length - 1)];
        source.Play();
    }

}
