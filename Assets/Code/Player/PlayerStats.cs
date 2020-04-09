using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Player))]
public class PlayerStats : MonoBehaviour, IAlive, ISkillCaster
{
    public const int V = 10;
    [SerializeField] private Player player;
    [SerializeField] Equipment equipment;
    [SerializeField] Text textGold;

    [Header("Уровень игрока")]
    public int XP;
    public int playerLevel =1 ;
    public int gold=1000;

    [Header("Bars")]
    public HealthBar healthBar;
    public float healthPoint;



    [Header("Player atribute")]
    public float defence;
    public float attackBase;
    public float magicAmplify;
    public float criticalChance;
    public float criticalDamage;



    [Header("Характеристики с учётом предметов")]
    public float hPRegen;
    public float strength;
    public float agility;
    public float intelligence;

    [Header("Базовые характеристики с учётом характеристик за уровень")]
    public float baseRegen = 0;
    public float baseStrength = V;
    public float baseAgility = V;
    public float baseIntelligence = V;

    [Header("Характеристики полученные от заклинаний")]
    public List<Skill> skillsCastOnPlayer;


    public List<AtributeOfItem> atributeOfItems;

    public event Action ChanchingStats;

   
    private void Awake()
    {
        atributeOfItems = new List<AtributeOfItem>();
        foreach(TypeOfAtribute elem in Enum.GetValues(typeof(TypeOfAtribute)))
        {
            atributeOfItems.Add(new AtributeOfItem(elem, 0));
        }
        UpdateStats();      
    }


    private void UpdateStats()
    {
        float currentPercent = healthBar.currentValue / healthBar.MaxHealthPoint;
        healthBar.MaxHealthPoint = healthPoint+ atributeOfItems[(int)TypeOfAtribute.HP].value;
        healthBar.currentValue = healthBar.MaxHealthPoint * currentPercent;
        agility = baseAgility + atributeOfItems[(int)TypeOfAtribute.Agility].value;
        strength = baseStrength + atributeOfItems[(int)TypeOfAtribute.Strength].value;
        intelligence = baseIntelligence + atributeOfItems[(int)TypeOfAtribute.Intelligence].value;
        hPRegen = baseRegen + atributeOfItems[(int)TypeOfAtribute.HPRegen].value;
        player.damage = attackBase + strength + atributeOfItems[(int)TypeOfAtribute.Attack].value;
        defence = agility + atributeOfItems[(int)TypeOfAtribute.Defence].value;
        magicAmplify = intelligence + atributeOfItems[(int)TypeOfAtribute.Defence].value;
        criticalChance = atributeOfItems[(int)TypeOfAtribute.CriticalChance].value;
        criticalDamage = atributeOfItems[(int)TypeOfAtribute.CriticalDamage].value;
        ChanchingStats?.Invoke();
    }

    private void Start()
    {
        player.TakeEX.AddListener(TakeEx);
        healthBar.MaxHealthPoint = healthPoint;
        healthBar.currentValue = healthPoint;
        equipment.Equipmting += () => RecalculationValueAdd();
        equipment.UnEquipmting += () => RecalculationValueMinus();
        player.DealingDamage += () => SetDamage();
        StartCoroutine(HpRegeneration());
        
    }
    private void Update()
    {
        textGold.text = gold.ToString();
        if (Input.GetKeyDown(KeyCode.X))
        {
            playerLevel++;
        }
    }
    private IEnumerator HpRegeneration()
    {
        yield return new WaitForSeconds(10f);
        healthBar.AdjustCurrentValue(hPRegen);
        StartCoroutine(HpRegeneration());
    }
    private void RecalculationValueAdd() 
    {
        if (equipment.EquipmentSlot.item != null)
        {
            foreach (AtributeOfItem atribute in equipment.EquipmentSlot.item.atributes)
            {
                atributeOfItems[(int) atribute.atribute].value += atribute.value;
            }
        }
        UpdateStats();
       
    }
    private void RecalculationValueMinus()
    {
        if (equipment.EquipmentSlot.item != null)
        {
            foreach (AtributeOfItem atribute in equipment.EquipmentSlot.item.atributes)
            {
                atributeOfItems[(int)atribute.atribute].value -= atribute.value;
            }
        }
        UpdateStats();
    }
    delegate float AnotherDeleg(int x, float y);
    private void SetDamage()
    {
        
        AnotherDeleg deleg = (x, y) => { return x < criticalChance ? criticalDamage / 100 * y : y; };
        float damage = deleg(UnityEngine.Random.Range(0, 100), attackBase + strength + atributeOfItems[(int)TypeOfAtribute.Attack].value);
        player.damage = damage;
        
    }

    public int TakeDamage(float damage)
    {

        healthBar.MinusCurrentValue(MultiplayerDamage(damage));
        if (healthBar.currentValue <= 0)
        {
            player.HealthLevelZero();
        }
        return 0;
    }

    private float MultiplayerDamage(float damage) {
        return (damage * (1 - ((0.052f * defence) / (0.9f + 0.048f * Math.Abs(defence)))));
    }

    public void TakeEx(int value)
    {
        XP += value;
    }
    public void CastSkillOnMe(Skill skill)
    {
        Debug.Log("Способность использованна: " + skill.name);
    }
}
