using System.Collections;
using UnityEngine;

public class FlameScript : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.AddComponent<BurningStatus>();
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        Destroy(other.gameObject.GetComponent<BurningStatus>());
    }
}
