//CoinRewarder.cs
/* Spawns coins (ChocoChips and SugarLumps) when something is destroyed.
 * TODO: Can be extended to spawn everything with an interface maybe
 * TODO: Update with random instantiation when maxReward<go2spawn.Length
 * 
 * Scripted by Simone Siragusa 306067 @ PoliTO | Game Design & Gamification Exam
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRewarder : MonoBehaviour
{
    [SerializeField] private int maxReward = 3;
    [SerializeField] private GameObject[] prefab2spawn;                         //The order of game object's prefabs matters: the first has more chances to appear

    [SerializeField] private GameObject collectablesContainer;

    public void Spawn()
    {
                
        for (int i=0; i < prefab2spawn.Length && maxReward>0; i++)
        {
            int count, tot=0;

            if (i < prefab2spawn.Length - 1)
                count = Random.Range(0, maxReward);
            else
                count = maxReward;

            for (int j=0; j<count; j++, tot++)
            {
                /*
                 * var newInstance = Instantiate(
                                        prefab2spawn[i].GetComponent<Rigidbody>(),
                                        transform.position + new Vector3(0,1,0),
                                        Quaternion.Euler(Random.Range(0,90), tot*360/maxReward, Random.Range(0,90)) /*transform.rotation);

                newInstance.transform.parent = collectablesContainer.transform;

                Rigidbody rbInstance = newInstance as Rigidbody;

                rbInstance.AddForce(transform.up * 150f);       //Use AddExplosionForce instead??
                */

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