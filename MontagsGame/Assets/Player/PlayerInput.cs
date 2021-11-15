// GENERATED AUTOMATICALLY FROM 'Assets/Player/PlayerInput 1.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput 1"",
    ""maps"": [
        {
            ""name"": ""CharacterControls"",
            ""id"": ""85abc30a-1fb2-42b5-b321-a98983c90255"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""8c710dca-dc8b-4840-b39b-b13674b4218a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""PassThrough"",
                    ""id"": ""7c4713a2-15d7-445f-85df-1d376d319265"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MeleeAttack"",
                    ""type"": ""PassThrough"",
                    ""id"": ""b7ff0c40-930a-4ad7-8040-a9d846b3ee39"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""IncreaseFOV"",
                    ""type"": ""PassThrough"",
                    ""id"": ""fca45622-c364-4c3d-a45f-a6ae829b9905"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Skull"",
                    ""type"": ""PassThrough"",
                    ""id"": ""bde3253c-12ec-497b-b993-e2335f0a184d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Helm"",
                    ""type"": ""PassThrough"",
                    ""id"": ""530a02cd-3ba1-4871-a5fe-2ab875610128"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Telescope"",
                    ""type"": ""PassThrough"",
                    ""id"": ""7ac36089-9ca9-439c-9db8-e52b19281fab"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""7453c558-b8b8-4029-9f98-2807ad0804a6"",
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
                    ""id"": ""0173999a-e005-4140-94bc-47419d9351e8"",
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
                    ""id"": ""890106a9-6682-4996-94e0-408a244d8be0"",
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
                    ""id"": ""91e4454b-baa7-4701-badc-d530bd812b50"",
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
                    ""id"": ""62e9e872-747d-422b-8e18-132f77a23ed6"",
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
                    ""id"": ""43f10c62-0ed7-4e69-a484-a52bbf8128e1"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5226547b-f716-4ab4-b7b2-8eecb4e699dc"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press(pressPoint=0.1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MeleeAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""effb69db-74d2-40e0-bdfe-43bc702db2d2"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skull"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""16b830e9-7750-4d7f-bd40-7fc122c9245a"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Helm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0e56cad2-8fa4-4bd1-a713-627cc88fbf0c"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Telescope"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d96fdd9c-b78e-46f4-8003-23eff3b7104c"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""IncreaseFOV"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // CharacterControls
        m_CharacterControls = asset.FindActionMap("CharacterControls", throwIfNotFound: true);
        m_CharacterControls_Move = m_CharacterControls.FindAction("Move", throwIfNotFound: true);
        m_CharacterControls_Fire = m_CharacterControls.FindAction("Fire", throwIfNotFound: true);
        m_CharacterControls_MeleeAttack = m_CharacterControls.FindAction("MeleeAttack", throwIfNotFound: true);
        m_CharacterControls_IncreaseFOV = m_CharacterControls.FindAction("IncreaseFOV", throwIfNotFound: true);
        m_CharacterControls_Skull = m_CharacterControls.FindAction("Skull", throwIfNotFound: true);
        m_CharacterControls_Helm = m_CharacterControls.FindAction("Helm", throwIfNotFound: true);
        m_CharacterControls_Telescope = m_CharacterControls.FindAction("Telescope", throwIfNotFound: true);
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

    // CharacterControls
    private readonly InputActionMap m_CharacterControls;
    private ICharacterControlsActions m_CharacterControlsActionsCallbackInterface;
    private readonly InputAction m_CharacterControls_Move;
    private readonly InputAction m_CharacterControls_Fire;
    private readonly InputAction m_CharacterControls_MeleeAttack;
    private readonly InputAction m_CharacterControls_IncreaseFOV;
    private readonly InputAction m_CharacterControls_Skull;
    private readonly InputAction m_CharacterControls_Helm;
    private readonly InputAction m_CharacterControls_Telescope;
    public struct CharacterControlsActions
    {
        private @PlayerInput m_Wrapper;
        public CharacterControlsActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_CharacterControls_Move;
        public InputAction @Fire => m_Wrapper.m_CharacterControls_Fire;
        public InputAction @MeleeAttack => m_Wrapper.m_CharacterControls_MeleeAttack;
        public InputAction @IncreaseFOV => m_Wrapper.m_CharacterControls_IncreaseFOV;
        public InputAction @Skull => m_Wrapper.m_CharacterControls_Skull;
        public InputAction @Helm => m_Wrapper.m_CharacterControls_Helm;
        public InputAction @Telescope => m_Wrapper.m_CharacterControls_Telescope;
        public InputActionMap Get() { return m_Wrapper.m_CharacterControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CharacterControlsActions set) { return set.Get(); }
        public void SetCallbacks(ICharacterControlsActions instance)
        {
            if (m_Wrapper.m_CharacterControlsActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnMove;
                @Fire.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnFire;
                @MeleeAttack.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnMeleeAttack;
                @MeleeAttack.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnMeleeAttack;
                @MeleeAttack.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnMeleeAttack;
                @IncreaseFOV.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnIncreaseFOV;
                @IncreaseFOV.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnIncreaseFOV;
                @IncreaseFOV.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnIncreaseFOV;
                @Skull.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnSkull;
                @Skull.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnSkull;
                @Skull.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnSkull;
                @Helm.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnHelm;
                @Helm.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnHelm;
                @Helm.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnHelm;
                @Telescope.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnTelescope;
                @Telescope.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnTelescope;
                @Telescope.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnTelescope;
            }
            m_Wrapper.m_CharacterControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
                @MeleeAttack.started += instance.OnMeleeAttack;
                @MeleeAttack.performed += instance.OnMeleeAttack;
                @MeleeAttack.canceled += instance.OnMeleeAttack;
                @IncreaseFOV.started += instance.OnIncreaseFOV;
                @IncreaseFOV.performed += instance.OnIncreaseFOV;
                @IncreaseFOV.canceled += instance.OnIncreaseFOV;
                @Skull.started += instance.OnSkull;
                @Skull.performed += instance.OnSkull;
                @Skull.canceled += instance.OnSkull;
                @Helm.started += instance.OnHelm;
                @Helm.performed += instance.OnHelm;
                @Helm.canceled += instance.OnHelm;
                @Telescope.started += instance.OnTelescope;
                @Telescope.performed += instance.OnTelescope;
                @Telescope.canceled += instance.OnTelescope;
            }
        }
    }
    public CharacterControlsActions @CharacterControls => new CharacterControlsActions(this);
    public interface ICharacterControlsActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
        void OnMeleeAttack(InputAction.CallbackContext context);
        void OnIncreaseFOV(InputAction.CallbackContext context);
        void OnSkull(InputAction.CallbackContext context);
        void OnHelm(InputAction.CallbackContext context);
        void OnTelescope(InputAction.CallbackContext context);
    }
}
