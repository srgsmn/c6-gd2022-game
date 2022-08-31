using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrsettoVerde : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletOrsettoVerde;
    public float bulletSpeed = 2F;

    public Transform target;

    public bool inRange = false;
    public float radius = 20f;

    public AudioSource audioSource;

    private HealthController healthController;


    void Start()
    {
        var rendum = Random.Range(1F,3F);
        InvokeRepeating("Shuut", 3, rendum);
        healthController = gameObject.GetComponent<HealthController>();
    }

    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider nearbyObject in colliders)
            {
                //Debug.Log(nearbyObject);
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                if(rb != null && rb.gameObject.tag == "Player")
                {
                    Debug.Log("DEFINITIVO");
                    transform.LookAt(target);
                }
            }
        if(healthController.GetHealth() <= 0) {
            Destroy(gameObject);
        }
    }
    void Shuut()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider nearbyObject in colliders)
            {
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                if(rb != null && rb.gameObject.tag == "Player")
                {
                    var bullet = Instantiate(bulletOrsettoVerde, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                    bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * 8;
                    audioSource.Play();
                }
            }
    }


    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Stuzzicadenti")
        {
            healthController.TakeDamage(50);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}
