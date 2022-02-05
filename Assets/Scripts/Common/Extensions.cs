using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Extensions
{
    public static void DestroyChildren(this Transform transform)
    {
        List<Transform> children = transform.GetComponentsInChildren<Transform>().ToList();

        foreach (Transform child in children)
        {
            if (child != transform)
            {
                Object.Destroy(child.gameObject);
            }
        }
    }
}
