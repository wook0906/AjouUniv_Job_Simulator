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

    public Vector2 windowAnchor;
    public int fontSize;
    public bool isButton;
    public bool isNeedArrow;
    public Vector2 arrowAnchor;
}
