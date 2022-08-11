/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Globals;
using static Globals.Functions;

public class InputManager : MonoBehaviour
{
    private PlayerInput inputs;

    // DEBUG VARIABLES _________________________________________________________ DEBUG VARIABLES

    [Header("Character Controls:")]

    [Header("Debug:")]
    [SerializeField][ReadOnlyInspector] private bool debugOn = false;
    [SerializeField][ReadOnlyInspector] private bool debugSwitcher = false;
    [SerializeField][ReadOnlyInspector] private bool debugHealth = false;
    [SerializeField][ReadOnlyInspector] private bool debugArmor = false;
    [SerializeField][ReadOnlyInspector] private bool debugSL = false;
    [SerializeField][ReadOnlyInspector] private bool debugCC = false;
    [SerializeField][ReadOnlyInspector] private bool debugInc = false;
    [SerializeField][ReadOnlyInspector] private bool debugDec = false;
    [SerializeField][ReadOnlyInspector] private bool debugMax = false;
    [SerializeField][ReadOnlyInspector] private bool debugRst = false;
    [SerializeField][ReadOnlyInspector] private bool debugSave = false;
    [SerializeField][ReadOnlyInspector] private bool debugLoad = false;
    [SerializeField][ReadOnlyInspector] private bool debugQuit = false;

    //[Header("UI:")]

    // COMPONENT LIFECYCLE METHODS _____________________________________________ COMPONENT LIFECYCLE METHODS

    private void Awake()
    {
        inputs = new PlayerInput();

        EventSubscriber();
    }

    private void OnEnable()
    {
        inputs.Enable();
    }

    //Start()

    private void Update()
    {
        if (debugOn)
        {
            if (debugSwitcher)  OnDebugModeSwitch?.Invoke();

            if (debugHealth && debugInc) OnDebugValueUpdate?.Invoke(DebugValue.H, DebugAction.Inc);
            if (debugHealth && debugDec) OnDebugValueUpdate?.Invoke(DebugValue.H, DebugAction.Dec);
            if (debugHealth && debugMax) OnDebugValueUpdate?.Invoke(DebugValue.H, DebugAction.Max);
            if (debugHealth && debugRst) OnDebugValueUpdate?.Invoke(DebugValue.H, DebugAction.Rst);
            if (debugArmor && debugInc) OnDebugValueUpdate?.Invoke(DebugValue.A, DebugAction.Inc);
            if (debugArmor && debugDec) OnDebugValueUpdate?.Invoke(DebugValue.A, DebugAction.Dec);
            if (debugArmor && debugMax) OnDebugValueUpdate?.Invoke(DebugValue.A, DebugAction.Max);
            if (debugArmor && debugRst) OnDebugValueUpdate?.Invoke(DebugValue.A, DebugAction.Rst);
            if (debugSL && debugInc) OnDebugValueUpdate?.Invoke(DebugValue.SL, DebugAction.Inc);
            if (debugSL && debugDec) OnDebugValueUpdate?.Invoke(DebugValue.SL, DebugAction.Dec);
            if (debugSL && debugMax) OnDebugValueUpdate?.Invoke(DebugValue.SL, DebugAction.Max);
            if (debugSL && debugRst) OnDebugValueUpdate?.Invoke(DebugValue.SL, DebugAction.Rst);
            if (debugCC && debugInc) OnDebugValueUpdate?.Invoke(DebugValue.CC, DebugAction.Inc);
            if (debugCC && debugDec) OnDebugValueUpdate?.Invoke(DebugValue.CC, DebugAction.Dec);
            if (debugCC && debugMax) OnDebugValueUpdate?.Invoke(DebugValue.CC, DebugAction.Max);
            if (debugCC && debugRst) OnDebugValueUpdate?.Invoke(DebugValue.CC, DebugAction.Rst);

            if (debugSave) OnDebugSave?.Invoke();
            if (debugLoad) OnDebugLoad?.Invoke();
            if (debugQuit) OnDebugQuit?.Invoke();
        }
    }

    private void OnDisable()
    {
        inputs.Disable();
    }

