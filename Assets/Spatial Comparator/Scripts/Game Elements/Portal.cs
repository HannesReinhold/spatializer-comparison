using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{

    public string PortalName;
    public string SceneName = "";
    public bool AlignTextToPlayer = true;
    public TextMeshPro text;
    Camera cam;
    private bool alreadyTeleporting = false;

    private void Start()
    {
        text.text = SceneName;
    }

    private void Update()
    {
        if (!AlignTextToPlayer) return;

        cam = Camera.current;
        if (cam == null) return;

        text.transform.LookAt(cam.transform.position);
        text.transform.Rotate(Vector3.up - new Vector3(0, 180, 0));


        if (!alreadyTeleporting && Vector3.Distance(cam.transform.position, transform.position) < 2)
        {
            alreadyTeleporting = true;
            Debug.Log("Teleport to scene:" + SceneName);
            Invoke("LoadScene", 2);
        }
    }

    private void OnTriggerEnter()
    {
        if (!alreadyTeleporting && Vector3.Distance(cam.transform.position, transform.position) < 2)
        {
            alreadyTeleporting = true;
            Debug.Log("Teleport to scene:" + SceneName);
            Invoke("LoadScene", 2);
        }
    }

    private void LoadScene()
    {
        GameManager.Instance.LoadScene(SceneName);
    }
}
