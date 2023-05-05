using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDown : MonoBehaviour
{
    public int Count = 3;
    public int currentCount;

    public TextMeshProUGUI text;

    public void ResetCountdown()
    {
        currentCount = Count;
    }

    public void StartCountdown()
    {
        ResetCountdown();
        Invoke("Step", 1);
        text.text = currentCount.ToString();
    }

    private void Step()
    {
        currentCount--;
        text.text = currentCount.ToString();
        if (currentCount > 0)
        {
            Invoke("Step", 1);
        }
        else
        {
            ResetCountdown();
            Invoke("Hide", 1);
        }
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
