using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TextChar : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Text textValue;

    [SerializeField] TooltipForInventory tooltip;

    [Header("If Misc/Consumable")]
    [TextArea(3, 10)]

    [SerializeField] string tooltipText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.GenerateToolTip(tooltipText);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.gameObject.SetActive(false);
    }

    public void UpdateValue(string newValue)
    {
        textValue.text = newValue;
    }

    
}
