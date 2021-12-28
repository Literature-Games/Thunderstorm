using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    public static InputManager im;

    [HideInInspector] public bool moving = false;

    //private variables below
    Vector2 moveDirection = Vector2.zero;
    bool movePressed = false;
    bool interactPressed = false;
    bool submitPressed = false;
    bool pausePressed = false;
    bool slotPressed = false;

    void Awake()
    {
        if(im != null)
        {
            Debug.LogError("More than one Input Manager");
        }
        if(im == null)
            im = this.GetComponent<InputManager>();
    }

    public void MovedButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            moving = true;
            moveDirection = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            moving = false;
            moveDirection = context.ReadValue<Vector2>();
        } 
    }

    public void InteractButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            interactPressed = true;
        }
        else if (context.canceled)
        {
            interactPressed = false;
        }
    }

    public void SubmitButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            submitPressed = true;
        }
        else if (context.canceled)
        {
            submitPressed = false;
        }
    }

    public void PauseButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            pausePressed = true;
        }
        else if (context.canceled)
        {
            pausePressed = false;
        }
    }

    public void SlotButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            slotPressed = true;
        }
        else if (context.canceled)
        {
            slotPressed = false;
        }
    }

    public Vector2 GetMoveDirection() 
    {
        return moveDirection;
    }

    public bool GetInteractPressed()
    {
        bool result = interactPressed;
        interactPressed = false;
        return result;
    }

    public bool GetSubmitPressed()
    {
        bool result = submitPressed;
        submitPressed = false;
        return result;
    }

    public bool GetPausePressed()
    {
        bool result = pausePressed;
        pausePressed = false;
        return result;
    }

    public bool GetSlotPressed()
    {
        bool result = slotPressed;
        slotPressed = false;
        return result;
    }

    public void registerSubmitPressed()
    {
        submitPressed = false;
    }

}
