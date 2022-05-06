//CoinSpawner.cs
/* This script spawns coins (ChocoChips and SugarLumps) when something is destroyed.
 * TODO: Can be extended to spawn everything with an interface maybe
 * TODO: Update with random instantiation when maxReward<go2spawn.Length
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRewarder : MonoBehaviour
{
    [SerializeField] private int maxReward = 3;
    [SerializeField] private GameObject[] prefab2spawn;                         //The order of game object's prefabs matters: the first has more chances to appear

    public void Spawn()
    {
                
        for (int i=0; i < prefab2spawn.Length && maxReward>0; i++)
        {
            int count;

            if (i < prefab2spawn.Length - 1)
                count = Random.Range(0, maxReward);
            else
                count = maxReward;

            for (int j=0; j<count; j++)
            {
                Rigidbody rewardInstance = Instantiate(prefab2spawn[i].GetComponent<Rigidbody>(), transform.position, transform.rotation) as Rigidbody;

                rewardInstance.AddForce(transform.up * 350f);
            }

            maxReward -= count;
        }

        /*
        for (int s = 0; s < count; s++)
        {
            Instantiate(slPrefab, transform.position, Quaternion.identity);
        }
        for (int c = 0; c < maxReward - count; c++)
        {
            Instantiate(ccPrefab, transform.position, Quaternion.identity);
        }
        */
    }
}


// Scripted by Simone Siragusa 306067 @ PoliTO | Game Design & Gamification Exam