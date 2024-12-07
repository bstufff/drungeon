using UnityEditor;
using UnityEngine;

public class DragonSelector : MonoBehaviour
{
    private GameObject[] _dragons = new GameObject[3];
    private int _selectedDragon = 0; // Index du dragon sélectionné

    private void Start()
    {
        // Peuple le tableau dragons
        int i = 0;
        foreach (Transform child in transform)
        {
            _dragons[i] = child.gameObject;
            i++;
        }
    }
    // 

    public int SelectedDragon { 
        get { return _selectedDragon; } 
        set {
            // Fait en sorte que l'index boucle quand il sort des limites du tableau
            if (_selectedDragon < 0)
            {
                _selectedDragon = 2;
            }
            else if (_selectedDragon > 2)
            {
                _selectedDragon = 0;
            }
        }
    }

    // Change le dragon actif
    public void ChangeSelectedDragon(int selection)
    {
        _dragons[_selectedDragon].SetActive(false);

        SelectedDragon += selection;
        
        _dragons[_selectedDragon].SetActive(true);
    }
}
