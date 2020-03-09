// GENERATED AUTOMATICALLY FROM 'Assets/Resources/Input/InputControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputControls : IInputActionCollection, IDisposable
{
    private InputActionAsset asset;
    public @InputControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputControls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""41b15f8d-b7d9-4f72-84bf-e02b405ff5af"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""142a3f5c-c458-4e46-ae14-8cf83be3f893"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""StickDeadzone"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""1fe6b96d-2347-46e5-9571-039c352d6f2c"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Press to Run"",
                    ""type"": ""Button"",
                    ""id"": ""83d6b4cc-952d-475e-ac8b-f9cbf9e0d012"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Hold to Run"",
                    ""type"": ""Button"",
                    ""id"": ""c99baa36-9070-4f13-8875-48bc19d28cfa"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Camera"",
                    ""type"": ""Value"",
                    ""id"": ""671a6f4d-9793-4534-90a5-06fd9bc549e6"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Press to Target"",
                    ""type"": ""Button"",
                    ""id"": ""136c860b-9254-41e3-835a-680d5d13d64d"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Hold to Target (Press)"",
                    ""type"": ""Button"",
                    ""id"": ""82a2e625-d12c-4197-bf9f-602bc6e376a3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Hold to Target (Release)"",
                    ""type"": ""Button"",
                    ""id"": ""6727b048-a2c6-4fe3-83d5-6c33c263bbf3"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Tongue Button Press"",
                    ""type"": ""Button"",
                    ""id"": ""1522ce41-6f87-41bb-9660-0ec8784096d9"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Tongue Button Release"",
                    ""type"": ""Button"",
                    ""id"": ""ee97b943-e8d8-4bbc-95b7-5bbac7534f29"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""3a4c4bfb-6c70-49ab-b8d7-2e1809f27fe8"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b2102b57-dfa1-4f41-a287-9072175d2ca9"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9f84b445-d6e2-446e-b712-728b0333e6cb"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c32b44a6-e858-4645-9103-cdf480dbeaa7"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Press to Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""12d31b8e-7941-43f3-91a6-a7778c07eee4"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hold to Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""95fd0c94-4559-44dd-a6f5-652d14ba3e90"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6fc3065e-3412-4bad-916a-e1093927566d"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Press to Target"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""28882016-6872-4500-b6f8-2ea59279a459"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Press to Target"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ad13f534-2b7f-4560-94d2-202a053e6764"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hold to Target (Press)"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e35e7431-16db-44fd-bf25-b5fe33c99fc3"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hold to Target (Release)"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""253f5b13-4afb-47b1-9aa3-e12887f55f5e"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tongue Button Press"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bc8b6ce2-c35f-4a7b-8dff-40fe6e4365e0"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tongue Button Release"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""28094fa8-94ce-48d3-a1f5-1d704b33cb18"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menu"",
            ""id"": ""a36616ec-d836-4ab4-a2fb-a549b4956c59"",
            ""actions"": [],
            ""bindings"": []
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_PresstoRun = m_Player.FindAction("Press to Run", throwIfNotFound: true);
        m_Player_HoldtoRun = m_Player.FindAction("Hold to Run", throwIfNotFound: true);
        m_Player_Camera = m_Player.FindAction("Camera", throwIfNotFound: true);
        m_Player_PresstoTarget = m_Player.FindAction("Press to Target", throwIfNotFound: true);
        m_Player_HoldtoTargetPress = m_Player.FindAction("Hold to Target (Press)", throwIfNotFound: true);
        m_Player_HoldtoTargetRelease = m_Player.FindAction("Hold to Target (Release)", throwIfNotFound: true);
        m_Player_TongueButtonPress = m_Player.FindAction("Tongue Button Press", throwIfNotFound: true);
        m_Player_TongueButtonRelease = m_Player.FindAction("Tongue Button Release", throwIfNotFound: true);
        m_Player_Pause = m_Player.FindAction("Pause", throwIfNotFound: true);
        // Menu
        m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_PresstoRun;
    private readonly InputAction m_Player_HoldtoRun;
    private readonly InputAction m_Player_Camera;
    private readonly InputAction m_Player_PresstoTarget;
    private readonly InputAction m_Player_HoldtoTargetPress;
    private readonly InputAction m_Player_HoldtoTargetRelease;
    private readonly InputAction m_Player_TongueButtonPress;
    private readonly InputAction m_Player_TongueButtonRelease;
    private readonly InputAction m_Player_Pause;
    public struct PlayerActions
    {
        private @InputControls m_Wrapper;
        public PlayerActions(@InputControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @PresstoRun => m_Wrapper.m_Player_PresstoRun;
        public InputAction @HoldtoRun => m_Wrapper.m_Player_HoldtoRun;
        public InputAction @Camera => m_Wrapper.m_Player_Camera;
        public InputAction @PresstoTarget => m_Wrapper.m_Player_PresstoTarget;
        public InputAction @HoldtoTargetPress => m_Wrapper.m_Player_HoldtoTargetPress;
        public InputAction @HoldtoTargetRelease => m_Wrapper.m_Player_HoldtoTargetRelease;
        public InputAction @TongueButtonPress => m_Wrapper.m_Player_TongueButtonPress;
        public InputAction @TongueButtonRelease => m_Wrapper.m_Player_TongueButtonRelease;
        public InputAction @Pause => m_Wrapper.m_Player_Pause;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @PresstoRun.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPresstoRun;
                @PresstoRun.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPresstoRun;
                @PresstoRun.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPresstoRun;
                @HoldtoRun.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHoldtoRun;
                @HoldtoRun.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHoldtoRun;
                @HoldtoRun.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHoldtoRun;
                @Camera.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCamera;
                @Camera.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCamera;
                @Camera.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCamera;
                @PresstoTarget.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPresstoTarget;
                @PresstoTarget.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPresstoTarget;
                @PresstoTarget.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPresstoTarget;
                @HoldtoTargetPress.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHoldtoTargetPress;
                @HoldtoTargetPress.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHoldtoTargetPress;
                @HoldtoTargetPress.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHoldtoTargetPress;
                @HoldtoTargetRelease.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHoldtoTargetRelease;
                @HoldtoTargetRelease.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHoldtoTargetRelease;
                @HoldtoTargetRelease.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHoldtoTargetRelease;
                @TongueButtonPress.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTongueButtonPress;
                @TongueButtonPress.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTongueButtonPress;
                @TongueButtonPress.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTongueButtonPress;
                @TongueButtonRelease.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTongueButtonRelease;
                @TongueButtonRelease.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTongueButtonRelease;
                @TongueButtonRelease.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTongueButtonRelease;
                @Pause.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @PresstoRun.started += instance.OnPresstoRun;
                @PresstoRun.performed += instance.OnPresstoRun;
                @PresstoRun.canceled += instance.OnPresstoRun;
                @HoldtoRun.started += instance.OnHoldtoRun;
                @HoldtoRun.performed += instance.OnHoldtoRun;
                @HoldtoRun.canceled += instance.OnHoldtoRun;
                @Camera.started += instance.OnCamera;
                @Camera.performed += instance.OnCamera;
                @Camera.canceled += instance.OnCamera;
                @PresstoTarget.started += instance.OnPresstoTarget;
                @PresstoTarget.performed += instance.OnPresstoTarget;
                @PresstoTarget.canceled += instance.OnPresstoTarget;
                @HoldtoTargetPress.started += instance.OnHoldtoTargetPress;
                @HoldtoTargetPress.performed += instance.OnHoldtoTargetPress;
                @HoldtoTargetPress.canceled += instance.OnHoldtoTargetPress;
                @HoldtoTargetRelease.started += instance.OnHoldtoTargetRelease;
                @HoldtoTargetRelease.performed += instance.OnHoldtoTargetRelease;
                @HoldtoTargetRelease.canceled += instance.OnHoldtoTargetRelease;
                @TongueButtonPress.started += instance.OnTongueButtonPress;
                @TongueButtonPress.performed += instance.OnTongueButtonPress;
                @TongueButtonPress.canceled += instance.OnTongueButtonPress;
                @TongueButtonRelease.started += instance.OnTongueButtonRelease;
                @TongueButtonRelease.performed += instance.OnTongueButtonRelease;
                @TongueButtonRelease.canceled += instance.OnTongueButtonRelease;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Menu
    private readonly InputActionMap m_Menu;
    private IMenuActions m_MenuActionsCallbackInterface;
    public struct MenuActions
    {
        private @InputControls m_Wrapper;
        public MenuActions(@InputControls wrapper) { m_Wrapper = wrapper; }
        public InputActionMap Get() { return m_Wrapper.m_Menu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
        public void SetCallbacks(IMenuActions instance)
        {
            if (m_Wrapper.m_MenuActionsCallbackInterface != null)
            {
            }
            m_Wrapper.m_MenuActionsCallbackInterface = instance;
            if (instance != null)
            {
            }
        }
    }
    public MenuActions @Menu => new MenuActions(this);
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnPresstoRun(InputAction.CallbackContext context);
        void OnHoldtoRun(InputAction.CallbackContext context);
        void OnCamera(InputAction.CallbackContext context);
        void OnPresstoTarget(InputAction.CallbackContext context);
        void OnHoldtoTargetPress(InputAction.CallbackContext context);
        void OnHoldtoTargetRelease(InputAction.CallbackContext context);
        void OnTongueButtonPress(InputAction.CallbackContext context);
        void OnTongueButtonRelease(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
    public interface IMenuActions
    {
    }
}
