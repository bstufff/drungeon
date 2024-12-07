using UnityEngine;

public class SpellManager : MonoBehaviour
{
    [SerializeField] private Transform _parentSpellObject;
    [SerializeField] private SpellFactory _spellFactory;
    [SerializeField] private ManaManager _manager;
    private GameObject activeSpell; // Sort actuellement en train d'�tre plac�

    void Update()
    {
        if (activeSpell != null)  // Si un sort est en train d'�tre plac�
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = -5;
            activeSpell.transform.position = mousePosition;

            if (Input.GetMouseButtonDown(0)) // Place le sort
            {
                activeSpell.GetComponent<Spell>().PlaceSpell(_manager);
                activeSpell = null;
            }
            if (Input.GetKeyDown(KeyCode.Escape)) // Supprime le sort 
            {
                Destroy(activeSpell);
                activeSpell = null;
            }
        }

    }
    public void UseSpell(int spellIndex)
    {
        if (activeSpell == null) // V�rifie qu'aucun sort est en train d'�tre plac�
        {
            SpellType spellType = (SpellType)spellIndex;

            // V�rifie que le joueur a assez de mana pour lancer le sort
            float manaCost = _spellFactory.GetManaCost(spellType);
            if (_manager.CurrentMana < manaCost) 
            {
                Debug.Log("Not enough mana !");
                return; 
            }

            // D�place le sort jusqu'� la souris
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            // Utilise l'usine � spells pour cr�er le sort
            activeSpell = _spellFactory.CreateSpell(spellType, mousePosition, _parentSpellObject);
            activeSpell.SetActive(true);
        }
    }

    public void DestroyAllSpells() 
    {
        // Supprime tous les sorts pr�sents dans la sc�ne
        foreach (Transform spell in _parentSpellObject)
        {
            Destroy(spell.gameObject);
        }
    }

}
