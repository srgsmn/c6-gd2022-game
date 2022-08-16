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
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    /* SPOSTATO IN COLLISION MANAGER
    private void OnTriggerEnter(Collider obj)
    {
        if (obj.CompareTag("Player"))
        {
            //gameManager.lastPosition = transform.position;
            GameManager.SaveGame();
            //player.position = transform.position;
            //player.level = SceneManager.GetActiveScene().buildIndex;
            //player.SavePlayer();
            //player.SavePlayer();
            //GameManager.SaveGame();
        }
    }
    */
}
