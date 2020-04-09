using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TooltipForInventory : MonoBehaviour
{
    private Text tooltipText;

    void Start()
    {
        tooltipText = GetComponentInChildren<Text>();
        gameObject.SetActive(false);
    }

    public void GenerateToolTip(Item item)
    {
        string tempRarity = TakeNameOfAtribute.TakeNameOfRarity(item.rarity);
        string color = tempRarity.Substring(tempRarity.IndexOf(' ')+1);
        string statText = "";
        if (item.atributes.Count > 0)
        {
            foreach(var stat in item.atributes)
            {
                statText += TakeNameOfAtribute.TakeAtribute(stat.atribute) + ": " + stat.value.ToString() + "\n";
            }
        }
        string tooltip = string.Format("<color=" + color + "><b>{0}</b>\n{1}\n\n<b>{2}</b>\n<b> {3} </b>\n{4}</color>",
            item.title, item.description, statText,tempRarity.Substring(0, tempRarity.IndexOf(' ')),"Цена: " + item.price.ToString());
        tooltipText.text = tooltip;
        gameObject.SetActive(true);
    }

    public void GenerateToolTip(string text)
    {
        tooltipText.text = text;
        gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        gameObject.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }

}
