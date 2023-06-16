using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubjectiveTutorial : MonoBehaviour
{
    public List<GameObject> TutorialPages;

    public Loudspeaker Speaker1;
    public Loudspeaker Speaker2;

    private void OnEnable()
    {
        SetTutorialState(0);
    }

    public void StartSpeakers()
    {
        Speaker1.SetActive(true);
        Speaker1.SetSpatializer(0);
        Speaker2.SetActive(true);
        Speaker2.SetSpatializer(1);
    }
    public void StopSpeakers()
    {
        Speaker1.SetActive(false);
        Speaker2.SetActive(false);
    }

    public void SwitchDirect(float vol)
    {
        FMODUnity.StudioEventEmitter audioEvent1 = Speaker1.GetActiveEmitter();
        audioEvent1.EventInstance.setVolume(1f-vol/100f);
        FMODUnity.StudioEventEmitter audioEvent2 = Speaker2.GetActiveEmitter();
        audioEvent2.EventInstance.setVolume(vol/100f);
    }

    public void SetTutorialState(int index)
    {
        for(int i = 0; i < TutorialPages.Count; i++)
        {
            TutorialPages[i].SetActive(index == i);
        }

        switch (index)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
        }
    }


}
