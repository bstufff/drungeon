using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public abstract class EnemyStatus : MonoBehaviour 
{
    // Classe parent pour les statuts d'un ennemi
    public string StatusName;
    public bool isActive = false;
    public abstract IEnumerator Effect(float delay, float value); 
}


// État causé par la zone de feu (FlameArea)
public class BurningStatus : EnemyStatus
{
    float _damage = 10f;
    float _delay = 1f;
    private void OnEnable()
    {
        isActive = true;
        StartCoroutine(Effect(_delay, _damage));
    }
    public override IEnumerator Effect(float delay, float value)
    {
        while (isActive) // Effectue des dégats uniquement si le sort est placé
        {
            // Applique les dégats et crée un délai
            transform.GetComponent<HealthManager>().TakeDamage(value);
            yield return new WaitForSeconds(delay);
        }
    }
}
// État causé par le blizzard
public class FreezingStatus : EnemyStatus
{
    float _damage = 5f;
    float _delay = 1f;
    private void OnEnable()
    {
        isActive = true;
        StartCoroutine(Effect(_delay, _damage));
    }
    public override IEnumerator Effect(float delay, float value)
    {
        while (isActive) // Effectue des dégats uniquement si le sort est placé
        {
            // Applique les dégats et crée un délai
            transform.GetComponent<HealthManager>().TakeDamage(value);
            yield return new WaitForSeconds(delay);
        }
    }
}