using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
/*    [Header("Название окон, которые используются")]
    public string inventoryName = "Inventory";
    public string selectedItemName = "SelectedItem";
    public string tooltipName = "TooltipInventory";
    public string menuItemName = "Menu";

*/


    public Item item;
    public ItemScenePresenter itemScenePresenter;
    protected Image spriteImage;
    [SerializeField] protected UIItem selectedItem;
    [SerializeField] protected TooltipForInventory tooltip;
    [SerializeField] protected Inventory inventory;
    [SerializeField] protected MenuItem menuItem;

    public GameObject slot;
    public event Action Droping; 

    public UIItem SelectedItem { get => selectedItem; }

    protected void Awake()
    {
        spriteImage = GetComponent<Image>();
        
        UpdateItem(null);
        /*selectedItem = GameObject.Find(selectedItemName).GetComponent<UIItem>();
        tooltip = GameObject.Find(tooltipName).GetComponent<TooltipForInventory>();
        inventory = GameObject.Find(inventoryName).GetComponent<Inventory>();
        menuItem = GameObject.Find(menuItemName).GetComponent<MenuItem>();*/
        if(slot!=null)
            slot.transform.localScale = new Vector3(1, 1, 1); 
    }

    public void Initialization(SingletonInventory singletonInventory)
    {
        selectedItem = singletonInventory.SelectedItem;
        tooltip = singletonInventory.Tooltip;
        inventory = singletonInventory.Inventory;
        menuItem = singletonInventory.MenuItem;
    }
    public void UpdateItem(Item item)
    {
        this.item = item;
        if (this.item != null)
        {
            spriteImage.color = Color.white;
            spriteImage.sprite = this.item.icon;
            spriteImage.preserveAspect = true;
        }
        else
        {
            spriteImage.color = Color.clear;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Swap();
        }
        else if (eventData.button == PointerEventData.InputButton.Right && this.item != null && selectedItem.item == null)
        {
            ClickOnRightButton();
        }      
    }

    protected void ClickOnRightButton()
    {
        tooltip.gameObject.SetActive(false);
        menuItem.gameObject.SetActive(true);
        menuItem.ActiveMenu(transform.position.x, transform.position.y, this);
    }

    public void DropItem()
    {
        ItemScenePresenter itemScene = Instantiate<ItemScenePresenter>(itemScenePresenter, new Vector2(0, 0), new Quaternion());
        itemScene.CreateItemOnScene(this.item);
        inventory.RemoveItemByIndex(item);
        UpdateItem(null);
    }

    protected void Swap()
    {
        if (this.item != null)
        {
            if (selectedItem.item != null)
            {

                Item clone = (Item) ScriptableObject.CreateInstance(typeof(Item));
                clone.Clone(selectedItem.item);
                selectedItem.UpdateItem(this.item);
                UpdateItem(clone);
            }
            else
            {
                selectedItem.UpdateItem(this.item);
                UpdateItem(null);
            }
        }
        else if (selectedItem.item != null)
        {
            UpdateItem(selectedItem.item);
            selectedItem.UpdateItem(null);
        }
    }


    public void OnPointerExit(PointerEventData eventData)
    {
            tooltip.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.item != null)
        {
            tooltip.GenerateToolTip(this.item);
        }
    }
}
