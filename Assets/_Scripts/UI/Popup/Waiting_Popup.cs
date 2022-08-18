using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waiting_Popup : UI_Popup
{
    enum Sprites
    {
        Loading_Img
    }

    public override void Init()
    {
        if (OnInit != null)
            OnInit.Invoke();

        base.Init();

        Bind<UISprite>(typeof(Sprites));

        StartCoroutine("RunLoadingAnimation");

        if(OnOpened != null)
            OnOpened.Invoke();
    }

    private IEnumerator RunLoadingAnimation()
    {
        UISprite loaingImg = GetSprite((int)Sprites.Loading_Img);
        while (true)
        {
            Vector3 rotation = loaingImg.transform.eulerAngles;
            rotation.z += 360.0f * Time.deltaTime;
            loaingImg.transform.eulerAngles = rotation;
            yield return null;
        }
    }
}
