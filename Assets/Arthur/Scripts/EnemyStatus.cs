using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public abstract class EnemyStatus : MonoBehaviour 
{
    public string StatusName;
    public bool isActive = false;
    public abstract IEnumerator Effect(float delay, float value);
}

public class BurningStatus : EnemyStatus
{
    float damage = 10f;
    float delay = 1f;
    private void OnEnable()
    {
        isActive = true;
        StartCoroutine(Effect(delay, damage));
    }
    public override IEnumerator Effect(float delay, float value)
    {
        while (isActive)
        {
            transform.GetComponent<HealthManager>().TakeDamage(value);
            yield return new WaitForSeconds(delay);
        }
    }
}
public class FreezingStatus : EnemyStatus
{
    float damage = 5f;
    float delay = 1f;
    private void OnEnable()
    {
        isActive = true;
        StartCoroutine(Effect(delay, damage));
    }
    public override IEnumerator Effect(float delay, float value)
    {
        while (isActive)
        {
            transform.GetComponent<HealthManager>().TakeDamage(value);
            yield return new WaitForSeconds(delay);
        }
    }
}