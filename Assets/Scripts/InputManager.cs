using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    PlayerController controller;
    public PlayerControls playerControls;
    public float movementInput = 0;
    public float jumpInput = 0;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        if(playerControls == null)
        {
            playerControls = new PlayerControls();
        }
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>().x;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            controller.Jump();
        }
    }

    public void OnMine(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            controller.Mine();
        }
    }
}
