using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarshController : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;

    private Rigidbody myRB;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Marshall is now living");
    }

    // Update is called once per frame
    void Update()
    {
        myRB = GetComponent<Rigidbody>();
        myRB.isKinematic = false;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            Debug.Log("Marsh's moving forward");
            transform.position += new Vector3(0f, 0f, speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            Debug.Log("Marsh's moving backward");
            transform.position += new Vector3(0f, 0f, -speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("Marsh's moving right");
            transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("Marsh's moving left");
            transform.position += new Vector3(-speed * Time.deltaTime, 0f, 0f);
        }

        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("\tPreparing to jump");
            transform.localScale += new Vector3(0f, -.25f, 0f);
        }

        if (Input.GetButtonUp("Jump"))
        {
            Debug.Log("\tNow jumping!!!");
            transform.localScale += new Vector3(0f, .25f, 0f);
            myRB.isKinematic = true;
            transform.position += new Vector3(0f, 2f, 0f);
            myRB.isKinematic = false;
        }
    }

    private void OnDestroy()
    {
        Debug.Log("\tOh no! Marshall is now dead! :(");
    }
}
