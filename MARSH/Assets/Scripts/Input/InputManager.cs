/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 * 
 *  TODO:
 *      - UI input management (isGUIActive when menu are open)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Globals;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private PlayerInput inputs;



    // DEBUG VARIABLES _________________________________________________________ DEBUG VARIABLES

    [Header("Character Controls:")]
    [SerializeField][ReadOnlyInspector] private Vector2 movementInput;
    [SerializeField][ReadOnlyInspector] private bool jumpInput;
    [SerializeField][ReadOnlyInspector] private bool runInput;

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

    [Header("UI:")]
    [SerializeField][ReadOnlyInspector] private bool isGUIActive = false;

    // COMPONENT LIFECYCLE METHODS _____________________________________________ COMPONENT LIFECYCLE METHODS

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
        }

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
        /*
        if (debugOn)
        {
            
            if (debugSwitcher)  

            if (debugHealth && debugInc)
            {
                Deb("Update(): Detected input command for Inc H");
                OnDebugValueUpdate?.Invoke(DebugValue.H, DebugAction.Inc);
            }
            if (debugHealth && debugDec)
            {
                Deb("Update(): Detected input command for Dec H");
                OnDebugValueUpdate?.Invoke(DebugValue.H, DebugAction.Dec);
            }
            if (debugHealth && debugMax)
            {
                Deb("Update(): Detected input command for Max H");
                OnDebugValueUpdate?.Invoke(DebugValue.H, DebugAction.Max);
            }
            if (debugHealth && debugRst)
            {
                Deb("Update(): Detected input command for Rst H");
                OnDebugValueUpdate?.Invoke(DebugValue.H, DebugAction.Rst);
            }
            if (debugArmor && debugInc)
            {
                Deb("Update(): Detected input command for Inc A");
                OnDebugValueUpdate?.Invoke(DebugValue.A, DebugAction.Inc);
            }
            if (debugArmor && debugDec)
            {
                Deb("Update(): Detected input command for Dec A");
                OnDebugValueUpdate?.Invoke(DebugValue.A, DebugAction.Dec);
            }
            if (debugArmor && debugMax)
            {
                Deb("Update(): Detected input command for Max A");
                OnDebugValueUpdate?.Invoke(DebugValue.A, DebugAction.Max);
            }
            if (debugArmor && debugRst)
            {
                Deb("Update(): Detected input command for Rst A");
                OnDebugValueUpdate?.Invoke(DebugValue.A, DebugAction.Rst);
            }
            if (debugSL && debugInc)
            {
                Deb("Update(): Detected input command for Inc SL");
                OnDebugValueUpdate?.Invoke(DebugValue.SL, DebugAction.Inc);
            }
            if (debugSL && debugDec)
            {
                Deb("Update(): Detected input command for Dec SL");
                OnDebugValueUpdate?.Invoke(DebugValue.SL, DebugAction.Dec);
            }
            if (debugSL && debugMax)
            {
                Deb("Update(): Detected input command for Max SL");
                OnDebugValueUpdate?.Invoke(DebugValue.SL, DebugAction.Max);
            }
            if (debugSL && debugRst)
            {
                Deb("Update(): Detected input command for Rst SL");
                OnDebugValueUpdate?.Invoke(DebugValue.SL, DebugAction.Rst);
            }
            if (debugCC && debugInc)
            {
                Deb("Update(): Detected input command for Inc CC");
                OnDebugValueUpdate?.Invoke(DebugValue.CC, DebugAction.Inc);
            }
            if (debugCC && debugDec)
            {
                Deb("Update(): Detected input command for Dec CC");
                OnDebugValueUpdate?.Invoke(DebugValue.CC, DebugAction.Dec);
            }
            if (debugCC && debugMax)
            {
                Deb("Update(): Detected input command for Max CC");
                OnDebugValueUpdate?.Invoke(DebugValue.CC, DebugAction.Max);
            }
            if (debugCC && debugRst)
            {
                Deb("Update(): Detected input command for Rst CC");
                OnDebugValueUpdate?.Invoke(DebugValue.CC, DebugAction.Rst);
            }

            if (debugSave)
            {
                Deb("Update(): Detected input command for Save");
                OnDebugSave?.Invoke();
            }
            if (debugLoad)
            {
                Deb("Update(): Detected input command for Load");
                OnDebugLoad?.Invoke();
            }
            if (debugQuit)
            {
                Deb("Update(): Detected input command for Quit");
                OnDebugQuit?.Invoke();
            }
        }
        */
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

    // Debug

    public delegate void DebugModeSwitcherEvent();
    public static DebugModeSwitcherEvent OnDebugModeSwitch;

    public delegate void DebugValueUpdateEvent(DebValue value, DebAction action);
    public static DebugValueUpdateEvent OnDebugValueUpdate;

    public delegate void DebugSaveEvent();
    public static DebugSaveEvent OnDebugSave;
    public delegate void DebugLoadEvent();
    public static DebugLoadEvent OnDebugLoad;
    public delegate void DebugQuitEvent();
    public static DebugQuitEvent OnDebugQuit;

    // Character Controls

    public delegate void MovementInputEvent(Vector2 movementInput);
    public static MovementInputEvent OnMovementInput;
    public delegate void JumpInputEvent(bool isPressed);
    public static JumpInputEvent OnJumpInput;
    public delegate void RunInputEvent(bool isPressed);
    public static RunInputEvent OnRunInput;

    // GameState and UI
    public delegate void PauseInputEvent();
    public static PauseInputEvent OnPause;
    public delegate void BackInputEvent();
    public static BackInputEvent OnBack;


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
            inputs.Debug.CC.started += OnDebugCCPressed;
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

            inputs.CharacterControls.Move.started += OnMovementInputPressed;
            inputs.CharacterControls.Move.canceled += OnMovementInputPressed;
            inputs.CharacterControls.Move.performed += OnMovementInputPressed;
            inputs.CharacterControls.Run.started += OnRunInputPressed;
            inputs.CharacterControls.Run.canceled += OnRunInputPressed;
            inputs.CharacterControls.Jump.started += OnJumpInputPressed;
            inputs.CharacterControls.Jump.canceled += OnJumpInputPressed;

            inputs.UI.Pause.started += OnPausePressed;
            inputs.UI.Back.performed += OnBackPressed;
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
            inputs.Debug.CC.started -= OnDebugCCPressed;
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

            inputs.CharacterControls.Move.started -= OnMovementInputPressed;
            inputs.CharacterControls.Move.canceled -= OnMovementInputPressed;
            inputs.CharacterControls.Move.performed -= OnMovementInputPressed;
            inputs.CharacterControls.Run.started -= OnRunInputPressed;
            inputs.CharacterControls.Run.canceled -= OnRunInputPressed;
            inputs.CharacterControls.Jump.started -= OnJumpInputPressed;
            inputs.CharacterControls.Jump.canceled -= OnJumpInputPressed;

            inputs.UI.Pause.started -= OnPausePressed;
            inputs.UI.Back.performed -= OnBackPressed;
        }
    }

    // EVENT CALLBACKS _________________________________________________________ EVENT CALLBACKS

    // Debug

    private void OnDebugPressed(InputAction.CallbackContext context)
    {
        //Deb("OnIsDebug(): " + context.ReadValueAsButton() );

        debugOn = context.ReadValueAsButton();
    }

    private void OnDebugSwitcherPressed(InputAction.CallbackContext context)
    {
        //Deb("OnDebugSwitcher(): " + context.ReadValueAsButton());

        if (debugOn) debugSwitcher = context.ReadValueAsButton();

        if(debugSwitcher) OnDebugModeSwitch?.Invoke();
    }

    private void OnDebugHealthPressed(InputAction.CallbackContext context)
    {
        //Deb("OnDebugHealth(): " + context.ReadValueAsButton());

        if (debugOn) debugHealth = context.ReadValueAsButton();
    }

    private void OnDebugArmorPressed(InputAction.CallbackContext context)
    {
        //Deb("OnDebugArmor(): " + context.ReadValueAsButton());

        if (debugOn) debugArmor = context.ReadValueAsButton();
    }

    private void OnDebugSLPressed(InputAction.CallbackContext context)
    {
        //Deb("OnDebugSL(): " + context.ReadValueAsButton());

        if (debugOn) debugSL = context.ReadValueAsButton();
    }

    private void OnDebugCCPressed(InputAction.CallbackContext context)
    {
        //Deb("OnDebugCC(): " + context.ReadValueAsButton());

        if (debugOn) debugCC = context.ReadValueAsButton();
    }

    private void OnDebugIncPressed(InputAction.CallbackContext context)
    {
        //Deb("OnDebugInc(): " + context.ReadValueAsButton());

        if (debugOn) debugInc = context.ReadValueAsButton();

        if (debugOn)
        {
            Deb("OnDebugInc(): check: SL = " + debugSL + " CC = " + debugCC);

            debugInc = context.ReadValueAsButton();
            DebValue? param = ParameterHandler();

            if (param != null && debugInc)
            {
                Deb("OnDebugInc(): invoking inc event for param " + param);
                OnDebugValueUpdate?.Invoke((DebValue)param, DebAction.Inc);
            }
        }
    }

    private void OnDebugDecPressed(InputAction.CallbackContext context)
    {
        //Deb("OnDebugDec(): " + context.ReadValueAsButton());

        if (debugOn)
        {
            debugDec = context.ReadValueAsButton();
            DebValue? param = ParameterHandler();

            if (param != null && debugDec)
            {
                Deb("OnDebugDec(): invoking dec event for param " + param);
                OnDebugValueUpdate?.Invoke((DebValue)param, DebAction.Dec);
            }
        }
    }

    private void OnDebugMaxPressed(InputAction.CallbackContext context)
    {
        //Deb("OnDebugMax(): " + context.ReadValueAsButton());

        if (debugOn)
        {
            debugMax = context.ReadValueAsButton();
            DebValue? param = ParameterHandler();

            if (param != null && debugMax)
            {
                Deb("OnDebugMax(): invoking max event for param " + param);
                OnDebugValueUpdate?.Invoke((DebValue)param, DebAction.Max);
            }
        }
    }

    private void OnDebugResetPressed(InputAction.CallbackContext context)
    {
        //Deb("OnDebugReset(): " + context.ReadValueAsButton());

        if (debugOn)
        {
            debugRst = context.ReadValueAsButton();
            DebValue? param = ParameterHandler();

            if (param != null && debugRst)
            {
                Deb("OnDebugReset(): invoking reset event for param " + param);
                OnDebugValueUpdate?.Invoke((DebValue)param, DebAction.Rst);
            }
        }
    }

    private DebValue? ParameterHandler()
    {
        if (debugHealth) return DebValue.H;
        if (debugArmor) return DebValue.A;
        if (debugSL) return DebValue.SL;
        if (debugCC) return DebValue.CC;

        return null;
    }

    private void OnDebugSavePressed(InputAction.CallbackContext context)
    {
        //Deb("OnDebugSavePressed(): " + context.ReadValueAsButton());

        if (debugOn) debugSave = context.ReadValueAsButton();

        if(debugSave) OnDebugSave?.Invoke();
    }

    private void OnDebugLoadPressed(InputAction.CallbackContext context)
    {
        //Deb("OnDebugLoadPressed(): " + context.ReadValueAsButton());

        if (debugOn) debugLoad = context.ReadValueAsButton();

        if (debugLoad) OnDebugLoad?.Invoke();
    }

    private void OnDebugQuitPressed(InputAction.CallbackContext context)
    {
        //Deb("OnDebugLoadPressed(): " + context.ReadValueAsButton());

        if (debugOn) debugQuit = context.ReadValueAsButton();

        if (debugQuit) OnDebugQuit?.Invoke();
    }

    // Character Controls

    private void OnMovementInputPressed(InputAction.CallbackContext context)
    {
        //Deb("OnMovementInput(): " + context.ReadValue<Vector2>());

        if (!debugOn)
        {
            movementInput = context.ReadValue<Vector2>();

            OnMovementInput?.Invoke(movementInput);
        }
    }

    private void OnRunInputPressed(InputAction.CallbackContext context)
    {
        //Deb("OnMovementInput(): " + context.ReadValueAsButton());

        if (!debugOn)
        {
            runInput = context.ReadValueAsButton();

            OnRunInput?.Invoke(runInput);
        }
    }

    private void OnJumpInputPressed(InputAction.CallbackContext context)
    {
        //Deb("OnMovementInput(): " + context.ReadValueAsButton());

        if (!debugOn)
        {
            jumpInput = context.ReadValueAsButton();

            OnJumpInput?.Invoke(jumpInput);
        }
    }

    private void OnPausePressed(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            OnPause?.Invoke();
        }
    }

    private void OnBackPressed(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            OnBack?.Invoke();
        }
    }

    // DEBUG PRINTER ___________________________________________________________ DEBUG PRINTER

    private void Deb(string msg, DebMsgType type = DebMsgType.log)
    {
        switch (type)
        {
            case DebMsgType.log:
                Debug.Log(this.GetType().Name + " > " + msg);

                break;

            case DebMsgType.warn:
                Debug.LogWarning(this.GetType().Name + " > " + msg);

                break;

            case DebMsgType.err:
                Debug.LogError(this.GetType().Name + " > " + msg);


                break;
        }
    }
}
