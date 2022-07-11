/* Scripted by Simone Siragusa 306067 @ PoliTO | Game Design & Gamification Exam
 * 
 * TODO:
 *  - Qui si potrebbe implementare la questione del cambio di UI Del checkpoint
 *  
 * Ref:
 *  - (Checkpoints) https://www.youtube.com/watch?v=ofCLJsSUom0
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private GameObject cpAnimated;
    [SerializeField] private Canvas canvas;

    private GameObject cpText;
    private float timeRemaining;

    private void Start()
    {
        timeRemaining = 2f;
    }

    private void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && !IsOnStart())
        {
            cpText = Instantiate(cpAnimated, canvas.transform);
            Destroy(cpText, 2f);
        }
    }

    private bool IsOnStart()
    {
        if (timeRemaining > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
