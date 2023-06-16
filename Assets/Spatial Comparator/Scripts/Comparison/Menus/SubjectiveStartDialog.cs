using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubjectiveStartDialog : MonoBehaviour
{
    public TextMeshProUGUI Header;
    public TextMeshProUGUI Content;



    public void SetHeader(ConcreteSubjectiveEvaluation evaluationInfo)
    {
        Header.text = "Evaluation "+evaluationInfo.evaluationID + " / "+5;
        Content.text = evaluationInfo.description;
    }
}
