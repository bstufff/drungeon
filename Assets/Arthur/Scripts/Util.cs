using UnityEngine;

//Used to access useful methods across the program
public static class Util
{
    public static Transform[] GetChildArray(Transform parent)
    {//Creates a transform array of all the children of a given transform
        Transform[] children = new Transform[parent.childCount];

        for (int i = 0; i < parent.transform.childCount; i++)
        {
            children[i] = parent.transform.GetChild(i);
        }
        return children;
    }
}
