using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Helper class
/// </summary>
public class BoxCleaner : MonoBehaviour
{
    public void cleanAllBoxes()
    {
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");
        foreach (var box in boxes)
        {
            Destroy(box);
        }
    }
}
