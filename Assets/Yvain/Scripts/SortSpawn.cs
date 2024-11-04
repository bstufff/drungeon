using UnityEditor;
using UnityEngine;

public class SortSpawn : MonoBehaviour
{
    public GameObject spell;
    private bool freezeSpell = false;
    private bool canSpawnSpell = true;
    public void SpellSelect()
    {
        if(canSpawnSpell)
        {
            Instantiate(spell);
            spell.SetActive(true);
        }
    }
    
    void Update()
    {
        if (!freezeSpell)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = -5;
            spell.transform.position = mousePosition;
            canSpawnSpell = false;
            if (Input.GetMouseButtonDown(0))
            {
                freezeSpell = true;
                canSpawnSpell = true;
            }
        }
        
    }
    
}
