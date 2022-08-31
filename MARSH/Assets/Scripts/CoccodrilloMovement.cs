using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoccodrilloMovement : MonoBehaviour
{

    [SerializeField] GameObject[] waypoints;
    int currentWaypointIndex = 0;
    [SerializeField] float speed = 3f;
    public float smooth = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if(Vector3.Distance(transform.position, waypoints [currentWaypointIndex].transform.position) < .1f)
         {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
            currentWaypointIndex = 0;
            }
         }

        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, speed * Time.deltaTime);
    
    }
/*
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.name == "Player") {
            Debug.Log("ECCOCI");
        }
    }
    
    
    private void OnCollisionEnter(Collision collider) {
        if(collider.gameObject.name == "Player") {
            Debug.Log("ECCOCI");
            collider.gameObject.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit(Collision collider) {
        if(collider.gameObject.name == "Player") {
            collider.gameObject.transform.SetParent(null);
        }
    }
    */
}
