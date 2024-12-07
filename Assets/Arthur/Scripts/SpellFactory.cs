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
    [SerializeField] private GameObject[] spellPrefabs;

    public GameObject CreateSpell(SpellType spellType, Vector3 position, Transform parent)
    {
        GameObject spellPrefab = GetPrefab(spellType);
        GameObject spellInstance = Instantiate(spellPrefab, position, Quaternion.identity, parent);
        return spellInstance;
    }

    public float GetManaCost(SpellType spellType)
    {
        return GetPrefab(spellType).GetComponent<Spell>().ManaCost;
    }

    private GameObject GetPrefab(SpellType spellType)
    {
        return spellType switch
        {
            SpellType.FlameArea => spellPrefabs[0],
            SpellType.Meteor => spellPrefabs[1],
            SpellType.IceWall => spellPrefabs[2],
            SpellType.Blizzard => spellPrefabs[3],
            SpellType.ElectricCanon => spellPrefabs[4],
            SpellType.ElectricMinion => spellPrefabs[5],
            _ => throw new System.ArgumentException("Invalid spell type!")
        };
    }
}
