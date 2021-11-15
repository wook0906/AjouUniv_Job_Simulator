//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Video;

//public class IntroVideo_Popup : UI_Popup
//{
//    public VideoPlayer video;
//    public bool isFinished = false;

//    enum GameObjects
//    {
//        VideoPlayer
//    }
//    public override void Init()
//    {
//        base.Init();

//        Bind<GameObject>(typeof(GameObjects));

//        video = Get<GameObject>((int)GameObjects.VideoPlayer).GetComponent<VideoPlayer>();
//        video.loopPointReached += IsFinished;
//    }

//    public void Play()
//    {
//        video.Play();
//    }
//    public bool IsReadyToPlay()
//    {
//        return video.isPrepared;
//    }
//    public void IsFinished(VideoPlayer video)
//    {
//        isFinished = true;
//    }

//}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class IntroVideo_Popup : MonoBehaviour
{
    public VideoPlayer video;
    public bool isFinished = false;

    void Start()
    {
        video.loopPointReached += IsFinished;
        //video.clip = Managers.Resource.Load<UnityEngine.Video.VideoClip>("Intro.mp4");
        video.Prepare();
    }

    public void Play()
    {
        video.Play();
    }
    public bool IsReadyToPlay()
    {
        return video.isPrepared;
    }
    public void IsFinished(VideoPlayer video)
    {
        Destroy(this.gameObject);
    }

}
