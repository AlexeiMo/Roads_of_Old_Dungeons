using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISkill : MonoBehaviour, IPointerClickHandler
{
    public Skill skill;
    public SkillScenePresenter skillScenePresenter;

    public event Action Casting;

    protected Image spriteImage;

    [SerializeField] private GameObject parent;
    public GameObject Slot { get => parent; }


    private void Awake()
    {
        spriteImage = gameObject.GetComponent<Image>();
        spriteImage.color = Color.clear;
    }
    public void UpdateSkill(Skill skill)
    {
        this.skill = skill;
       
        if (skill != null)
        {
            spriteImage.sprite = skill.icon;
            spriteImage.color = Color.white;
            spriteImage.preserveAspect = true;
        }
        else
        {
            spriteImage.color = Color.white;
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        Casting?.Invoke();
    }
}
