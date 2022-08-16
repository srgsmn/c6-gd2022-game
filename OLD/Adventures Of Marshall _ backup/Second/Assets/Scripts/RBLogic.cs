/* RBLogic.cs
 * Logic that supports the Rigid Body + Capsule Collider management method
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
 *  - 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBLogic : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float gravity = Physics.gravity.y;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float groundDistance = 0.2f;
    [SerializeField] private float dashDistance = 5f;
    [SerializeField] private LayerMask ground;

    private Rigidbody body;
    private Vector3 inputs = Vector3.zero;
    private bool isGrounded = true;
    private Transform groundChecker;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        groundChecker = transform.GetChild(0);
    }

    void Update()
    {
        //GROUND CHECKER
        isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, ground, QueryTriggerInteraction.Ignore);

        //INPUTS
        inputs.x = Input.GetAxis("Horizontal");
        inputs.z = Input.GetAxis("Vertical");
        if (inputs != Vector3.zero)
            transform.forward = inputs;

        //JUMP
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            body.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * gravity), ForceMode.VelocityChange);
        }

        //DASH
        if (Input.GetButtonDown("Dash"))
        {
            Vector3 dashVelocity = Vector3.Scale(transform.forward, dashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * body.drag + 1)) / -Time.deltaTime), 0, (Mathf.Log(1f / (Time.deltaTime * body.drag + 1)) / -Time.deltaTime)));
            body.AddForce(dashVelocity, ForceMode.VelocityChange);
        }
    }

    //Framerate independent updates, more approrpiate for physics
    void FixedUpdate()
    {
        //CHANGE POSITION
        body.MovePosition(body.position + inputs * speed * Time.fixedDeltaTime);
    }
}
