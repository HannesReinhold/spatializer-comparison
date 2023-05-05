using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

    private MenuManager menuManagerRef;
    public Slider VolumeSlider;

    private void OnEnable()
    {
        VolumeSlider.value = menuManagerRef.GlobalVolume;
    }

    public void Reset()
    {
         
    }

    public void OnVolumeChanged(float vol)
    {
        menuManagerRef.GlobalVolume = vol;
    }

    public void OnBackClicked()
    {
        menuManagerRef.SetMenu(MenuState.Closed);
    }

    public void OnMainMenuClicked()
    {
        menuManagerRef.SetMenu(MenuState.MainMenu);
    }

    public void SetMenuManager(MenuManager man)
    {
        menuManagerRef = man;
    }
}
