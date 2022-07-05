using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableCanvas : MonoBehaviour
{
    [SerializeField] private GameObject canvas;


    //Check if something has entered the collider this script is on
    void OnTriggerEnter(Collider collider)
    {
        //Check if the object has the tag car
        if (collider.tag == "Player")
        {
            //Activate the canvas
            canvas.SetActive(true);
        }
    }
}
