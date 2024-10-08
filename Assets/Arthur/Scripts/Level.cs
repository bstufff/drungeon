using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Level
{
    public string levelName;
    public int manaAvailable;
    public List<Path> paths;
    public List<Wave> waves;
}
