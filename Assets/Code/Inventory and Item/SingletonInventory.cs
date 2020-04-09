using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonInventory : MonoBehaviour
{
    [SerializeField] private UIItem selectedItem;
    [SerializeField] private TooltipForInventory tooltip;
    [SerializeField] private Inventory inventory;
    [SerializeField] private MenuItem menuItem;

    public UIItem SelectedItem { get => selectedItem; }
    public TooltipForInventory Tooltip { get => tooltip; }
    public Inventory Inventory { get => inventory;  }
    public MenuItem MenuItem { get => menuItem;  }
}
