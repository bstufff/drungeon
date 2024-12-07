using UnityEngine;

public class Spell : MonoBehaviour
{
    public virtual float ManaCost { get; } = 10f; // Coût par défaut d'un sort
    public virtual void PlaceSpell(ManaManager manaManager)
    {
        // Fonctionnement de base utilisé par tous les sorts
        GetComponent<Collider2D>().enabled = true;
        manaManager.ManaUse(ManaCost);
    }
}
