using System.Collections;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuItem : MonoBehaviour, IPointerExitHandler
{
    public Equipment equipment;
    public Inventory Inventory;
    private UIItem uItem;
    [SerializeField] private Text textUse;
    [SerializeField] private Text textDrop;

    private bool isEquipmentMenu = false;
    

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void ActiveMenu(float x, float y, UIItem uIItem)
    {
        this.uItem = uIItem;
        if(uIItem.item.itemType == ItemType.Misc)
        {
            textUse.text = "";
        }
        else if(uIItem.item.itemType == ItemType.Consumable)
        {
            textUse.text = "ИСПОЛЬЗОВАТЬ";
        }
        else
        {
            textUse.text = "НАДЕТЬ";
        }
        gameObject.transform.position = new Vector2(x, y);
        isEquipmentMenu = false;
    }


    public void ActiveEquipmentMenu(float x, float y, EquipmentSlot equipmentSlot)
    {
        textUse.text = "СНЯТЬ";
        gameObject.transform.position = new Vector2(x, y);
        uItem = equipmentSlot;
        isEquipmentMenu = true;     
    }

    public void UseItem()
    {
        if (!isEquipmentMenu)
        {
            if (uItem.item.itemType == ItemType.Gear)
            {
                equipment.EquipItem(uItem);
            }
        }
        else
        {
            UseEquipmentMenu();
        }
        gameObject.SetActive(false);
    }


    private void UseEquipmentMenu() 
    {
        if (Inventory.IHaveFreeSpaceInInventory())
        {
            Inventory.UnEquipmentItem(uItem.item);
            EquipmentSlot equipmentSlot = (EquipmentSlot)uItem;
            equipmentSlot.RemoveItem();
         
        }
    }

    public void DropItem()
    {
        uItem.DropItem();
        gameObject.SetActive(false);
    }



    public void OnPointerExit(PointerEventData eventData)
    {
        uItem = null;
        gameObject.SetActive(false);
    }
}
