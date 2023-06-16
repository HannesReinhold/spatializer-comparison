using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogBox : MonoBehaviour
{

    private CanvasGroup canvasGroup;
    public float animationSpeed;


    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }


    private void OnEnable()
    {
        canvasGroup.alpha = 0;
        canvasGroup.LeanAlpha(1, animationSpeed).setEaseOutExpo();

        transform.localPosition = new Vector2(0, -Screen.height*1.5f);
        transform.LeanMoveLocalY(0, animationSpeed).setEaseOutExpo().delay = 0.1f;
    }

    public  void Close()
    {
        canvasGroup.LeanAlpha(0, animationSpeed).setEaseOutExpo();
        transform.LeanMoveLocalY(-Screen.height * 1.5f, animationSpeed).setEaseOutExpo().setOnComplete(OnComplete);
    }

    void OnComplete()
    {
        gameObject.SetActive(false);
    }
}
