using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPLayerStats : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;


    [SerializeField] private TextChar health;
    [SerializeField] private TextChar regen;
    [SerializeField] private TextChar intel;
    [SerializeField] private TextChar agil;
    [SerializeField] private TextChar str;
    [SerializeField] private TextChar defence;
    [SerializeField] private TextChar attackBase;
    [SerializeField] private TextChar magicAmplify;
    [SerializeField] private TextChar criticalChance;
    [SerializeField] private TextChar criticalDamage;

    private void Awake()
    {
        playerStats.ChanchingStats += () => UpdateData();
    }

    private void UpdateData()
    {
        health.UpdateValue(((int)playerStats.healthBar.currentValue).ToString());
        regen.UpdateValue(playerStats.hPRegen.ToString());
        intel.UpdateValue(playerStats.intelligence.ToString());
        agil.UpdateValue(playerStats.agility.ToString());
        str.UpdateValue(playerStats.strength.ToString());
        defence.UpdateValue(playerStats.defence.ToString());
        attackBase.UpdateValue((playerStats.attackBase + playerStats.strength + playerStats.atributeOfItems[(int)TypeOfAtribute.Attack].value).ToString());
        magicAmplify.UpdateValue(playerStats.magicAmplify.ToString()); ;
        criticalChance.UpdateValue(playerStats.criticalChance.ToString());
        criticalDamage.UpdateValue(playerStats.criticalDamage.ToString());
    }

    private void OnEnable()
    {
        UpdateData();
    }
}
