using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]

public class Skill : ScriptableObject
{
    public int id;
    public string title;
    public string description;
    public Sprite icon;

    [Header("Атрибуты способности")]

    public SkillTarget skillTarget;
    public SkillType skillType;

    public void Clone(Skill skill)
    {

    }

    public void CreateStats()
    {

    }

    
}


public enum SkillTarget
{
    CastOnOwner = 1, CastOnTarget =2
}

public enum SkillType
{
    Active = 1, Passive = 2 
}

public enum SkillAtributeProperties
{
    Slowdown = 1,
    VariableDamage = 2,
    Stan = 3
}

public enum SkillAtributePassivePart
{
    HPRegen = 1,
    Defence = 2,
    Attack = 3,
    AmplificationSpell = 4,

}

public enum SkillAtributeActivePart
{
    Damage = 1,
    RestoreHP = 2,
    IncreaseDamage = 3,
    IncreaseDefence = 4,
    CriticalChance = 5,
    AmplificationSpell = 6
}