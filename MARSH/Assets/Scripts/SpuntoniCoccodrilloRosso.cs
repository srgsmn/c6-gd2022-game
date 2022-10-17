using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpuntoniCoccodrilloRosso : MonoBehaviour
{

    private MCHealthController MCHealthController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            MCHealthController = collider.gameObject.GetComponent<MCHealthController>();
            MCHealthController.TakeDamage(20f);
        }
    }
}
