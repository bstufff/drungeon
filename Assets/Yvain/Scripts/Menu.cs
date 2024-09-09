using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject menuUI;
    public void StartGame()
    {
        menuUI.SetActive(false);
    }

    public void QuitGame()
    {   
        Application.Quit();
    }

}
