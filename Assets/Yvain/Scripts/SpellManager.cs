using UnityEngine;

public class SpellManager : MonoBehaviour
{
    [SerializeField] private Transform parentSpellObject;
    [SerializeField] private GameObject[] spells;
    private GameObject activeSpell; // Sort actuellement en train d'être placé
    public void SelectSpell(int spellIndex)
    {
        ManaManager manager = FindAnyObjectByType<ManaManager>();
        if (activeSpell == null && manager.currentMana >= manager.manaPrice) // Si aucun sort est en train d'être placé
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            activeSpell = Instantiate(spells[spellIndex], mousePosition, transform.rotation, parentSpellObject); // Créée l'instance du sort à placer
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
                activeSpell.GetComponent<ISpell>().InitializeSpell(); // Active le sort
                activeSpell.GetComponent<Collider2D>().enabled = true;
                activeSpell = null;
            }
        }

    }
}
