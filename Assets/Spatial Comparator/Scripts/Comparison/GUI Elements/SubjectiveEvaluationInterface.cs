using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SubjectiveEvaluationInterface : MonoBehaviour
{
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI minValueText;
    public TextMeshProUGUI maxValueText;
    public TextMeshProUGUI headerText;

    public Slider slider;

    [HideInInspector] public GUIEvaluationManager EvaluationManager;

    private EvaluationData currentEvaluation;


    private void OnEnable()
    {
        slider.onValueChanged.AddListener(SetRating);
    }

    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(SetRating);
    }

    public void SetEvaluation(EvaluationData data)
    {
        currentEvaluation = data;
    }

    public void SetInterface(int numData)
    {
        descriptionText.text = currentEvaluation.description;
        minValueText.text = currentEvaluation.minValue;
        maxValueText.text = currentEvaluation.maxValue;
        headerText.text = "Evaluation number " + currentEvaluation.id + " of " + numData + ": " + currentEvaluation.title;

        slider.value = 50;
    }

    void SetRating(float value)
    {
        currentEvaluation.rating = value;
    }
}
