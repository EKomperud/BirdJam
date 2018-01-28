using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlaybackController : MonoBehaviour {


    public VideoClip[] videos;
    VideoPlayer vPlayer;

	// Use this for initialization
	void Start () {
        vPlayer = GetComponent<VideoPlayer>();
        StartCoroutine(PlayIntroVideo());

	}

    IEnumerator PlayIntroVideo()
    {
        vPlayer.clip = videos[0];
        vPlayer.Play();

        yield return new WaitForSeconds((float)vPlayer.clip.length - .5f);
        while (true){
            
            vPlayer.time = 7;
            vPlayer.isLooping = true;
            yield return new WaitForSeconds((float)vPlayer.clip.length - 7);

    }
        //vPlayer.clip = videos[1];
        //vPlayer.Play();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
