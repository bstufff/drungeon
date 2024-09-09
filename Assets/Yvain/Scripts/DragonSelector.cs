using UnityEditor;
using UnityEngine;

public class DragonSelector : MonoBehaviour
{
    public GameObject redDragon;
    public GameObject blueDragon;
    public GameObject whiteDragon;
    public GameObject redCone;
    public GameObject blueLine;
    public GameObject whiteCircle;
    
    bool started = false;
    public void StartTheGame()
    {
        started = true;
    }
    private void Start()
    {
        redDragon.SetActive(true);
        blueDragon.SetActive(false);
        whiteDragon.SetActive(false);
        redCone.SetActive(true);
        blueLine.SetActive(false);
        whiteCircle.SetActive(false);
    }
    void Update()
    {
        if (started)
        {
            if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                redDragon.SetActive(true);
                blueDragon.SetActive(false);
                whiteDragon.SetActive(false);
                redCone.SetActive(true);
                blueLine.SetActive(false);
                whiteCircle.SetActive(false);
            }
            if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                redDragon.SetActive(false);
                blueDragon.SetActive(true);
                whiteDragon.SetActive(false);
                redCone.SetActive(false);
                blueLine.SetActive(true);
                whiteCircle.SetActive(false);
            }
            if (Input.GetKeyUp(KeyCode.Alpha3))
            {
                redDragon.SetActive(false);
                blueDragon.SetActive(false);
                whiteDragon.SetActive(true);
                redCone.SetActive(false);
                blueLine.SetActive(false);
                whiteCircle.SetActive(true);
            }
        }
    }
}
