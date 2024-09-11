using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject startButton;
    public GameObject quitButton;
    public GameObject leftButton;
    public GameObject rightButton;
    public GameObject selectButton;
    void Start()
    {
        startButton.SetActive(true);
        quitButton.SetActive(true);
        leftButton.SetActive(false);
        rightButton.SetActive(false);
        selectButton.SetActive(false);
    }
    public void StartGame()
    {
        startButton.SetActive(false);
        quitButton.SetActive(false);
        leftButton.SetActive(true);
        rightButton.SetActive(true);
        selectButton.SetActive(true);
    }
    public void Select()
    {
        leftButton.SetActive(false);
        rightButton.SetActive(false);
        selectButton.SetActive(false);
    }
    public void QuitGame()
    {   
        Application.Quit();
    }

}
