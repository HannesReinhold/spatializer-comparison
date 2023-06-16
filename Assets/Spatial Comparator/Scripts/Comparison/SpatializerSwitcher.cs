using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpatializerSwitcher : MonoBehaviour
{
    public GameObject UnitySource;

    public List<GameObject> Sources;

    public void SetSource(int index)
    {
        if (index == 6)
        {
            UnitySource.SetActive(true);
            UnitySource.GetComponent<AudioSource>().Play();
        }
        else
        {
            UnitySource.GetComponent<AudioSource>().Stop();
            UnitySource.SetActive(false);
        }

        for (int i = 0; i < Sources.Count; i++)
        {
            Sources[i].SetActive(index==i);
        }
    }

    public FMODUnity.StudioEventEmitter GetSourceAt(int i)
    {
        return Sources[i].GetComponent<FMODUnity.StudioEventEmitter>();
    }

    public void DeactivateAll()
    {
        SetSource(Sources.Count);
    }
}
