using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    private PlayerInputs playerInputs;

    public float scrollValue;

    public event Action<InputAction.CallbackContext> AnnounceLeftClick;

    public event Action<InputAction.CallbackContext> AnnounceRightClick;

    public event Action<float> AnnounceMouseScroll;

    public void Start()
    {
        playerInputs = new PlayerInputs();
        playerInputs.Enable();
        
        playerInputs.WeaponInputs.LeftClick.performed += LeftClick;
        playerInputs.WeaponInputs.LeftClick.canceled += LeftClick;

        playerInputs.WeaponInputs.RightClick.performed += RightClick;
        playerInputs.WeaponInputs.RightClick.canceled += RightClick;

        playerInputs.WeaponInputs.Scroll.performed += x => scrollValue = x.ReadValue<float>();
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
        playerInputs.WeaponInputs.LeftClick.performed -= LeftClick;
        playerInputs.WeaponInputs.LeftClick.canceled -= LeftClick;
        playerInputs.WeaponInputs.RightClick.performed -= RightClick;
        playerInputs.WeaponInputs.RightClick.canceled -= RightClick;
    }
}