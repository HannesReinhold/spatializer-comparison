using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GUIManager : MonoBehaviour
{
    //public CurrentSpatializerDisplay currentSpatializerDisplay;

    public GameObject settings;

    public InputActionReference input;
    void Start()
    {

        input.action.performed += ToggleSettingsMenu;
    }

    private void OnDisable()
    {
        input.action.performed -= ToggleSettingsMenu;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetCurrentSpatializer(string spatializer)
    {
        // currentSpatializerDisplay.SetSpatializer(spatializer);
    }

    public void ToggleSettingsMenu(InputAction.CallbackContext context)
    {
        Debug.Log("Settings");
        settings.SetActive(!settings.activeSelf);

        Vector3 direction = Camera.main.transform.forward;
        direction.y = 0;
        direction.Normalize();
        settings.transform.position = Camera.main.transform.position + direction * 2f;
        settings.transform.LookAt(Camera.main.transform.position);
        settings.transform.Rotate(Vector3.up - new Vector3(0, 180, 0));
    }
}
