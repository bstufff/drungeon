using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ManaManager : MonoBehaviour
{
    public float maxMana;
    public float currentMana;
    public GameObject manaBar;
    public float manaPrice = 0;
    void Start()
    {
        currentMana = maxMana;
    }
    void Update()
    {
        manaBar.GetComponent<Image>().fillAmount = currentMana;
    }
    public void ManaUse(float manaCost)
    {
        manaPrice = manaCost / 10;
        currentMana-= manaPrice;
    }
}
