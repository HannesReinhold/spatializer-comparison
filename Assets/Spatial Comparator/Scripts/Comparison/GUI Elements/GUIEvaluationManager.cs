using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIEvaluationManager : MonoBehaviour
{
    public Button PrevBtn;
    public Button NextBtn;




}



public struct EvaluationData
{
    public string title;
    public string description;
    public string minValue;
    public string maxValue;
    public float rating;
    public int id;

    public EvaluationData(string t, string desc, string min, string max, float rating, int id)
    {
        title = t;
        description = desc;
        minValue = min;
        maxValue = max;
        this.rating = 0;
        this.id = id;
    }
}