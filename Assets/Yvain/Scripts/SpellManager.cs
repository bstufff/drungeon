using System;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;

public class SpellManager : MonoBehaviour
{
    [SerializeField] private Transform _parentSpellObject;
    [SerializeField] private SpellFactory _spellFactory;
    [SerializeField] private ManaManager _manager;
    [SerializeField] private GameObject[] _spellButtons;
    [SerializeField] private GameObject[] _spellSelection; 
    private GameObject activeSpell; // Sort actuellement en train d'être placé

    void Update()
    {
        if (activeSpell != null)  // Si un sort est en train d'être placé
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
        if (activeSpell == null) // Vérifie qu'aucun sort est en train d'être placé
        {
            SpellType spellType = (SpellType)spellIndex;

            // Vérifie que le joueur a assez de mana pour lancer le sort
            float manaCost = _spellFactory.GetManaCost(spellType);
            if (_manager.CurrentMana < manaCost) 
            {
                Debug.Log("Not enough mana !");
                Destroy(activeSpell);
                activeSpell = null;
                return; 
            }

            // Déplace le sort jusqu'à la souris
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            // Utilise l'usine à spells pour créer le sort
            activeSpell = _spellFactory.CreateSpell(spellType, mousePosition, _parentSpellObject);
            activeSpell.SetActive(true);
        }
    }

    public void DestroyAllSpells() 
    {
        // Supprime tous les sorts présents dans la scène
        foreach (Transform spell in _parentSpellObject)
        {
            Destroy(spell.gameObject);
        }
    }

    public void SelectSpell(int index)
    {
        switch (index)
        {
            case 0:
                _spellSelection[0] = _spellButtons[index];
                _spellButtons[index].SetActive(true);
                _spellButtons[index + 1].SetActive(false);
                break;
            case 1:
                _spellSelection[0] = _spellButtons[index];
                _spellButtons[index].SetActive(true);
                _spellButtons[index - 1].SetActive(false);
                break;
            case 2:
                _spellSelection[1] = _spellButtons[index];
                _spellButtons[index].SetActive(true);
                _spellButtons[index + 1].SetActive(false);
                break;
            case 3:
                _spellSelection[1] = _spellButtons[index];
                _spellButtons[index].SetActive(true);
                _spellButtons[index - 1].SetActive(false);
                break;
            case 4:
                _spellSelection[2] = _spellButtons[index];
                _spellButtons[index].SetActive(true);
                _spellButtons[index + 1].SetActive(false);
                break;
            case 5:
                _spellSelection[2] = _spellButtons[index];
                _spellButtons[index].SetActive(true);
                _spellButtons[index - 1].SetActive(false);
                break;

        }

    }

    public GameObject[] SpellSelection {
        get {
            return _spellSelection;
        }
        set
        {
            
            foreach (var item in value)
            {
                SelectSpell(Array.IndexOf(_spellButtons, item));
            }
            
        }
    }

}
