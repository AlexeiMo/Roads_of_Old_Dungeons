using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDataBase : MonoBehaviour
{
    private List<Skill> skills = new List<Skill>();

    public List<Skill> Skills { get => skills; }

    private void Awake()
    {
        BuildDataBase();
    }
    public Skill GetSkill(int id)
    {
        return skills.Find(skill => skill.id == id);
    }

    public Skill GetSkill(string skillName)
    {
        return skills.Find(skill => skill.title == skillName);
    }
    private void BuildDataBase()
    {
        skills = new List<Skill>();
        Skill[] itemBase = Resources.LoadAll<Skill>("Skills");
        foreach (Skill elem in itemBase)
        {
            elem.CreateStats();
            skills.Add(elem);
            Debug.Log("Добавление способности: " + elem.title);
        }
    }
}
