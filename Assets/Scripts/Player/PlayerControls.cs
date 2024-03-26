using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    private PlayerInputs playerInputs;

    public float scrollValue;

    public Vector2 mousePos;

    public event Action<InputAction.CallbackContext> AnnounceLeftClick;

    public event Action<InputAction.CallbackContext> AnnounceRightClick;

    public event Action<float> AnnounceMouseScroll;

    public void Start()
    {
        playerInputs = new PlayerInputs();
        playerInputs.Enable();
        
        playerInputs.MouseInputs.LeftClick.performed += LeftClick;
        playerInputs.MouseInputs.LeftClick.canceled += LeftClick;

        playerInputs.MouseInputs.RightClick.performed += RightClick;
        playerInputs.MouseInputs.RightClick.canceled += RightClick;

        playerInputs.MouseInputs.Scroll.performed += x => scrollValue = x.ReadValue<float>();
        
        playerInputs.MouseInputs.MousePoint.performed += ctx => mousePos = ctx.ReadValue<Vector2>();
    }

    private void LeftClick(InputAction.CallbackContext input)
    {
        AnnounceLeftClick?.Invoke(input);
    }

    private void RightClick(InputAction.CallbackContext input)
    {
        AnnounceRightClick?.Invoke(input);
    }
    
    private void Update()
    {
        if (scrollValue > 0)
        {
            AnnounceMouseScroll?.Invoke(scrollValue);
        }

        else if (scrollValue < 0)
        {
            AnnounceMouseScroll?.Invoke(scrollValue);
        }
    }

    void OnDisable()
    {
        playerInputs.MouseInputs.LeftClick.performed -= LeftClick;
        playerInputs.MouseInputs.LeftClick.canceled -= LeftClick;
        playerInputs.MouseInputs.RightClick.performed -= RightClick;
        playerInputs.MouseInputs.RightClick.canceled -= RightClick;
    }
}