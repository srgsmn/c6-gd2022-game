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


    private CharacterController characterController;


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

    private void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        playerInput.CharacterControls.Disable();
    }
}

/*

[SerializeField] private float speed = 5f;
    [SerializeField] private float gravity = Physics.gravity.y;
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private float DashDistance = 5f;
    //[SerializeField] private LayerMask Ground;
    [SerializeField] private Vector3 Drag;

    private Vector3 velocity;
    private CharacterController CTRL;

    private PlayerHealthController healthCTRL;

    [Header("SFX:")]
    [SerializeField] AudioSource jumpSound;

    //CHECKPOINT TODO: To refine
    private GameManager gameManager;
    private Player player;

    void Start()
    {
        CTRL = GetComponent<CharacterController>();
        Debug.Log("Player controller is now on");

        healthCTRL = GetComponent<PlayerHealthController>();

        //LOAD CHECKPOINT FIXME!!!
        
        //gameManager = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        //transform.position = gameManager.lastPosition;
        

        //LOAD GAME     FIXME
        //player = GetComponent<Player>();
    }

    void Update()
    {
        //MOVEMENT
        Vector3 input = new(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        CTRL.Move(input * Time.deltaTime * speed);

        //DIRECTION CONTROL
        if (input != Vector3.zero)
            transform.forward = input;
        //GRAVITY
        velocity.y += gravity * Time.deltaTime;
        CTRL.Move(velocity * Time.deltaTime);

        if (CTRL.isGrounded && velocity.y < 0)
            velocity.y = 0f;

        //JUMP
        if (Input.GetButtonDown("Jump") && CTRL.isGrounded)
            velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);

        // DASH
        if (Input.GetButtonDown("Dash"))
        {
            Debug.Log("Dash");
            velocity += Vector3.Scale(transform.forward,
                                       DashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * Drag.x + 1)) / -Time.deltaTime),
                                                                  0,
                                                                  (Mathf.Log(1f / (Time.deltaTime * Drag.z + 1)) / -Time.deltaTime)));
        }

        // FRICTION SIMULATION
        velocity.x /= 1 + Drag.x * Time.deltaTime;
        velocity.y /= 1 + Drag.y * Time.deltaTime;
        velocity.z /= 1 + Drag.z * Time.deltaTime;

        CTRL.Move(velocity * Time.deltaTime);
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("COLLISION DETECTED (w/ name: " + collision.gameObject.name + "; tag: " + collision.gameObject.tag + ")");

        if (collision.gameObject.CompareTag("Enemy Head"))
        {
            Debug.Log("You collided with " + collision.gameObject.tag);

            var killable = collision.transform.parent.gameObject.GetComponent<IKillable>();

            if (killable != null)
            {
                Debug.Log("KILLING THE ENEMY BY JUMPING ON HIS HEAD (OR SORTA)");
                killable.Kill();
                //Destroy(collision.transform.gameObject);
            }

            Jump();
        }
    }
    */


/* OLD UPDATE:
 * float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDir = Vector3.zero;

        // DIRECTION MANAGER
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= .1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        
        if (moveDir != Vector3.zero)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        // JUMP ACTION MANAGER
        if (controller.isGrounded)
        {
            verticalVelocity = gravity * Time.deltaTime;
            if (Input.GetButton("Jump"))
            {
                Jump();
            }
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        // DO MOVE
        //moveVector.y = verticalVelocity;

        //controller.Move(moveVector * Time.deltaTime);
*/

/* OLD JUMP
 * private void Jump()
    {
        verticalVelocity = jumpForce;
        jumpSound.Play();  
    }
*/