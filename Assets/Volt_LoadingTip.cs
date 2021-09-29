using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volt_LoadingTip : MonoBehaviour
{
    UILabel tipText;
    float changeTimer = 0;
    int prevIdx;
    int currentIdx;

    int maxIdx;
    
    private void Awake()
    {
        
    }
    void Start()
    {
        tipText = GetComponent<UILabel>();
    }

    void Update()
    {
        if(Time.time - changeTimer > 5f)
        {
            ChangeTip();
        }
    }
    void ChangeTip()
    {
        changeTimer = Time.time;
        do
        {
            currentIdx = Random.Range(0, maxIdx+1);
        }
        while (prevIdx == currentIdx);
        tipText.text = "Tip : " + Managers.Localization.GetLocalizedValue($"TipInMatchingScreen_{currentIdx}");
        prevIdx = currentIdx;
    }

}
