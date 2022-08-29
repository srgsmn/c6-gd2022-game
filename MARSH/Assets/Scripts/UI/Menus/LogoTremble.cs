/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoTremble : MonoBehaviour
{
    private const float TIMING_VALUE = 10f;

    [SerializeField] private GameObject[] letters;
    [SerializeField]
    [ReadOnlyInspector] private float timer;
    [SerializeField]
    [ReadOnlyInspector] private int index;

    void Start()
    {
        timer = TIMING_VALUE + 2.5f;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.unscaledDeltaTime;

        if (timer <= 0)
        {
            timer = TIMING_VALUE;

            index = Random.Range(0, letters.Length);

            letters[index].GetComponent<Animator>().SetTrigger("LetterTremble");
        }
    }
}
