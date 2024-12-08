using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private string _saveFilePath;

    private void Start()
    {
        _saveFilePath = Application.persistentDataPath + "/savefile.json";
    }

    public void SaveGame(SaveData saveData)
    {
        string jsonData = JsonUtility.ToJson(saveData, true); // Convertit l'objet GameData en JSON
        File.WriteAllText(_saveFilePath, jsonData); // Écrit dans un fichier
        Debug.Log("Game Saved at " + _saveFilePath);
    }
    public SaveData LoadGame()
    {
        if (File.Exists(_saveFilePath))
        {
            string jsonData = File.ReadAllText(_saveFilePath); // Lit le fichier JSON
            SaveData saveData = JsonUtility.FromJson<SaveData>(jsonData); // Convertit le JSON en objet GameData
            Debug.Log("Game Loaded");
            return saveData;
        }
        else
        {
            Debug.LogWarning("Save file not found!");
            return null;
        }
    }
}
public class SaveData
{
    public int SelectedDragon;
    public GameObject[] SelectedSpells;
    public int LevelProgressionIndex;
}

