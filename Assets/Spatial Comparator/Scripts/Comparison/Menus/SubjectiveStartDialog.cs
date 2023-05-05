using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubjectiveStartDialog : MonoBehaviour
{
    public TextMeshProUGUI Header;
    public TextMeshProUGUI Content;



    public void SetHeader(SubjectiveEvaluation evaluationInfo)
    {
        Header.text = "Evaluation "+evaluationInfo.id + " / "+5;
        Content.text = evaluationInfo.description;
    }
}
