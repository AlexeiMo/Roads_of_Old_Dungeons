using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Item : ScriptableObject
{
    public int id;
    public string title;
    public string description;
    public Sprite icon;

    [Header("Характеристики предмета")]
    public Rarity rarity;
    public List<AtributeOfItem> atributes = new List<AtributeOfItem>();
    public GearMainType gearMainType;
    public ItemType itemType;
    public int price;

    public List<TypeOfAtribute> listOfType;
    public List<float> listOfValue;
 
    public Dictionary<string, float> stats = new Dictionary<string, float>();

    public Item(int id, string title, string description,
        Dictionary<string, float> stats)
    {
        this.id = id;
        this.title = title;
        this.description = description;
        this.icon = Resources.Load("Sprites/Items/"+title) as Sprite;
        this.stats = stats;
    }

    public Item(Item item)
    {
        this.title = item.title;
        this.id = item.id;
        this.description = item.description;
        this.icon = item.icon;
        this.stats = item.stats;

        this.rarity = item.rarity;
        this.atributes = item.atributes;
        this.gearMainType = item.gearMainType;
        this.itemType = item.itemType;
    }

    public void Clone(Item item)
    {
        this.title = item.title;
        this.id = item.id;
        this.description = item.description;
        this.icon = item.icon;
        this.stats = item.stats;

        this.rarity = item.rarity;
        this.atributes = item.atributes;
        this.gearMainType = item.gearMainType;
        this.itemType = item.itemType;
        this.price = item.price;

        this.atributes = item.atributes;
    }

    public void CrateStats()
    {
        atributes = new List<AtributeOfItem>();
        for (int i = 0; i < listOfType.Count; i++)
        {
            atributes.Add(new AtributeOfItem(listOfType[i], listOfValue[i]));
        }
    }


}


public class TakeNameOfAtribute
{
    private static List<string> lrarity = new List<string>() { { "Обычный"+" white"  }, { "Редкий"+" blue" }, { "Эпический"+ " fuchsia" }, { "Мифический"+" red" }, { "Легендарный"+" orange" } };
    private static List<string> lgearMainType = new List<string>() { { "Оружие" }, { "Броня" }, { "Кольцо" }, { "Шлем" }, { "Амулет" } };
    private static List<string> latribute = new List<string>() { { "Урон" }, { "Магия" }, { "Защита" }, { "Крит(%)" }, { "Шанс крита(%)" }, { "Сила" }, { "Ловкость" }, { "Интеллект" }, { "Регенерация" }, { "Здоровье" } };


    static public string  TakeNameOfRarity(Rarity itemRarity)
    {
        return lrarity[(int)itemRarity];
    }
    
    static public string  TakeGearMainType(GearMainType itemGearMainType)
    {
        return lgearMainType[(int)itemGearMainType];
    }  
    
    static public string  TakeAtribute(TypeOfAtribute itemAtibute)
    {
        return latribute[(int)itemAtibute];
    } 
}

public enum Rarity
{
    Normal = 0, Rare = 1, Epic = 2, Mythic = 3, Legendary = 4
}

public enum TypeOfAtribute
{
    Attack = 0, Magic = 1, Defence = 2, CriticalDamage, CriticalChance, Strength, Agility, Intelligence, HPRegen, HP
}


public enum GearMainType
{
    Weapon = 0, Armor = 1, Ring = 2, Helmet = 3, Amulet = 4
}

public enum ItemType
{
    Gear = 0, Misc = 1, Consumable = 2
}
public class AtributeOfItem
{
    public TypeOfAtribute atribute;
    public float value;

    public AtributeOfItem(TypeOfAtribute atribute, float value) 
    {
        this.atribute = atribute;
        this.value = value;
    }
}