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
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""9881f8eb-01f9-487c-8956-99c9e85064f5"",
                    ""expectedControlType"": ""Dpad"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotateSideClockwise"",
                    ""type"": ""Button"",
                    ""id"": ""871dfb53-321b-4f59-819b-1cf8515a41ba"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotateSideCounterclockwise"",
                    ""type"": ""Button"",
                    ""id"": ""412b49e7-a8f2-4f45-8825-e735961ae94b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Undo"",
                    ""type"": ""Button"",
                    ""id"": ""3ee2c48f-0142-43bf-b11a-3a643f803ec0"",
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
                    ""id"": ""a903ce63-bef4-420d-aec7-a93475b056d6"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""ASDW"",
                    ""id"": ""b4397c98-ef04-4059-8a67-782d72065458"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""eecc36c3-1d11-437f-9d55-49474cf14982"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""2cb82886-54f4-47b6-8d78-5e2aef7af410"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""cdc848ba-947f-426b-b091-6c48ca436580"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""29369f6d-faac-49a7-b114-3be692022796"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""eed6729a-34b2-4ce7-a21f-870bb70979d2"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateSideClockwise"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f22a8998-d486-4525-85f2-027326df0a88"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateSideClockwise"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4ff2fcbe-b1be-4aa2-9de3-a91ab18493c3"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateSideCounterclockwise"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fec0cc7d-1eeb-4a1a-a8fb-e70f98c7fd18"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateSideCounterclockwise"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""66a452e9-9c7f-40ff-8a9c-64a9ad849c78"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Undo"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d44798f0-81b8-4232-9054-4f94132fc04a"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Undo"",
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
                },
                {
                    ""name"": ""CycleSelection"",
                    ""type"": ""Button"",
                    ""id"": ""183dd5f0-aad9-4d4f-85ac-c0d24b471ffd"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""44c635a0-c339-4286-972d-f642be8095a2"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CycleSelection"",
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
        m_PlayerActions_Move = m_PlayerActions.FindAction("Move", throwIfNotFound: true);
        m_PlayerActions_RotateSideClockwise = m_PlayerActions.FindAction("RotateSideClockwise", throwIfNotFound: true);
        m_PlayerActions_RotateSideCounterclockwise = m_PlayerActions.FindAction("RotateSideCounterclockwise", throwIfNotFound: true);
        m_PlayerActions_Undo = m_PlayerActions.FindAction("Undo", throwIfNotFound: true);
        // CheatActions
        m_CheatActions = asset.FindActionMap("CheatActions", throwIfNotFound: true);
        m_CheatActions_Shuffle = m_CheatActions.FindAction("Shuffle", throwIfNotFound: true);
        m_CheatActions_CycleSelection = m_CheatActions.FindAction("CycleSelection", throwIfNotFound: true);
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
    private readonly InputAction m_PlayerActions_Move;
    private readonly InputAction m_PlayerActions_RotateSideClockwise;
    private readonly InputAction m_PlayerActions_RotateSideCounterclockwise;
    private readonly InputAction m_PlayerActions_Undo;
    public struct PlayerActionsActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerActionsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Press => m_Wrapper.m_PlayerActions_Press;
        public InputAction @MousePosition => m_Wrapper.m_PlayerActions_MousePosition;
        public InputAction @Move => m_Wrapper.m_PlayerActions_Move;
        public InputAction @RotateSideClockwise => m_Wrapper.m_PlayerActions_RotateSideClockwise;
        public InputAction @RotateSideCounterclockwise => m_Wrapper.m_PlayerActions_RotateSideCounterclockwise;
        public InputAction @Undo => m_Wrapper.m_PlayerActions_Undo;
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
                @Move.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnMove;
                @RotateSideClockwise.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRotateSideClockwise;
                @RotateSideClockwise.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRotateSideClockwise;
                @RotateSideClockwise.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRotateSideClockwise;
                @RotateSideCounterclockwise.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRotateSideCounterclockwise;
                @RotateSideCounterclockwise.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRotateSideCounterclockwise;
                @RotateSideCounterclockwise.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRotateSideCounterclockwise;
                @Undo.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnUndo;
                @Undo.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnUndo;
                @Undo.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnUndo;
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
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @RotateSideClockwise.started += instance.OnRotateSideClockwise;
                @RotateSideClockwise.performed += instance.OnRotateSideClockwise;
                @RotateSideClockwise.canceled += instance.OnRotateSideClockwise;
                @RotateSideCounterclockwise.started += instance.OnRotateSideCounterclockwise;
                @RotateSideCounterclockwise.performed += instance.OnRotateSideCounterclockwise;
                @RotateSideCounterclockwise.canceled += instance.OnRotateSideCounterclockwise;
                @Undo.started += instance.OnUndo;
                @Undo.performed += instance.OnUndo;
                @Undo.canceled += instance.OnUndo;
            }
        }
    }
    public PlayerActionsActions @PlayerActions => new PlayerActionsActions(this);

    // CheatActions
    private readonly InputActionMap m_CheatActions;
    private ICheatActionsActions m_CheatActionsActionsCallbackInterface;
    private readonly InputAction m_CheatActions_Shuffle;
    private readonly InputAction m_CheatActions_CycleSelection;
    public struct CheatActionsActions
    {
        private @PlayerControls m_Wrapper;
        public CheatActionsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Shuffle => m_Wrapper.m_CheatActions_Shuffle;
        public InputAction @CycleSelection => m_Wrapper.m_CheatActions_CycleSelection;
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
                @CycleSelection.started -= m_Wrapper.m_CheatActionsActionsCallbackInterface.OnCycleSelection;
                @CycleSelection.performed -= m_Wrapper.m_CheatActionsActionsCallbackInterface.OnCycleSelection;
                @CycleSelection.canceled -= m_Wrapper.m_CheatActionsActionsCallbackInterface.OnCycleSelection;
            }
            m_Wrapper.m_CheatActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Shuffle.started += instance.OnShuffle;
                @Shuffle.performed += instance.OnShuffle;
                @Shuffle.canceled += instance.OnShuffle;
                @CycleSelection.started += instance.OnCycleSelection;
                @CycleSelection.performed += instance.OnCycleSelection;
                @CycleSelection.canceled += instance.OnCycleSelection;
            }
        }
    }
    public CheatActionsActions @CheatActions => new CheatActionsActions(this);
    public interface IPlayerActionsActions
    {
        void OnPress(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnRotateSideClockwise(InputAction.CallbackContext context);
        void OnRotateSideCounterclockwise(InputAction.CallbackContext context);
        void OnUndo(InputAction.CallbackContext context);
    }
    public interface ICheatActionsActions
    {
        void OnShuffle(InputAction.CallbackContext context);
        void OnCycleSelection(InputAction.CallbackContext context);
    }
}
