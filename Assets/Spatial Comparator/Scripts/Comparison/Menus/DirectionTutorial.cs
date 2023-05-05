using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionTutorial : MonoBehaviour
{
    public List<GameObject> TutorialPages;

    private void OnEnable()
    {
        SetTutorialState(0);
    }

    public void SetTutorialState(int index)
    {
        for (int i = 0; i < TutorialPages.Count; i++)
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
