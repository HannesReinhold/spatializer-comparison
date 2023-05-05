using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubjectiveEvaluationMenu : MonoBehaviour
{

    private MenuManager menuManagerRef;

    public bool alreadyDidSubjectiveIntroduction;
    public bool alreadyCompleted;

    public EvaluationState evaluationState;

    public List<GameObject> EvaluationMenus;

    public SubjectiveEvaluationInterface1 EvaluationRef;

    public EvaluationManager Manager;

    


    public void OnEnable()
    {
        evaluationState = EvaluationState.Menu;
        SetEvaluationMenuState(0);
    }

    public void Reset()
    {
        alreadyDidSubjectiveIntroduction = false;
        alreadyCompleted = false;
    }

    public void OnBackClicked()
    {
        if (menuManagerRef != null)
            menuManagerRef.SetMenu(MenuState.Closed);
        else
            GameManager.Instance.LoadScene("MainHub");
    }

    public void OnTutorialClicked()
    {
        SetEvaluationMenuState(1);
    }

    public void OnStartClicked()
    {
        SetEvaluationMenuState(2);
        Manager.SetupEvaluation();
    }

    public void OnExitClicked()
    {
        if (menuManagerRef != null)
            menuManagerRef.SetMenu(MenuState.Closed);
        else
            GameManager.Instance.LoadScene("MainHub");
    }

    public void SetTutorialState()
    {

    }

    public void SetEvaluationMenuState(int index)
    {
        if (alreadyCompleted) index = 3;

        for(int i = 0; i < EvaluationMenus.Count; i++)
        {
            EvaluationMenus[i].SetActive(index==i);
        }
    }

    public void SetMenuManager(MenuManager man)
    {
        menuManagerRef = man;
        //EvaluationRef.SetMenuManager(man);
    }


    
}

public enum EvaluationState
{
    Menu,
    Tutorial,
    Evaluation
}