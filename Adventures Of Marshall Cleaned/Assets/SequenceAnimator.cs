/*
 * REF:
 *  - https://www.youtube.com/watch?v=JubT4ldOwZQ
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceAnimator : MonoBehaviour
{
    [SerializeField] float waitBetween = .05f;
    [SerializeField] float waitEnd = 1.2f;

    private List<Animator> animators;

    private void Start()
    {
        animators = new List<Animator>(GetComponentsInChildren<Animator>());

        StartCoroutine(DoAnimation());
    }

    private IEnumerator DoAnimation()
    {
        while (true)
        {
            foreach(var animator in animators)
            {
                animator.SetTrigger("DoAnimation");
                yield return new WaitForSeconds(waitBetween);
            }

            yield return new WaitForSeconds(waitEnd);
        }
    }
}
