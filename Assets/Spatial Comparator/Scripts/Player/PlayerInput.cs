using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public InputActionReference ResetMenuInput;
    public InputActionReference GuessInput;

    private void OnEnable()
    {
        ResetMenuInput.action.performed += ResetMenu;
        GuessInput.action.performed += Guess;
    }

    private void OnDisable()
    {
        ResetMenuInput.action.performed -= ResetMenu;
        GuessInput.action.performed -= Guess;
    }

    public void ResetMenu(InputAction.CallbackContext context)
    {
        MenuFollower follower = FindObjectOfType<MenuFollower>();
        follower.Follow();
    }

    public void Guess(InputAction.CallbackContext context)
    {
        DirectionGuessingManager manager = FindObjectOfType<DirectionGuessingManager>();
        if (manager != null) manager.SelectGuess();
    }
}
