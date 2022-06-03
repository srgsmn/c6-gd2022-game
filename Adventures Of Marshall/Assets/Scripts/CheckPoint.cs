/* Scripted by Simone Siragusa 306067 @ PoliTO | Game Design & Gamification Exam
 * 
 * TODO:
 *  - 
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
    [SerializeField] private Player player;

    private GameMaster gameMaster;
    //private Player player;

    private void Start()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameMaster.lastPosition = transform.position;
            player.position = transform.position;
            player.level = SceneManager.GetActiveScene().buildIndex;
            player.SavePlayer();
            //player.SavePlayer();
            //GameManager.SaveGame();
        }
    }
}
