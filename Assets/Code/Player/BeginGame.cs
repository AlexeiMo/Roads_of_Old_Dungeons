using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BeginGame : MonoBehaviour
{
    public void begin(string OtherSceneName)
    {
        SceneManager.LoadScene(OtherSceneName);
    }

    public void exit()
    {
        Application.Quit();
    }

}
