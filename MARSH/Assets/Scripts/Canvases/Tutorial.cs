/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 */
using System.Collections;
using System.Collections.Generic;
using Globals;
using UnityEngine;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class Tutorial : MonoBehaviour
{
    // COMPONENT ATTRIBUTES ____________________________________________________ COMPONENT ATTRIBUTES

    [SerializeField] private GameObject[] panels;
    [SerializeField][ReadOnlyInspector] private int panelIndex;
    [SerializeField][ReadOnlyInspector] private TutorialPhase phase;
    [SerializeField][ReadOnlyInspector] private bool awaiting = false;
    [SerializeField][ReadOnlyInspector][Tooltip("Only eventually true if tutorial phase is Sprint")] private bool moving = false;

    // COMPONENT LIFECYCLE METHODS _____________________________________________ COMPONENT LIFECYCLE METHODS

    private void Awake()
    {
        EventSubscriber();
    }

    void Start()
    {
        StartTutorial();
    }

    private void OnEnable()
    {
        StartTutorial();
    }

    private void OnDestroy()
    {
        EventSubscriber(false);
    }

    private void Freeze(bool flag=true)
    {
        GameManager.Instance.Freeze(flag);
    }

    private void StartTutorial()
    {
        panelIndex = 0;
        panels[panelIndex].SetActive(true);
        phase = TutorialPhase.Welcome;
        Freeze();
    }

    // PROVIDED EVENTS _________________________________________________________ PROVIDED EVENTS

    public delegate void TutorialEndEvent();
    public static TutorialEndEvent OnTutorialEnd;

    // EVENT SUBSCRIBER ________________________________________________________ EVENT SUBSCRIBER

    private void EventSubscriber(bool subscribing = true)
    {
        if (subscribing)
        {
            InputManager.OnEnter += OnEnter;
            GameManager.OnStartTutorial += StartTutorial;
        }
        else
        {
            InputManager.OnEnter -= OnEnter;
            GameManager.OnStartTutorial -= StartTutorial;
        }
    }

    // EVENT CALLBACKS _________________________________________________________ EVENT CALLBACKS

    private void OnEnter()
    {
        switch (phase)
        {
            case TutorialPhase.Welcome:
                panels[panelIndex++].SetActive(false);
                panels[panelIndex].SetActive(true);
                phase = TutorialPhase.View;

                break;

            case TutorialPhase.View:
                panels[panelIndex++].SetActive(false);
                panels[panelIndex].SetActive(true);
                phase = TutorialPhase.Movement;

                break;

            case TutorialPhase.Movement:
                if (!awaiting)
                {
                    panels[panelIndex++].SetActive(false);
                    InputManager.OnMovementInput += OnMovementInput;
                    Freeze(false);

                    //awaiting = true;
                }

                break;

            case TutorialPhase.Jump:
                if (!awaiting)
                {
                    panels[panelIndex++].SetActive(false);
                    InputManager.OnJumpInput += OnJumpInput;
                    Freeze(false);

                    awaiting = true;
                }

                break;

            case TutorialPhase.Sprint:
                if (!awaiting)
                {
                    panels[panelIndex++].SetActive(false);
                    InputManager.OnMovementInput += OnMovementInput;
                    InputManager.OnRunInput += OnRunInput;

                    Freeze(false);

                    //awaiting = true;
                }

                break;

            case TutorialPhase.Attack:
                if (!awaiting)
                {
                    panels[panelIndex++].SetActive(false);
                    InputManager.OnAttackInput += OnAttackInput;

                    Freeze(false);

                    awaiting = true;
                }

                break;

            case TutorialPhase.Collectables:
                panels[panelIndex++].SetActive(false);
                phase = TutorialPhase.Places;
                panels[panelIndex].SetActive(true);

                break;

            case TutorialPhase.Places:
                panels[panelIndex++].SetActive(false);
                phase = TutorialPhase.Pause;
                panels[panelIndex].SetActive(true);

                break;

            case TutorialPhase.Pause:
                panels[panelIndex++].SetActive(false);
                phase = TutorialPhase.Final;
                panels[panelIndex].SetActive(true);

                break;

            case TutorialPhase.Final:
                panels[panelIndex].SetActive(false);
                phase = TutorialPhase.None;

                gameObject.SetActive(false);

                OnTutorialEnd?.Invoke();
                //Unfreezed in game manager

                break;
        }
    }

    private void OnMovementInput(Vector2 input)
    {
        if (input != Vector2.zero && !awaiting && phase == TutorialPhase.Movement)
        {
            InputManager.OnMovementInput -= OnMovementInput;

            MCMovementController.OnMove += OnMoveDone;

            awaiting = true;

            /*
            awaiting = false;

            panels[2].SetActive(true);
            Freeze();

            phase = TutorialPhase.Jump;
            */
        }

        if(phase == TutorialPhase.Sprint && !awaiting)
        {
            if (input != Vector2.zero)
                moving = true;
            else
                moving = false;
        }
    }

    private void OnAttackInput(bool flag)
    {
        Deb("OnAttackInput(): flag =" + flag + ", awaiting =: " + awaiting + ", phase = " + phase);

        if(awaiting && phase == TutorialPhase.Attack && flag)
        {
            InputManager.OnAttackInput -= OnAttackInput;

            awaiting = false;

            panels[panelIndex].SetActive(true);
            Freeze();

            phase = TutorialPhase.Collectables;
        }
    }

    private void OnMoveDone(bool flag)
    {
        if(awaiting && phase == TutorialPhase.Movement && !flag)
        {
            MCMovementController.OnMove += OnMoveDone;

            awaiting = false;

            panels[panelIndex].SetActive(true);
            Freeze();

            phase = TutorialPhase.Jump;
        }
    }

    private void OnJumpInput(bool input)
    {
        if (input && awaiting)
        {
            InputManager.OnJumpInput -= OnJumpInput;

            MCMovementController.OnJumpFlag += OnJumpDone;
        }
    }

    private void OnJumpDone(bool input)
    {
        if (!input)
        {
            MCMovementController.OnJumpFlag -= OnJumpDone;

            awaiting = false;
            panels[panelIndex].SetActive(true);
            Freeze();

            phase = TutorialPhase.Sprint;
        }
    }

    private void OnRunInput(bool input)
    {
        if(input && !awaiting && phase == TutorialPhase.Sprint)
        {
            InputManager.OnMovementInput -= OnMovementInput;
            InputManager.OnRunInput -= OnRunInput;
            MCMovementController.OnMove += OnRunDone;

            awaiting = true;
        }
    }

    private void OnRunDone(bool flag)
    {
        if (awaiting && phase == TutorialPhase.Sprint && !flag)
        {
            MCMovementController.OnMove -= OnRunDone;

            awaiting = false;

            panels[panelIndex].SetActive(true);
            Freeze();

            phase = TutorialPhase.Attack;
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
