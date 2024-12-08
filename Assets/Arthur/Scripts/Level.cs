using System;
using System.Collections.Generic;
using UnityEngine;

// Constitue un niveau
[Serializable]
public class Level
{
    public string LevelName;
    public GameObject LevelPrefab;
    public int ManaAvailable;
    public List<Path> Paths;
    public List<Wave> Waves;
    public float Zoom;
}
