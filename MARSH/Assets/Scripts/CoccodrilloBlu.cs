using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoccodrilloBlu : MonoBehaviour
{
    [SerializeField] GameObject[] waypoints;
    int currentWaypointIndex = 0;
    [SerializeField] float speed = 2f;
    public float smooth = 2f;
    private float time = 0f;
    private bool timestart = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(Vector3.Distance(transform.position, waypoints [currentWaypointIndex].transform.position) < .1f)
            {
                if(!timestart && currentWaypointIndex==1) {
                    time = 0f;
                    timestart = true;
                }
                if(time>=3f)
                {
                    currentWaypointIndex++;
                    timestart = false;
                }
                if (currentWaypointIndex >= waypoints.Length)
                {
                currentWaypointIndex = 0;
                }
            }
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, speed * Time.deltaTime);
    }
}
