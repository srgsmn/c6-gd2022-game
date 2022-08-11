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


    // EVENT SUBSCRIBER ________________________________________________________ EVENT SUBSCRIBER

    private void EventSubscriber(bool subscribing = true)
    {
        if (subscribing)
        {
            // Debug inputs
            inputs.Debug.IsDebug.started += OnIsDebug;
            inputs.Debug.IsDebug.canceled += OnIsDebug;
            inputs.Debug.DebugSwitcher.started += OnDebugSwitcher;
            inputs.Debug.DebugSwitcher.canceled += OnDebugSwitcher;
            inputs.Debug.Health.started += OnDebugHealth;
            inputs.Debug.Health.canceled += OnDebugHealth;
            inputs.Debug.Armor.started += OnDebugArmor;
            inputs.Debug.Armor.canceled += OnDebugArmor;
            inputs.Debug.SL.started += OnDebugSL;
            inputs.Debug.SL.canceled += OnDebugSL;
            inputs.Debug.CC.started += OnDebugSL;
            inputs.Debug.CC.canceled += OnDebugCC;
            inputs.Debug.Inc.started += OnDebugInc;
            inputs.Debug.Inc.canceled += OnDebugInc;
            inputs.Debug.Dec.started += OnDebugDec;
            inputs.Debug.Dec.canceled += OnDebugDec;
            inputs.Debug.Max.started += OnDebugMax;
            inputs.Debug.Max.canceled += OnDebugMax;
            inputs.Debug.Reset.started += OnDebugReset;
            inputs.Debug.Reset.canceled += OnDebugReset;
            inputs.Debug.Save.performed += OnDebugSavePressed;
            inputs.Debug.Load.performed += OnDebugLoadPressed;
        }
        else
        {
            // Debug inputs
            inputs.Debug.IsDebug.started -= OnIsDebug;
            inputs.Debug.IsDebug.canceled -= OnIsDebug;
            inputs.Debug.DebugSwitcher.started -= OnDebugSwitcher;
            inputs.Debug.DebugSwitcher.canceled -= OnDebugSwitcher;
            inputs.Debug.Health.started -= OnDebugHealth;
            inputs.Debug.Health.canceled -= OnDebugHealth;
            inputs.Debug.Armor.started -= OnDebugArmor;
            inputs.Debug.Armor.canceled -= OnDebugArmor;
            inputs.Debug.SL.started -= OnDebugSL;
            inputs.Debug.SL.canceled -= OnDebugSL;
            inputs.Debug.CC.started -= OnDebugSL;
            inputs.Debug.CC.canceled -= OnDebugCC;
            inputs.Debug.Inc.started -= OnDebugInc;
            inputs.Debug.Inc.canceled -= OnDebugInc;
            inputs.Debug.Dec.started -= OnDebugDec;
            inputs.Debug.Dec.canceled -= OnDebugDec;
            inputs.Debug.Max.started -= OnDebugMax;
            inputs.Debug.Max.canceled -= OnDebugMax;
            inputs.Debug.Reset.started -= OnDebugReset;
            inputs.Debug.Reset.canceled -= OnDebugReset;
            inputs.Debug.Save.performed -= OnDebugSavePressed;
            inputs.Debug.Load.performed -= OnDebugLoadPressed;
        }
    }

    // EVENT CALLBACKS _________________________________________________________ EVENT CALLBACKS

    private void OnIsDebug(InputAction.CallbackContext context)
    {
        Deb("OnIsDebug(): " + context.ReadValueAsButton() );

        debugOn = context.ReadValueAsButton();
    }

    private void OnDebugSwitcher(InputAction.CallbackContext context)
    {
        Deb("OnDebugSwitcher(): " + context.ReadValueAsButton());

        debugSwitcher = context.ReadValueAsButton();
    }

    private void OnDebugHealth(InputAction.CallbackContext context)
    {
        Deb("OnDebugHealth(): " + context.ReadValueAsButton());

        debugHealth = context.ReadValueAsButton();
    }

    private void OnDebugArmor(InputAction.CallbackContext context)
    {
        Deb("OnDebugArmor(): " + context.ReadValueAsButton());

        debugArmor = context.ReadValueAsButton();
    }

    private void OnDebugSL(InputAction.CallbackContext context)
    {
        Deb("OnDebugSL(): " + context.ReadValueAsButton());

        debugSL = context.ReadValueAsButton();
    }

    private void OnDebugCC(InputAction.CallbackContext context)
    {
        Deb("OnDebugCC(): " + context.ReadValueAsButton());

        debugCC = context.ReadValueAsButton();
    }

    private void OnDebugInc(InputAction.CallbackContext context)
    {
        Deb("OnDebugInc(): " + context.ReadValueAsButton());

        debugInc = context.ReadValueAsButton();
    }

    private void OnDebugDec(InputAction.CallbackContext context)
    {
        Deb("OnDebugDec(): " + context.ReadValueAsButton());

        debugDec = context.ReadValueAsButton();
    }

    private void OnDebugMax(InputAction.CallbackContext context)
    {
        Deb("OnDebugMax(): " + context.ReadValueAsButton());

        debugMax = context.ReadValueAsButton();
    }

    private void OnDebugReset(InputAction.CallbackContext context)
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
