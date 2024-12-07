using UnityEngine;

public class Spell : MonoBehaviour
{
    public virtual float ManaCost { get; } = 10f; // Default mana cost
    public virtual void PlaceSpell(ManaManager manaManager)
    {
        // Base behavior that is used by every spell
        GetComponent<Collider2D>().enabled = true;
        manaManager.ManaUse(ManaCost);
    }
}
