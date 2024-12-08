using UnityEngine;

public enum SpellType
{
    FlameArea,
    Meteor,
    IceWall,
    Blizzard,
    ElectricCanon,
    ElectricMinion
}

public class SpellFactory : MonoBehaviour
{
    [SerializeField] private GameObject[] _spellPrefabs;

    // Place un sort dans le niveau
    public GameObject CreateSpell(SpellType spellType, Vector3 position, Transform parent)
    {
        GameObject spellPrefab = GetPrefab(spellType);
        GameObject spellInstance = Instantiate(spellPrefab, position, Quaternion.identity, parent);
        return spellInstance;
    }

    // Récupère le coût en mana
    public float GetManaCost(SpellType spellType)
    {
        return GetPrefab(spellType).GetComponent<Spell>().ManaCost;
    }

    // Récupère le prefab du sort en fonction de son type
    private GameObject GetPrefab(SpellType spellType)
    {
        return spellType switch
        {
            SpellType.FlameArea => _spellPrefabs[0],
            SpellType.Meteor => _spellPrefabs[1],
            SpellType.IceWall => _spellPrefabs[2],
            SpellType.Blizzard => _spellPrefabs[3],
            SpellType.ElectricCanon => _spellPrefabs[4],
            SpellType.ElectricMinion => _spellPrefabs[5],
            _ => throw new System.ArgumentException("Invalid spell type!")
        };
    }
}
