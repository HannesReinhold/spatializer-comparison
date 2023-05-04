using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    private MenuManager menuManager;

    private void Start()
    {
        Canvas[] list = Resources.FindObjectsOfTypeAll<Canvas>();    
        foreach (Canvas c in list)
        {
            c.worldCamera = FindObjectOfType<Camera>();
        }
    }

    public void SetMenuManager(MenuManager man)
    {
        this.menuManager = man;
    }

    void StartNewSession()
    {
        Debug.Log("Start New Session");
        menuManager.SetMenu(MenuState.Introduction);
    }

    public void OnNewSessionClicked()
    {
        StartNewSession();
    }

    public void OnQuitClick()
    {
        Application.Quit();
    }
}
