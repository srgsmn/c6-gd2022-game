/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 * 
 *  TODO:
 *      - Implement Animator
 *  REFS:
 *      - (CharacterController Explained) https://www.youtube.com/watch?v=UUJMGQTT5ts
 *      - (CC, RB+CapsColl, Custom differences) https://www.youtube.com/watch?v=e94KggaEAr4
 *      - (How to move character) https://www.youtube.com/watch?v=bXNFxQpp2qk
 *      - (Jump) https://www.youtube.com/watch?v=h2r3_KjChf4
 *      - https://www.youtube.com/watch?v=4HpC--2iowE
 *      - (Checkpoints) https://www.youtube.com/watch?v=ofCLJsSUom0
 */

using System.Collections;
using System.Collections.Generic;
using Globals;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
//[RequireComponent(typeof(Animator))]
public class MCMovementController : MonoBehaviour
{
    // COMPONENT ATTRIBUTES ____________________________________________________ COMPONENT ATTRIBUTES

    [Header("Required components:")]
    private Animator animator;
    [SerializeField] private CharacterController characterController;
    [SerializeField]
    [ReadOnlyInspector] private Transform cam;

    [Header("Input:")]
    [SerializeField]
    [ReadOnlyInspector] private bool isMovementPressed;
    [SerializeField]
    [ReadOnlyInspector] private Vector3 currentMovementInput;
    [SerializeField]
    [ReadOnlyInspector] private bool isJumpPressed;
    [SerializeField]
    [ReadOnlyInspector] private bool isRunPressed;

    [Header("Camera:")]
    [SerializeField]
    [ReadOnlyInspector] private Vector3 camFw;
    [SerializeField]
    [ReadOnlyInspector] private Vector3 camRt;

    [Header("Movement:")]
    [SerializeField]
    [ReadOnlyInspector] private bool isMoving;
    [SerializeField] private float speed;
    [SerializeField] private float runMultiplier;
    [SerializeField]
    [ReadOnlyInspector] private Vector3 currentMovement;
    [SerializeField]
    [ReadOnlyInspector] private Vector3 fwRelativeVerticalInput;
    [SerializeField]
    [ReadOnlyInspector] private Vector3 rtRelativeVerticalInput;
    [SerializeField]
    [ReadOnlyInspector] private Vector3 camRelMovement;
    [SerializeField]
    [ReadOnlyInspector] private Vector3 appliedMovement;

    [Header("Rotation:")]
    [SerializeField] private float rotationFactorPerFrame = 1f;
    [SerializeField]
    [ReadOnlyInspector] private Quaternion currentRotation;
    [SerializeField]
    [ReadOnlyInspector] private Vector3 positionToLookAt;
    [SerializeField]
    [ReadOnlyInspector] private Quaternion targetRotation;

    [Header("Jump:")]
    [SerializeField]
    [ReadOnlyInspector] private bool isJumping;
    [SerializeField]
    [ReadOnlyInspector] private float initialJumpVelocity;
    [SerializeField] private float maxJumpHeight = 2f;
    [SerializeField] private float maxJumpTime = .75f;

