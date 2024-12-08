using UnityEngine;

public class IceWall : Spell
{
    [SerializeField] private float _destroyTimer = 10f;
    public override float ManaCost => 75; // D�finit le c�ut du sort
    public override void PlaceSpell(ManaManager manaManager)
    {
        base.PlaceSpell(manaManager); // Comportement de base du sort

        // Comportement unique au sort
        Destroy(gameObject, _destroyTimer); 
    }
}
