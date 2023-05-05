using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuControls : MonoBehaviour
{

    private XRIDefaultInputActions actions;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        actions.XRIRightHandInteraction.OpenMenu.performed += OpenMenu;
    }

    private void OnDisable()
    {
        actions.XRIRightHandInteraction.OpenMenu.performed -= OpenMenu;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OpenMenu(InputAction.CallbackContext context)
    {

    }
}
