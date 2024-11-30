using System.Collections;
using UnityEngine;

public class BlizzardScript : Spell
{
    [SerializeField] private float destroyTimer = 10f;
    public override float ManaCost => 30;
    public override void PlaceSpell(ManaManager manaManager)
    {
        base.PlaceSpell(manaManager);
        Destroy(gameObject, destroyTimer);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.AddComponent<FreezingStatus>();
        other.gameObject.GetComponent<Enemy>().speed /= 2;
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        Destroy(other.gameObject.GetComponent<FreezingStatus>());
        Enemy otherEnemyScript = other.gameObject.GetComponent<Enemy>();
        otherEnemyScript.speed = otherEnemyScript.baseSpeed;
    }
}
