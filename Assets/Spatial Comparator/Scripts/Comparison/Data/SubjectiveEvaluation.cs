using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SubjectiveEvaluation
{

    public int id;
    public string spatializerName;
    public string description;
    public string evaluationAspect;
    public string minValue;
    public string maxValue;

    public SubjectiveEvaluation(int id, string name, string desc, string aspect, string min, string max)
    {
        this.id = id;
        this.spatializerName = name;
        this.description = desc;
        evaluationAspect = aspect;
        this.minValue = min;
        this.maxValue = max;
    }
    
}


public static class EvaluationList
{

}

