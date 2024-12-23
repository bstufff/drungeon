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
        // Met � jour la barre de mana et v�rifie qu'elle ne d�passe pas les limites
        Mathf.Clamp(CurrentMana, 0, MaxMana);
        _manaBar.fillAmount = CurrentMana / MaxMana;
        _manaText.text = CurrentMana + "/" + MaxMana;
    }
    public void ResetMana(float maxMana)
    {
        // R�nitialise la barre de mana
        this.MaxMana = maxMana;
        CurrentMana = maxMana;
        RefreshManaBar();
    }
}
