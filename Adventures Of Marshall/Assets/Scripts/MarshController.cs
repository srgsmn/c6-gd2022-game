using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarshController : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float jumpForce = 5.0f;


    private Rigidbody rigidBody;
    [SerializeField] private bool isJumping;

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

        transform.position += new Vector3(speed * hInput * Time.deltaTime, 0f, speed * vInput * Time.deltaTime);

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
        if(collision.gameObject.CompareTag("Enemy Head"))
        {
            Destroy(collision.transform.parent.gameObject);
            Jump();
        }
    }

    private void Jump()
    {
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, jumpForce, rigidBody.velocity.z);
    }

    private void OnDestroy()
    {
        Debug.Log("\tOh no! Marshall is now dead! :(");
    }
}
