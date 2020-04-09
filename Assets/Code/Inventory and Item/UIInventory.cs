using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public List<UIItem> uIItems = new List<UIItem>();
    public GameObject slotPrefab;
    public Transform slopPanel;
    public int NumberOfSlots = 16;
    public SingletonInventory singletonInventory;

    public event Action Enabling;
   // public event Action Disabling;

/*    private void Awake()
    {
        for(int i=0;i<NumberOfSlots; i++)
        {
            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(slopPanel);
            uIItems.Add(instance.GetComponentInChildren<UIItem>());

        }
    }*/

    private void OnEnable()
    {
        for (int i = 0; i < NumberOfSlots; i++)
        {
            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(slopPanel);
            instance.transform.localScale = new Vector3(1, 1, 1);
            UIItem tempUIItem = instance.GetComponentInChildren<UIItem>();
            tempUIItem.Initialization(singletonInventory);
            uIItems.Add(tempUIItem);         
        }
        Cursor.visible=true;   
        Enabling?.Invoke();
    }

    private void OnDisable()
    {
        foreach(UIItem item in uIItems)
        {
            Destroy(item.slot);
        }
        uIItems.Clear();
        Cursor.visible = false;
    }

    public void UpdateSlot(int slot, Item item)
    {
        uIItems[slot].UpdateItem(item);
    }

    public void AddNewItem(Item item)
    {
        UpdateSlot(uIItems.FindIndex(i => i.item == null), item);
    }

    public void RemoveItem(Item item)
    {
        UpdateSlot(uIItems.FindIndex(i => i.item == item), null);
    }
}
