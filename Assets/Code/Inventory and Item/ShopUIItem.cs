using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopUIItem : UIItem, IPointerClickHandler
{

    public bool isShopCell=false;
    public new void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            if (!isShopCell)
            {
                inventory.SellItem(this);
            }
            else
            {
                inventory.BuyItem(this);
            }
        }
    }

    
}
