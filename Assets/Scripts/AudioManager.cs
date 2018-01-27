using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    static AudioManager _instance;


    public AudioData ambience;
    public AudioData pigeonSound;
    public AudioData poopSplashSound;
    public AudioData poopLaunchSound;
    public AudioData swearSound;
    public AudioData thankYouSound;
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

    public void PlayPoopSplashSound(AudioSource source, int index){
        source.clip = poopSplashSound.sounds[index];
        source.Play();
    }

    public void PlayPigeonSound(AudioSource source, bool happy){
        if (happy)
            source.clip = pigeonSound.sounds[Random.Range(0, 1)];
        else
            source.clip = poopSplashSound.sounds[2];
        source.Play();
    }

    public void PlayPoopLaunchSound(AudioSource source, int size){
        source.clip = poopLaunchSound.sounds[size];
        source.Play();
    }

    public void PlaySwearSound(AudioSource source){
        source.clip = swearSound.sounds[Random.Range(0, swearSound.sounds.Length - 1)];
        source.Play();
    }

    public void PlayThankyouSound(AudioSource source){
        source.clip = thankYouSound.sounds[Random.Range(0, thankYouSound.sounds.Length - 1)];
        source.Play();
    }

}
