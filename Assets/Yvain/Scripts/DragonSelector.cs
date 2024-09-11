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

    public int choice = 1;

    public void AddRightChoice()
    {
        choice++;
    }
    public void AddLeftChoice() 
    {
        choice--;
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
        if(choice < 1)
        {
            choice = 3;
        }
        else if(choice > 3)
        {
            choice = 1;
        }

        if (choice == 1)
        {
            redDragon.SetActive(true);
            blueDragon.SetActive(false);
            whiteDragon.SetActive(false);
            redCone.SetActive(true);
            blueLine.SetActive(false);
            whiteCircle.SetActive(false);
        }
        if (choice == 2)
        {
            redDragon.SetActive(false);
            blueDragon.SetActive(true);
            whiteDragon.SetActive(false);
            redCone.SetActive(false);
            blueLine.SetActive(true);
            whiteCircle.SetActive(false);
        }
        if (choice == 3)
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
