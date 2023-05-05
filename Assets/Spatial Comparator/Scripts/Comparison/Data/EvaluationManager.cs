using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvaluationManager : MonoBehaviour
{
    public List<GameObject> MenuPages;

    public SubjectiveStartDialog Dialog;
    public SubjectiveEvaluationInterface1 EvaluationInterface;

    public int currentEvaluationIndex = 0;

    public SubjectiveEvaluation evaluationData;


    public List<string> SpatializerNames = new List<string> { "FMod Default Spatializer", "Oculus Spatializer", "Google Resonance Spatializer", "Steam Spatializer" };

    public List<SubjectiveEvaluation> Evaluations;

    public SpatializerSwitcher spatializerSwitcher;

    private void Awake()
    {
        Evaluations = new List<SubjectiveEvaluation> {
            new SubjectiveEvaluation(1, SpatializerNames[0], 0, "How Realistic does the Spatialzer sound like?", "Realism", "Very Artificial", "Very Realistic"),
            new SubjectiveEvaluation(2, SpatializerNames[1], 1, "How Realistic does the Spatialzer sound like?", "Realism", "Very Artificial", "Very Realistic"),
            new SubjectiveEvaluation(3, SpatializerNames[2], 2, "How Realistic does the Spatialzer sound like?", "Realism", "Very Artificial", "Very Realistic"),
            new SubjectiveEvaluation(4, SpatializerNames[3], 3, "How Realistic does the Spatialzer sound like?", "Realism", "Very Artificial", "Very Realistic"),
            new SubjectiveEvaluation(5, SpatializerNames[0], 0, "Description Test 5", "Test Aspect", "Min", "Max")
        };
    }

    private void OnEnable()
    {
        SetEvaluationMenuState(0);
    }

    public void EnableAudioSource(bool enable)
    {
        spatializerSwitcher.gameObject.SetActive(enable);
    }



    public void SetEvaluationMenuState(int i)
    {
        for(int j = 0; j < MenuPages.Count; j++)
        {
            MenuPages[j].SetActive(i==j);
        }

        switch (i)
        {
            case 0:
                EnableAudioSource(false);
                break;
            case 1:
                EnableAudioSource(true);
                break;
            default:
                EnableAudioSource(false);
                break;
        }
        spatializerSwitcher.SetSource(Evaluations[currentEvaluationIndex].spatializerID);
    }

    public void SetupEvaluation()
    {
        evaluationData = Evaluations[currentEvaluationIndex];
        Dialog.SetHeader(evaluationData);
        EvaluationInterface.SetInterface(evaluationData);
        EvaluationInterface.SetEvaluationData(currentEvaluationIndex);

        spatializerSwitcher.SetSource(Evaluations[currentEvaluationIndex].spatializerID);
    }

    public void SetNextEvaluation()
    {
        currentEvaluationIndex++;
        evaluationData = Evaluations[currentEvaluationIndex];
        Dialog.SetHeader(evaluationData);
        EvaluationInterface.SetInterface(evaluationData);
        EvaluationInterface.SetEvaluationData(currentEvaluationIndex);
    }
}
