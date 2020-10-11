// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""PlayerActions"",
            ""id"": ""89abbe21-61fc-4d5c-8c16-df1b1d0f2658"",
            ""actions"": [
                {
                    ""name"": ""Press"",
                    ""type"": ""Button"",
                    ""id"": ""a53e851e-8cb3-4a81-a561-1901c956350b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""667bcae8-f23c-4036-b2c6-f925fe858cc2"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3208c2f4-acf4-4c39-8267-7f51481eae33"",
                    ""path"": ""<Pointer>/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Press"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b0288937-88c2-4cd8-a7ec-68ae6a4013ff"",
                    ""path"": ""<Pointer>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""CheatActions"",
            ""id"": ""6bc44b23-0702-43fc-944c-c6e49be5a969"",
            ""actions"": [
                {
                    ""name"": ""TestRotations"",
                    ""type"": ""Button"",
                    ""id"": ""b300db09-fdcd-4ee4-876d-91ba57d97f92"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""fac1ff7f-9a2b-4d7e-aa25-c891699c6958"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TestRotations"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerActions
        m_PlayerActions = asset.FindActionMap("PlayerActions", throwIfNotFound: true);
        m_PlayerActions_Press = m_PlayerActions.FindAction("Press", throwIfNotFound: true);
        m_PlayerActions_MousePosition = m_PlayerActions.FindAction("MousePosition", throwIfNotFound: true);
        // CheatActions
        m_CheatActions = asset.FindActionMap("CheatActions", throwIfNotFound: true);
        m_CheatActions_TestRotations = m_CheatActions.FindAction("TestRotations", throwIfNotFound: true);
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

    // PlayerActions
    private readonly InputActionMap m_PlayerActions;
    private IPlayerActionsActions m_PlayerActionsActionsCallbackInterface;
    private readonly InputAction m_PlayerActions_Press;
    private readonly InputAction m_PlayerActions_MousePosition;
    public struct PlayerActionsActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerActionsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Press => m_Wrapper.m_PlayerActions_Press;
        public InputAction @MousePosition => m_Wrapper.m_PlayerActions_MousePosition;
        public InputActionMap Get() { return m_Wrapper.m_PlayerActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActionsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActionsActions instance)
        {
            if (m_Wrapper.m_PlayerActionsActionsCallbackInterface != null)
            {
                @Press.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnPress;
                @Press.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnPress;
                @Press.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnPress;
                @MousePosition.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnMousePosition;
            }
            m_Wrapper.m_PlayerActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Press.started += instance.OnPress;
                @Press.performed += instance.OnPress;
                @Press.canceled += instance.OnPress;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
            }
        }
    }
    public PlayerActionsActions @PlayerActions => new PlayerActionsActions(this);

    // CheatActions
    private readonly InputActionMap m_CheatActions;
    private ICheatActionsActions m_CheatActionsActionsCallbackInterface;
    private readonly InputAction m_CheatActions_TestRotations;
    public struct CheatActionsActions
    {
        private @PlayerControls m_Wrapper;
        public CheatActionsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @TestRotations => m_Wrapper.m_CheatActions_TestRotations;
        public InputActionMap Get() { return m_Wrapper.m_CheatActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CheatActionsActions set) { return set.Get(); }
        public void SetCallbacks(ICheatActionsActions instance)
        {
            if (m_Wrapper.m_CheatActionsActionsCallbackInterface != null)
            {
                @TestRotations.started -= m_Wrapper.m_CheatActionsActionsCallbackInterface.OnTestRotations;
                @TestRotations.performed -= m_Wrapper.m_CheatActionsActionsCallbackInterface.OnTestRotations;
                @TestRotations.canceled -= m_Wrapper.m_CheatActionsActionsCallbackInterface.OnTestRotations;
            }
            m_Wrapper.m_CheatActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @TestRotations.started += instance.OnTestRotations;
                @TestRotations.performed += instance.OnTestRotations;
                @TestRotations.canceled += instance.OnTestRotations;
            }
        }
    }
    public CheatActionsActions @CheatActions => new CheatActionsActions(this);
    public interface IPlayerActionsActions
    {
        void OnPress(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
    }
    public interface ICheatActionsActions
    {
        void OnTestRotations(InputAction.CallbackContext context);
    }
}
