using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class ManaManager : MonoBehaviour
{
    public float maxMana;
    public float currentMana;
    public Image manaBar;
    public TextMeshProUGUI manaText;
    public float manaPrice = 0;
    public void ManaUse(float manaCost)
    {
        if (currentMana - manaCost > 0)
        {
            currentMana -= manaCost;
            RefreshManaBar();
        }
    }
    public void RefreshManaBar()
    {
        Mathf.Clamp(currentMana, 0, maxMana);
        manaBar.fillAmount = currentMana / maxMana;
        manaText.text = currentMana + "/" + maxMana;
    }
}