    [Header("Gravity:")]
    [SerializeField]
    [ReadOnlyInspector] private bool isFalling;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] float fallMultiplier = 2.0f;
    [SerializeField] private float maxFallVelocity = -20f;
    [SerializeField]
    [ReadOnlyInspector] private float groundedGravity = -.05f;
    [SerializeField]
    [ReadOnlyInspector] private float previousYVelocity;

    [Header("Animation:")]
    [SerializeField]
    [ReadOnlyInspector] private int isWalkingHash;
    [SerializeField]
    [ReadOnlyInspector] private int isRunningHash;
    /*
    //Finire video sui salti per animazione
    private int isJumpingHash;
    private bool isJumpAnimating = false;
    private int jumpCount = 0;
    Dictionary<int, float> initialJumpVelocitues = new Dictionary<int, float>();
    Dictionary<int, float> jumpGravities = new Dictionary<int, float>();
    */

    private Vector3 oldPos = Vector3.zero;
    private Quaternion oldRot = Quaternion.identity;

    // COMPONENT LIFECYCLE METHODS _____________________________________________ COMPONENT LIFECYCLE METHODS

    private void Awake()
    {
        EventSubscriber();
    }

    private void Start()
    {
        cam = Camera.main.transform;

        SetupJumpVariables();

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        UpdateViewParams();

        if (isMovementPressed)
        {
            HandleMovement();
            HandleRotation();

            animator.SetBool("camminata", false);

            if (isRunPressed)
            {
                appliedMovement *= runMultiplier;
            }
        }
        else {
            animator.SetBool("camminata", true);
        }

        HandleGravity();
        HandleJump();

        if (isMovementPressed || isJumping)
            characterController.Move(appliedMovement * Time.deltaTime);

        appliedMovement = Vector3.zero;

    }

    private void LateUpdate()
    {
        if (oldPos != transform.position)
        {
            oldPos = transform.position;
            OnTransformChanged?.Invoke(ChParam.Pos, transform.position);
        }

        if (oldRot != transform.rotation)
        {
            oldRot = transform.rotation;
            OnTransformChanged?.Invoke(ChParam.Rot, transform.rotation);
        }
    }

    private void OnDestroy()
    {
        EventSubscriber(false);
    }

    // PROVIDED EVENTS _________________________________________________________ PROVIDED EVENTS

    public delegate void TransformChangedEvent(ChParam parameter, object value);
    public static TransformChangedEvent OnTransformChanged;

    public delegate void MoveEvent(bool isMoving);
    public static MoveEvent OnMove;
    public delegate void JumpFlagEvent(bool isJumping);
    public static JumpFlagEvent OnJumpFlag;
    public delegate void JumpEvent();
    public static JumpEvent OnJump;
    public delegate void LandingEvent();
    public static LandingEvent OnLanding;

    // EVENT SUBSCRIBER ________________________________________________________ EVENT SUBSCRIBER

    private void EventSubscriber(bool subscribing = true)
    {
        if (subscribing)
        {
            InputManager.OnMovementInput += OnMovementInput;
            InputManager.OnJumpInput += OnJumpInput;
            InputManager.OnRunInput += OnRunInput;
            InputManager.OnAttackInput += OnAttackInput;

            GameManager.OnNewState += OnNewState;
        }
        else
        {
            InputManager.OnMovementInput -= OnMovementInput;
            InputManager.OnJumpInput -= OnJumpInput;
            InputManager.OnRunInput -= OnRunInput;
            InputManager.OnAttackInput -= OnAttackInput;

            GameManager.OnNewState -= OnNewState;
        }
    }

    // EVENT CALLBACKS _________________________________________________________ EVENT CALLBACKS

    private void OnAttackInput(bool input)
    {
        if(input) {
            animator.SetTrigger("attack");
        }
    }

    private void OnMovementInput(Vector2 inputValue)
    {
        //Deb("OnMovementInput(): input value for movement is " + inputValue);

        isMovementPressed = inputValue.x != 0 || inputValue.y != 0;

        currentMovementInput.x = inputValue.x;
        currentMovementInput.y = 0;
        currentMovementInput.z = inputValue.y;

        isMoving = isMovementPressed;

        OnMove?.Invoke(isMoving);
    }

    private void OnNewState(GameState state)
    {
        if (state == GameState.Pause)
        {
            currentMovementInput = Vector3.zero;
            /*
            if (currentMovement.y <= 0.1f) currentMovement.y = 0;
            currentMovement.x = 0;
            currentMovement.z = 0;
            */
            currentMovement = Vector3.zero;
            appliedMovement = Vector3.zero;
        }
    }

    private void OnJumpInput(bool inputValue)
    {
        //Deb("OnJumpInput(): input value for jump is " + inputValue);

        isJumpPressed = inputValue;
    }

    private void OnRunInput(bool inputValue)
    {
        //Deb("OnJumpInput(): input value for run is " + inputValue);

        isRunPressed = inputValue;
    }

    // HANDLER FUNCTIONS _______________________________________________________ HANDLER FUNCTIONS

    /// <summary>
    /// It retrieves camera parameters to update relative ones and move the character in accordance to view
    /// </summary>
    private void UpdateViewParams()
    {
        camFw = cam.forward;
        camFw.y = 0;
        camFw = camFw.normalized;

        camRt = cam.right;
        camRt.y = 0;
        camRt = camRt.normalized;
    }

    private void HandleMovement()
    {
        fwRelativeVerticalInput = currentMovementInput.x * camRt * speed;
        rtRelativeVerticalInput = currentMovementInput.z * camFw * speed;

        camRelMovement = fwRelativeVerticalInput + rtRelativeVerticalInput;

        currentMovement.x = camRelMovement.x;
        currentMovement.z = camRelMovement.z;

        appliedMovement = currentMovement;

    }

    private void HandleRotation()
    {
        positionToLookAt = new Vector3(currentMovement.x, 0, currentMovement.z);

        currentRotation = transform.rotation;

        float ratio = rotationFactorPerFrame * Time.deltaTime;

        if (isMovementPressed)
        {
            targetRotation = Quaternion.LookRotation(positionToLookAt);

            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, (isRunPressed ? ratio*runMultiplier : ratio));
        }
    }

    private void HandleJump()
    {
        if (!isJumping && characterController.isGrounded && isJumpPressed)
        {
            isJumping = true;

            OnJump?.Invoke();

            //Altra roba qui per più tipi di salto e animazioni da video #3
            currentMovement.y = initialJumpVelocity;   //Se stai guardando il video #3 di I<3Gamedev e non tornano le cose, è perché ha sbagliato e le ha corrette in #4
            appliedMovement.y = initialJumpVelocity;
        }
        else if (!isJumpPressed && isJumping && characterController.isGrounded)
        {
            isJumping = false;

            OnLanding?.Invoke();
        }

        OnJumpFlag?.Invoke(isJumping);
    }

    private void HandleGravity()
    {
        isFalling = currentMovement.y <= .0f || !isJumpPressed;

        if (characterController.isGrounded)
        {
            currentMovement.y = groundedGravity;
            appliedMovement.y = groundedGravity; //vedi commento in HandleJump()
        }
        else if (isFalling)
        {
            previousYVelocity = currentMovement.y;
            currentMovement.y = currentMovement.y + (gravity * fallMultiplier * Time.deltaTime);
            appliedMovement.y = Mathf.Max((previousYVelocity + currentMovement.y) * .5f, maxFallVelocity);
        }
        else
        {
            previousYVelocity = currentMovement.y;
            currentMovement.y = currentMovement.y + (gravity * Time.deltaTime);
            appliedMovement.y = (previousYVelocity + currentMovement.y) * .5f;
        }
    }


    /*    
    void HandleAnimation()  //TODO Solo da desilenziare quando si hanno le animazioni
    {

        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);

        if(isMovementPressed && !isWalking){
            animator.SetBool(isWalkingHash, true);
        }
        else if(!isMovementPressed && isWalking){
            animator.SetBool(isRunningHash, false);
        }

        if((isMovementPressed && isRunPressed) && !isRunning)
        {
            animator.SetBool(isRunningHash, true);
        }
        else if((!isMovementPressed || !isRunPressed) && isRunning)
        {
            animator.SetBool(isRunningHash, false);
        }
    }
    */

    // OTHER METHODS ___________________________________________________________ OTHER METHODS

    private void SetupJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);

        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
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
