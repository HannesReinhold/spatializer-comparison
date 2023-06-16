using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loudspeaker : MonoBehaviour
{
    public Light indicatorLight;
    public Renderer indicatorColor;

    public SpatializerSwitcher switcher;

    public bool IsActive;
    private int activeSource;


    private void Awake()
    {
        SetActive(IsActive);
    }


    public void SetSpatializer(int id)
    {
        switcher.SetSource(id);
        activeSource = id;
    }

    public FMODUnity.StudioEventEmitter GetActiveEmitter()
    {
        return switcher.GetSourceAt(activeSource);
    }

    public void SetActive(bool f)
    {
        if (f)
        {
            indicatorLight.color = Color.green;
            indicatorColor.material.SetColor("_Color", Color.green);
            switcher.SetSource(0);
        }
        else
        {
            indicatorLight.color = Color.black;
            indicatorColor.material.SetColor("_Color", Color.black);
            switcher.DeactivateAll();
        }
    }
}
