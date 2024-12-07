using System.Collections;
using UnityEngine;

public class BlizzardScript : Spell
{
    [SerializeField] private float _destroyTimer = 10f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ajoute un effet de givre et r�duit la vitesse de l'ennemi
        other.gameObject.AddComponent<FreezingStatus>();
        other.gameObject.GetComponent<EnemyMovement>().CurrentSpeed /= 2;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        // Enl�ve les effets de givre et r�tablit la vitesse de l'ennemi
        Destroy(other.gameObject.GetComponent<FreezingStatus>());
        EnemyMovement otherMovementScript = other.gameObject.GetComponent<EnemyMovement>();
        otherMovementScript.CurrentSpeed = otherMovementScript.BaseSpeed;
    }

    public override float ManaCost => 30; // D�finit le c�ut du sort
    public override void PlaceSpell(ManaManager manaManager)
    {
        base.PlaceSpell(manaManager); // Comportement de base du sort

        // Comportement unique au sort
        Destroy(gameObject, _destroyTimer);
    }
}
