using UnityEngine;

public class IceWall : Spell
{
    [SerializeField] private float destroyTimer = 10f;
    public override float ManaCost => 75;
    public override void PlaceSpell(ManaManager manaManager)
    {
        base.PlaceSpell(manaManager);
        Destroy(gameObject, destroyTimer);
    }
}
