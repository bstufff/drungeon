using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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
            StartCoroutine(DealDamage(other.gameObject));
    }
    private IEnumerator DealDamage(GameObject target)
    {
        // Effectue des dégâts
        target.TryGetComponent(out HealthManager health);
        health.TakeDamage(_dmg);

        yield return new WaitForSeconds(_delay); // Applique un délai
        gameObject.SetActive(false); // Se désactive après utilisation
    }
}
