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
                },
                {
                    ""name"": ""Up"",
                    ""type"": ""Button"",
                    ""id"": ""00249e63-8dea-4fab-af3e-ad492c8ccce8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Down"",
                    ""type"": ""Button"",
                    ""id"": ""f50e0f7d-bdef-42a1-a854-1e273fc864e2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Left"",
                    ""type"": ""Button"",
                    ""id"": ""6108224c-5edc-44b6-bf18-fc2a2f001a83"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Right"",
                    ""type"": ""Button"",
                    ""id"": ""af61a7db-e026-4702-9f61-243cee00ef89"",
                    ""expectedControlType"": ""Button"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""48dd5a98-38d7-4f38-923b-8ffee88becb7"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""566a0bc9-a0e2-4478-9e00-1b018df0132f"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2e04a598-610b-419e-8449-d61fd2a9f61a"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8ab659da-552c-45a7-81c8-f160046764ee"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""11cbcc22-f4ce-4575-9d9e-1aad2fb476d0"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8d4c4c9f-e4cd-4bd2-a39a-fbf0d420f73b"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""beb3cdf6-4409-437b-ba7a-8956cfe22f10"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2c1b9e35-f507-4e7b-bf8b-ee8dffea1c43"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right"",
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
                    ""name"": ""Shuffle"",
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
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shuffle"",
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
        m_PlayerActions_Up = m_PlayerActions.FindAction("Up", throwIfNotFound: true);
        m_PlayerActions_Down = m_PlayerActions.FindAction("Down", throwIfNotFound: true);
        m_PlayerActions_Left = m_PlayerActions.FindAction("Left", throwIfNotFound: true);
        m_PlayerActions_Right = m_PlayerActions.FindAction("Right", throwIfNotFound: true);
        // CheatActions
        m_CheatActions = asset.FindActionMap("CheatActions", throwIfNotFound: true);
        m_CheatActions_Shuffle = m_CheatActions.FindAction("Shuffle", throwIfNotFound: true);
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
    private readonly InputAction m_PlayerActions_Up;
    private readonly InputAction m_PlayerActions_Down;
    private readonly InputAction m_PlayerActions_Left;
    private readonly InputAction m_PlayerActions_Right;
    public struct PlayerActionsActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerActionsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Press => m_Wrapper.m_PlayerActions_Press;
        public InputAction @MousePosition => m_Wrapper.m_PlayerActions_MousePosition;
        public InputAction @Up => m_Wrapper.m_PlayerActions_Up;
        public InputAction @Down => m_Wrapper.m_PlayerActions_Down;
        public InputAction @Left => m_Wrapper.m_PlayerActions_Left;
        public InputAction @Right => m_Wrapper.m_PlayerActions_Right;
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
                @Up.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnUp;
                @Up.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnUp;
                @Up.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnUp;
                @Down.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnDown;
                @Down.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnDown;
                @Down.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnDown;
                @Left.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnLeft;
                @Left.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnLeft;
                @Left.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnLeft;
                @Right.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRight;
                @Right.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRight;
                @Right.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRight;
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
                @Up.started += instance.OnUp;
                @Up.performed += instance.OnUp;
                @Up.canceled += instance.OnUp;
                @Down.started += instance.OnDown;
                @Down.performed += instance.OnDown;
                @Down.canceled += instance.OnDown;
                @Left.started += instance.OnLeft;
                @Left.performed += instance.OnLeft;
                @Left.canceled += instance.OnLeft;
                @Right.started += instance.OnRight;
                @Right.performed += instance.OnRight;
                @Right.canceled += instance.OnRight;
            }
        }
    }
    public PlayerActionsActions @PlayerActions => new PlayerActionsActions(this);

    // CheatActions
    private readonly InputActionMap m_CheatActions;
    private ICheatActionsActions m_CheatActionsActionsCallbackInterface;
    private readonly InputAction m_CheatActions_Shuffle;
    public struct CheatActionsActions
    {
        private @PlayerControls m_Wrapper;
        public CheatActionsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Shuffle => m_Wrapper.m_CheatActions_Shuffle;
        public InputActionMap Get() { return m_Wrapper.m_CheatActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CheatActionsActions set) { return set.Get(); }
        public void SetCallbacks(ICheatActionsActions instance)
        {
            if (m_Wrapper.m_CheatActionsActionsCallbackInterface != null)
            {
                @Shuffle.started -= m_Wrapper.m_CheatActionsActionsCallbackInterface.OnShuffle;
                @Shuffle.performed -= m_Wrapper.m_CheatActionsActionsCallbackInterface.OnShuffle;
                @Shuffle.canceled -= m_Wrapper.m_CheatActionsActionsCallbackInterface.OnShuffle;
            }
            m_Wrapper.m_CheatActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Shuffle.started += instance.OnShuffle;
                @Shuffle.performed += instance.OnShuffle;
                @Shuffle.canceled += instance.OnShuffle;
            }
        }
    }
    public CheatActionsActions @CheatActions => new CheatActionsActions(this);
    public interface IPlayerActionsActions
    {
        void OnPress(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
        void OnUp(InputAction.CallbackContext context);
        void OnDown(InputAction.CallbackContext context);
        void OnLeft(InputAction.CallbackContext context);
        void OnRight(InputAction.CallbackContext context);
    }
    public interface ICheatActionsActions
    {
        void OnShuffle(InputAction.CallbackContext context);
    }
}
