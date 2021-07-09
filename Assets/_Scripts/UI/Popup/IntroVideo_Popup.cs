using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class IntroVideo_Popup : UI_Popup
{
    VideoPlayer video;

    enum GameObjects
    {
        VideoPlayer
    }
    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        video = Get<GameObject>((int)GameObjects.VideoPlayer).GetComponent<VideoPlayer>();
    }

    public void Play()
    {
        video.Play();
    }
    public bool IsPlaying()
    {
        if(video.isPlaying)
            return true;
        return false;
    }

}
