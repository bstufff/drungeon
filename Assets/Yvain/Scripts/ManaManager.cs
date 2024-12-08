using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class ManaManager : MonoBehaviour
{
    public float MaxMana;
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
        Mathf.Clamp(CurrentMana, 0, MaxMana);
        _manaBar.fillAmount = CurrentMana / MaxMana;
        _manaText.text = CurrentMana + "/" + MaxMana;
    }
    public void ResetMana(float maxMana)
    {
        // Rénitialise la barre de mana
        this.MaxMana = maxMana;
        CurrentMana = maxMana;
        RefreshManaBar();
    }
}
