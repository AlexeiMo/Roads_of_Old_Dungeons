using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    
    private void Awake()
    {
        BuildDataBase();
    }

    public Item GetItem(int id)
    {
        return items.Find(item => item.id == id);
    }

    public Item GetItem(string itemName)
    {
        return items.Find(item => item.title == itemName);
    }
    private void BuildDataBase()
    {
        items = new List<Item>();
        Item[] itemBase = Resources.LoadAll<Item>("Items");
        foreach(Item elem in itemBase)
        {
            elem.CrateStats();
            items.Add(elem);
            
        }
        ChanceOfDrop.SortInventory(items);
    }
}
