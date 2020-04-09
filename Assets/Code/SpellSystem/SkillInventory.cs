using System.Collections.Generic;
using UnityEngine;

class SkillInventory: MonoBehaviour
{
    public SkillDataBase skillDataBase;

    public List<Skill> skills = new List<Skill>();

    public UISkillManager iSkillManager;


    private void Awake()
    {
        foreach(Skill skill in skillDataBase.Skills)
        {
            skills.Add(skill);
        }
        iSkillManager.Enabling += () =>
        {
            foreach (Skill skill in skills)
            {
                iSkillManager.AddNewItem(skill);
            }
        };
    }

   


}

