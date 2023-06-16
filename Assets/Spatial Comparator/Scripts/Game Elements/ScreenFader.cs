using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFader : MonoBehaviour
{

    public float FadeDuration;
    public Color FadeColor;
    private Renderer rend;


    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeIn()
    {
        Fade(1,0);
    }

    public void FadeOut()
    {
        Fade(0,1);
    }

    public void Fade(float alphaIn, float alphaOut)
    {
        StartCoroutine(FadeRoutine(alphaIn, alphaOut));
    }

    public IEnumerator FadeRoutine(float alphaIn, float alphaOut)
    {
        float timer = 0;
        while (timer <= FadeDuration)
        {
            Color newColor = FadeColor;
            newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer/FadeDuration);

            rend.material.SetColor("_Color", newColor);

            timer += Time.deltaTime;
            yield return null;
        }

        Color newColor2 = FadeColor;
        newColor2.a = alphaOut;
        rend.material.SetColor("_Color", newColor2);
    }
}
