using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class FlameScript : Spell
{
    [SerializeField] private float destroyTimer = 10f;
    public override float ManaCost => 25;
    public override void PlaceSpell(ManaManager manaManager)
    {
        base.PlaceSpell(manaManager);
        Destroy(gameObject, destroyTimer);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.AddComponent<BurningStatus>();
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        Destroy(other.gameObject.GetComponent<BurningStatus>());
    }
}
