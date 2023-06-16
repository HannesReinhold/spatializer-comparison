using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportManager : MonoBehaviour
{
    public ScreenFader fader;

    private void Start()
    {
        fader = FindObjectOfType<ScreenFader>();
        Debug.Log(fader);
    }

    public void TeleportToScene(string sceneName)
    {
        fader = FindObjectOfType<ScreenFader>();
        StartCoroutine(TeleportToLocation(sceneName));
    }

    IEnumerator TeleportToLocation(string sceneName)
    {
        
        fader.FadeOut();
        yield return new WaitForSeconds(fader.FadeDuration);
        SceneManager.LoadScene(sceneName);
        fader.FadeIn();

    }
}
