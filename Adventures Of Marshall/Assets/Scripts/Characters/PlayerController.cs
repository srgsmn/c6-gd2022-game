/* PlayerController.cs
 * Manages player's interactions with the environment (movements, collision detection, etc)
 * 
 * Scripted by Simone Siragusa 306067 @ PoliTO | Game Design & Gamification Exam
 * 
 * TODO:
 *  - Perfezionare movimenti in accordo con la camera
 *  
 * Ref:
 *  - (CharacterController Explained) https://www.youtube.com/watch?v=UUJMGQTT5ts
 *  - (CC, RB+CapsColl, Custom differences) https://www.youtube.com/watch?v=e94KggaEAr4
 *  - (How to move character) https://www.youtube.com/watch?v=bXNFxQpp2qk
 *  - (Jump) https://www.youtube.com/watch?v=h2r3_KjChf4
 *  - https://www.youtube.com/watch?v=4HpC--2iowE
 *  - (Checkpoints) https://www.youtube.com/watch?v=ofCLJsSUom0
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
//[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    //private Animator animator;    //See How to move char. video to attach animations

    // variables to store optimized setter/getter parameter IDs
    int isWalkingHash;
    int isRunningHash;


    private Vector2 currMovementInput;
    private Vector3 currMovement;
    private Vector3 currRunMovement;
    private Vector3 appliedMovement;
    private bool isMovementPressed;
    private bool isRunPressed;

    [SerializeField] private float speed = 1f;
    [SerializeField] private float rotationFactorPerFrame = 1.0f;
    [SerializeField] private float runMultiplier = 3.0f;

    int zero = 0; //?

    //gravity vars
    private float gravity = -9.81f;
    private float groundedGravity = -.05f;
    [SerializeField] private float maxFallVelocity = -20f;

    //jumping vars
    private bool isJumpPressed = false;
    private float initialJumpVelocity;
    [SerializeField] private float maxJumpHeight = 2f;
    [SerializeField] private float maxJumpTime = .75f;
    private bool isJumping = false;
    /*
    //Finire video sui salti
    private int isJumpingHash;
    private bool isJumpAnimating = false;
    private int jumpCount = 0;
    Dictionary<int, float> initialJumpVelocitues = new Dictionary<int, float>();
    Dictionary<int, float> jumpGravities = new Dictionary<int, float>();
    */


    //TURNING COSE
    [SerializeField] private float turnSmoothTime = .1f;
    float turnSmoothVelocity;

    private CharacterController characterController;
    [SerializeField] private Transform camera;

    //EVENTS
    public delegate void PositionUpdateEvent(Vector3 value);
    public static event PositionUpdateEvent OnPositionUpdate;

    void Awake()
    {
        playerInput = new PlayerInput();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");

        // set the player input callbacks
        playerInput.CharacterControls.Move.started += OnMovementInput;
        playerInput.CharacterControls.Move.canceled += OnMovementInput;
        playerInput.CharacterControls.Move.performed += OnMovementInput;    //Per i controller

        playerInput.CharacterControls.Run.started += OnRun;
        playerInput.CharacterControls.Run.canceled += OnRun;

        playerInput.CharacterControls.Jump.started += OnJump;
        playerInput.CharacterControls.Jump.canceled += OnJump;

        characterController = GetComponent<CharacterController>();
        //animator = GetComponent<Animator>();

        setupJumpVariables();
    }

    private void OnDestroy()
    {
        playerInput.CharacterControls.Move.started -= OnMovementInput;
        playerInput.CharacterControls.Move.canceled -= OnMovementInput;
        playerInput.CharacterControls.Move.performed -= OnMovementInput;    //Per i controller

        playerInput.CharacterControls.Run.started -= OnRun;
        playerInput.CharacterControls.Run.canceled -= OnRun;

        playerInput.CharacterControls.Jump.started -= OnJump;
        playerInput.CharacterControls.Jump.canceled -= OnJump;
    }

    void setupJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);

        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    //  CALLBACK FUNCTIONS
    void OnMovementInput(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValue<Vector2>());
        currMovementInput = context.ReadValue<Vector2>();
        currMovement.x = currMovementInput.x * speed;
        currMovement.z = currMovementInput.y * speed;

        currRunMovement.x = currMovementInput.x * runMultiplier;
        currRunMovement.z = currMovementInput.y * runMultiplier;

        isMovementPressed = currMovementInput.x != 0 || currMovementInput.y != 0;
    }

    /*
    void OnMovementInput(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValue<Vector2>());

        currMovementInput = context.ReadValue<Vector2>();

        float targetAngle = Mathf.Atan2(currMovementInput.x, currMovementInput.y) * Mathf.Rad2Deg + camera.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        moveDir = moveDir.normalized;

        currMovement.x = moveDir.x * currMovementInput.x * speed;
        currMovement.z = moveDir.z * currMovementInput.y * speed;

        currRunMovement.x = moveDir.x * currMovementInput.x * runMultiplier;
        currRunMovement.z = moveDir.z * currMovementInput.y * runMultiplier;

        isMovementPressed = currMovementInput.x != 0 || currMovementInput.y != 0;
    }
    */
    
    void OnRun(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValueAsButton());
        isRunPressed = context.ReadValueAsButton();
    }

    void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValueAsButton());
        isJumpPressed = context.ReadValueAsButton();
    }

    void HandleRotation()
    {
        Vector3 positionToLookAt;

        positionToLookAt = new Vector3(currMovement.x, 0, currMovement.z);

        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
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

    void HandleGravity()
    {
        bool isFalling = currMovement.y <= .0f || !isJumpPressed;
        float fallMultiplier = 2.0f;

        if (characterController.isGrounded)
        {
            currMovement.y = groundedGravity;
            appliedMovement.y = groundedGravity; //vedi commento in HandleJump()
        }
        else if (isFalling)
        {
            float previousYVelocity = currMovement.y;
            currMovement.y = currMovement.y + (gravity * fallMultiplier * Time.deltaTime);
            appliedMovement.y = Mathf.Max((previousYVelocity + currMovement.y) * .5f, maxFallVelocity);
        }
        else
        {
            float previousYVelocity = currMovement.y;
            currMovement.y = currMovement.y + (gravity * Time.deltaTime);
            appliedMovement.y = (previousYVelocity + currMovement.y) * .5f;
        }
    }

    void HandleJump()
    {
        if(!isJumping && characterController.isGrounded && isJumpPressed)
        {
            isJumping = true;
            //Altra roba qui per più tipi di salto e animazioni da video #3
            currMovement.y = initialJumpVelocity;   //Se stai guardando il video #3 di I<3Gamedev e non tornano le cose, è perché ha sbagliato e le ha corrette in #4
            appliedMovement.y = initialJumpVelocity;
        }else if(!isJumpPressed && isJumping && characterController.isGrounded)
        {
            isJumping = false;
        }
    }
    
    private void Update()
    {
        HandleRotation();
        //HandleAnimation();    TODO
        

        if (isRunPressed)
        {
            //characterController.Move(currRunMovement * Time.deltaTime);
            appliedMovement.x = currRunMovement.x;
            appliedMovement.z = currRunMovement.z;
        }
        else
        {
            //characterController.Move(currMovement * Time.deltaTime);
            appliedMovement.x = currMovement.x;
            appliedMovement.z = currMovement.z;
        }

        characterController.Move(appliedMovement * Time.deltaTime);

        HandleGravity();
        HandleJump();
    }

    private void LateUpdate()
    {
        OnPositionUpdate(transform.position);
    }

    private void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        playerInput.CharacterControls.Disable();
    }
}
