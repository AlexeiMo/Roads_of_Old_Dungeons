using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCaster : MonoBehaviour
{
    public GameObject whoCaster;
    
    public void CastSpell(Skill skill)
    {
        if (skill.skillTarget == SkillTarget.CastOnOwner)
            CastSkillOnCaster(skill);
        else
            CastSkillOnTarget(skill);
    }


    private void CastSkillOnCaster(Skill skill)
    {
        whoCaster.GetComponent<ISkillCaster>().CastSkillOnMe(skill);
    }


    private void CastSkillOnTarget(Skill skill)
    {

    }
}
