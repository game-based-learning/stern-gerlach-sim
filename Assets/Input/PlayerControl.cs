//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Input/PlayerControl.inputactions
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

namespace SternGerlach.Input
{
    public partial class @PlayerControl : IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @PlayerControl()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControl"",
    ""maps"": [
        {
            ""name"": ""Game"",
            ""id"": ""1d938242-6288-40ef-a883-13af4b308bb8"",
            ""actions"": [
                {
                    ""name"": ""MouseX"",
                    ""type"": ""PassThrough"",
                    ""id"": ""1fe92e08-82db-41ee-95e5-e37e6020151e"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c963f306-5fa3-47bb-a08d-3c0422877443"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RMB"",
                    ""type"": ""Value"",
                    ""id"": ""d3ee026c-4462-4e25-a676-1a3257150e65"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""LMB"",
                    ""type"": ""Value"",
                    ""id"": ""ae0b5916-2c11-4a95-b5e4-db9db538138b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""838e7bd2-dc36-4ebb-9c48-5bcbb3e754b2"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""45085728-57cb-4238-b406-b73c84bf72df"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1a5a0ad2-09ba-4a19-b3b5-decd23d49f95"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RMB"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2c3fbb1b-48cf-4109-af72-37f28d8e40d1"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LMB"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Game
            m_Game = asset.FindActionMap("Game", throwIfNotFound: true);
            m_Game_MouseX = m_Game.FindAction("MouseX", throwIfNotFound: true);
            m_Game_Zoom = m_Game.FindAction("Zoom", throwIfNotFound: true);
            m_Game_RMB = m_Game.FindAction("RMB", throwIfNotFound: true);
            m_Game_LMB = m_Game.FindAction("LMB", throwIfNotFound: true);
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

        // Game
        private readonly InputActionMap m_Game;
        private IGameActions m_GameActionsCallbackInterface;
        private readonly InputAction m_Game_MouseX;
        private readonly InputAction m_Game_Zoom;
        private readonly InputAction m_Game_RMB;
        private readonly InputAction m_Game_LMB;
        public struct GameActions
        {
            private @PlayerControl m_Wrapper;
            public GameActions(@PlayerControl wrapper) { m_Wrapper = wrapper; }
            public InputAction @MouseX => m_Wrapper.m_Game_MouseX;
            public InputAction @Zoom => m_Wrapper.m_Game_Zoom;
            public InputAction @RMB => m_Wrapper.m_Game_RMB;
            public InputAction @LMB => m_Wrapper.m_Game_LMB;
            public InputActionMap Get() { return m_Wrapper.m_Game; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(GameActions set) { return set.Get(); }
            public void SetCallbacks(IGameActions instance)
            {
                if (m_Wrapper.m_GameActionsCallbackInterface != null)
                {
                    @MouseX.started -= m_Wrapper.m_GameActionsCallbackInterface.OnMouseX;
                    @MouseX.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnMouseX;
                    @MouseX.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnMouseX;
                    @Zoom.started -= m_Wrapper.m_GameActionsCallbackInterface.OnZoom;
                    @Zoom.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnZoom;
                    @Zoom.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnZoom;
                    @RMB.started -= m_Wrapper.m_GameActionsCallbackInterface.OnRMB;
                    @RMB.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnRMB;
                    @RMB.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnRMB;
                    @LMB.started -= m_Wrapper.m_GameActionsCallbackInterface.OnLMB;
                    @LMB.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnLMB;
                    @LMB.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnLMB;
                }
                m_Wrapper.m_GameActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @MouseX.started += instance.OnMouseX;
                    @MouseX.performed += instance.OnMouseX;
                    @MouseX.canceled += instance.OnMouseX;
                    @Zoom.started += instance.OnZoom;
                    @Zoom.performed += instance.OnZoom;
                    @Zoom.canceled += instance.OnZoom;
                    @RMB.started += instance.OnRMB;
                    @RMB.performed += instance.OnRMB;
                    @RMB.canceled += instance.OnRMB;
                    @LMB.started += instance.OnLMB;
                    @LMB.performed += instance.OnLMB;
                    @LMB.canceled += instance.OnLMB;
                }
            }
        }
        public GameActions @Game => new GameActions(this);
        public interface IGameActions
        {
            void OnMouseX(InputAction.CallbackContext context);
            void OnZoom(InputAction.CallbackContext context);
            void OnRMB(InputAction.CallbackContext context);
            void OnLMB(InputAction.CallbackContext context);
        }
    }
}
