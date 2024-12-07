using System;
using System.Collections.Generic;
using UnityEngine;

// Constitue un niveau
[Serializable]
public class Level
{
    public string levelName;
    public GameObject levelPrefab;
    public int manaAvailable;
    public List<Path> paths;
    public List<Wave> waves;
    public float zoom;
}
