using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volt_ModuleTooltip : MonoBehaviour
{
    public UILabel moduleNameLabel;
    public UILabel moduleExplainationLabel;
    UIPanel panel;
    private void Awake()
    {
        panel = GetComponent<UIPanel>();
        panel.alpha = 0f;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowTooltip(string cardName)
    {
        ModuleDescriptionInfo moduleInfo = Volt_ModuleDescriptionInfos.GetModuleDescriptionInfo(cardName);

        if (moduleInfo.card == Card.NONE)
            return;

        panel.alpha = 1f;
        moduleNameLabel.text = Managers.Localization.GetLocalizedValue($"ModuleName_{cardName}"); ;
        moduleExplainationLabel.text = Managers.Localization.GetLocalizedValue($"ModuleDescription_{cardName}");
    }
    public void CloseTooltip()
    {
        Destroy(this.gameObject);
    }
}
