using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tutorial/PopupSetting")]
public class TutorialExplainPopupSetupData : ScriptableObject
{
    [TextArea]
    public string contents;
    public int width;
    public int height;
    public Vector2 windowAnchor = new Vector3(0.5f, 0.5f);
    public int fontSize;
    public bool isButton;
    public bool isNeedArrow;
    public Vector2 arrowAnchor = new Vector3(0.5f,0.5f);
}
