using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class FlameScript : Spell
{
    [SerializeField] private float _destroyTimer = 10f;
    public override float ManaCost => 25;
    public override void PlaceSpell(ManaManager manaManager)
    {
        base.PlaceSpell(manaManager); // Comportement de base du sort

        // Comportement unique au sort
        Destroy(gameObject, _destroyTimer);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.AddComponent<BurningStatus>();
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Destroy(other.gameObject.GetComponent<BurningStatus>());
    }
}
