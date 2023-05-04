using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Introduction : MonoBehaviour
{
    public List<GameObject> Menus;

    public Slider VolumeSlider;

    private MenuManager menuManagerRef;

    private void OnEnable()
    {
        SetMenu(1);
    }

    private void SetMenu(int index)
    {
        for(int i=0; i< Menus.Count; i++)
        {
            Menus[i].SetActive(index==i);
        }
    }

    public void SetMenuManager(MenuManager man)
    {
        menuManagerRef = man;
    }


    public void OnIntroductionNextClick()
    {
        SetMenu(2);
    }

    public void OnBackToMenuClick()
    {
        menuManagerRef.SetMenu(MenuState.MainMenu);
    }

    public void OnConsentClick()
    {
        SetMenu(3);
    }

    public void OnVolumeSettingsClick()
    {
        SetMenu(4);
    }

    public void OnFinishClick()
    {
        menuManagerRef.SetMenu(MenuState.Closed);
    }

    public void SetVolume(float vol)
    {
        Debug.Log(vol);
    }
}
