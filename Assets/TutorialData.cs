using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TutorialData : MonoBehaviour
{
    public static TutorialData S;
    public List<TutorialExplainPopupSetupData> datas;
    public bool isOnTutorialPopup = false;
    public int curTutorialIdx;


    public void Awake()
    {
        S = this;
    }
}
