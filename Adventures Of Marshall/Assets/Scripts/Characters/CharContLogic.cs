/* CharContLogic.cs
 * Logic that supports the Character Controller management method
 * 
 * Scripted by Simone Siragusa 306067 @ PoliTO | Game Design & Gamification Exam
 * 
 * TODO:
 *  - 
 * 
 * REF:
 *  - https://medium.com/ironequal/unity-character-controller-vs-rigidbody-a1e243591483
 *  
 * MEMO:
 *  - Dash = lett. corsa
 *  - Drag = lett. trascinamento, serve per simulare l'attrito, sennò il soggetto procederebbe all'infinito
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharContLogic : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float gravity = Physics.gravity.y;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float DashDistance = 5f;
    //[SerializeField] private LayerMask Ground;
    [SerializeField] private Vector3 Drag;

    //FIXME
    [Header("EXPERIMENTAL")]
    [SerializeField] private bool enable = false;
    [SerializeField] private Transform mainCamera;
    [SerializeField] private float turnSmoothTime = .1f;
    private float turnSmoothVelocity;



    private Vector3 velocity;
    private CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        mainCamera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!enable)
        {
            NotExperimental();

        }
        else
        {
            Experimental();
        }
    }

    private void NotExperimental()
    {
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

    private void Experimental()
    {
        //READING INPUT
        Vector3 input = new(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 moveDir = Vector3.zero;

        //DIRECTION MANAGER
        Vector3 direction = new(input.x, 0f, input.z);

        if(direction.magnitude >= .1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(speed * Time.deltaTime * moveDir.normalized);
        }

        //######################################################################
        controller.Move(speed * Time.deltaTime * input);

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
}