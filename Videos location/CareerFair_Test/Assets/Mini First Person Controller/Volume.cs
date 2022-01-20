using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Volume : MonoBehaviour
{
    // Start is called before the first frame update

    public VideoPlayer videoPlayer;
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    void OnTriggerEnter(Collider other)
    {
        videoPlayer.SetDirectAudioVolume(0, 1);
    }
    
    void OnTriggerExit(Collider other)
    {
        videoPlayer.SetDirectAudioVolume(0, 0);
    }
}
