using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarshController : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;

    private Rigidbody myRB;
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
        myRB = GetComponent<Rigidbody>();
        myRB.isKinematic = false;

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

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            Debug.Log("\tPreparing to jump");
            transform.localScale += new Vector3(0f, -.25f, 0f);
        }

        if (Input.GetButtonUp("Jump") && !isJumping)
        {
            isJumping = true;
            Debug.Log("\tNow jumping!!!");
            transform.localScale += new Vector3(0f, .25f, 0f);
            myRB.isKinematic = true;
            transform.position += new Vector3(0f, 2f, 0f);
            myRB.isKinematic = false;

            if (myRB.velocity.y == 0)
                isJumping = false;
        }

        /*while (myRB.velocity.y != 0)
            isJumping = true;
        */

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy Head"))
        {
            Destroy(collision.transform.parent.gameObject);
        }
    }

    private void OnDestroy()
    {
        Debug.Log("\tOh no! Marshall is now dead! :(");
    }
}
