using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemProps : MonoBehaviour , IPointerClickHandler
{
    //Script for the item GameObject itself and how it manages all the commands.
    public Item MyItem;
    public int MyPlaceInHome;
    public Image MyImage;
    Text CounterText;
    HoverManager AccHM;
    InventoryManager AccInv;
    ShopManager AccShop;
    EquipmentManager AccEq;
    ItemScene ItemScene;
    Player player;

    public Action DropItemAction;

    public ItemHome MyHome;


    void Start () 
	{
        //loads neccessary Managers
        if (GameObject.Find("ItemHoverer") != null)
        {
            AccHM = GameObject.Find("ItemHoverer").GetComponent<HoverManager>();
        }
        if (GameObject.Find("InventoryWindow") != null)
        {
            AccInv = GameObject.Find("InventoryWindow").GetComponent<InventoryManager>();
        }
        if (GameObject.Find("ShopWindow") != null)
        {
            AccShop = GameObject.Find("ShopWindow").GetComponent<ShopManager>();
        }
        if (GameObject.Find("EquipmentWindow") != null)
        {
            AccEq = GameObject.Find("EquipmentWindow").GetComponent<EquipmentManager>();
        }
        if (GameObject.Find("ItemScene").GetComponent<ItemScene>()!=null)
        {
            ItemScene = GameObject.Find("ItemScene").GetComponent<ItemScene>();
        }
        if (GameObject.Find("Player").GetComponent<Player>() != null)
        {
            player = GameObject.Find("Player").GetComponent<Player>();
        }
    }



    public void TakeInfo(Item TheItem, int ThePlaceInInventroy, ItemHome TheHome)//When the item is created, it takes these paramets to determine its function and to determine where it is placed (inventory or shop, etc)
    {
        MyItem = TheItem;
        MyImage = transform.Find("ItemSpriteIcon"). GetComponent<Image>();
        CounterText = transform.Find("ItemCounter").GetComponent<Text>();
        CounterText.text = "";
        MyPlaceInHome = ThePlaceInInventroy;
        MyHome = TheHome;
        
        MyImage.sprite = TheItem.itemIcon;

    }

    public void ChangeStacks(int NumberOfStacks)
    {
        if (NumberOfStacks > 1)
        {
            CounterText.text = NumberOfStacks.ToString();
        }
        else
        {
            CounterText.text = "";
        }
    }

    public void MouseOn()//When hovering over the item
    {
        if (AccHM != null)
        {
            AccHM.CallHoverer(transform.position, this);
        }
    }

    public void MouseOut()
    {
        if (AccHM != null)
            AccHM.HideTooltip();
    }

    public void OnPointerClick(PointerEventData eventData) // when the mouse is clicked
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            MouseClick();
        }

    }

    public void MouseClick()
    {

        if (MyHome == ItemHome.Inventory) //if the item is in the inventory
        {
            if (ShopManager.IsSellMode)
            {
                FromInventoryToSell();
            }
            else if (InventoryManager.isDrop)
            {
                DropFromInventory();
            }
            else if (MyItem.itemType == ItemType.Consumable)
            {
                ConsumeItem();
            }
            else if (MyItem.itemType == ItemType.Gear)
            {
                FromInventoryToEquip();
            }
           
        }
        else if (MyHome == ItemHome.PlayerBuyTab)//if the item is in the shop
        {
            FromBuyToInventory();
        }
        else if (MyHome == ItemHome.Equiped)//if the item is in Equipment inventory
        {
            FromEquipToInventory();
        }

    }

    void ConsumeItem()//when the item is consumed (for cosumable items)
    {
        if (AccInv != null)
        {
            Debug.Log("ITEM CONSUMED");//Do the consuming function instead of this Debug.Log
            ErrorMessageText.instance.ShowMessage("item is consumed");

            bool IsLastStack = false;
            if (MyItem.IsStackable)
            {
                if (AccInv.ReduceStackableSize(MyPlaceInHome))
                {
                    IsLastStack = true;
                }
            }

            if (MyItem.IsStackable == false || IsLastStack)
            {
                //Give player money for selling the item (the sell price is MyItem.SellPrice) .. Example: playerData.instance.AddGold(MyItem.SellPrice);
                AccInv.RemoveItemFromInventory(MyPlaceInHome);
                DestroyItem();
            }
        }
    }

    void FromBuyToInventory()
    {
        if (AccShop != null)
        {
            if (AccShop.BuyFromShop(MyPlaceInHome))//If the player can buy this item
            {
                DestroyItem();
            }
        }
    }

    void FromEquipToInventory()
    {
        if (AccInv != null && AccEq != null)
        {
            if (AccInv.AddItemToInventory(MyItem))//If the player can add this item to the inventroy (example: enough space in Inventory)
            {
                AccEq.RemoveItemFromEquipmentInventory(MyItem.gearMainType);
                DestroyItem();
            }
        }
    }


    void FromInventoryToEquip()
    {
        if (AccEq != null)
        {
            if (AccEq.AddItemToEquipmentInventory(this))//If the player can add this item to Equipment inventory
            {
                DestroyItem();
            }
        }
    }

    void FromInventoryToSell()
    {
        if (AccInv!=null)
        {
            bool IsLastStack = false;
            if (MyItem.IsStackable)
            {
                ShopManager.TestScenePlayerGold += MyItem.SellPrice;
                if (AccInv.ReduceStackableSize(MyPlaceInHome))
                {
                    IsLastStack = true;
                }
            }

            if (MyItem.IsStackable == false || IsLastStack)
            {
                //Give player money for selling the item (the sell price is MyItem.SellPrice) .. Example: playerData.instance.AddGold(MyItem.SellPrice);
                if (IsLastStack == false)
                {
                    ShopManager.TestScenePlayerGold += MyItem.SellPrice;
                }
                AccInv.RemoveItemFromInventory(MyPlaceInHome);
                DestroyItem();
            }

        }
    }

    void DropFromInventory()
    {
        if (AccInv != null)
        {
            ItemScene item = Instantiate(ItemScene, new Vector2
                (player.gameObject.transform.position.x,player.gameObject.transform.position.y), new Quaternion());
            item.Present(MyItem);
            bool IsLastStack = false;
            if (MyItem.IsStackable)
            {
                if (AccInv.ReduceStackableSize(MyPlaceInHome))
                {
                    IsLastStack = true;
                }
            }

            if (MyItem.IsStackable == false || IsLastStack)
            {
                AccInv.RemoveItemFromInventory(MyPlaceInHome);
                DestroyItem();
            }

        }
    }
    public void DestroyItem()
    {
        if (AccHM != null)
        {
            AccHM.HideTooltip();
        }
        Destroy(gameObject);
    }

}

public enum ItemHome
{
    Inventory = 0, PlayerBuyTab = 1, Dropped = 2, Equiped = 3
}


