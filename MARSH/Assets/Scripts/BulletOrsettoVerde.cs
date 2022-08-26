using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletOrsettoVerde : MonoBehaviour
{
    public float life = 20;

    //private PlayerHealthController playerHealthController;

    void Awake()
    {
        Destroy(gameObject, life);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //playerHealthController = collision.gameObject.GetComponent<PlayerHealthController>();
            //playerHealthController.TakeDamage(20f);
            Destroy(gameObject);
        }
    }
}
