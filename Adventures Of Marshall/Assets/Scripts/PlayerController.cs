//PlayerController.cs
/* Manages player's movement in the environment.
 * 
 * Scripted by Simone Siragusa 306067 @ PoliTO | Game Design & Gamification Exam
 * 
 * Ref:
 *  https://www.youtube.com/watch?v=4HpC--2iowE
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float jumpForce = 7.5f;
    [SerializeField] private float turnSmoothTime = .1f;
    [SerializeField] private float gravity = Physics.gravity.y;
    [SerializeField] private Transform cam;

    [Header("SFX:")]
    [SerializeField] AudioSource jumpSound;

    private float turnSmoothVelocity;
    private float verticalVelocity;
    private CharacterController controller;
    //private Vector3 moveVector;
    private bool isMoving = false;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        Debug.Log("Player controller is now on");
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
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
        
    }

    
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
    
    private void Jump()
    {
        verticalVelocity = jumpForce;
        jumpSound.Play();  
    }
}
