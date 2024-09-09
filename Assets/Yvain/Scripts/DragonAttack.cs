using UnityEngine;

public class DragonAttack : MonoBehaviour
{
    public GameObject DragonFlame;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            DragonFlame.SetActive(true);
        }
        else
        {
            DragonFlame.SetActive(false);
        }
    }
}
