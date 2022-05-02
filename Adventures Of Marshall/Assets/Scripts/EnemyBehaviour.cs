using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private int maxReward = 3;
    [SerializeField] private GameObject ccPrefab, slPrefab;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        int count = Random.Range(0, maxReward);
        for(int s=0; s<count; s++)
        {
            Instantiate(slPrefab, transform.position, Quaternion.identity);
        }
        for (int c = 0; c < maxReward-count; c++)
        {
            Instantiate(ccPrefab, transform.position, Quaternion.identity);
        }
    }
}
