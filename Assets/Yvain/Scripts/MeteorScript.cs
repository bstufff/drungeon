using System.Collections;
using UnityEngine;

public class MeteorScript : MonoBehaviour, ISpell
{
    private float dmg = 100f;
    private float delay = 0.3f;
    [SerializeField] private float destroyTimer = 10f;
    public void InitializeSpell()
    {
        Destroy(gameObject, destroyTimer);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(DealDamage(other.gameObject.GetComponent<HealthManager>()));
    }
    IEnumerator DealDamage(HealthManager target)
    {
        target.TakeDamage(dmg);
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
