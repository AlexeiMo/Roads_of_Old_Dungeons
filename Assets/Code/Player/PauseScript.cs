
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{

    [SerializeField] private GameObject _pauseMenu;

    public void resume()
    {
        Time.timeScale = 1;
        _pauseMenu.SetActive(false);
    }

    public void exit(string OtherSceneName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(OtherSceneName);
    }
}

