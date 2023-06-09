//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Input/InputControl.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @InputControl : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputControl()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputControl"",
    ""maps"": [
        {
            ""name"": ""Control"",
            ""id"": ""201c4812-2342-4c01-b85d-b6e98dccc3c8"",
            ""actions"": [
                {
                    ""name"": ""Macro"",
                    ""type"": ""Button"",
                    ""id"": ""c75fbe1a-6fdf-4084-b29d-f163d7aaf97a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Silver"",
                    ""type"": ""Button"",
                    ""id"": ""ce70affc-6ee7-4c3b-bd02-48b1d501080d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2419b6d0-5aa9-44d9-8b3c-19fdc1eb4522"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Macro"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a304478f-15ed-43eb-b52d-9d37fe73ca8b"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Silver"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Control
        m_Control = asset.FindActionMap("Control", throwIfNotFound: true);
        m_Control_Macro = m_Control.FindAction("Macro", throwIfNotFound: true);
        m_Control_Silver = m_Control.FindAction("Silver", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Control
    private readonly InputActionMap m_Control;
    private IControlActions m_ControlActionsCallbackInterface;
    private readonly InputAction m_Control_Macro;
    private readonly InputAction m_Control_Silver;
    public struct ControlActions
    {
        private @InputControl m_Wrapper;
        public ControlActions(@InputControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @Macro => m_Wrapper.m_Control_Macro;
        public InputAction @Silver => m_Wrapper.m_Control_Silver;
        public InputActionMap Get() { return m_Wrapper.m_Control; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ControlActions set) { return set.Get(); }
        public void SetCallbacks(IControlActions instance)
        {
            if (m_Wrapper.m_ControlActionsCallbackInterface != null)
            {
                @Macro.started -= m_Wrapper.m_ControlActionsCallbackInterface.OnMacro;
                @Macro.performed -= m_Wrapper.m_ControlActionsCallbackInterface.OnMacro;
                @Macro.canceled -= m_Wrapper.m_ControlActionsCallbackInterface.OnMacro;
                @Silver.started -= m_Wrapper.m_ControlActionsCallbackInterface.OnSilver;
                @Silver.performed -= m_Wrapper.m_ControlActionsCallbackInterface.OnSilver;
                @Silver.canceled -= m_Wrapper.m_ControlActionsCallbackInterface.OnSilver;
            }
            m_Wrapper.m_ControlActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Macro.started += instance.OnMacro;
                @Macro.performed += instance.OnMacro;
                @Macro.canceled += instance.OnMacro;
                @Silver.started += instance.OnSilver;
                @Silver.performed += instance.OnSilver;
                @Silver.canceled += instance.OnSilver;
            }
        }
    }
    public ControlActions @Control => new ControlActions(this);
    public interface IControlActions
    {
        void OnMacro(InputAction.CallbackContext context);
        void OnSilver(InputAction.CallbackContext context);
    }
}
