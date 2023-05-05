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

    private void Awake()
    {
        Evaluations = new List<SubjectiveEvaluation> {
            new SubjectiveEvaluation(1, SpatializerNames[0], "How Realistic does the Spatialzer sound like?", "Realism", "Very Artificial", "Very Realistic"),
            new SubjectiveEvaluation(2, SpatializerNames[1], "How Realistic does the Spatialzer sound like?", "Realism", "Very Artificial", "Very Realistic"),
            new SubjectiveEvaluation(3, SpatializerNames[2], "How Realistic does the Spatialzer sound like?", "Realism", "Very Artificial", "Very Realistic"),
            new SubjectiveEvaluation(4, SpatializerNames[3], "How Realistic does the Spatialzer sound like?", "Realism", "Very Artificial", "Very Realistic"),
            new SubjectiveEvaluation(5, SpatializerNames[0], "Description Test 5", "Test Aspect", "Min", "Max")
        };
    }



    public void SetEvaluationMenuState(int i)
    {
        for(int j = 0; j < MenuPages.Count; j++)
        {
            MenuPages[j].SetActive(i==j);
        }

    }

    public void SetupEvaluation()
    {
        evaluationData = Evaluations[currentEvaluationIndex];
        Dialog.SetHeader(evaluationData);
        EvaluationInterface.SetInterface(evaluationData);
        EvaluationInterface.SetEvaluationData(currentEvaluationIndex);
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
