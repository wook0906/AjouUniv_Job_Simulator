using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class IntroVideo_Popup : UI_Popup
{
    VideoPlayer video;
    public bool isPlayed = false;

    enum GameObjects
    {
        VideoPlayer
    }
    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        video = Get<GameObject>((int)GameObjects.VideoPlayer).GetComponent<VideoPlayer>();
        video.loopPointReached += IsFinished;
    }

    public void Play()
    {
        video.Play();
        
    }
    public void IsFinished(VideoPlayer video)
    {
        isPlayed = true;
    }

}
