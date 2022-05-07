using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private float turnSmoothTime = .1f;
    [SerializeField] private float gravity = Physics.gravity.y;
    [SerializeField] private Transform cam;

    private float turnSmoothVelocity;
    private float verticalVelocity;
    private CharacterController controller;
    private Vector3 moveVector;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        Debug.Log("Player controller in now on");
    }

    void Update()
    {
        moveVector.x = Input.GetAxis("Horizontal") * speed;
        moveVector.z = Input.GetAxis("Vertical") * speed;

        Vector3 direction = new Vector3(moveVector.x, 0f, moveVector.z).normalized;

        //transform.position += new Vector3(speed * moveVector.x * Time.deltaTime, 0f, speed * moveVector.z * Time.deltaTime);

        if (direction.magnitude >= .1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        if (controller.isGrounded)
        {
            verticalVelocity = gravity * Time.deltaTime;
            if (Input.GetButton("Jump"))
            {
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
            moveVector.x = 0;
            moveVector.z = 0;
        }
        moveVector.y = verticalVelocity;

        controller.Move(moveVector * Time.deltaTime);
    }
}
