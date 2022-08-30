using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrsettoRosso : MonoBehaviour
{
    public bool hasStarted = false;
    public float delay = 3f;
    public float radius = 5f;
    public float explosionForce = 300f;

    float countdown;
    bool hasExploded = false;

    public GameObject explosionEffect;
    public GameObject meshOrsetto;

    private MCHealthController MCHealthController;

    public AudioSource audioSource;

    void Start()
    {
        
    }

    void Update()
    {
        if(hasStarted)
        {
            countdown -= Time.deltaTime;
            if(countdown<=0 && !hasExploded)
            {
                Explode();
                hasExploded = true;
            }
        }
        else
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider nearbyObject in colliders)
            {
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                if(rb != null && rb.gameObject.tag == "Player")
                {
                    StartCountdown();
                }
            }
        }
    }

    void StartCountdown()
    {
        countdown = delay;
        hasStarted = true;
        Debug.Log("STARTED !!!!!");
        audioSource.Play();
    }

    void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if(rb != null && rb.gameObject.tag == "Player")
            {
                rb.AddExplosionForce(explosionForce, transform.position, radius);
                
                MCHealthController = rb.gameObject.GetComponent<MCHealthController>();
                MCHealthController.TakeDamage(30f);
            }
        }

        Destroy(meshOrsetto);
    }
}
