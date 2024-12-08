using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellManager : MonoBehaviour
{
    [SerializeField] private Transform _parentSpellObject;
    [SerializeField] private SpellFactory _spellFactory;
    [SerializeField] private ManaManager _manager;
    [SerializeField] private GameObject[] _spellButtons;
    private GameObject[] _spellSelection; 
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
                Destroy(activeSpell);
                activeSpell = null;
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

    public GameObject[] SpellSelection {
        get {
            // Utilise une liste temporaire pour stocker les boutons
            List<GameObject> interactableButtons = new List<GameObject>();

            foreach (GameObject obj in _spellButtons)
            {
                // V�rifie que l'objet est un bouton
                Button button = obj.GetComponent<Button>();
                if (button != null & !button.interactable)
                {
                    // Ajoute � la liste les boutons avec qui on peut interagir
                    interactableButtons.Add(obj);
                }
            }

            // Retourne la liste convertie en tableau
            return interactableButtons.ToArray();
        }
        set
        {
            foreach (GameObject button in _spellButtons)
            {
                if (Array.Exists(_spellButtons, obj => obj == button))
                {
                    button.SetActive(true);
                }
                _spellSelection = value;

            }
        }
    }

}
