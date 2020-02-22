using UnityEngine;

public class ItemScene : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    // private Inventory inventory;
    private bool isPick = false;
    [SerializeField] private InventoryManager inventoryManager;
    private Item item;





    public void Present(Item item)
    {      
        this.item = item;
        _renderer.sprite = item.itemIcon;
        // _renderer.name = item.itemName;
        gameObject.tag = "Item";
        // this.item = item;
    }

    public void PickUp()
    {
        // inventory.PickUpItem(item);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!isPick)
        if(collision.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            if (inventoryManager.isActiveAndEnabled)
            {
                    inventoryManager.AddItemToInventory(item);
                    isPick = true;
                    Destroy(gameObject);
            }      
        }
    }

}
