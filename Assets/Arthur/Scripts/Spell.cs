using UnityEngine;

public class Spell : MonoBehaviour
{
    public virtual float ManaCost { get; } = 10f; // Co�t par d�faut d'un sort
    public virtual void PlaceSpell(ManaManager manaManager)
    {
        // Fonctionnement de base utilis� par tous les sorts
        GetComponent<Collider2D>().enabled = true;
        manaManager.ManaUse(ManaCost);
    }
}
