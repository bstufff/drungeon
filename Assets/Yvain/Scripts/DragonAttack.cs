using UnityEngine;

public class DragonAttack : MonoBehaviour
{
    public GameObject dragonFlame;

    bool started = false;
    public void Select()
    {
        started = true;
    }
    private void Start()
    {
        dragonFlame.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                dragonFlame.SetActive(true);
            }
            else
            {
                dragonFlame.SetActive(false);
            }
        }
    }
}
