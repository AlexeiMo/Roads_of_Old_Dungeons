using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{

    [SerializeField] private Image _healthBar;
    private float current;
    private float maxValue { get; set; }

    public void Start()
    {
        maxValue = 1;
        current = 1;
    }

    public float currentValue
    {
        get { return current; }
    }

    void Update()
    {
        
        if (current < 0)
            current = 0;
        if (current > maxValue)
            current = maxValue;
        _healthBar.gameObject.transform.localScale = new Vector3( current*1,1,1);
    }
    public  void AdjustCurrentValue(float adjust)
    {
        current += adjust;
    }
}