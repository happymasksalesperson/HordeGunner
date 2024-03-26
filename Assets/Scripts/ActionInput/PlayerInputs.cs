// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/PlayerControls/PlayerInputs.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputs : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputs"",
    ""maps"": [
        {
            ""name"": ""WeaponInputs"",
            ""id"": ""05604190-b960-4dc9-a9c6-f1138421f59e"",
            ""actions"": [
                {
                    ""name"": ""Scroll"",
                    ""type"": ""PassThrough"",
                    ""id"": ""659b69fa-4f33-494c-b717-5147a944988c"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftClick"",
                    ""type"": ""Button"",
                    ""id"": ""2dbf6a0d-18b0-48dd-81a6-80d6e738cf10"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightClick"",
                    ""type"": ""Button"",
                    ""id"": ""20041e18-336b-4e2e-b212-51b0eb8c9ffa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""02b9cc86-3e89-41ef-8523-c7e8348f243e"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Scroll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5d0477c5-02f3-401c-bf13-fe6305b9f7ef"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""154aa4b3-92fc-49f5-b62a-0a1b357f9f54"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // WeaponInputs
        m_WeaponInputs = asset.FindActionMap("WeaponInputs", throwIfNotFound: true);
        m_WeaponInputs_Scroll = m_WeaponInputs.FindAction("Scroll", throwIfNotFound: true);
        m_WeaponInputs_LeftClick = m_WeaponInputs.FindAction("LeftClick", throwIfNotFound: true);
        m_WeaponInputs_RightClick = m_WeaponInputs.FindAction("RightClick", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // WeaponInputs
    private readonly InputActionMap m_WeaponInputs;
    private IWeaponInputsActions m_WeaponInputsActionsCallbackInterface;
    private readonly InputAction m_WeaponInputs_Scroll;
    private readonly InputAction m_WeaponInputs_LeftClick;
    private readonly InputAction m_WeaponInputs_RightClick;
    public struct WeaponInputsActions
    {
        private @PlayerInputs m_Wrapper;
        public WeaponInputsActions(@PlayerInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Scroll => m_Wrapper.m_WeaponInputs_Scroll;
        public InputAction @LeftClick => m_Wrapper.m_WeaponInputs_LeftClick;
        public InputAction @RightClick => m_Wrapper.m_WeaponInputs_RightClick;
        public InputActionMap Get() { return m_Wrapper.m_WeaponInputs; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(WeaponInputsActions set) { return set.Get(); }
        public void SetCallbacks(IWeaponInputsActions instance)
        {
            if (m_Wrapper.m_WeaponInputsActionsCallbackInterface != null)
            {
                @Scroll.started -= m_Wrapper.m_WeaponInputsActionsCallbackInterface.OnScroll;
                @Scroll.performed -= m_Wrapper.m_WeaponInputsActionsCallbackInterface.OnScroll;
                @Scroll.canceled -= m_Wrapper.m_WeaponInputsActionsCallbackInterface.OnScroll;
                @LeftClick.started -= m_Wrapper.m_WeaponInputsActionsCallbackInterface.OnLeftClick;
                @LeftClick.performed -= m_Wrapper.m_WeaponInputsActionsCallbackInterface.OnLeftClick;
                @LeftClick.canceled -= m_Wrapper.m_WeaponInputsActionsCallbackInterface.OnLeftClick;
                @RightClick.started -= m_Wrapper.m_WeaponInputsActionsCallbackInterface.OnRightClick;
                @RightClick.performed -= m_Wrapper.m_WeaponInputsActionsCallbackInterface.OnRightClick;
                @RightClick.canceled -= m_Wrapper.m_WeaponInputsActionsCallbackInterface.OnRightClick;
            }
            m_Wrapper.m_WeaponInputsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Scroll.started += instance.OnScroll;
                @Scroll.performed += instance.OnScroll;
                @Scroll.canceled += instance.OnScroll;
                @LeftClick.started += instance.OnLeftClick;
                @LeftClick.performed += instance.OnLeftClick;
                @LeftClick.canceled += instance.OnLeftClick;
                @RightClick.started += instance.OnRightClick;
                @RightClick.performed += instance.OnRightClick;
                @RightClick.canceled += instance.OnRightClick;
            }
        }
    }
    public WeaponInputsActions @WeaponInputs => new WeaponInputsActions(this);
    public interface IWeaponInputsActions
    {
        void OnScroll(InputAction.CallbackContext context);
        void OnLeftClick(InputAction.CallbackContext context);
        void OnRightClick(InputAction.CallbackContext context);
    }
}
