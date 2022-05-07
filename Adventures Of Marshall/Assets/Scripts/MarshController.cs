using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarshController : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private float turnSmoothTime = .1f;

    private float turnSmoothVelocity;


    private Rigidbody rigidBody;
    [SerializeField] private bool isJumping;
    [SerializeField] AudioSource jumpSound;

    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform cam;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Marshall is now living");
        isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.isKinematic = false;

        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(hInput, 0f, vInput).normalized;

        transform.position += new Vector3(speed * hInput * Time.deltaTime, 0f, speed * vInput * Time.deltaTime);

        if(direction.magnitude >= .1f) //TODO: implementare controller. Vedi [11:20] circa di https://www.youtube.com/watch?v=4HpC--2iowE
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            //controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        /*
        //if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        if (Input.GetKey("up"))
        {
            Debug.Log("Marsh's moving forward");
            transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
        }

        //if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        if (Input.GetKey("down"))
            {
            Debug.Log("Marsh's moving backward");
            transform.position += new Vector3(-speed * Time.deltaTime, 0f, 0f);
        }

        //if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        if (Input.GetKey("right"))
        {
            Debug.Log("Marsh's moving right");
            transform.position += new Vector3(0f, 0f, -speed * Time.deltaTime);
        }

        //if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        if (Input.GetKey("left"))
        {
            Debug.Log("Marsh's moving left");
            transform.position += new Vector3(0f, 0f, +speed * Time.deltaTime);
        }
        */

        if (Input.GetButtonUp("Jump") && !isJumping)
        {
            
            Jump();

        }
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
            
            Jump();//TODO must refine!!
        }
    }

    private void Jump()
    {
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, jumpForce, rigidBody.velocity.z);
        jumpSound.Play();
    }

    private void OnDestroy()
    {
        Debug.Log("\tOh no! Marshall is now dead! :(");
    }
}
