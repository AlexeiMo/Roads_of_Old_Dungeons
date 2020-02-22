using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryContoller : MonoBehaviour
{

    private GameObject inventoty;
    // Start is called before the first frame update
    void Start()
    {
        inventoty = GameObject.Find("Inventory");
        Invoke("disable", 0.1f);
       
    }
    private void disable()
    {
        inventoty.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!inventoty.activeSelf)
                inventoty.gameObject.SetActive(true);
            else
                inventoty.gameObject.SetActive(false);
        }
    }
}
