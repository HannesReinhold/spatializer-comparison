using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroductionMenu : MonoBehaviour
{
    public List<DialogBox> pages;
    public int startPage = 0;
    public float animationSpeed = 0.3f;

    private DialogBox currentPage;

    private void Awake()
    {
        closeAllPages();
        OpenPage(startPage);
    }

    public void closeAllPages()
    {
        for (int i = 0; i < pages.Count; i++)
        {
            pages[i].animationSpeed = animationSpeed;
            pages[i].gameObject.SetActive(false);
        }
    }


    public void OpenPage(int index)
    {
        if (currentPage != null) currentPage.Close();
        currentPage = pages[index];
        Debug.Log(currentPage);

        Invoke("ActivatePage", animationSpeed);
    }

    private void ActivatePage()
    {
        currentPage.gameObject.SetActive(true);
    }
}
