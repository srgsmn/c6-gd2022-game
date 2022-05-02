using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private bool dead = false;

    void Update()
    {
        if (transform.position.y <= -3.5f && !dead)
        {
            Die("Falling from the ground");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy Body"))       //Collision with enemy manager
        {
            Die("Collision with enemy");
        }

    }

    void Die(string msg)
    {
        Component[] meshRenderers;

        GetComponent<MeshRenderer>().enabled = false;                           //TODO: far sparire gli occhietti (non così importante se già nel modello finale
        meshRenderers = GetComponentsInChildren<MeshRenderer>();

        foreach (Component mr in meshRenderers)
            enabled = false;

        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<MarshController>().enabled = false;
        Invoke(nameof(ReloadLevel), 1.3f);

        dead = true;

        Debug.Log("Player is dead: " + msg + "");
        
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    




    //##########################################################################
    /*// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
