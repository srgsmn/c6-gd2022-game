using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGroundChecker : MonoBehaviour
{
    [Header("Virtual Cameras")]
    [SerializeField] private GameObject standardCam;
    [SerializeField] private GameObject jellyCam;
    /*
    private void Awake()
    {
        standardCam.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Jelly"))
        {
            standardCam.SetActive(false);
            jellyCam.SetActive(true);
        }
        else
        {
            standardCam.SetActive(true);
            jellyCam.SetActive(false);
        }
    }
    */
}
