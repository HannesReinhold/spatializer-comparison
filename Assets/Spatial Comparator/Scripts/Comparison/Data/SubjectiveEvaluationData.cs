using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SubjectiveEvaluationData
{
    public int evaluationID;
    public string spatializerName;
    public string evaluationAspect;
    public float evaluationValue;

    public SubjectiveEvaluationData(int id)
    {
        evaluationID = id;
    }
}
