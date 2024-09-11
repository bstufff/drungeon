using UnityEngine;

public class DragonAttack : MonoBehaviour
{
    public GameObject dragonFlame;
    public GameObject dragonThunder;
    public GameObject dragonShockwave;

    bool started = false;
    public int choice = 1;

    public void AddRightChoice()
    {
        choice++;
    }
    public void AddLeftChoice()
    {
        choice--;
    }
    public void Select()
    {
        started = true;
    }
    private void Start()
    {
        dragonFlame.SetActive(false);
        dragonThunder.SetActive(false);
        dragonShockwave.SetActive(false);
    }
    void Update()
    {
        if (choice < 1)
        {
            choice = 3;
        }
        else if (choice > 3)
        {
            choice = 1;
        }

        if (started)
        {
            if (Input.GetMouseButton(0) && choice == 1)
            {
                dragonFlame.SetActive(true);
            }
            else if(Input.GetMouseButton(0) && choice == 2)
            {
                dragonThunder.SetActive(true);
            }
            else if(Input.GetMouseButton(0) && choice == 3)
            {
                dragonShockwave.SetActive(true);
            }
            else
            {
                dragonFlame.SetActive(false);
                dragonThunder.SetActive(false);
                dragonShockwave.SetActive(false);
            }
        }
    }
}
