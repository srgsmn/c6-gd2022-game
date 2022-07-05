/* PlayerManager.cs
 * Attach it to player, it'll automatically add all the required components that manage the player
 * 
 * Scripted by Simone Siragusa 306067 @ PoliTO | Game Design & Gamification Exam
 * 
 * TODO:
 *  - Perfezionare movimenti in accordo con la camera
 *  
 * Ref:
 *  - https://www.youtube.com/watch?v=4HpC--2iowE
 *  - (Checkpoints) https://www.youtube.com/watch?v=ofCLJsSUom0
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerHealthController))]
[RequireComponent(typeof(CollectablesManager))]
[RequireComponent(typeof(CollisionManager))]
public class PlayerManager : MonoBehaviour
{
    [Header("Debug purpose readonly fields")]
    [SerializeField][ReadOnlyInspector] private PlayerController playerController;
    [SerializeField][ReadOnlyInspector] private PlayerHealthController playerHealthController;
    [SerializeField][ReadOnlyInspector] private CollectablesManager collectablesManager;
    [SerializeField][ReadOnlyInspector] private CollisionManager collisionManager;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerHealthController = GetComponent<PlayerHealthController>();
        collectablesManager = GetComponent<CollectablesManager>();
        collisionManager = GetComponent<CollisionManager>();
    }

}

// OLD
/*
Rigidbody rb;
    public bool isGrounded;
    [SerializeField] private float _speed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -3.5f)
        {
            Die("Falling from the ground");
        }
        
        if (Input.GetKeyDown("space") && isGrounded==true) {
            rb.AddForce(new Vector3(0.0f, 5.0f, 0.0f), ForceMode.Impulse);
            //rb.velocity = new Vector3(0, 3 * _speed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow)) {
            transform.position += new Vector3(0f, 0f, _speed * Time.deltaTime);
            //rb.velocity = new Vector3(0, 0, _speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            transform.position += new Vector3(_speed * Time.deltaTime, 0f, 0f);
            //rb.velocity = new Vector3(_speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            transform.position += new Vector3(0f, 0f, - _speed * Time.deltaTime);
            //rb.velocity = new Vector3(0, 0, -_speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.position += new Vector3(- _speed * Time.deltaTime, 0f, 0f);
            //rb.velocity = new Vector3(-_speed * Time.deltaTime, 0, 0);
        }
    }

    void OnCollisionEnter (Collision collision) {
        isGrounded = true;
    }
    void OnCollisionExit (Collision collision) {
        isGrounded = false;
    }

    void Die(string msg)
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<PlayerManager>().enabled = false;
        Invoke(nameof(ReloadLevel), 1.3f);
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
*/