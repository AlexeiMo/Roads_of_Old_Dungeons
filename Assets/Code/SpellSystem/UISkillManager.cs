using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISkillManager : MonoBehaviour
{
    public List<UISkill> uISkills = new List<UISkill>();
    public GameObject slotPrefab;
    public Transform slopPanel;
    public int NumberOfSlots = 16;

    public event Action Enabling;

    private void OnEnable()
    {
        for (int i = 0; i < NumberOfSlots; i++)
        {
            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(slopPanel);
            instance.transform.localScale = new Vector3(1, 1, 1);
            UISkill tempUIItem = instance.GetComponentInChildren<UISkill>();
          //  tempUIItem.Initialization(singletonInventory);
            uISkills.Add(tempUIItem);
        }
        Cursor.visible = true;
        Enabling?.Invoke();
    }

    private void OnDisable()
    {
        foreach (UISkill skill in uISkills)
        {
            Destroy(skill.Slot);
        }
        uISkills.Clear();
        Cursor.visible = false;
    }

    public void UpdateSlot(int slot, Skill skill)
    {
       uISkills[slot].UpdateSkill(skill);
    }

    public void AddNewItem(Skill skill)
    {
        UpdateSlot(uISkills.FindIndex(i => i.skill == null), skill);
    }

    public void RemoveItem(Skill skill)
    {
        UpdateSlot(uISkills.FindIndex(i => i.skill == skill), null);
    }
}
