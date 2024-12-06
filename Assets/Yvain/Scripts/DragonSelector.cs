using UnityEditor;
using UnityEngine;

public class DragonSelector : MonoBehaviour
{
    private GameObject[] dragons = new GameObject[3];
    private int selectedDragon = 0;

    private void Start()
    {
        int i = 0;
        foreach (Transform child in transform)
        {
            dragons[i] = child.gameObject;
            i++;
        }
    }

    public void ChangeSelectedDragon(int selection)
    {
        dragons[selectedDragon].SetActive(false);

        selectedDragon += selection;

        if (selectedDragon < 0)
        {
            selectedDragon = 2;
        }
        else if (selectedDragon > 2)
        {
            selectedDragon = 0;
        }

        dragons[selectedDragon].SetActive(true);
    }
}
