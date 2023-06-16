using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubjectiveMenuManager : MonoBehaviour
{
    public List<DialogBox> pages;
    public int startPage = 0;
    private DialogBox currentPage;


    private void Awake()
    {
        closeAllPages();
        OpenPage(startPage);
    }

    private void Start()
    {
        
    }

    public void closeAllPages()
    {
        for (int i = 0; i < pages.Count; i++)
        {
            pages[i].animationSpeed = 0.5f;
            pages[i].gameObject.SetActive(false);
        }
    }


    public void OpenPage(int index)
    {
        if (currentPage != null) currentPage.Close();
        currentPage = pages[index];
        Debug.Log(currentPage);

        Invoke("ActivatePage", 0.5f);
    }

    private void ActivatePage()
    {
        currentPage.gameObject.SetActive(true);
    }
}
