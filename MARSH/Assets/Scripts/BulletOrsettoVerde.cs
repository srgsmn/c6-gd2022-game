using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletOrsettoVerde : MonoBehaviour
{
    public float life = 20;

    private MCHealthController MCHealthController;

    void Awake()
    {
        Destroy(gameObject, life);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            MCHealthController = collision.gameObject.GetComponent<MCHealthController>();
            MCHealthController.TakeDamage(20f);
            Destroy(gameObject);
        }
    }
}
