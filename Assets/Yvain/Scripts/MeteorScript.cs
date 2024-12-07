using System.Collections;
using UnityEngine;

public class MeteorScript : Spell
{
    [SerializeField] private float _dmg = 100f;
    [SerializeField] private float _delay = 0.3f;
    [SerializeField] private float _destroyTimer = 10f;

    public override float ManaCost => 35;
    public override void PlaceSpell(ManaManager manaManager)
    {
        base.PlaceSpell(manaManager); // Comportement de base du sort

        // Comportement unique au sort
        Destroy(gameObject, _destroyTimer);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        // Applique des dommages au premier ennemi qui touche le sort
        if (other.gameObject.CompareTag("Enemy") && gameObject.activeSelf)
            StartCoroutine(DealDamage(other.gameObject.GetComponent<HealthManager>()));
    }
    IEnumerator DealDamage(HealthManager target)
    {
        // Désactive 
        target.TakeDamage(_dmg);
        yield return new WaitForSeconds(_delay);
        gameObject.SetActive(false); // Désactive le sort qui sera ensuite détruit
    }
}
