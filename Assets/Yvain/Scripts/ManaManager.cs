using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class ManaManager : MonoBehaviour
{
    public float maxMana;
    public float CurrentMana;
    
    [SerializeField] private Image _manaBar;
    [SerializeField] private TextMeshProUGUI _manaText;
    public void ManaUse(float manaCost)
    {
        if (CurrentMana - manaCost > 0)
        {
            CurrentMana -= manaCost;
            RefreshManaBar();
        }
    }
    public void RefreshManaBar()
    {
        // Met à jour la barre de mana et vérifie qu'elle ne dépasse pas les limites
        Mathf.Clamp(CurrentMana, 0, maxMana);
        _manaBar.fillAmount = CurrentMana / maxMana;
        _manaText.text = CurrentMana + "/" + maxMana;
    }
    public void ResetMana(float maxMana)
    {
        // Rénitialise la barre de mana
        this.maxMana = maxMana;
        CurrentMana = maxMana;
        RefreshManaBar();
    }
}
