using UnityEngine;

public class SpellManager : MonoBehaviour
{
    [SerializeField] private Transform parentSpellObject;
    [SerializeField] private GameObject[] spells;
    private GameObject activeSpell; // Sort actuellement en train d'être placé
    private ManaManager manager;
    
    public void UseSpell(int spellIndex)
    {
        manager = FindAnyObjectByType<ManaManager>();
        if (activeSpell == null) // Si aucun sort est en train d'être placé
        {
            float activeSpellCost = spells[spellIndex].GetComponent<Spell>().ManaCost;
            if (manager.currentMana < activeSpellCost) 
            {
                Debug.Log("Not enough mana !");
                return; // Exit early
            }

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            activeSpell = Instantiate(spells[spellIndex].gameObject, mousePosition, transform.rotation, parentSpellObject); // Créée l'instance du sort à placer
            activeSpell.SetActive(true);
        }
        else
        {
        }
    }

    public void DestroyAllSpells() 
    {
        foreach (Transform spell in parentSpellObject)
        {
            Destroy(spell.gameObject);
        }
    }

    void Update()
    {
        if (activeSpell != null)  // Si un sort est en train d'être placé
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = -5;
            activeSpell.transform.position = mousePosition;
            if (Input.GetMouseButtonDown(0))
            {
                activeSpell.GetComponent<Spell>().PlaceSpell(manager);
                activeSpell = null;
            }
        }

    }
}
public class Spell : MonoBehaviour
{
    public virtual float ManaCost { get; } = 10f; // Default mana cost
    public virtual void PlaceSpell(ManaManager manaManager)
    {
        // Base behavior that is used by every spell
        GetComponent<Collider2D>().enabled = true;
        manaManager.ManaUse(ManaCost);
    }
}
