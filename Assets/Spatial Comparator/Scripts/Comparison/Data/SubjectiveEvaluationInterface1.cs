using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SubjectiveEvaluationInterface1 : MonoBehaviour
{
    public TextMeshProUGUI Header;
    public TextMeshProUGUI Description;
    public TextMeshProUGUI Min;
    public TextMeshProUGUI Max;


    public SubjectiveEvaluationData evaluationData;

    public EvaluationManager evaluationManager;

    public Slider slider;

    public void Reset()
    {

    }

    private void OnEnable()
    {
        slider.value = 50;
    }

    public void SetInterface(SubjectiveEvaluation evaluationInfo)
    {
        Header.text = "Evaluation " + evaluationInfo.id + " / " + evaluationManager.Evaluations.Count;
        Description.text = evaluationInfo.description;

        Min.text = evaluationInfo.minValue;
        Max.text = evaluationInfo.maxValue;
    }

    public void SetEvaluationData(int index)
    {
        evaluationData = new SubjectiveEvaluationData(index);
        evaluationData.spatializerName = evaluationManager.Evaluations[index].spatializerName;
        evaluationData.evaluationAspect = evaluationManager.Evaluations[index].evaluationAspect;
        GameManager.Instance.dataManager.currentSessionData.subjectiveEvaluationResults.Add(evaluationData);
    }

    public void SaveEvaluationData()
    {
        GameManager.Instance.dataManager.SaveSession();
    }

    public void OnSliderChanged(float value)
    {
        evaluationData.evaluationValue = value;
    }

    public void OnNextClicked()
    {
        SaveEvaluationData();
        if (evaluationManager.currentEvaluationIndex >= evaluationManager.Evaluations.Count-1)
        {
            evaluationManager.SetEvaluationMenuState(2);
            
            return;
        }
        
        
        evaluationManager.SetEvaluationMenuState(0);
        evaluationManager.SetNextEvaluation();
        SetInterface(evaluationManager.evaluationData);
        slider.value = 50;

    }
}
