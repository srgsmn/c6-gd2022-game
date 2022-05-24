/*  PlayerSFX.cs
 * Manages SFX related to player
 * 
 * Scripted by Simone Siragusa 306067 @ PoliTO | Game Design & Gamification Exam
 * 
 * TODO:
 *  - Fare tutto
 *  
 * REF:
 *  - 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioSource))]
public class PlayerSFX : MonoBehaviour
{
    private PlayerHealth healthInfo;

    [SerializeField] AudioSource fallingSound;
    [SerializeField] AudioSource enemyCollisionSound;
    [SerializeField] AudioSource drowningSound;
    [SerializeField] AudioSource restartSound;

    private void Update()
    {
        //TODO
    }
}
