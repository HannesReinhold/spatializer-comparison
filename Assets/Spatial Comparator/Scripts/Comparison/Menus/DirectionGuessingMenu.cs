using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionGuessingMenu : MonoBehaviour
{
    private MenuManager menuManagerRef;

    public bool alreadyDidDirectionIntroduction;

    public List<GameObject> Menus;

    public DirectionGuessingManager directionGuessingManagaer;


    public void Reset()
    {
        alreadyDidDirectionIntroduction = false;
    }

    public void SetMenuManager(MenuManager man)
    {
        menuManagerRef = man;
        directionGuessingManagaer.SetMenuManager(menuManagerRef);
    }

    public void SetMenuState(int index)
    {
        for(int i = 0; i < Menus.Count; i++)
        {
            Menus[i].SetActive(index==i);
        }
    }

    public void OnStartClicked()
    {
        SetMenuState(2);
        directionGuessingManagaer.StartGame();
    }

    public void OnTutorialClicked()
    {
        SetMenuState(1);
    }

    public void OnBackClicked()
    {
        menuManagerRef.SetMenu(MenuState.Closed);
    }
}
