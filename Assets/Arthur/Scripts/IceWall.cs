using UnityEngine;

public class IceWall : MonoBehaviour, ISpell
{
    [SerializeField] private float destroyTimer = 10f;
    public void InitializeSpell()
    {
        Destroy(gameObject, destroyTimer);
    }
}
