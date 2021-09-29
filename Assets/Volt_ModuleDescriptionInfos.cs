using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct ModuleDescriptionInfo
{
    public Card card;
    public SkillType skillType;
    public ModuleType moduleType;
}

public static class Volt_ModuleDescriptionInfos
{
    public static ModuleDescriptionInfo GetModuleDescriptionInfo(string name)
    {
        ModuleDescriptionInfo info = new ModuleDescriptionInfo();

        switch (name)
        {
            case "CROSSFIRE":
                info.card = Card.CROSSFIRE;
                info.skillType = SkillType.Active;
                info.moduleType = ModuleType.Attack;
                break;
            case "DOUBLEATTACK":

                info.card = Card.DOUBLEATTACK;
                info.skillType = SkillType.Active;
                info.moduleType = ModuleType.Attack;
                break;
            case "GRENADES":
                info.card = Card.GRENADES;
                info.skillType = SkillType.Active;
                info.moduleType = ModuleType.Attack;
                break;
            case "PERNERATE":
                info.card = Card.PERNERATE;
                info.skillType = SkillType.Active;
                info.moduleType = ModuleType.Attack;
                break;
            case "POWERBEAM":
                info.card = Card.POWERBEAM;
                info.skillType = SkillType.Active;
                info.moduleType = ModuleType.Attack;
                break;
            case "SAWBLADE":
                info.card = Card.SAWBLADE;
                info.skillType = SkillType.Passive;
                info.moduleType = ModuleType.Attack;
                break;
            case "SHOCKWAVE":
                info.card = Card.SHOCKWAVE;
                info.skillType = SkillType.Passive;
                info.moduleType = ModuleType.Attack;
                break;
            case "TIMEBOMB":
                info.card = Card.TIMEBOMB;
                info.skillType = SkillType.Active;
                info.moduleType = ModuleType.Attack;
                break;
            case "DODGE":
                info.card = Card.DODGE;
                info.skillType = SkillType.Passive;
                info.moduleType = ModuleType.Movement;
                break;
            case "REPULSIONBLAST":
                info.card = Card.REPULSIONBLAST;
                info.skillType = SkillType.Passive;
                info.moduleType = ModuleType.Movement;
                break;
            case "STEERINGNOZZLE":
                info.card = Card.STEERINGNOZZLE;
                info.skillType = SkillType.Passive;
                info.moduleType = ModuleType.Movement;
                break;
            case "TELEPORT":
                info.card = Card.TELEPORT;
                info.skillType = SkillType.Active;
                info.moduleType = ModuleType.Movement;
                break;
            case "AMARGEDDON":
                info.card = Card.AMARGEDDON;
                info.skillType = SkillType.Passive;
                info.moduleType = ModuleType.Tactic;
                break;
            case "ANCHOR":
                info.card = Card.ANCHOR;
                info.skillType = SkillType.Passive;
                info.moduleType = ModuleType.Tactic;
                break;
            case "BOMB":
                info.card = Card.BOMB;
                info.skillType = SkillType.Passive;
                info.moduleType = ModuleType.Tactic;
                break;
            case "DUMMYGEAR":
                info.card = Card.DUMMYGEAR;
                info.skillType = SkillType.Passive;
                info.moduleType = ModuleType.Tactic;
                break;
            case "EMP":
                info.card = Card.EMP;
                info.skillType = SkillType.Active;
                info.moduleType = ModuleType.Tactic;
                break;
            case "HACKING":
                info.card = Card.HACKING;
                info.skillType = SkillType.Passive;
                info.moduleType = ModuleType.Tactic;
                break;
            case "SHIELD":
                info.card = Card.SHIELD;
                info.skillType = SkillType.Passive;
                info.moduleType = ModuleType.Tactic;
                break;
            default:
                Debug.Log("GetModuleDescriptionInfo Error");
                break;
        }
        return info;
    }
}
