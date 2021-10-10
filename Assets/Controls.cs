// GENERATED AUTOMATICALLY FROM 'Assets/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""custom"",
            ""id"": ""62100aca-f4b4-43f6-908d-a913ceaa0acd"",
            ""actions"": [
                {
                    ""name"": ""PrimaryStick"",
                    ""type"": ""Value"",
                    ""id"": ""acbe9398-f8e2-4296-ac59-ec733215aef8"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SecondaryStick"",
                    ""type"": ""Value"",
                    ""id"": ""408546ee-8576-40b4-8cd2-50e8f17efc70"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Trigger"",
                    ""type"": ""Button"",
                    ""id"": ""de57c233-f6bf-48fc-8ef2-209b5fb1b135"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""020038d9-c44e-464f-9dbe-ea745f09f6cc"",
                    ""path"": ""*/{Primary2DAxis}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6922f1d5-aa87-42c5-90fb-965bf7f63ed9"",
                    ""path"": ""*/{Secondary2DAxis}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SecondaryStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7869f10f-9525-4aaf-ba68-331d4d143f17"",
                    ""path"": ""*/{PrimaryTrigger}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Trigger"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // custom
        m_custom = asset.FindActionMap("custom", throwIfNotFound: true);
        m_custom_PrimaryStick = m_custom.FindAction("PrimaryStick", throwIfNotFound: true);
        m_custom_SecondaryStick = m_custom.FindAction("SecondaryStick", throwIfNotFound: true);
        m_custom_Trigger = m_custom.FindAction("Trigger", throwIfNotFound: true);
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

    // custom
    private readonly InputActionMap m_custom;
    private ICustomActions m_CustomActionsCallbackInterface;
    private readonly InputAction m_custom_PrimaryStick;
    private readonly InputAction m_custom_SecondaryStick;
    private readonly InputAction m_custom_Trigger;
    public struct CustomActions
    {
        private @Controls m_Wrapper;
        public CustomActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @PrimaryStick => m_Wrapper.m_custom_PrimaryStick;
        public InputAction @SecondaryStick => m_Wrapper.m_custom_SecondaryStick;
        public InputAction @Trigger => m_Wrapper.m_custom_Trigger;
        public InputActionMap Get() { return m_Wrapper.m_custom; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CustomActions set) { return set.Get(); }
        public void SetCallbacks(ICustomActions instance)
        {
            if (m_Wrapper.m_CustomActionsCallbackInterface != null)
            {
                @PrimaryStick.started -= m_Wrapper.m_CustomActionsCallbackInterface.OnPrimaryStick;
                @PrimaryStick.performed -= m_Wrapper.m_CustomActionsCallbackInterface.OnPrimaryStick;
                @PrimaryStick.canceled -= m_Wrapper.m_CustomActionsCallbackInterface.OnPrimaryStick;
                @SecondaryStick.started -= m_Wrapper.m_CustomActionsCallbackInterface.OnSecondaryStick;
                @SecondaryStick.performed -= m_Wrapper.m_CustomActionsCallbackInterface.OnSecondaryStick;
                @SecondaryStick.canceled -= m_Wrapper.m_CustomActionsCallbackInterface.OnSecondaryStick;
                @Trigger.started -= m_Wrapper.m_CustomActionsCallbackInterface.OnTrigger;
                @Trigger.performed -= m_Wrapper.m_CustomActionsCallbackInterface.OnTrigger;
                @Trigger.canceled -= m_Wrapper.m_CustomActionsCallbackInterface.OnTrigger;
            }
            m_Wrapper.m_CustomActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PrimaryStick.started += instance.OnPrimaryStick;
                @PrimaryStick.performed += instance.OnPrimaryStick;
                @PrimaryStick.canceled += instance.OnPrimaryStick;
                @SecondaryStick.started += instance.OnSecondaryStick;
                @SecondaryStick.performed += instance.OnSecondaryStick;
                @SecondaryStick.canceled += instance.OnSecondaryStick;
                @Trigger.started += instance.OnTrigger;
                @Trigger.performed += instance.OnTrigger;
                @Trigger.canceled += instance.OnTrigger;
            }
        }
    }
    public CustomActions @custom => new CustomActions(this);
    public interface ICustomActions
    {
        void OnPrimaryStick(InputAction.CallbackContext context);
        void OnSecondaryStick(InputAction.CallbackContext context);
        void OnTrigger(InputAction.CallbackContext context);
    }
}