    private void OnDestroy()
    {
        EventSubscriber(false);
    }

    // PROVIDED EVENTS _________________________________________________________ PROVIDED EVENTS

    public delegate void DebugModeSwitcherEvent();
    public static DebugModeSwitcherEvent OnDebugModeSwitch;

    public delegate void DebugValueUpdateEvent(DebugValue value, DebugAction action);
    public static DebugValueUpdateEvent OnDebugValueUpdate;

    public delegate void DebugSaveEvent();
    public static DebugSaveEvent OnDebugSave;
    public delegate void DebugLoadEvent();
    public static DebugLoadEvent OnDebugLoad;
    public delegate void DebugQuitEvent();
    public static DebugQuitEvent OnDebugQuit;


    // EVENT SUBSCRIBER ________________________________________________________ EVENT SUBSCRIBER

    private void EventSubscriber(bool subscribing = true)
    {
        if (subscribing)
        {
            // Debug inputs
            inputs.Debug.IsDebug.started += OnDebugPressed;
            inputs.Debug.IsDebug.canceled += OnDebugPressed;
            inputs.Debug.DebugSwitcher.started += OnDebugSwitcherPressed;
            inputs.Debug.DebugSwitcher.canceled += OnDebugSwitcherPressed;
            inputs.Debug.Health.started += OnDebugHealthPressed;
            inputs.Debug.Health.canceled += OnDebugHealthPressed;
            inputs.Debug.Armor.started += OnDebugArmorPressed;
            inputs.Debug.Armor.canceled += OnDebugArmorPressed;
            inputs.Debug.SL.started += OnDebugSLPressed;
            inputs.Debug.SL.canceled += OnDebugSLPressed;
            inputs.Debug.CC.started += OnDebugSLPressed;
            inputs.Debug.CC.canceled += OnDebugCCPressed;
            inputs.Debug.Inc.started += OnDebugIncPressed;
            inputs.Debug.Inc.canceled += OnDebugIncPressed;
            inputs.Debug.Dec.started += OnDebugDecPressed;
            inputs.Debug.Dec.canceled += OnDebugDecPressed;
            inputs.Debug.Max.started += OnDebugMaxPressed;
            inputs.Debug.Max.canceled += OnDebugMaxPressed;
            inputs.Debug.Reset.started += OnDebugResetPressed;
            inputs.Debug.Reset.canceled += OnDebugResetPressed;
            inputs.Debug.Save.started += OnDebugSavePressed;
            inputs.Debug.Save.canceled += OnDebugSavePressed;
            inputs.Debug.Load.started += OnDebugLoadPressed;
            inputs.Debug.Load.canceled += OnDebugLoadPressed;
            inputs.Debug.Quit.started += OnDebugQuitPressed;
            inputs.Debug.Quit.canceled += OnDebugQuitPressed;

        }
        else
        {
            // Debug inputs
            inputs.Debug.IsDebug.started -= OnDebugPressed;
            inputs.Debug.IsDebug.canceled -= OnDebugPressed;
            inputs.Debug.DebugSwitcher.started -= OnDebugSwitcherPressed;
            inputs.Debug.DebugSwitcher.canceled -= OnDebugSwitcherPressed;
            inputs.Debug.Health.started -= OnDebugHealthPressed;
            inputs.Debug.Health.canceled -= OnDebugHealthPressed;
            inputs.Debug.Armor.started -= OnDebugArmorPressed;
            inputs.Debug.Armor.canceled -= OnDebugArmorPressed;
            inputs.Debug.SL.started -= OnDebugSLPressed;
            inputs.Debug.SL.canceled -= OnDebugSLPressed;
            inputs.Debug.CC.started -= OnDebugSLPressed;
            inputs.Debug.CC.canceled -= OnDebugCCPressed;
            inputs.Debug.Inc.started -= OnDebugIncPressed;
            inputs.Debug.Inc.canceled -= OnDebugIncPressed;
            inputs.Debug.Dec.started -= OnDebugDecPressed;
            inputs.Debug.Dec.canceled -= OnDebugDecPressed;
            inputs.Debug.Max.started -= OnDebugMaxPressed;
            inputs.Debug.Max.canceled -= OnDebugMaxPressed;
            inputs.Debug.Reset.started -= OnDebugResetPressed;
            inputs.Debug.Reset.canceled -= OnDebugResetPressed;
            inputs.Debug.Save.started -= OnDebugSavePressed;
            inputs.Debug.Save.canceled -= OnDebugSavePressed;
            inputs.Debug.Load.started -= OnDebugLoadPressed;
            inputs.Debug.Load.canceled -= OnDebugLoadPressed;
            inputs.Debug.Quit.started -= OnDebugQuitPressed;
            inputs.Debug.Quit.canceled -= OnDebugQuitPressed;
        }
    }

