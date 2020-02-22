using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    private GameObject _pauseMenu; 

    void Start()
    {
        _pauseMenu = GameObject.Find("PauseMenu");
        _pauseMenu.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_pauseMenu.activeSelf)
            {
                Time.timeScale = 0;
                _pauseMenu.SetActive(true);
            }
            else
            {
                _pauseMenu.SetActive(false);
                Time.timeScale = 1;
            }
                
        }        
    }
}
