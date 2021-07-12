using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TermsOfServicePanel : MonoBehaviour
{
    public UIToggle TOSToggle;
    public UIToggle PCToggle;
    public UIButton confirmBtn;

    [HideInInspector]
    public bool isConfirmed = false;

    private void Start()
    {
        confirmBtn.isEnabled = false;
    }
    public bool isReadyToStart
    {
        get { return TOSToggle.value && PCToggle.value; }
    }

    public void OnClickConfirmBtn()
    {
        isConfirmed = true;
        Destroy(this.gameObject);
    }
    
    public void OnToggleChangeValue()
    {

        if (isReadyToStart)
            confirmBtn.isEnabled = true;
        else
            confirmBtn.isEnabled = false;
    }
}