    // EVENT CALLBACKS _________________________________________________________ EVENT CALLBACKS

    private void OnDebugPressed(InputAction.CallbackContext context)
    {
        Deb("OnIsDebug(): " + context.ReadValueAsButton() );

        debugOn = context.ReadValueAsButton();
    }

    private void OnDebugSwitcherPressed(InputAction.CallbackContext context)
    {
        Deb("OnDebugSwitcher(): " + context.ReadValueAsButton());

        debugSwitcher = context.ReadValueAsButton();
    }

    private void OnDebugHealthPressed(InputAction.CallbackContext context)
    {
        Deb("OnDebugHealth(): " + context.ReadValueAsButton());

        debugHealth = context.ReadValueAsButton();
    }

    private void OnDebugArmorPressed(InputAction.CallbackContext context)
    {
        Deb("OnDebugArmor(): " + context.ReadValueAsButton());

        debugArmor = context.ReadValueAsButton();
    }

    private void OnDebugSLPressed(InputAction.CallbackContext context)
    {
        Deb("OnDebugSL(): " + context.ReadValueAsButton());

        debugSL = context.ReadValueAsButton();
    }

    private void OnDebugCCPressed(InputAction.CallbackContext context)
    {
        Deb("OnDebugCC(): " + context.ReadValueAsButton());

        debugCC = context.ReadValueAsButton();
    }

    private void OnDebugIncPressed(InputAction.CallbackContext context)
    {
        Deb("OnDebugInc(): " + context.ReadValueAsButton());

        debugInc = context.ReadValueAsButton();
    }

    private void OnDebugDecPressed(InputAction.CallbackContext context)
    {
        Deb("OnDebugDec(): " + context.ReadValueAsButton());

        debugDec = context.ReadValueAsButton();
    }

    private void OnDebugMaxPressed(InputAction.CallbackContext context)
    {
        Deb("OnDebugMax(): " + context.ReadValueAsButton());

        debugMax = context.ReadValueAsButton();
    }

    private void OnDebugResetPressed(InputAction.CallbackContext context)
    {
        Deb("OnDebugReset(): " + context.ReadValueAsButton());

        debugRst = context.ReadValueAsButton();
    }

    private void OnDebugSavePressed(InputAction.CallbackContext context)
    {
        Deb("OnDebugSavePressed(): " + context.ReadValueAsButton());

        debugSave = context.ReadValueAsButton();
    }

    private void OnDebugLoadPressed(InputAction.CallbackContext context)
    {
        Deb("OnDebugLoadPressed(): " + context.ReadValueAsButton());

        debugLoad = context.ReadValueAsButton();
    }

    private void OnDebugQuitPressed(InputAction.CallbackContext context)
    {
        Deb("OnDebugLoadPressed(): " + context.ReadValueAsButton());

        debugQuit = context.ReadValueAsButton();
    }

    // DEBUG PRINTER ___________________________________________________________ DEBUG PRINTER

    private void Deb(string msg, DebugMsgType type = DebugMsgType.log)
    {
        switch (type)
        {
            case DebugMsgType.log:
                Debug.Log(this.GetType().Name + " > " + msg);

                break;

            case DebugMsgType.warn:
                Debug.LogWarning(this.GetType().Name + " > " + msg);

                break;

            case DebugMsgType.err:
                Debug.LogError(this.GetType().Name + " > " + msg);


                break;
        }
    }
}
