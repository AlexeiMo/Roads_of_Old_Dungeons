using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : UIItem, IPointerClickHandler/*, IPointerEnterHandler, IPointerExitHandler*/
{
    public GearMainType gearMainType;

    public event Action SwapingItem;

    public event Action UnSwapingItem;
     

    
    
    public new void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if (this.SelectedItem.item != null)
            {
                if (gearMainType == this.SelectedItem.item.gearMainType)
                {
                    UnSwapingItem?.Invoke();
                    Swap();
                    SwapingItem?.Invoke();
                }
            }
            else
            {
                UnSwapingItem?.Invoke();
                Swap();
                SwapingItem?.Invoke();
            }
        }
        else if(eventData.button == PointerEventData.InputButton.Right && this.item != null && selectedItem.item == null)
        {
            menuItem.ActiveEquipmentMenu(transform.position.x, transform.position.y, this);
            menuItem.gameObject.SetActive(true);
        }

    }

    public void RemoveItem()
    {
        UnSwapingItem?.Invoke();
        UpdateItem(null);
    }
/*    public void OnPointerEnter(PointerEventData eventData)
    {
        if(this.SelectedItem.item.gearMainType != this.gearMainType)
        {
            this.gameObject.SetActive(false);
        }
    }  
    
    public void OnPointerExit(PointerEventData eventData)
    {
        this.gameObject.SetActive(false);
    }*/
    
    
}
