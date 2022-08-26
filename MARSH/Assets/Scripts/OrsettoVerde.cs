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

    void Start()
    {
        var rendum = Random.Range(1F,3F);
        InvokeRepeating("Shuut", 3, rendum);
    }

    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider nearbyObject in colliders)
            {
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                if(rb != null && rb.gameObject.name == "Player" && target != null)
                {
                    transform.LookAt(target);
                }
            }
    }
    void Shuut()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider nearbyObject in colliders)
            {
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                if(rb != null && rb.gameObject.name == "Player")
                {
                    var bullet = Instantiate(bulletOrsettoVerde, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                    bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * 8;
                }
            }
    }
}
