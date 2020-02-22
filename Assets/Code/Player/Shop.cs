using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
   // [SerializeField] private GameObject Inventory;
    [SerializeField] private ShopManager shopManager;
    public void Start()
    {
        shopManager = GameObject.Find("ShopWindow").GetComponent<ShopManager>();        
        Invoke("Disable", 0.5f);
    }

    private void Disable()
    {
        shopManager.gameObject.SetActive(false);
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            shopManager.gameObject.SetActive(true);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            shopManager.gameObject.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            shopManager.gameObject.SetActive(false);
        }
    }



}
