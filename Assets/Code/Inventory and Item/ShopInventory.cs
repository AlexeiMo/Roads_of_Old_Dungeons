using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInventory : Inventory
{
    private int indexCommon;
    private int indexRare;
    private int indexEpic;
    private int indexMythic;
    private int indexLegendary;

    private void Start()
    {
        characterItems = ItemDataBase.items;
        indexCommon = characterItems.FindLastIndex(i => i.rarity == Rarity.Normal);
        indexRare = characterItems.FindLastIndex(i => i.rarity == Rarity.Rare);
        indexEpic = characterItems.FindLastIndex(i => i.rarity == Rarity.Epic);
        indexMythic = characterItems.FindLastIndex(i => i.rarity == Rarity.Mythic);
        indexLegendary = characterItems.FindLastIndex(i => i.rarity == Rarity.Legendary);
        inventoryUI.Enabling += ()=> AddItemForUI();
    }

    protected  void AddItemForUI()
    {
        for(int i=0;i<this.inventoryUI.NumberOfSlots; i++)
        {
            
            this.inventoryUI.AddNewItem(this.characterItems[ChanceOfDrop.DropChance(playerStats, new int[] {indexCommon, indexRare, indexEpic, indexMythic, indexLegendary })]);/*, indexRare, indexEpic,  indexMythic,  indexLegendary*//*)]);*/
        }
    }

}




class  ChanceOfDrop 
{
    delegate int DropChanceDelegate(int x);
    static public int DropChance(PlayerStats playerStats, int[] indexs)
    {
       /* DropChanceDelegate dropChanceDelegate =(int x) =>
        {
            
            return x < 26 ? x / 5 : 5;
        };*/
        return Random.Range(0, indexs[RangeMax(playerStats.playerLevel)]+1);
    }
   
    static private int RangeMax(int x)
    {
        if (x < 20)
        {
            return x / 5;
        }
        else
            return 4;
    }

   // delegate int SortDelegate(Item x, Item y);
    static public void SortInventory(List<Item> lItems)
    {
       // SortDelegate sortDelegate = (x, y) => { return x.rarity > y.rarity ? 1 : -1; };
        lItems.Sort(Comparer);
    }



    static int Comparer(Item x, Item y)
    {
        return x.rarity > y.rarity ? 1 : -1;
    }

}
