using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpatializerSwitcher : MonoBehaviour
{
    public List<GameObject> Sources;

    public void SetSource(int index)
    {
        for(int i = 0; i < Sources.Count; i++)
        {
            Sources[i].SetActive(index==i);
        }
    }
}
