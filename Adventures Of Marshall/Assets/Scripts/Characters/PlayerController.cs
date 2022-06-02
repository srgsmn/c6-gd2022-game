//PlayerController.cs
/* Manages player's movement in the environment.
 * 
 * Scripted by Simone Siragusa 306067 @ PoliTO | Game Design & Gamification Exam
 * 
 * TODO:
 *  - Perfezionare movimenti in accordo con la camera
 *  
 * Ref:
 *  https://www.youtube.com/watch?v=4HpC--2iowE
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float gravity = Physics.gravity.y;
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private float DashDistance = 5f;
    //[SerializeField] private LayerMask Ground;
    [SerializeField] private Vector3 Drag;

    private Vector3 velocity;
    private CharacterController controller;

    private PlayerHealthManager healthManager;

    [Header("SFX:")]
    [SerializeField] AudioSource jumpSound;

    //CHECKPOINT TODO: To refine
    private GameMaster gameMaster;
    private Player player;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Debug.Log("Player controller is now on");

        healthManager = GetComponent<PlayerHealthManager>();

        //LOAD CHECKPOINT
        
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        transform.position = gameMaster.lastPosition;
        

        //LOAD GAME     FIXME
        //player = GetComponent<Player>();
    }

    void Update()
    {
        //IS ALIVE
        if (!healthManager.isAlive)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            healthManager.isAlive=true;
        }

        //MOVEMENT
        Vector3 input = new(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        controller.Move(input * Time.deltaTime * speed);

        //DIRECTION CONTROL
        if (input != Vector3.zero)
            transform.forward = input;
        //GRAVITY
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0)
            velocity.y = 0f;

        //JUMP
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
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

        controller.Move(velocity * Time.deltaTime);
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
}

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