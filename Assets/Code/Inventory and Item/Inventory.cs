using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> characterItems = new List<Item>();
    public List<Item> characterEquipmentItems = new List<Item>();
    public ItemDataBase ItemDataBase;
    public UIInventory inventoryUI;
    public UIInventory inventoryInShop;   
    public Equipment equipment;
    public int sizeOfInventory=16;


    public PlayerStats playerStats;
    private void Start()
    {
        characterItems = ItemDataBase.items;
        equipment.UnEquipmting += () => WrittenOnEventUnEquipment();
        equipment.Equipmting += () => WrittenOnEventEquipment();
        inventoryUI.Enabling += () => AddItemForUI(inventoryUI);
        inventoryInShop.Enabling += () => AddItemForUI(inventoryInShop);
    }

    protected void AddItemForUI(UIInventory uIInventory)
    {
        foreach (Item item in characterItems)
        {
            uIInventory.AddNewItem(item);
        }
    }

  /*  private void OnEnable()
    {
        equipment.UnEquipmting += () => WrittenOnEventEquipment();
        equipment.Equipmting += () => WrittenOnEventUnEquipment();
        foreach(Item elem in characterItems)
        {
            inventoryUI.AddNewItem(elem);
        }
    }

    private void OnDisable()
    {
        equipment.UnEquipmting -= () => WrittenOnEventEquipment();
        equipment.Equipmting -= () => WrittenOnEventUnEquipment();
        foreach (Item elem in characterItems)
        {
            inventoryUI.RemoveItem(elem);
        }
    }*/

    private void WrittenOnEventUnEquipment()
    {
        if (characterItems.Count < sizeOfInventory && equipment.EquipmentSlot.item != null)
        {
            characterEquipmentItems.Remove(equipment.EquipmentSlot.item);
            characterItems.Add(equipment.EquipmentSlot.item);
        }
    }

    public void SellItem(ShopUIItem shopUIItem)
    {
        playerStats.gold += shopUIItem.item.price;
        RemoveItem(shopUIItem.item);
        characterItems.Remove(shopUIItem.item);
        shopUIItem.UpdateItem(null);
    }

    public void BuyItem(ShopUIItem shopUIItem)
    {
        if(playerStats.gold > shopUIItem.item.price && characterItems.Count < sizeOfInventory)
        {
            playerStats.gold -= shopUIItem.item.price;
            characterItems.Add(shopUIItem.item);
            inventoryInShop.AddNewItem(shopUIItem.item);
            shopUIItem.UpdateItem(null);
        }
        
    }

    private void WrittenOnEventEquipment()
    {
        if (equipment.EquipmentSlot.item != null)
        {
            characterEquipmentItems.Add(equipment.EquipmentSlot.item);
            characterItems.Remove(equipment.EquipmentSlot.item);
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
            GiveItem(0);
    }

    public void GiveItem(int id)
    {
        if (characterItems.Count < sizeOfInventory)
        {
            Item itemToAdd = ItemDataBase.GetItem(id);
            characterItems.Add(itemToAdd);
            inventoryUI.AddNewItem(itemToAdd);
            Debug.Log("Added item: " + itemToAdd.title);
        }
    }
    
    public void GiveItem(Item item)
    {
        if (characterItems.Count < sizeOfInventory)
        {
            characterItems.Add(item);
            inventoryUI.AddNewItem(item);
            Debug.Log("Added item: " + item.title);
        }

    }
    public void UnEquipmentItem(Item item)
    {
        if (characterItems.Count < sizeOfInventory)
        {
            inventoryUI.AddNewItem(item);
            Debug.Log("Added item: " + item.title);
        }
    }


    public Item CheckForItem(int id)
    {
        return characterItems.Find(item => item.id == id);
    }

    public Item CheckForItem(Item itemForCheck)
    {
        return characterItems.Find(item => item == itemForCheck);
    }

    public void RemoveItem(Item item)
    {
        characterEquipmentItems.Remove(item);
    }
    public void RemoveItemByIndex(Item item)
    {
        Item itemToRemove = CheckForItem(item);
        if (itemToRemove != null)
        {
            characterItems.Remove(itemToRemove);
            Debug.Log("Item removed: " + itemToRemove.title);
        }
    }

    public bool IHaveFreeSpaceInInventory()
    {
        return characterItems.Count != sizeOfInventory;
    }
}
