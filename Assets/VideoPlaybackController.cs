using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoPlaybackController : MonoBehaviour {


    public VideoClip[] videos;
    VideoPlayer vPlayer;
    Coroutine curCoroutine;
    public float skipTime = 7;
    public string levelName;
    int count=0;
	// Use this for initialization
	void Start () {
        vPlayer = GetComponent<VideoPlayer>();
        curCoroutine = StartCoroutine(PlayIntroVideo());

	}

    IEnumerator PlayIntroVideo()
    {
        vPlayer.clip = videos[0];
        vPlayer.Play();

        yield return new WaitForSeconds((float)vPlayer.clip.length - .5f);
        while (true){
            
            vPlayer.time = skipTime;
            vPlayer.isLooping = true;
            yield return new WaitForSeconds((float)vPlayer.clip.length - skipTime - .5f);

    }

    }
    
    IEnumerator SkipVideo()
	{
        while (true)
        {
            vPlayer.time = skipTime;
            vPlayer.isLooping = true;
            yield return new WaitForSeconds((float)vPlayer.clip.length - skipTime - .5f);
        }
		//vPlayer.clip = videos[1];
		//vPlayer.Play();
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.anyKeyDown){

            if (count == 0)
            {
                StopCoroutine(curCoroutine);
                StartCoroutine(SkipVideo());
            }
            else
             
            {
                SceneManager.LoadScene(levelName);
            }
            count++;
        }
	}
}
