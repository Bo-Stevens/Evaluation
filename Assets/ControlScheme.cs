//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/ControlScheme.inputactions
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

public partial class @ControlScheme : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @ControlScheme()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""ControlScheme"",
    ""maps"": [
        {
            ""name"": ""CameraMovement"",
            ""id"": ""58894e8e-216c-42cd-83cb-71073081fea0"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""de81bb4c-18e5-4d42-baa6-5dc6e2c2c4f6"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""Value"",
                    ""id"": ""250f53d7-e436-46f3-a820-fb9222646e5d"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""SwivelMode"",
                    ""type"": ""Button"",
                    ""id"": ""26e66d49-9036-4253-b7bb-df1463d46fcc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=0.1)"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SwivelCamera"",
                    ""type"": ""Value"",
                    ""id"": ""7c5ebca6-53e5-4543-8575-4629ea332188"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""20b5d62f-a917-4242-aa36-5c1b403c6e2a"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""5b46fc6b-1738-4bb1-9896-dcad54dbc7fc"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""835a1f2d-27c6-470d-88b7-cafe61affb13"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f9b424ff-de50-4865-a980-db0633676590"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""9b3ff35b-f70f-45cc-9e8a-b7b8f6fd942a"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""21db617e-aeac-466e-b3ec-02ef4861b22a"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""b50a023e-9205-4bf3-b1cd-170468a29756"",
                    ""path"": ""<Mouse>/scroll/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""7b00fe4d-3787-46e2-80bb-0f10abe2c44f"",
                    ""path"": ""<Mouse>/scroll/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b628d50d-b5c2-4fcc-8977-4afcf75102ec"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwivelMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""ccf34874-7c66-4ae3-8311-e9c8bc70039e"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwivelCamera"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""24f5cfd0-a876-4eaf-9841-d2853fb669c2"",
                    ""path"": ""<Mouse>/delta/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwivelCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""6f89d847-55c1-47ef-b18b-9cff6d16da2b"",
                    ""path"": ""<Mouse>/delta/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwivelCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""a46fa930-25a1-4238-a9bf-459204baee10"",
                    ""path"": ""<Mouse>/delta/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwivelCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""945b5222-6d9c-4ee0-9ce4-a3561881bd3d"",
                    ""path"": ""<Mouse>/delta/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwivelCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""PC"",
            ""bindingGroup"": ""PC"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<VirtualMouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // CameraMovement
        m_CameraMovement = asset.FindActionMap("CameraMovement", throwIfNotFound: true);
        m_CameraMovement_Movement = m_CameraMovement.FindAction("Movement", throwIfNotFound: true);
        m_CameraMovement_Zoom = m_CameraMovement.FindAction("Zoom", throwIfNotFound: true);
        m_CameraMovement_SwivelMode = m_CameraMovement.FindAction("SwivelMode", throwIfNotFound: true);
        m_CameraMovement_SwivelCamera = m_CameraMovement.FindAction("SwivelCamera", throwIfNotFound: true);
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

    // CameraMovement
    private readonly InputActionMap m_CameraMovement;
    private ICameraMovementActions m_CameraMovementActionsCallbackInterface;
    private readonly InputAction m_CameraMovement_Movement;
    private readonly InputAction m_CameraMovement_Zoom;
    private readonly InputAction m_CameraMovement_SwivelMode;
    private readonly InputAction m_CameraMovement_SwivelCamera;
    public struct CameraMovementActions
    {
        private @ControlScheme m_Wrapper;
        public CameraMovementActions(@ControlScheme wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_CameraMovement_Movement;
        public InputAction @Zoom => m_Wrapper.m_CameraMovement_Zoom;
        public InputAction @SwivelMode => m_Wrapper.m_CameraMovement_SwivelMode;
        public InputAction @SwivelCamera => m_Wrapper.m_CameraMovement_SwivelCamera;
        public InputActionMap Get() { return m_Wrapper.m_CameraMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraMovementActions set) { return set.Get(); }
        public void SetCallbacks(ICameraMovementActions instance)
        {
            if (m_Wrapper.m_CameraMovementActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_CameraMovementActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_CameraMovementActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_CameraMovementActionsCallbackInterface.OnMovement;
                @Zoom.started -= m_Wrapper.m_CameraMovementActionsCallbackInterface.OnZoom;
                @Zoom.performed -= m_Wrapper.m_CameraMovementActionsCallbackInterface.OnZoom;
                @Zoom.canceled -= m_Wrapper.m_CameraMovementActionsCallbackInterface.OnZoom;
                @SwivelMode.started -= m_Wrapper.m_CameraMovementActionsCallbackInterface.OnSwivelMode;
                @SwivelMode.performed -= m_Wrapper.m_CameraMovementActionsCallbackInterface.OnSwivelMode;
                @SwivelMode.canceled -= m_Wrapper.m_CameraMovementActionsCallbackInterface.OnSwivelMode;
                @SwivelCamera.started -= m_Wrapper.m_CameraMovementActionsCallbackInterface.OnSwivelCamera;
                @SwivelCamera.performed -= m_Wrapper.m_CameraMovementActionsCallbackInterface.OnSwivelCamera;
                @SwivelCamera.canceled -= m_Wrapper.m_CameraMovementActionsCallbackInterface.OnSwivelCamera;
            }
            m_Wrapper.m_CameraMovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Zoom.started += instance.OnZoom;
                @Zoom.performed += instance.OnZoom;
                @Zoom.canceled += instance.OnZoom;
                @SwivelMode.started += instance.OnSwivelMode;
                @SwivelMode.performed += instance.OnSwivelMode;
                @SwivelMode.canceled += instance.OnSwivelMode;
                @SwivelCamera.started += instance.OnSwivelCamera;
                @SwivelCamera.performed += instance.OnSwivelCamera;
                @SwivelCamera.canceled += instance.OnSwivelCamera;
            }
        }
    }
    public CameraMovementActions @CameraMovement => new CameraMovementActions(this);
    private int m_PCSchemeIndex = -1;
    public InputControlScheme PCScheme
    {
        get
        {
            if (m_PCSchemeIndex == -1) m_PCSchemeIndex = asset.FindControlSchemeIndex("PC");
            return asset.controlSchemes[m_PCSchemeIndex];
        }
    }
    public interface ICameraMovementActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
        void OnSwivelMode(InputAction.CallbackContext context);
        void OnSwivelCamera(InputAction.CallbackContext context);
    }
}