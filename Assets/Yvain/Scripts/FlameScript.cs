using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class FlameScript : MonoBehaviour, ISpell
{
    [SerializeField] private float destroyTimer = 10f;
    public void InitializeSpell()
    {
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
