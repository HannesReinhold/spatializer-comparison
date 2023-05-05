using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public MenuState menuState = 0;

    public MainMenu MainMenuRef;
    public Introduction IntroductionRef;
    public SettingsMenu SettingsMenuRef;
    public DirectionGuessingMenu DirectionGuessingRef;
    public SubjectiveEvaluationMenu SubjectiveEvaluationRef;

    public List<GameObject> MenuList;


    public float GlobalVolume;


    // Start is called before the first frame update
    void Start()
    {
        MainMenuRef.SetMenuManager(this);
        IntroductionRef.SetMenuManager(this);
        SettingsMenuRef.SetMenuManager(this);
        DirectionGuessingRef.SetMenuManager(this);
        SubjectiveEvaluationRef.SetMenuManager(this);

        SetMenu(MenuState.MainMenu);

    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void ResetMenu()
    {
        IntroductionRef.Reset();
        SettingsMenuRef.Reset();
        DirectionGuessingRef.Reset();
        SubjectiveEvaluationRef.Reset();
    }

    public void SetMenu(MenuState state)
    {
        for(int i=0; i< MenuList.Count; i++)
        {
            MenuList[i].SetActive(i==(int)state);
        }

        switch (state)
        {
            case MenuState.MainMenu:
                break;
            case MenuState.Introduction:
                break;
            case MenuState.Settings:
                break;
            case MenuState.DirectionGuessing:
                break;
            case MenuState.SubjectiveEvaluation:
                break;
        }
    }
}


public enum MenuState
{
    MainMenu,
    Introduction,
    Settings,
    DirectionGuessing,
    SubjectiveEvaluation,
    Closed
}