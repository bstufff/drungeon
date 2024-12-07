using UnityEngine;

public class SpellManager : MonoBehaviour
{
    [SerializeField] private Transform parentSpellObject;
    [SerializeField] private SpellFactory spellFactory;
    [SerializeField] private ManaManager manager;
    private GameObject activeSpell; // Sort actuellement en train d'être placé
    public void UseSpell(int spellIndex)
    {
        if (activeSpell == null) // Si aucun sort est en train d'être placé
        {
            SpellType spellType = (SpellType)spellIndex;
            float manaCost = spellFactory.GetManaCost(spellType);
            if (manager.currentMana <= manaCost)
            {
                Debug.Log("Not enough mana !");
                return; // Exit early
            }

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            activeSpell = spellFactory.CreateSpell(spellType, mousePosition, parentSpellObject);
            activeSpell.SetActive(true);
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
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Destroy(activeSpell);
                activeSpell = null;
            }
        }

    }
}
